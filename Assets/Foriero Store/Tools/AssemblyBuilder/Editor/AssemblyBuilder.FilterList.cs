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
		ReorderableList filterList = null;

		ReorderableList GetFilterList (SerializedObject s_Object, SerializedProperty s_List)
		{
			ReorderableList list = new ReorderableList (s_Object, 
				                       s_List, 
				                       true, true, true, true);

			list.drawElementCallback = 
				(Rect rect, int index, bool isActive, bool isFocused) => {
				var element = list.serializedProperty.GetArrayElementAtIndex (index);
				rect.y += 2;

				EditorGUI.PropertyField (
					new Rect (rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative ("include"), GUIContent.none);

				EditorGUI.PropertyField (
					new Rect (rect.x + 20, rect.y, rect.width - 20, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative ("name"), GUIContent.none);							
			};

			list.drawHeaderCallback = (Rect rect) => {  
				EditorGUI.LabelField (rect, "Include, Filter");
			};
															
			list.onChangedCallback = (ReorderableList l) => {

			};

			list.onRemoveCallback = (ReorderableList l) => {
				l.serializedProperty.DeleteArrayElementAtIndex (l.index);
				settingsObject.ApplyModifiedProperties ();
			};

			list.onAddCallback = (ReorderableList l) => {
				l.serializedProperty.InsertArrayElementAtIndex (0);
				selectedPreset = l.serializedProperty.GetArrayElementAtIndex (0);
				settingsObject.ApplyModifiedProperties ();
			};

			return list;
		}
	}
}
