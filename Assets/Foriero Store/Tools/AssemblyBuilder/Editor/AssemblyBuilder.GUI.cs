/* Copyright Marek Ledivna, Foriero Studio */

using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

using ForieroEngine.Extensions;

namespace ForieroEditor.AssemblyBuilder
{
	public partial class AssemblyBuilder : EditorWindow
	{
		m_AssemblyBuilderSettings m_preset;

		string resultedDll = "";

		bool PresetNameExists (SerializedProperty items, string presetName)
		{
			for (int i = 0; i < items.arraySize; i++) {
				if (items.GetArrayElementAtIndex (i).FindPropertyRelative ("presetname").stringValue == presetName) {
					return true;
				}
			}
			return false;
		}

		string[] GetSubdirectories (string aDirectory)
		{
			return System.IO.Directory.GetDirectories (aDirectory, "*", System.IO.SearchOption.AllDirectories);
		}

		bool IsObjectDirectory (UnityEngine.Object anObject)
		{
			var selpath = AssetDatabase.GetAssetPath (anObject);
			if (selpath == "")
				return false;

			var dummypath = System.IO.Path.Combine (selpath, "fake.asset");
			var assetpath = AssetDatabase.GenerateUniqueAssetPath (dummypath);

			if (assetpath == "") {
				// couldn't generate a path, current asset must be a file
				//UnityEngine.Debug.Log("File: " + anObject.name);
				return false;
			} else {
				//UnityEngine.Debug.Log("Directory: " + anObject.name);
				return true;
			}
		}

		bool IsFolder (UnityEngine.Object obj)
		{
			if (obj != null) {
				string asset_path = AssetDatabase.GetAssetPath (obj);
				if (asset_path.Length > 0) {
					string file_path = Application.dataPath + "/" + asset_path.Replace ("Assets/", "");
					System.IO.FileAttributes file_attr = System.IO.File.GetAttributes (file_path);
					if ((file_attr & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory) {
						return true;
					} else {
						return false;
					}
				} else {
					return false;
				}
			}
			return false;
		}

		Color backgroundColor = Color.grey;
		string guid = "";

		Color selectedColor = Color.yellow;

		void OnGUI ()
		{
			backgroundColor = GUI.backgroundColor;

			if (!window) {
				window = EditorWindow.GetWindow (typeof(AssemblyBuilder)) as AssemblyBuilder;
			}

			EditorGUI.BeginChangeCheck ();
																							
			if (presetList != null) {
				presetList.DoLayoutList ();
			}
		
			if (presetList == null || presetList.serializedProperty.arraySize == 0) {
				EditorGUILayout.HelpBox ("Click the plus button for adding new build preset.", MessageType.Warning);
			}

			if (m_preset != null && m_preset.m_DllName.stringValue == "") {
				EditorGUILayout.HelpBox ("Write dll name into 'dll name' column. You can optionally add preset name in case you want to build the same dll name into different projects or you want to build like release or debug version of your dll." +
				" If path is empty the dll will be stored relativelly to your project folder 'PROJECT/AsseblyBuilder/' other wise you can specify path where to store you final dll.", MessageType.Warning);
			}

			GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (2));
			GUILayout.BeginHorizontal ();

			EditorGUILayout.PropertyField (compiler, GUILayout.Width (200));
//			switch ((AssemblyBuilderSettings.Compiler)compiler.intValue) {
//			case AssemblyBuilderSettings.Compiler.MCS:
//				EditorGUILayout.PropertyField (mcsPath);
//				break;
//			}
			GUILayout.EndHorizontal ();
			GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (2));

			if (selectedPreset == null) {
				return;
			}

			GUILayout.BeginHorizontal ();

			GUI.backgroundColor = (m_preset.tab == AssemblyBuilderSettings.Tab.unityDlls ? Color.green : backgroundColor);
			if (GUILayout.Button ("Unity Dlls", EditorStyles.toolbarButton, GUILayout.Width (65))) {
				m_preset.tab = AssemblyBuilderSettings.Tab.unityDlls;
			}
			GUI.backgroundColor = backgroundColor;

			GUI.backgroundColor = (m_preset.tab == AssemblyBuilderSettings.Tab.projectDlls ? Color.green : backgroundColor);
			if (GUILayout.Button ("Project Dlls", EditorStyles.toolbarButton, GUILayout.Width (65))) {
				m_preset.tab = AssemblyBuilderSettings.Tab.projectDlls;
			}
			GUI.backgroundColor = backgroundColor;

			GUI.backgroundColor = (m_preset.tab == AssemblyBuilderSettings.Tab.scripts ? Color.green : backgroundColor);
			if (GUILayout.Button ("Scripts", EditorStyles.toolbarButton, GUILayout.Width (65))) {
				m_preset.tab = AssemblyBuilderSettings.Tab.scripts;
			}
			GUI.backgroundColor = backgroundColor;

			GUI.backgroundColor = (m_preset.tab == AssemblyBuilderSettings.Tab.missings ? Color.green : backgroundColor);
			if (GUILayout.Button ("Missings", EditorStyles.toolbarButton, GUILayout.Width (65))) {
				m_preset.tab = AssemblyBuilderSettings.Tab.missings;
				CheckFiles (m_preset);
			}
			GUI.backgroundColor = backgroundColor;

			EditorGUI.BeginDisabledGroup (string.IsNullOrEmpty (m_preset.m_DllName.stringValue));

			GUI.backgroundColor = (m_preset.tab == AssemblyBuilderSettings.Tab.responseFile ? Color.green : backgroundColor);
			if (GUILayout.Button ("Response", EditorStyles.toolbarButton, GUILayout.Width (65))) {
				m_preset.tab = AssemblyBuilderSettings.Tab.responseFile;
				response = GetResponseFileString (m_preset, false);
			}
			GUI.backgroundColor = backgroundColor;

			GUI.backgroundColor = (m_preset.tab == AssemblyBuilderSettings.Tab.build ? Color.green : backgroundColor);
			if (GUILayout.Button ("Build", EditorStyles.toolbarButton, GUILayout.Width (65))) {
				m_preset.tab = AssemblyBuilderSettings.Tab.build;
				string path = Path.Combine (Directory.GetCurrentDirectory (), "AssemblyBuilder/" + selectedPreset.FindPropertyRelative ("dllname").stringValue + ".dll");
				string directory = Path.GetDirectoryName (path);
				if (!Directory.Exists (directory)) {
					Directory.CreateDirectory (directory);
				}
				BuildAssembly (m_preset, false);
			}
			GUI.backgroundColor = backgroundColor;

			EditorGUI.EndDisabledGroup ();

			GUILayout.FlexibleSpace ();

			GUILayout.Label (m_preset.m_PresetName.stringValue + " - " + m_preset.m_DllName.stringValue + ".dll");

			GUILayout.FlexibleSpace ();

			GUILayout.EndHorizontal ();

			GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (2));

			selectedColor = Color.yellow;

			Rect lastRect = GUILayoutUtility.GetLastRect ();
			float totalHeight = 0;
			float scrollHeight = 0;

			switch (m_preset.tab) {
			case AssemblyBuilderSettings.Tab.unityDlls: 
				DrawUnityDllsHeader ();
				lastRect = GUILayoutUtility.GetLastRect ();
				totalHeight = (m_preset.m_UnityDlls.arraySize + unityDlls.Count + 1) * EditorGUIUtility.singleLineHeight;
				scrollHeight = window.position.height - (lastRect.y + lastRect.height + 4);
				unityDllsScroll = GUI.BeginScrollView (new Rect (0, lastRect.y + lastRect.height + 4, window.position.width, scrollHeight), unityDllsScroll, new Rect (0, 0, window.position.width, totalHeight));
				DrawUnityDlls (scrollHeight);
				GUI.EndScrollView ();
				break;
			case AssemblyBuilderSettings.Tab.projectDlls: 
				DrawProjectDllsHeader ();
				projectDllsScroll = GUILayout.BeginScrollView (projectDllsScroll);
				DrawProjectDlls ();
				GUILayout.EndScrollView ();
				break;
			case AssemblyBuilderSettings.Tab.scripts: 
				DrawScriptsHeader ();
				lastRect = GUILayoutUtility.GetLastRect ();
				totalHeight = (m_preset.m_Scripts.arraySize + selectedScripts.Count + 1) * EditorGUIUtility.singleLineHeight;
				scrollHeight = window.position.height - (lastRect.y + lastRect.height + 4);
				scriptsScroll = GUI.BeginScrollView (new Rect (0, lastRect.y + lastRect.height + 4, window.position.width, scrollHeight), scriptsScroll, new Rect (0, 0, window.position.width, totalHeight));
				GUI.Label (new Rect (0, 0, window.position.width, totalHeight), "");
				DrawScripts (scrollHeight);
				GUI.EndScrollView ();
				break;
			case AssemblyBuilderSettings.Tab.missings: 
				DrawMissingsHeader ();
				missingsScroll = GUILayout.BeginScrollView (missingsScroll);
				DrawMissings ();
				GUILayout.EndScrollView ();
				break;
			case AssemblyBuilderSettings.Tab.responseFile: 
				responseScroll = GUILayout.BeginScrollView (responseScroll);
				DrawResponse ();
				GUILayout.EndScrollView ();
				break;
			case AssemblyBuilderSettings.Tab.build: 
				buildScroll = GUILayout.BeginScrollView (buildScroll);
				DrawBuild ();
				GUILayout.EndScrollView ();
				break;
			}				

			if (EditorGUI.EndChangeCheck ()) {
				settingsObject.ApplyModifiedProperties ();
			}
		}

		void DrawResponse ()
		{
			GUILayout.TextArea (response);
		}

		void DrawBuild ()
		{
			GUI.backgroundColor = Color.green;
			if (!string.IsNullOrEmpty (resultedDll)) {
				if (GUILayout.Button (resultedDll)) {
					EditorUtility.OpenWithDefaultApp (Path.GetDirectoryName (resultedDll));
				}
			}
			GUI.backgroundColor = backgroundColor;

			GUILayout.TextArea (output);
			GUILayout.TextArea (errors);


		}

		int lines = 0;

		Rect GetRect (float x, float width)
		{
			return new Rect (x, lines * EditorGUIUtility.singleLineHeight, width, EditorGUIUtility.singleLineHeight);
		}
	}
}
