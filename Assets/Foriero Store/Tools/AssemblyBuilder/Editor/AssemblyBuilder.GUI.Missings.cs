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
		void DrawMissingsHeader ()
		{
			GUILayout.BeginHorizontal ();
			GUI.backgroundColor = Color.red;
			if (GUILayout.Button ("", EditorStyles.toolbarButton, GUILayout.Width (100))) {
			}
			GUILayout.Box ("Missing", EditorStyles.toolbarButton);
			if (GUILayout.Button ("", EditorStyles.toolbarButton, GUILayout.Width (100))) {
			}
			GUI.backgroundColor = backgroundColor;
			GUILayout.EndHorizontal ();	
		}

		void DrawMissings ()
		{
			bool missing = false;

			for (int i = 0; i < m_preset.m_UnityDlls.arraySize; i++) {
				var item = m_preset.m_UnityDlls.GetArrayElementAtIndex (i);
				if (!item.FindPropertyRelative ("found").boolValue) {
					missing = true;
					GUILayout.BeginHorizontal ();
					GUILayout.Box ("Unity Dll", EditorStyles.toolbarButton, GUILayout.Width (100));
					GUI.backgroundColor = Color.red;
					if (GUILayout.Button ("Remove", EditorStyles.toolbarButton, GUILayout.Width (50))) {
						m_preset.RemoveUnityDll (item.FindPropertyRelative ("location").stringValue);
					}
					GUI.backgroundColor = backgroundColor;
					GUILayout.TextField (Path.GetFileName (item.FindPropertyRelative ("location").stringValue), GUILayout.Width (200));
					GUILayout.TextField (item.FindPropertyRelative ("location").stringValue);
					GUILayout.EndHorizontal ();
				}
			}

			for (int i = 0; i < m_preset.m_ProjectDlls.arraySize; i++) {
				var item = m_preset.m_ProjectDlls.GetArrayElementAtIndex (i);
				if (!item.FindPropertyRelative ("found").boolValue) {
					missing = true;
					GUILayout.BeginHorizontal ();
					GUILayout.Box ("Project Dll", EditorStyles.toolbarButton, GUILayout.Width (100));
					GUI.backgroundColor = Color.red;
					if (GUILayout.Button ("Remove", EditorStyles.toolbarButton, GUILayout.Width (50))) {
						m_preset.RemoveProjectDll (item.FindPropertyRelative ("location").stringValue);
					}
					GUI.backgroundColor = backgroundColor;
					GUILayout.TextField (Path.GetFileName (item.FindPropertyRelative ("location").stringValue), GUILayout.Width (200));
					GUILayout.TextField (item.FindPropertyRelative ("location").stringValue);
					GUILayout.EndHorizontal ();
				}
			}

			for (int i = 0; i < m_preset.m_LibraryDlls.arraySize; i++) {
				var item = m_preset.m_LibraryDlls.GetArrayElementAtIndex (i);
				if (!item.FindPropertyRelative ("found").boolValue) {
					missing = true;
					GUILayout.BeginHorizontal ();
					GUILayout.Box ("Library Dll", EditorStyles.toolbarButton, GUILayout.Width (100));
					GUI.backgroundColor = Color.red;
					if (GUILayout.Button ("Remove", EditorStyles.toolbarButton, GUILayout.Width (50))) {
						m_preset.RemoveLibraryDll (item.FindPropertyRelative ("location").stringValue);
					}
					GUI.backgroundColor = backgroundColor;
					GUILayout.TextField (Path.GetFileName (item.FindPropertyRelative ("location").stringValue), GUILayout.Width (200));
					GUILayout.TextField (item.FindPropertyRelative ("location").stringValue);
					GUILayout.EndHorizontal ();
				}
			}

			for (int i = 0; i < m_preset.m_Scripts.arraySize; i++) {
				var item = m_preset.m_Scripts.GetArrayElementAtIndex (i);
				if (!item.FindPropertyRelative ("found").boolValue) {
					missing = true;
					GUILayout.BeginHorizontal ();
					GUILayout.Box ("Script", EditorStyles.toolbarButton, GUILayout.Width (100));
					GUI.backgroundColor = Color.red;
					if (GUILayout.Button ("Remove", EditorStyles.toolbarButton, GUILayout.Width (50))) {
						m_preset.RemoveScript (item.FindPropertyRelative ("location").stringValue);
					}
					GUI.backgroundColor = backgroundColor;
					GUILayout.TextField (Path.GetFileName (item.FindPropertyRelative ("location").stringValue), GUILayout.Width (200));
					GUILayout.TextField (item.FindPropertyRelative ("location").stringValue);
					GUILayout.EndHorizontal ();
				}
			}

			if (!missing) {
				EditorGUILayout.HelpBox ("Everything seems to be good here!", MessageType.Info);
			}
		}
	}
}
