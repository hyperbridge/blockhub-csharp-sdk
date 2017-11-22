using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ForieroEditor.AssemblyBuilder
{
	public class AssemblyBuilderSettings : ScriptableObject
	{
		public enum Tab
		{
			unityDlls,
			projectDlls,
			scripts,
			missings,
			responseFile,
			build
		}

		public enum Compiler
		{
			GMCS,
			MCS
		}

		public Compiler compiler = Compiler.GMCS;

		public string mcsPathWin = "";
		public string mcsPathOsx = "";
		public string mcsPathLinux = "";

		[System.Serializable]
		public class AssemblyPreset
		{
			

			public string guid = "";
			public string presetname = "";
			public string dllname = "";
			public string osxPath = "";
			public string winPath = "";
			public string linuxPath = "";
			public bool debug = false;
			public bool delaysign = false;
		
			public PlatformEnum platform = PlatformEnum.Default;
			public CodepageEnum codepage = CodepageEnum.Default;
			public string define = "";
			public bool noconfig = false;

			[System.Serializable]
			public class CompilationItem
			{
				public bool found = false;
				public string location = "";
			}

			public Tab selectedTab = Tab.unityDlls;

			public string unityDllsFilter = "";
			public bool unityDllsSelected = false;
			public string projectDllsFilter = "";
			public bool projectDllsSelected = false;
			public string scriptsFilter = "";
			public bool scriptsSelected = false;

			public List<CompilationItem> osxUnityDlls = new List<CompilationItem> ();
			public List<CompilationItem> winUnityDlls = new List<CompilationItem> ();
			public List<CompilationItem> linuxUnityDlls = new List<CompilationItem> ();
			public List<CompilationItem> projectDlls = new List<CompilationItem> ();
			public List<CompilationItem> libraryDlls = new List<CompilationItem> ();
			public List<CompilationItem> scripts = new List<CompilationItem> ();
		}

		public enum PlatformEnum
		{
			Default,
			Anycpu,
			X86,
			X64,
			Itanium
		}

		public enum CodepageEnum
		{
			Default,
			Number,
			UTF8,
			Reset
		}

		public int selectedIndex = -1;

		public List<AssemblyPreset> presets = new List<AssemblyPreset> ();

		[System.Serializable]
		public class Filter
		{
			public string name = "";
			public bool include = true;
		}

		public List<Filter> osxFilters = new List<Filter> () {
			new Filter{ name = "Managed", include = true },
			new Filter{ name = "UnityExtensions", include = true },
		};

		public List<Filter> winFilters = new List<Filter> () {
			new Filter{ name = "Managed", include = true },
			new Filter{ name = "UnityExtensions", include = true },
		};

		public List<Filter> linuxFilters = new List<Filter> () {
			new Filter{ name = "Managed", include = true },
			new Filter{ name = "UnityExtensions", include = true },
		};

		private static AssemblyBuilderSettings _instance = null;

		public static AssemblyBuilderSettings instance {
			get {
				if (_instance == null) {
					string[] presetPaths = AssetDatabase.FindAssets ("t:AssemblyBuilderSettings");
					if (presetPaths.Length > 0) {
						_instance = AssetDatabase.LoadAssetAtPath<AssemblyBuilderSettings> (AssetDatabase.GUIDToAssetPath (presetPaths [0]));
					} else {
						_instance = ForieroEngine.FResources.EditorInstance<AssemblyBuilderSettings> ("AssemblyBuilderSettings", "Assets/Editor/AssemblyBuilder");
					}
				}
				return _instance;
			}
		}
	}
}
