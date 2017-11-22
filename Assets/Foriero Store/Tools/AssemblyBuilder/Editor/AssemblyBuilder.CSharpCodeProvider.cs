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
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Mono.CSharp;
using Debug = UnityEngine.Debug;

namespace ForieroEditor.AssemblyBuilder
{
	public partial class AssemblyBuilder : EditorWindow
	{
		//https://msdn.microsoft.com/en-us/library/ms228625.aspx
		//https://msdn.microsoft.com/en-us/library/microsoft.csharp.csharpcodeprovider(v=vs.110).aspx

		public Assembly CompileAssembly (SerializedProperty preset, string outputPath)
		{
			output = "";
			errors = "";
										
			var options = new Dictionary<string,string> {
				{ "CompilerVersion", "v2.0" }
			};

			var codeProvider = new CSharpCodeProvider (options);
		
			var compilerParameters = new CompilerParameters {
				GenerateExecutable = false,
				GenerateInMemory = false,
				IncludeDebugInformation = true,
				OutputAssembly = output,
				CompilerOptions = "/target:library /optimize /warn:0"
			};


			for (int i = 0; i < preset.FindPropertyRelative ("unityDlls").arraySize; i++) {
				var item = preset.FindPropertyRelative ("unityDlls").GetArrayElementAtIndex (i);
				var path = Path.Combine (EditorApplication.applicationContentsPath, item.FindPropertyRelative ("location").stringValue.Remove (0, 1));
				Debug.Log (path);
				compilerParameters.ReferencedAssemblies.Add (path);
			}
				
			for (int i = 0; i < preset.FindPropertyRelative ("projectDlls").arraySize; i++) {
				var item = preset.FindPropertyRelative ("projectDlls").GetArrayElementAtIndex (i);
				var path = Path.Combine (Application.dataPath, item.FindPropertyRelative ("location").stringValue.Remove (0, 1));
				Debug.Log (path);
				compilerParameters.ReferencedAssemblies.Add (path);
			}
				
			for (int i = 0; i < preset.FindPropertyRelative ("libraryDlls").arraySize; i++) {
				var item = preset.FindPropertyRelative ("libraryDlls").GetArrayElementAtIndex (i);
				var path = Path.Combine (Path.Combine (Directory.GetCurrentDirectory (), "Library"), item.FindPropertyRelative ("location").stringValue.Remove (0, 1));
				Debug.Log (path);
				compilerParameters.ReferencedAssemblies.Add (path);
			}
									
			List<string> sourceFiles = new List<string> ();

			for (int i = 0; i < preset.FindPropertyRelative ("scripts").arraySize; i++) {
				var item = preset.FindPropertyRelative ("scripts").GetArrayElementAtIndex (i);
				var path = Path.Combine (Application.dataPath, item.FindPropertyRelative ("location").stringValue.Remove (0, 1));
				Debug.Log (path);
				sourceFiles.Add (path);
			}

			var result = codeProvider.CompileAssemblyFromFile (compilerParameters, sourceFiles.ToArray ()); 

			foreach (string o in result.Output) {
				output += o;
			}

			foreach (CompilerError e in result.Errors) {
				errors += e.ErrorText;
			}

			return result.CompiledAssembly;
		}
	}
}
