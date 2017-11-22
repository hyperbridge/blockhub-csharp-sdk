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
		void DrawScriptsHeader ()
		{

			GUILayout.BeginHorizontal ();

			GUI.backgroundColor = m_preset.m_ScriptsSelected.boolValue ? Color.yellow : backgroundColor;
			if (GUILayout.Button ("Selected", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				m_preset.m_ScriptsSelected.boolValue = !m_preset.m_ScriptsSelected.boolValue;
			}
			GUI.backgroundColor = backgroundColor;

			m_preset.m_ScriptsFilter.stringValue = GUILayout.TextField (m_preset.m_ScriptsFilter.stringValue, GUILayout.Width (150));

			if (GUILayout.Button ("Editor", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				List<LibraryHelper> editorScripts = new List<LibraryHelper> ();
				for (int i = selectedScripts.Count - 1; i >= 0; i--) {
					if (selectedScripts [i].location.ToLower ().Contains ("/editor/") || selectedScripts [i].location.ToLower ().Contains (@"\editor\")) {
						editorScripts.Add (selectedScripts [i]);
					}
				}
				selectedScripts = editorScripts;
			}

			if (GUILayout.Button ("Player", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				List<LibraryHelper> playerScripts = new List<LibraryHelper> ();
				for (int i = selectedScripts.Count - 1; i >= 0; i--) {
					if (selectedScripts [i].location.ToLower ().Contains ("/editor/") || selectedScripts [i].location.ToLower ().Contains (@"\editor\")) {
						continue;
					}
					playerScripts.Add (selectedScripts [i]);
				}
				selectedScripts = playerScripts;
			}

			GUI.backgroundColor = Color.green;
			GUILayout.Box (Application.dataPath.FixOSPath (), EditorStyles.toolbarButton);
			GUI.backgroundColor = backgroundColor;

			if (GUILayout.Button ("ADD", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				for (int i = 0; i < selectedScripts.Count; i++) {
					m_preset.AddScript (selectedScripts [i].location);
					selectedScripts [i].exists = true;
				}
			}

			if (GUILayout.Button ("CLEAN", EditorStyles.toolbarButton, GUILayout.Width (50))) {
				for (int i = m_preset.m_Scripts.arraySize - 1; i >= 0; i--) {
					var item = m_preset.m_Scripts.GetArrayElementAtIndex (i);
					string location = item.FindPropertyRelative ("location").stringValue.FixOSPath ();

					foreach (LibraryHelper script in selectedScripts) {
						if (script.location == location) {
							m_preset.RemoveScript (script.location);
							script.exists = false;
							break;
						}
					}
				}

				m_preset.m_Scripts.ClearArray ();
			}

//			if (GUI.Button (GetRect (window.position.width - 50, 50), "SORT", EditorStyles.toolbarButton)) {
//
//			}
			GUILayout.EndHorizontal ();
			GUI.backgroundColor = backgroundColor;
		}

		SerializedProperty item = null;
		string location = "";
		string filename = "";

		void DrawScripts (float scrollHeight)
		{
			lines = 0;

			for (int i = 0; i < selectedScripts.Count; i++) {
				LibraryHelper script = selectedScripts [i];

				if (m_preset.m_ScriptsSelected.boolValue) {
					break;
				}

				if (!string.IsNullOrEmpty (m_preset.m_ScriptsFilter.stringValue)) {
					if (!script.fileName.Contains (m_preset.m_ScriptsFilter.stringValue, System.StringComparison.OrdinalIgnoreCase)) {
						continue;
					}
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) < (scriptsScroll.y)) {
					lines++;
					continue;
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) > (scriptsScroll.y + scrollHeight)) {
					break;
				}

				if (!script.initialized) {
					script.initialized = true;
					script.exists = m_preset.ScriptDllExists (script.location);
				}

				if (script.exists) {
					GUI.backgroundColor = Color.yellow;
					if (GUI.Button (GetRect (0, 50), "Remove", EditorStyles.toolbarButton)) {
						m_preset.RemoveScript (script.location);
						script.exists = false;
					}
					GUI.backgroundColor = backgroundColor;
				} else {
					if (GUI.Button (GetRect (0, 50), "Add", EditorStyles.toolbarButton)) {
						m_preset.AddScript (script.location);
						script.exists = true;
					}
				}
				GUI.TextField (GetRect (50, 200), script.fileName);
				GUI.TextField (GetRect (250, window.position.width - 250), script.location);
				lines++;
			}

			for (int i = m_preset.m_Scripts.arraySize - 1; i >= 0; i--) {
				item = m_preset.m_Scripts.GetArrayElementAtIndex (i);
				location = item.FindPropertyRelative ("location").stringValue.FixOSPath ();
				filename = Path.GetFileName (location);

				if (!string.IsNullOrEmpty (m_preset.m_ScriptsFilter.stringValue)) {
					if (!filename.Contains (m_preset.m_ScriptsFilter.stringValue, System.StringComparison.OrdinalIgnoreCase)) {
						continue;
					}
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) < (scriptsScroll.y)) {
					lines++;
					continue;
				}

				if ((lines * EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight) > (scriptsScroll.y + scrollHeight)) {
					break;
				}

				if (!m_preset.m_ScriptsSelected.boolValue) {
					bool continueLoop = false;
					foreach (LibraryHelper script in selectedScripts) {
						if (script.location == location) {
							continueLoop = true;
							break;
						}
					}

					if (continueLoop) {
						lines++;
						continue;
					}
				}

				GUI.backgroundColor = item.FindPropertyRelative ("found").boolValue ? selectedColor : Color.red;
				if (GUI.Button (GetRect (0, 50), "Remove", EditorStyles.toolbarButton)) {
					foreach (LibraryHelper script in selectedScripts) {
						if (script.location == location) {
							script.exists = false;
							break;
						}
					}
					m_preset.RemoveScript (location);
					GUI.backgroundColor = backgroundColor;
					break;
				}

				GUI.backgroundColor = backgroundColor;
				GUI.TextField (GetRect (50, 250), filename);
				GUI.TextField (GetRect (250, window.position.width - 250), location);
				GUI.backgroundColor = backgroundColor;

				lines++;
			}

			if (m_preset.m_Scripts.arraySize == 0 && selectedScripts.Count == 0) {
				Rect rect = GetRect (0, window.position.width);
				rect.height += rect.height;
				EditorGUI.HelpBox (rect, "Select directories or scripts on the right side of project view in order to see your scripts here!", MessageType.Info);
				lines++;
				lines++;
			}
		}
	}
}
