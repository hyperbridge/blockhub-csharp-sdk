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
		void DrawUnityDllsHeader ()
		{
			filterList.DoLayoutList ();
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Refresh", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				RefreshUnityDlls ();
			}


			GUI.backgroundColor = m_preset.m_UnityDllsSelected.boolValue ? Color.yellow : backgroundColor;
			if (GUILayout.Button ("Selected", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				m_preset.m_UnityDllsSelected.boolValue = !m_preset.m_UnityDllsSelected.boolValue;
			}
			GUI.backgroundColor = backgroundColor;

			m_preset.m_UnityDllsFilter.stringValue = GUILayout.TextField (m_preset.m_UnityDllsFilter.stringValue, GUILayout.Width (150));

			GUI.backgroundColor = Color.green;
			GUILayout.Box (unityDllsPath, EditorStyles.toolbarButton);
			if (GUILayout.Button ("", EditorStyles.toolbarButton, GUILayout.Width (100))) {
			}
			GUI.backgroundColor = backgroundColor;


			if (GUILayout.Button ("ADD", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				for (int i = 0; i < unityDlls.Count; i++) {
					m_preset.AddUnityDll (unityDlls [i].location);
					unityDlls [i].exists = true;
				}
			}

			if (GUILayout.Button ("CLEAN", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				for (int i = m_preset.m_UnityDlls.arraySize - 1; i >= 0; i--) {
					var item = m_preset.m_UnityDlls.GetArrayElementAtIndex (i);
					string location = item.FindPropertyRelative ("location").stringValue.FixOSPath ();

					foreach (LibraryHelper dll in unityDlls) {
						if (dll.location == location) {
							m_preset.RemoveUnityDll (dll.location);
							dll.exists = false;
							break;
						}
					}
				}

				m_preset.m_UnityDlls.ClearArray ();
			}
			GUILayout.EndHorizontal ();
		}

		void DrawUnityDlls (float scrollHeight)
		{
			lines = 0;

			for (int i = 0; i < unityDlls.Count; i++) {
				LibraryHelper dll = unityDlls [i];

				if (m_preset.m_UnityDllsSelected.boolValue) {
					break;
				}

				if (!string.IsNullOrEmpty (m_preset.m_UnityDllsFilter.stringValue)) {
					if (!dll.fileName.Contains (m_preset.m_UnityDllsFilter.stringValue, System.StringComparison.OrdinalIgnoreCase)) {
						continue;
					}
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) < (unityDllsScroll.y)) {
					lines++;
					continue;
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) > (unityDllsScroll.y + scrollHeight)) {
					break;
				}

				if (!dll.initialized) {
					dll.initialized = true;
					dll.exists = m_preset.UnityDllExists (dll.location);
				}

				if (dll.exists) {
					GUI.backgroundColor = Color.yellow;
					if (GUI.Button (GetRect (0, 50), "Remove", EditorStyles.toolbarButton)) {
						m_preset.RemoveUnityDll (dll.location);
						dll.exists = false;
					}
					GUI.backgroundColor = backgroundColor;
				} else {
					if (GUI.Button (GetRect (0, 50), "Add", EditorStyles.toolbarButton)) {
						m_preset.AddUnityDll (dll.location);
						dll.exists = true;
					}
				}
				GUI.TextField (GetRect (50, 200), dll.fileName);
				GUI.TextField (GetRect (250, window.position.width - 250), dll.location);
				lines++;
			}

			for (int i = 0; i < m_preset.m_UnityDlls.arraySize; i++) {
				item = m_preset.m_UnityDlls.GetArrayElementAtIndex (i);
				location = item.FindPropertyRelative ("location").stringValue.FixOSPath ();
				filename = Path.GetFileName (location);

				if (!string.IsNullOrEmpty (m_preset.m_UnityDllsFilter.stringValue)) {
					if (!filename.Contains (m_preset.m_UnityDllsFilter.stringValue, System.StringComparison.OrdinalIgnoreCase)) {
						continue;
					}
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) < (unityDllsScroll.y)) {
					lines++;
					continue;
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) > (unityDllsScroll.y + scrollHeight)) {
					break;
				}

				if (!m_preset.m_UnityDllsSelected.boolValue) {
					bool continueLoop = false;
					foreach (LibraryHelper dll in unityDlls) {
						if (dll.location == location) {
							continueLoop = true;
							break;
						}
					}

					if (continueLoop) {
						continue;
					}
				}

				GUI.backgroundColor = m_preset.UnityDllFound (location) ? selectedColor : Color.red;
				if (GUI.Button (GetRect (0, 50), "Remove", EditorStyles.toolbarButton)) {
					foreach (LibraryHelper dll in unityDlls) {
						if (dll.location == location) {
							dll.exists = false;
							break;
						}
					}
					m_preset.RemoveUnityDll (location);
				}

				GUI.backgroundColor = backgroundColor;
				GUI.TextField (GetRect (50, 250), filename);
				GUI.TextField (GetRect (250, window.position.width - 250), location);
				GUI.backgroundColor = backgroundColor;

				lines++;
			}

			if (m_preset.m_UnityDlls.arraySize == 0 && unityDlls.Count == 0) {
				Rect rect = GetRect (0, window.position.width);
				rect.height += rect.height;
				EditorGUI.HelpBox (rect, "Seems like no NET dll found in your project!", MessageType.Info);
				lines++;
				lines++;
			}
		}
	}
}
