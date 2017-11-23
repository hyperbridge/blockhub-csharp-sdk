using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


class Reader {
	private static Thread inputThread;
	private static AutoResetEvent getInput, gotInput;
	private static string input;

	static Reader() {
		getInput = new AutoResetEvent(false);
		gotInput = new AutoResetEvent(false);
		inputThread = new Thread(reader);
		inputThread.IsBackground = true;
		inputThread.Start();
	}

	private static void reader() {

		while (true) {
			getInput.WaitOne();

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

			input = new string(buffer);

			gotInput.Set();
		}
	}

	// omit the parameter to read a line without a timeout
	public static string ReadLine(int timeOutMillisecs = Timeout.Infinite) {
		getInput.Set();
		bool success = gotInput.WaitOne(timeOutMillisecs);
		if (success)
			return input;
		else
			throw new TimeoutException("User did not provide input within the timelimit.");
	}
}

public class BridgeMessage
{
	public string Command {get; set;}
	public string Value {get; set;}
}

public class ChromeNativeMessagingManager : MonoBehaviour {

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		this.SendMessage ("event", "initialized");
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

		//Console.Error.Write(Encoding.ASCII.GetString(bytes));
		Console.Error.Write(message);
		Console.Error.Flush();
	}
	
	// Update is called once per frame
	void Update () {
		try {
			string message = Reader.ReadLine(5);
			if (message != null && message != "") {
				Debug.Log("stdin message received: " + message);
				BridgeMessage data = JsonConvert.DeserializeObject<BridgeMessage>(message);
				this.ProcessMessage(data);
			}
		} catch (TimeoutException) {
			//this.SendMessage("debug", "xx");
		}
	}
}
