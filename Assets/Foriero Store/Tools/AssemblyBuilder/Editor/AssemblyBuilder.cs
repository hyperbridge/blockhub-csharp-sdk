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
using System.Security;
using Debug = UnityEngine.Debug;

using ForieroEngine.Extensions;

namespace ForieroEditor.AssemblyBuilder
{
	public partial class AssemblyBuilder : EditorWindow
	{
		
		[MenuItem ("Foriero/Tools/Assembly Builder &a")]
		static void Init ()
		{
			window = EditorWindow.GetWindow (typeof(AssemblyBuilder)) as AssemblyBuilder;
			window.titleContent = new GUIContent ("Assembly Dll");
		}

		public static AssemblyBuilder window;

		Vector2 unityDllsScroll = Vector2.zero;
		Vector2 projectDllsScroll = Vector2.zero;
		Vector2 scriptsScroll = Vector2.zero;
		Vector2 missingsScroll = Vector2.zero;
		Vector2 responseScroll = Vector2.zero;
		Vector2 buildScroll = Vector2.zero;

		class LibraryHelper
		{
			public string fileName = "";
			public string location = "";
			public bool exists = false;
			public bool initialized = false;
		}

		List<LibraryHelper> unityDlls = new List<LibraryHelper> ();
		List<LibraryHelper> projectDlls = new List<LibraryHelper> ();
		List<LibraryHelper> selectedScripts = new List<LibraryHelper> ();

		string unityDllsPath = "";

		SerializedObject settingsObject = null;
		SerializedProperty selectedPreset = null;
		string filterName = "";

		SerializedProperty compiler = null;

		void OnEnable ()
		{
			settingsObject = new SerializedObject (AssemblyBuilderSettings.instance);
			compiler = settingsObject.FindProperty ("compiler");
			presetList = GetPresetList (settingsObject, settingsObject.FindProperty ("presets"));
			presetList.index = settingsObject.FindProperty ("selectedIndex").intValue;
			presetList.onSelectCallback.Invoke (presetList);
			#if UNITY_EDITOR_OSX
			filterName = "osxFilters";
			#elif UNITY_EDITOR_WIN
			filterName = "winFilters";
			#elif UNITY_EDITOR_LINUX
			filterName = "linuxFilters";
			#endif
			filterList = GetFilterList (settingsObject, settingsObject.FindProperty (filterName));
			RefreshUnityDlls ();
			RefreshProjectDlls ();
		}

		void RefreshUnityDlls ()
		{
			unityDlls = new List<LibraryHelper> ();

			#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
			unityDllsPath = (EditorApplication.applicationPath + "/Contents").FixOSPath ();
			#elif UNITY_EDITOR_WIN
			unityDllsPath = Path.Combine (Path.GetDirectoryName (EditorApplication.applicationPath), "Data").FixOSPath();
			#endif

			string[] dlls = Directory.GetFiles (unityDllsPath, "*.dll", SearchOption.AllDirectories);
			for (int i = 0; i < dlls.Length; i++) {
				bool containsInclude = false;	
				bool containsExclude = false;

				var filters = settingsObject.FindProperty (filterName);

				for (int f = 0; f < filters.arraySize; f++) {
					var item = filters.GetArrayElementAtIndex (f);
					string filter = item.FindPropertyRelative ("name").stringValue;
					bool include = item.FindPropertyRelative ("include").boolValue;
					
					if (dlls [i].Contains (filter)) {
						if (include) {
							containsInclude = true;
						} else {
							containsExclude = true;
						}
					}
				}

				if (filters.arraySize != 0) {
					if (!containsInclude || containsExclude) {
						continue;
					}
				}

				if (!isValidAssembly (dlls [i])) {
					continue;
				}

				string location = dlls [i].FixOSPath ();

				unityDlls.Add (new LibraryHelper {
					fileName = Path.GetFileName (location),
					location = location.Replace (unityDllsPath.FixOSPath (), "").Remove (0, 1)
				});

			}

			unityDlls = unityDlls.OrderBy (i => i.fileName).ToList ();
		}

		void RefreshProjectDlls ()
		{
			projectDlls = new List<LibraryHelper> ();

			string[] dlls = Directory.GetFiles (Application.dataPath, "*.dll", SearchOption.AllDirectories);
			for (int i = 0; i < dlls.Length; i++) {
				if (!isValidAssembly (dlls [i])) {
					continue;
				}

				string location = dlls [i].FixOSPath ();

				projectDlls.Add (new LibraryHelper {
					fileName = Path.GetFileName (location),
					location = location.Replace (Application.dataPath.FixOSPath (), "").Remove (0, 1)
				});
			}

			projectDlls = projectDlls.OrderBy (i => i.fileName).ToList ();
		}

		void OnSelectionChange ()
		{
			selectedScripts.Clear ();
			foreach (UnityEngine.Object o in Selection.objects) {
				if (IsFolder (o)) {
					string[] files = Directory.GetFiles (Path.Combine (Directory.GetCurrentDirectory (), AssetDatabase.GetAssetPath (o)), "*.cs", SearchOption.AllDirectories);
					foreach (string file in files) {
						string path = file.Replace (Application.dataPath.FixOSPath (), "").Remove (0, 1).FixOSPath ();
						selectedScripts.Add (new LibraryHelper (){ location = path, fileName = Path.GetFileName (path) });	
					}
				} else {
					if (o.GetType () == typeof(MonoScript)) {
						string path = AssetDatabase.GetAssetPath (o).Replace ("Assets", "").Remove (0, 1).FixOSPath ();
						selectedScripts.Add (new LibraryHelper (){ location = path, fileName = Path.GetFileName (path) });	
					} 
				}
				Repaint ();	
			}
		}

		public static bool isValidAssembly (string sFileName)
		{
			try {
				using (FileStream fs = File.OpenRead (sFileName)) {
					if ((fs.ReadByte () != 'M') || (fs.ReadByte () != 'Z')) {
						fs.Close ();
						return false;
					}
					fs.Close ();
				}
							
				object foo = AssemblyName.GetAssemblyName (sFileName);
				return foo != null;
			} catch {
				return false;
			}
		}
	}
}