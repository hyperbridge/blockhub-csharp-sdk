using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace NativeMessagingHost
{
	class Program
	{
		public static void Main(string[] args)
		{
			JObject data;
			while ((data = Read()) != null)
			{
				var processed = ProcessMessage(data);
				//Write(processed);
				if (processed == "exit")
				{
					return;
				}
			}
		}

		public static string ProcessMessage(JObject data)
		{
			var message = data["text"].Value<string>();
			switch (message)
			{
			case "test":
				return "testing!";
			case "exit":
				return "exit";
			default:
				return "echo: " + message;
			}
		}

		public static JObject Read()
		{
			var stdin = Console.OpenStandardInput();
			var length = 0;

			var lengthBytes = new byte[4];
			stdin.Read(lengthBytes, 0, 4);
			length = BitConverter.ToInt32(lengthBytes, 0);

			var buffer = new char[length];
			using (var reader = new StreamReader(stdin))
			{
				while (reader.Peek() >= 0)
				{
					reader.Read(buffer, 0, buffer.Length);
				}
			}

			return (JObject)JsonConvert.DeserializeObject<JObject>(new string(buffer));
		}

		public static void Write(JObject data)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(data.ToString(Formatting.None));

			var stdout = Console.OpenStandardOutput();
			stdout.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
			stdout.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
			stdout.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
			stdout.WriteByte((byte)((bytes.Length >> 24) & 0xFF));
			stdout.Write(bytes, 0, bytes.Length);
			stdout.Flush();
		}
	}
}

public class ChromeNativeMessagingDebugButton : MonoBehaviour {

	public void SendCommand()
	{
		string msg = "{\"Command\": \"text\", \"Value\": \"" + GameObject.Find ("ChromeNativeMessagingDebugInput").GetComponent<Text>().text + "\"}";

		byte[] msgBytes = Encoding.ASCII.GetBytes (msg);
		byte[] bytes = new byte[4];

		bytes[0] = (byte)((msgBytes.Length >> 0) & 0xFF);
		bytes[1] = (byte)((msgBytes.Length >> 8) & 0xFF);
		bytes[2] = (byte)((msgBytes.Length >> 16) & 0xFF);
		bytes[3] = (byte)((msgBytes.Length >> 24) & 0xFF);

		Console.Error.Write(Encoding.ASCII.GetString(bytes));
		Console.Error.Write(msg);
		Console.Error.Flush();
	}

}
