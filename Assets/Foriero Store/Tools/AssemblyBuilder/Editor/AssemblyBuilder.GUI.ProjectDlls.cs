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
		void DrawProjectDllsHeader ()
		{
			GUILayout.BeginHorizontal ();

			if (GUILayout.Button ("Refresh", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				RefreshProjectDlls ();
			}

			GUI.backgroundColor = m_preset.m_ProjectDllsSelected.boolValue ? Color.yellow : backgroundColor;
			if (GUILayout.Button ("Selected", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				m_preset.m_ProjectDllsSelected.boolValue = !m_preset.m_ProjectDllsSelected.boolValue;
			}
			GUI.backgroundColor = backgroundColor;

			m_preset.m_ProjectDllsFilter.stringValue = GUILayout.TextField (m_preset.m_ProjectDllsFilter.stringValue, GUILayout.Width (150));

			GUI.backgroundColor = Color.green;
			GUILayout.Box (Application.dataPath.FixOSPath (), EditorStyles.toolbarButton);
			GUI.backgroundColor = backgroundColor;

			if (GUILayout.Button ("ADD", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				for (int i = 0; i < projectDlls.Count; i++) {
					m_preset.AddProjectDll (projectDlls [i].location);
					projectDlls [i].exists = true;
				}
			}

			if (GUILayout.Button ("CLEAN", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				for (int i = m_preset.m_ProjectDlls.arraySize - 1; i >= 0; i--) {
					var item = m_preset.m_ProjectDlls.GetArrayElementAtIndex (i);
					string location = item.FindPropertyRelative ("location").stringValue.FixOSPath ();

					foreach (LibraryHelper dll in projectDlls) {
						if (dll.location == location) {
							m_preset.RemoveProjectDll (dll.location);
							dll.exists = false;
							break;
						}
					}
				}

				m_preset.m_ProjectDlls.ClearArray ();
			}
			GUILayout.EndHorizontal ();	
		}

		void DrawProjectDlls ()
		{
			for (int i = 0; i < projectDlls.Count; i++) {
				LibraryHelper dll = projectDlls [i];

				if (m_preset.m_ProjectDllsSelected.boolValue) {
					break;
				}

				if (!string.IsNullOrEmpty (m_preset.m_ProjectDllsFilter.stringValue)) {
					if (!dll.fileName.Contains (m_preset.m_ProjectDllsFilter.stringValue, System.StringComparison.OrdinalIgnoreCase)) {
						continue;
					}
				}

				if (!dll.initialized) {
					dll.initialized = true;
					dll.exists = m_preset.ProjectDllExists (dll.location);
				}

				GUILayout.BeginHorizontal ();
				if (dll.exists) {
					GUI.backgroundColor = Color.yellow;
					if (GUILayout.Button ("Remove", EditorStyles.toolbarButton, GUILayout.Width (50))) {
						m_preset.RemoveProjectDll (dll.location);
						dll.exists = false;
					}
					GUI.backgroundColor = backgroundColor;
				} else {
					if (GUILayout.Button ("Add", EditorStyles.toolbarButton, GUILayout.Width (50))) {
						m_preset.AddProjectDll (dll.location);
						dll.exists = true;
					}
				}
				GUILayout.TextField (dll.fileName, GUILayout.Width (200));
				GUILayout.TextField (dll.location);
				GUILayout.EndHorizontal ();
			}

			for (int i = 0; i < m_preset.m_ProjectDlls.arraySize; i++) {
				item = m_preset.m_ProjectDlls.GetArrayElementAtIndex (i);
				location = item.FindPropertyRelative ("location").stringValue.FixOSPath ();
				filename = Path.GetFileName (location);

				if (!string.IsNullOrEmpty (m_preset.m_ProjectDllsFilter.stringValue)) {
					if (!filename.Contains (m_preset.m_ProjectDllsFilter.stringValue, System.StringComparison.OrdinalIgnoreCase)) {
						continue;
					}
				}

				if (!m_preset.m_ProjectDllsSelected.boolValue) {
					bool continueLoop = false;
					foreach (LibraryHelper dll in projectDlls) {
						if (dll.location == location) {
							continueLoop = true;
							break;
						}
					}

					if (continueLoop) {
						continue;
					}
				}

				GUILayout.BeginHorizontal ();
				GUI.backgroundColor = m_preset.ProjectDllFound (location) ? selectedColor : Color.red;
				if (GUILayout.Button ("Remove", EditorStyles.toolbarButton, GUILayout.Width (50))) {
					foreach (LibraryHelper dll in projectDlls) {
						if (dll.location == location) {
							dll.exists = false;
							break;
						}
					}
					m_preset.RemoveProjectDll (location);
				}
				GUI.backgroundColor = backgroundColor;
				GUILayout.TextField (filename, GUILayout.Width (200));
				GUILayout.TextField (location);
				GUILayout.EndHorizontal ();
				GUI.backgroundColor = backgroundColor;
			}

			if (m_preset.m_ProjectDlls.arraySize == 0 && projectDlls.Count == 0) {
				EditorGUILayout.HelpBox ("Seems like no NET dll found in your project!", MessageType.Info);
			}
		}
	}
}
