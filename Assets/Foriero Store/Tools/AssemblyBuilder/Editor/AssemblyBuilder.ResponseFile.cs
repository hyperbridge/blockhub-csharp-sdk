using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Debug = UnityEngine.Debug;

using ForieroEditor.Extensions;

namespace ForieroEditor.AssemblyBuilder
{
	public partial class AssemblyBuilder : EditorWindow
	{
		string GetCompiledDllPath (m_AssemblyBuilderSettings m_preset, bool compile)
		{
			string dllPath = "";
			string presetPath = m_preset.m_Path.stringValue;
			string presetDllName = m_preset.m_DllName.stringValue;

			if (compile) {
				if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) {
					dllPath = Path.Combine (Directory.GetCurrentDirectory (), "Temp/abtemp.dll");
				} else {
					dllPath = Path.Combine (Directory.GetCurrentDirectory (), "Temp\\abtemp.dll").Replace ("/", @"\");
				}
			} else {
				if (string.IsNullOrEmpty (presetPath) || !Directory.Exists (presetPath)) {
					if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) {
						dllPath = Path.Combine (Directory.GetCurrentDirectory (), "AssemblyBuilder/" + presetDllName + ".dll");
					} else {
						dllPath = Path.Combine (Directory.GetCurrentDirectory (), "AssemblyBuilder\\" + presetDllName + ".dll").Replace ("/", @"\");
					}
				} else {
					if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) {
						dllPath = Path.Combine (presetPath, presetDllName + ".dll");
					} else {
						dllPath = Path.Combine (presetPath, presetDllName + ".dll").Replace ("/", @"\");
					}
				}
			}

			return dllPath;
		}

		string GetResponseFileString (m_AssemblyBuilderSettings m_preset, bool compile)
		{
			string dllPath = GetCompiledDllPath (m_preset, compile);
					
			if (!Directory.Exists (Path.GetDirectoryName (dllPath))) {
				Directory.CreateDirectory (Path.GetDirectoryName (dllPath));
			}
		
			string result = "";
			result += "-target:library" + System.Environment.NewLine;
			result += "-out:" + string.Format ("\"{0}\"", dllPath) + System.Environment.NewLine;
			result += "-nowarn:0169" + System.Environment.NewLine;

			if (m_preset.m_Debug.boolValue) {
				result += "-debug" + System.Environment.NewLine;
			}
			if (m_preset.m_DelaySign.boolValue) {
				result += "-delaysign" + System.Environment.NewLine;
			}
			if (m_preset.m_NoConfig.boolValue) {
				result += "-noconfig" + System.Environment.NewLine;
			}
			if (m_preset.m_Platform.enumValueIndex > 0) {
				result += "-platform:" + ((AssemblyBuilderSettings.PlatformEnum)m_preset.m_Platform.enumValueIndex).ToString ().ToLower () + System.Environment.NewLine;
			}
			if (m_preset.m_CodePage.enumValueIndex > 0)
				result += "-codepage:" + ((AssemblyBuilderSettings.CodepageEnum)m_preset.m_CodePage.enumValueIndex).ToString ().ToLower () + System.Environment.NewLine;
			if (!string.IsNullOrEmpty (m_preset.m_Define.stringValue)) {
				string[] defines = m_preset.m_Define.stringValue.Split (';');
				foreach (string define in defines) {
					result += "-define:" + define + System.Environment.NewLine;
				}
			}
													
			for (int i = 0; i < m_preset.m_UnityDlls.arraySize; i++) {
				var item = m_preset.m_UnityDlls.GetArrayElementAtIndex (i);
				var path = GetUnityDllPath (item.FindPropertyRelative ("location").stringValue);
				result += "-r:" + path.FixOSPath ().DoubleQuotes () + System.Environment.NewLine;
			}
		
			for (int i = 0; i < m_preset.m_LibraryDlls.arraySize; i++) {
				var item = m_preset.m_LibraryDlls.GetArrayElementAtIndex (i);
				var path = Path.Combine (Path.Combine (Directory.GetCurrentDirectory (), "Library"), item.FindPropertyRelative ("location").stringValue);
				result += "-r:" + path.FixOSPath ().DoubleQuotes () + System.Environment.NewLine;
			}

			for (int i = 0; i < m_preset.m_ProjectDlls.arraySize; i++) {
				var item = m_preset.m_ProjectDlls.GetArrayElementAtIndex (i);
				var path = Path.Combine (Application.dataPath, item.FindPropertyRelative ("location").stringValue);
				result += "-r:" + path.FixOSPath ().DoubleQuotes () + System.Environment.NewLine;
			}
		
			for (int i = 0; i < m_preset.m_Scripts.arraySize; i++) {
				var item = m_preset.m_Scripts.GetArrayElementAtIndex (i);
				var path = Path.Combine (Application.dataPath, item.FindPropertyRelative ("location").stringValue);
				result += path.FixOSPath ().DoubleQuotes () + System.Environment.NewLine;
			}
							
			return result;
		}

		string GetUnityDllPath (string location)
		{
			foreach (LibraryHelper dll in unityDlls) {
				if (dll.location.Contains (location.FixOSPath ().Replace ("Frameworks/", "").Replace ("Frameworks\\", ""))) {
					return Path.Combine (unityDllsPath, dll.location);
				}
			}
			return location;
		}

		void CheckFiles (m_AssemblyBuilderSettings m_preset)
		{
			//	missingFoldOut = false;
			for (int i = 0; i < m_preset.m_UnityDlls.arraySize; i++) {
				var item = m_preset.m_UnityDlls.GetArrayElementAtIndex (i);
				var path = GetUnityDllPath (item.FindPropertyRelative ("location").stringValue);
				path = path.FixOSPath ();
				item.FindPropertyRelative ("found").boolValue = File.Exists (path);
				if (!item.FindPropertyRelative ("found").boolValue) {
					//		missingFoldOut = true;
				}
			}

			for (int i = 0; i < m_preset.m_LibraryDlls.arraySize; i++) {
				var item = m_preset.m_LibraryDlls.GetArrayElementAtIndex (i);
				var path = Path.Combine (Path.Combine (Directory.GetCurrentDirectory (), "Library"), item.FindPropertyRelative ("location").stringValue);
				path = path.FixOSPath ();
				item.FindPropertyRelative ("found").boolValue = File.Exists (path);
				if (!item.FindPropertyRelative ("found").boolValue) {
					//		missingFoldOut = true;
				}
			}

			for (int i = 0; i < m_preset.m_ProjectDlls.arraySize; i++) {
				var item = m_preset.m_ProjectDlls.GetArrayElementAtIndex (i);
				var path = Path.Combine (Application.dataPath, item.FindPropertyRelative ("location").stringValue);
				path = path.FixOSPath ();
				item.FindPropertyRelative ("found").boolValue = File.Exists (path);
				if (!item.FindPropertyRelative ("found").boolValue) {
					//		missingFoldOut = true;
				}
			}

			for (int i = 0; i < m_preset.m_Scripts.arraySize; i++) {
				var item = m_preset.m_Scripts.GetArrayElementAtIndex (i);
				var path = Path.Combine (Application.dataPath, item.FindPropertyRelative ("location").stringValue);
				path = path.FixOSPath ();
				item.FindPropertyRelative ("found").boolValue = File.Exists (path);
				if (!item.FindPropertyRelative ("found").boolValue) {
					//		missingFoldOut = true;
				}
			}
		}
	}
}
