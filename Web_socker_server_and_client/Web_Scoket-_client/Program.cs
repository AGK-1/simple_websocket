using System;
using WebSocketSharp;
using Newtonsoft.Json;

namespace Web_Socket_Client
{
	public class Vector
	{
		public double x { get; set; }
		public double y { get; set; }
	}
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Client");
			using (WebSocket ws = new WebSocket("ws://localhost:7891/Echo"))
			{
				ws.OnMessage += Ws_OnMessage;
				ws.Connect();

				Console.WriteLine("Connected to the WebSocket server.");

				// Continuously read user input and send it to the WebSocket server
				string message;
				while ((message = Console.ReadLine()) != null)
				{
					if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
					{
						break; // Exit the loop if the user types "exit"
					}

					ws.Send(message);  // Send the message entered by the user to the server
				}
			}


		}

		private static void Ws_OnMessage(object sender, MessageEventArgs e)
		{
			//Console.WriteLine("Received from the server: " + e.Data);

			if (e.Data.StartsWith("{") && e.Data.EndsWith("}"))
			{
				try
				{
					Vector pos = JsonConvert.DeserializeObject<Vector>(e.Data);
					DrawDot(pos.x, pos.y, 50, 15, 1);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Failed to deserialize vector: {ex.Message}");
				}
			}
			else
			{
				Console.WriteLine($"Received non-JSON data: {e.Data}");
			}
		}

		// Assuming you have a DrawDot method somewhere
		private static void DrawDot(double x, double y, int width, int height, int thickness)
		{
			Console.SetCursorPosition((int)x, (int)y);
			Console.Write(new string('*', thickness));

		}
	}
}