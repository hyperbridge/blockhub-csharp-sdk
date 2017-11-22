/* Copyright Marek Ledivna, Foriero Studio */

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using Debug = UnityEngine.Debug;

using ForieroEngine.Extensions;

namespace ForieroEditor.AssemblyBuilder
{
	public partial class AssemblyBuilder : EditorWindow
	{
		ReorderableList presetList = null;

		ReorderableList GetPresetList (SerializedObject s_Object, SerializedProperty s_List)
		{
			ReorderableList list = new ReorderableList (s_Object, 
				                       s_List, 
				                       true, true, true, true);

			list.drawElementCallback = 
				(Rect rect, int index, bool isActive, bool isFocused) => {
				var element = list.serializedProperty.GetArrayElementAtIndex (index);
				rect.y += 2;
				float w = 0;
				float tw = 0;

				w = 150;
				EditorGUI.PropertyField (
					new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative ("presetname"), GUIContent.none);
				tw += w;

				EditorGUI.BeginDisabledGroup (string.IsNullOrEmpty (element.FindPropertyRelative ("dllname").stringValue));
				w = 50;
				if (GUI.Button (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "Build")) {
					list.index = index;
					list.onSelectCallback.Invoke (list);
					m_preset.tab = AssemblyBuilderSettings.Tab.build;
					string path = Path.Combine (Directory.GetCurrentDirectory (), "AssemblyBuilder/" + element.FindPropertyRelative ("dllname").stringValue + ".dll");
					string directory = Path.GetDirectoryName (path);
					if (!Directory.Exists (directory)) {
						Directory.CreateDirectory (directory);
					}
					BuildAssembly (m_preset, false);
				}
				tw += w;
				EditorGUI.EndDisabledGroup ();

				if (string.IsNullOrEmpty (element.FindPropertyRelative ("dllname").stringValue)) {
					GUI.backgroundColor = Color.red;
				}
				w = 150;
				EditorGUI.PropertyField (
					new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative ("dllname"), GUIContent.none);
				tw += w;
				GUI.backgroundColor = backgroundColor;

				w = 20;
				if (GUI.Button (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "F")) {
					string dir = Path.GetDirectoryName (GetCompiledDllPath (new m_AssemblyBuilderSettings (element), false));
					if (Directory.Exists (dir)) {
						EditorUtility.OpenWithDefaultApp (dir);
					} else {
						Debug.LogWarning ("You have not likely compiled your dll yet. Right! :-)");
					}
				}
				tw += w;

				w = rect.width - 150 - 20 - 50 - 150 - 20 - 20 - 200;
				if (w < 100) {
					w = 100f;
				}
				string pathProperty = "";
				#if UNITY_EDITOR_OSX
				pathProperty = "osxPath";
				#elif UNITY_EDITOR_WIN
				pathProperty = "winPath";
				#elif UNITY_EDITOR_LINUX
				pathProperty = "linuxPath";
				#endif
				EditorGUI.PropertyField (
					new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative (pathProperty), GUIContent.none);
				tw += w;

				w = 20;
				if (GUI.Button (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "...")) {
					string path = EditorUtility.OpenFolderPanel ("Export path.", Directory.GetCurrentDirectory (), "").FixOSPath ();
					if (!string.IsNullOrEmpty (path)) {
						#if UNITY_EDITOR_OSX
						element.FindPropertyRelative ("osxPath").stringValue = path;
						#elif UNITY_EDITOR_WIN
				element.FindPropertyRelative ("winPath").stringValue = path;
						#elif UNITY_EDITOR_LINUX
				element.FindPropertyRelative ("linuxPath").stringValue = path;
						#endif
					}
				}
				tw += w;

				w = 20;
				EditorGUI.PropertyField (
					new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative ("debug"), GUIContent.none);
				tw += w;

				w = 200;
				EditorGUI.PropertyField (
					new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative ("define"), GUIContent.none);
				tw += w;
			};

			list.drawHeaderCallback = (Rect rect) => {  
				float w = 0;
				float tw = 12;
				
				w = 150;
				EditorGUI.LabelField (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "Preset Name");
				tw += w + 2;
				w = 48;
				if (GUI.Button (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "Build", EditorStyles.toolbarButton)) {
					for (int i = 0; i < presetList.serializedProperty.arraySize; i++) {
						BuildAssembly (new m_AssemblyBuilderSettings (presetList.serializedProperty.GetArrayElementAtIndex (i)), false);
					}
				}
				tw += w + 2;
				w = 150;
				EditorGUI.LabelField (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "Dll Name (no extension)");
				tw += w;
				tw += 20;
				w = rect.width - 150 - 20 - 50 - 150 - 20 - 20 - 10 - 200;
				if (w < 100) {
					w = 100f;
				}
				EditorGUI.LabelField (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "Path");
				tw += w;
				w = 40;
				EditorGUI.LabelField (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "Debug");
				tw += w;
				tw += 5;
				w = 200;
				EditorGUI.LabelField (new Rect (rect.x + tw, rect.y, w, EditorGUIUtility.singleLineHeight), "Defines ( semicolon separated )");
				tw += w;
			};


			list.onSelectCallback = (ReorderableList l) => {  
				if (l.index >= 0 && l.index < l.count) {
					selectedPreset = l.serializedProperty.GetArrayElementAtIndex (l.index);
				
					if (m_preset == null || m_preset.m_Preset != selectedPreset) {
						m_preset = new m_AssemblyBuilderSettings (selectedPreset);
					}
				
					if (guid != selectedPreset.FindPropertyRelative ("guid").stringValue) {
						guid = selectedPreset.FindPropertyRelative ("guid").stringValue;
					}
					settingsObject.FindProperty ("selectedIndex").intValue = l.index;
					settingsObject.ApplyModifiedProperties ();
				}
			}; 

			list.onChangedCallback = (ReorderableList l) => {

			};

			list.onRemoveCallback = (ReorderableList l) => {
				if (EditorUtility.DisplayDialog ("Remove", l.serializedProperty.GetArrayElementAtIndex (l.index).FindPropertyRelative ("presetname").stringValue + " " + l.serializedProperty.GetArrayElementAtIndex (l.index).FindPropertyRelative ("dllname").stringValue, "Yes", "No")) {
					selectedPreset = null;
					m_preset = null;
					l.serializedProperty.DeleteArrayElementAtIndex (l.index);
					settingsObject.FindProperty ("selectedIndex").intValue = l.index;
					l.index = -1;
					settingsObject.ApplyModifiedProperties ();
				}
			};

			list.onAddCallback = (ReorderableList l) => {
				l.serializedProperty.InsertArrayElementAtIndex (0);
				selectedPreset = l.serializedProperty.GetArrayElementAtIndex (0);
				selectedPreset.FindPropertyRelative ("guid").stringValue = System.Guid.NewGuid ().ToString ();
				
				if (m_preset == null || m_preset.m_Preset != selectedPreset) {
					m_preset = new m_AssemblyBuilderSettings (selectedPreset);
				}

				//SortPresets (settingsObject.FindProperty ("presets"), "presetname");
				m_preset.m_DllName.stringValue = "";
				m_preset.m_PresetName.stringValue = "";
				m_preset.m_Debug.boolValue = false;
				m_preset.m_UnityDlls.ClearArray ();
				m_preset.m_ProjectDlls.ClearArray ();
				m_preset.m_LibraryDlls.ClearArray ();
				m_preset.m_Scripts.ClearArray ();
				selectedPreset.FindPropertyRelative ("osxPath").stringValue = "";
				selectedPreset.FindPropertyRelative ("winPath").stringValue = "";
				selectedPreset.FindPropertyRelative ("linuxPath").stringValue = "";
				settingsObject.FindProperty ("selectedIndex").intValue = l.index;
				settingsObject.ApplyModifiedProperties ();
			};

			return list;
		}
	}
}
