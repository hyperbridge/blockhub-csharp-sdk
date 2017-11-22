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

using ForieroEditor.Extensions;

using Debug = UnityEngine.Debug;

namespace ForieroEditor.AssemblyBuilder
{
	public partial class AssemblyBuilder : EditorWindow
	{
		class m_AssemblyBuilderSettings
		{
			public SerializedProperty m_Preset;
			public SerializedProperty m_PresetName;
			public SerializedProperty m_DllName;
			public SerializedProperty m_Path;
			public SerializedProperty m_Debug;
			public SerializedProperty m_DelaySign;
			public SerializedProperty m_Platform;
			public SerializedProperty m_CodePage;
			public SerializedProperty m_Define;
			public SerializedProperty m_NoConfig;
			public SerializedProperty m_UnityDlls;
			public SerializedProperty m_ProjectDlls;
			public SerializedProperty m_LibraryDlls;
			public SerializedProperty m_Scripts;
			public SerializedProperty m_Tab;

			public SerializedProperty m_UnityDllsFilter;
			public SerializedProperty m_UnityDllsSelected;
			public SerializedProperty m_ProjectDllsFilter;
			public SerializedProperty m_ProjectDllsSelected;
			public SerializedProperty m_ScriptsFilter;
			public SerializedProperty m_ScriptsSelected;

			public AssemblyBuilderSettings.Tab tab {
				get {
					return (AssemblyBuilderSettings.Tab)m_Tab.enumValueIndex;
				}
				set {
					m_Tab.enumValueIndex = (int)value;
				}
			}

			public m_AssemblyBuilderSettings (SerializedProperty preset)
			{
				m_Preset = preset;
				m_PresetName = preset.FindPropertyRelative ("presetname");
				m_DllName = preset.FindPropertyRelative ("dllname");
				#if UNITY_EDITOR_OSX
				m_Path = preset.FindPropertyRelative ("osxPath");
				m_UnityDlls = preset.FindPropertyRelative ("osxUnityDlls");
				#elif UNITY_EDITOR_WIN
				m_Path = preset.FindPropertyRelative ("winPath");
				m_UnityDlls = preset.FindPropertyRelative ("winUnityDlls");
				#elif UNITY_EDITOR_LINUX
				m_Path = preset.FindPropertyRelative ("linuxPath");
				m_UnityDlls = preset.FindPropertyRelative ("linuxUnityDlls");
				#endif
				m_Debug = preset.FindPropertyRelative ("debug");
				m_DelaySign = preset.FindPropertyRelative ("delaysign");
				m_Platform = preset.FindPropertyRelative ("platform");
				m_CodePage = preset.FindPropertyRelative ("codepage");
				m_Define = preset.FindPropertyRelative ("define");
				m_NoConfig = preset.FindPropertyRelative ("noconfig");
				m_ProjectDlls = preset.FindPropertyRelative ("projectDlls");
				m_LibraryDlls = preset.FindPropertyRelative ("libraryDlls");
				m_Scripts = preset.FindPropertyRelative ("scripts");

				m_Tab = preset.FindPropertyRelative ("selectedTab");

				m_UnityDllsFilter = preset.FindPropertyRelative ("unityDllsFilter");
				m_UnityDllsSelected = preset.FindPropertyRelative ("unityDllsSelected");
				m_ProjectDllsFilter = preset.FindPropertyRelative ("projectDllsFilter");
				m_ProjectDllsSelected = preset.FindPropertyRelative ("projectDllsSelected");
				m_ScriptsFilter = preset.FindPropertyRelative ("scriptsFilter");
				m_ScriptsSelected = preset.FindPropertyRelative ("scriptsSelected");
			}

			bool Exists (string location, SerializedProperty array)
			{
				for (int i = array.arraySize - 1; i >= 0; i--) {
					if (array.GetArrayElementAtIndex (i).FindPropertyRelative ("location").stringValue.FixOSPath () == location.FixOSPath ()) {
						return true;
					}
				}
				return false;
			}

			bool Found (string location, SerializedProperty array)
			{
				for (int i = array.arraySize - 1; i >= 0; i--) {
					if (array.GetArrayElementAtIndex (i).FindPropertyRelative ("location").stringValue.FixOSPath () == location.FixOSPath ()) {
						return array.GetArrayElementAtIndex (i).FindPropertyRelative ("found").boolValue;
					}
				}
				return false;
			}

			void Add (string location, SerializedProperty array)
			{
				if (!Exists (location, array)) {
					array.InsertArrayElementAtIndex (0);
					var m_item = array.GetArrayElementAtIndex (0);
					m_item.FindPropertyRelative ("location").stringValue = location.FixOSPath ();
					m_item.FindPropertyRelative ("found").boolValue = true;
					m_Preset.serializedObject.ApplyModifiedProperties ();
				}
			}

			void Remove (string location, SerializedProperty array)
			{
				for (int i = array.arraySize - 1; i >= 0; i--) {
					if (array.GetArrayElementAtIndex (i).FindPropertyRelative ("location").stringValue.FixOSPath () == location.FixOSPath ()) {
						array.DeleteArrayElementAtIndex (i);
						m_Preset.serializedObject.ApplyModifiedProperties ();
						break;
					}
				}
			}

			public bool UnityDllExists (string location)
			{
				return Exists (location, m_UnityDlls);
			}

			public bool ProjectDllExists (string location)
			{
				return Exists (location, m_ProjectDlls);
			}

			public bool LibraryDllExists (string location)
			{
				return Exists (location, m_LibraryDlls);
			}

			public bool ScriptDllExists (string location)
			{
				return Exists (location, m_Scripts);
			}

			public bool UnityDllFound (string location)
			{
				return Exists (location, m_UnityDlls);
			}

			public bool ProjectDllFound (string location)
			{
				return Exists (location, m_ProjectDlls);
			}

			public bool LibraryDllFound (string location)
			{
				return Exists (location, m_LibraryDlls);
			}

			public bool ScriptDllFound (string location)
			{
				return Exists (location, m_Scripts);
			}

			public void AddUnityDll (string location)
			{
				Add (location, m_UnityDlls);
			}

			public void AddProjectDll (string location)
			{
				Add (location, m_ProjectDlls);
			}

			public void AddLibraryDll (string location)
			{
				Add (location, m_LibraryDlls);
			}

			public void AddScript (string location)
			{
				Add (location, m_Scripts);
			}

			public void RemoveUnityDll (string location)
			{
				Remove (location, m_UnityDlls);
			}

			public void RemoveProjectDll (string location)
			{
				Remove (location, m_ProjectDlls);
			}

			public void RemoveLibraryDll (string location)
			{
				Remove (location, m_LibraryDlls);
			}

			public void RemoveScript (string location)
			{
				Remove (location, m_Scripts);
			}

		}
	}
}
