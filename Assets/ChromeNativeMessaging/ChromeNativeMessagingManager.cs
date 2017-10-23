using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//using System;
//using System.IO;
//using System.Text;
//
//using UnityEngine;
//
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


///// <summary>
///// Redirects writes to System.Console to Unity3D's Debug.Log.
///// </summary>
///// <author>
///// Jackson Dunstan, http://jacksondunstan.com/articles/2986
///// </author>
//public static class UnitySystemConsoleRedirector
//{
//	private class UnityTextWriter : TextWriter
//	{
//		private StringBuilder buffer = new StringBuilder();
//
//		public override void Flush()
//		{
//			Debug.Log(buffer.ToString());
//			buffer.Length = 0;
//		}
//
//		public override void Write(string value)
//		{
//			buffer.Append(value);
//			if (value != null)
//			{
//				var len = value.Length;
//				if (len > 0)
//				{
//					var lastChar = value [len - 1];
//					if (lastChar == '\n')
//					{
//						Flush();
//					}
//				}
//			}
//		}
//
//		public override void Write(char value)
//		{
//			buffer.Append(value);
//			if (value == '\n')
//			{
//				Flush();
//			}
//		}
//
//		public override void Write(char[] value, int index, int count)
//		{
//			Write(new string (value, index, count));
//		}
//
//		public override Encoding Encoding
//		{
//			get { return Encoding.Default; }
//		}
//	}
//
//	public static void Redirect()
//	{
//		Console.SetOut(new UnityTextWriter());
//	}
//}

public class BridgeMessage
{
	public string Command {get; set;}
	public string Value {get; set;}
}

public class ChromeNativeMessagingManager : MonoBehaviour {

	void Awake() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 10;
	}

	// Use this for initialization
	void Start () {
//		System.IO.StreamWriter standardOutput = new System.IO.StreamWriter(System.Console.OpenStandardOutput());
//		standardOutput.AutoFlush = true;
//		System.Console.SetOut(standardOutput);
		//UnitySystemConsoleRedirector.Redirect();
		//Screen.SetResolution(640, 480, false);

		Console.Write ("aaa");
		string[] args = System.Environment.GetCommandLineArgs(); //new List<string>();

		//string input = "";
		for (int i = 0; i < args.Length; i++) {
			Debug.Log ("ARG " + i + ": " + args [i]);
			//if (args [i] == "-test") {
			GameObject.Find("ChromeNativeMessagingDebugText").GetComponent<Text> ().text = args [i];
				System.Console.WriteLine ("success");
				Debug.Log("Hello, my name ");
			//}
		}
//
//		// first read input till there are nonempty items, means they are not null and not ""
//		// also add read item to list do not need to read it again    

	}

	public bool ProcessMessage(BridgeMessage data)
	{
		switch (data.Command)
		{
		case "healthcheck":
			return true;
		case "exit":
			return true;
		case "text":
			GameObject.Find ("ChromeNativeMessagingDebugText").GetComponent<Text> ().text = data.Value;

			this.SendMessage ("event", "commandReceived");

			return true;
		default:
			Console.Error.Write ("Unhandled event: " + data.Command + ": " + data.Value);
			return false;
		}
	}

	void SendMessage(string key, string value) {
		string message = "{\"Command\": \"" + key + "\", \"Value\": \"" + value + "\"}";

		byte[] lengthBytes = Encoding.ASCII.GetBytes (message);
		byte[] bytes = new byte[4];

		bytes[0] = (byte)((lengthBytes.Length >> 0) & 0xFF);
		bytes[1] = (byte)((lengthBytes.Length >> 8) & 0xFF);
		bytes[2] = (byte)((lengthBytes.Length >> 16) & 0xFF);
		bytes[3] = (byte)((lengthBytes.Length >> 24) & 0xFF);

		Console.Error.Write(Encoding.ASCII.GetString(bytes));
		Console.Error.Write(message);
		Console.Error.Flush();
	}
	
	// Update is called once per frame
	void Update () {
		var stdin = Console.OpenStandardInput();
		var length = 0;
		var lengthBytes = new byte[4];

		stdin.Read (lengthBytes, 0, 4);

		length = BitConverter.ToInt32(lengthBytes, 0);

		//this.SendMessage("debug", "Reading length");

		var buffer = new char[length];
		using (var reader = new StreamReader(stdin))
		{
			int totalRead = 0;
			while (totalRead < length)
			{
				totalRead += reader.Read(buffer, 0, buffer.Length);
			}
		}

		//this.SendMessage("debug", "Read all");


		try
		{
			//string message = "{\"Command\":\"something\",\"Value\":\"nothing\"}";
			BridgeMessage data = JsonConvert.DeserializeObject<BridgeMessage> (new string(buffer)); //new string(buffer));

			//GameObject.Find ("ChromeNativeMessagingDebugText").GetComponent<Text> ().text = data.Command;

			this.ProcessMessage(data);
		}
		catch (Exception ex)
		{
			//this.SendMessage("Text", ex.Message);
		}



//		var stdin = Console.OpenStandardInput();
//		var reader = new StreamReader (stdin);
//		var length = 0;
//		var lengthBytes = new char[2];
//
//		//this.SendMessage("Reading length");
//		//Console.Error.Write ("Reading length");
//
//		reader.ReadBlock (lengthBytes, 0, 2);
//		//this.SendMessage("Done reading length");
//
//		if (reader.Peek() > 0) {
//			length = BitConverter.ToInt32 (Encoding.ASCII.GetBytes(lengthBytes), 0);
//			this.SendMessage (length.ToString ());
//
//			//this.SendMessage("Reading package");
//
//			var buffer = new char[length];
//
//			reader.ReadBlock (buffer, 0, length);
//
//			string newStr = new string (buffer);
//
//			this.SendMessage (newStr);
//
//			JObject obj = (JObject)JsonConvert.DeserializeObject<JObject> (newStr);
//
//			if (obj.Property ("Text") != null) {
//				//this.SendMessage(obj.Property("Text").ToString());
//				GameObject.Find ("ChromeNativeMessagingDebugText").GetComponent<Text> ().text = obj.Property ("Text").ToString ();
//			}
//		}



//
//
//		string line;
//
//		List<string> input = new List<string>();
//		while ((line = Console.ReadLine()) != null && line != "") {
//			input.Add(line);
//		}
//
//		for (int i = 0; i < input.Count; i++) {
//			GameObject.Find("ChromeNativeMessagingDebugText").GetComponent<Text> ().text = input[i];
//		}
//
//		
//		string[] args = System.Environment.GetCommandLineArgs(); //new List<string>();
//
//		//string input = "";
//		for (int i = 0; i < args.Length; i++) {
//			//if (args [i] == "-test") {
//			GameObject.Find("ChromeNativeMessagingDebugText").GetComponent<Text> ().text = args [i];
//			//}
//		}
	}
}
