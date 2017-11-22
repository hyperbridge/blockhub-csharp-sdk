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
using Debug = UnityEngine.Debug;

using ForieroEngine.Extensions;

namespace ForieroEditor.AssemblyBuilder
{
	public partial class AssemblyBuilder : EditorWindow
	{
		string output = "";
		string errors = "";
		string response = "";

		void BuildAssembly (m_AssemblyBuilderSettings m_preset, bool compile)
		{
		
			resultedDll = "";
			output = "";
			errors = "";
			response = GetResponseFileString (m_preset, compile);

			string[] files = null;
			string arguments = "";
			string responseFilePath = "";
			string cmd = "";

			if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) {
				responseFilePath = Path.Combine (Directory.GetCurrentDirectory (), "Temp/ab_response.res");
			} else {
				responseFilePath = Path.Combine (Directory.GetCurrentDirectory (), "Temp\\ab_response.res");
			}

			if (File.Exists (responseFilePath)) {
				File.Delete (responseFilePath);
			}

			File.WriteAllText (responseFilePath, response);

			Debug.Log (cmd + " " + responseFilePath);

			AssemblyBuilderSettings.Compiler c = (AssemblyBuilderSettings.Compiler)compiler.intValue;

			if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX) {

				switch (c) {
				case AssemblyBuilderSettings.Compiler.MCS:
					cmd = Path.Combine (EditorApplication.applicationContentsPath, "MonoBleedingEdge/bin/mcs");
					break;
				case AssemblyBuilderSettings.Compiler.GMCS:
					cmd = Path.Combine (EditorApplication.applicationContentsPath, "Mono/bin/gmcs");
					break;
				}

				files = Directory.GetFiles (Application.dataPath, "AssemblyBuilderCompile.sh", SearchOption.AllDirectories);
				arguments = string.Format ("\"{0}\" \"{1}\" \"{2}\"", Path.GetDirectoryName (cmd), Path.GetFileName (cmd), responseFilePath);
			} else {

				switch (c) {
				case AssemblyBuilderSettings.Compiler.MCS:
					cmd = Path.Combine (EditorApplication.applicationContentsPath.FixOSPath (), @"MonoBleedingEdge\bin\mcs.bat");
					break;
				case AssemblyBuilderSettings.Compiler.GMCS:
					cmd = Path.Combine (EditorApplication.applicationContentsPath.FixOSPath (), @"Mono\bin\gmcs.bat");
					break;
				}

				files = Directory.GetFiles (Application.dataPath.FixOSPath (), "AssemblyBuilderCompile.bat", SearchOption.AllDirectories);
				arguments = string.Format ("\"{0}\" \"{1}\" \"{2}\"", Path.GetDirectoryName (cmd), Path.GetFileName (cmd), responseFilePath);
			}

			#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX

			var process = new Process {
				StartInfo = {
					FileName = "chmod",
					Arguments = "+x \"" + files [0] + "\"",
					UseShellExecute = false,
					CreateNoWindow = true
				}
			};
			process.Start ();
			process.WaitForExit ();
			process.Dispose ();

			#endif

			Process pMCS = new Process ();

			try {
				pMCS.StartInfo.UseShellExecute = false;
				pMCS.StartInfo.WorkingDirectory = Path.GetDirectoryName (cmd);
				pMCS.StartInfo.FileName = files [0];
				pMCS.StartInfo.Arguments = arguments;
				pMCS.StartInfo.RedirectStandardOutput = true;
				pMCS.StartInfo.RedirectStandardError = true;

				pMCS.Start ();
				output = pMCS.StandardOutput.ReadToEnd ();
				errors = pMCS.StandardError.ReadToEnd ();
				pMCS.WaitForExit ();
				pMCS.Close ();

				resultedDll = GetCompiledDllPath (m_preset, compile);
				Debug.Log ("Dll stored : " + resultedDll);
			} catch (Exception e) {
				errors += e.Message + e.GetType ().ToString ();
				Debug.LogError (errors);
			} finally {
				pMCS.Dispose ();
			}
		}
	}
}
