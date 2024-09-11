using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Web_Socket_Server
{
	public class Echo : WebSocketBehavior
	{
		protected override void OnMessage(MessageEventArgs e)
		{

			Console.WriteLine("Message received: " + e.Data);
			Send(e.Data);  // Echoes the received message back to the client
		}
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Server");
			// Create a WebSocketServer at the specified URL, without the /Echo path
			WebSocketServer wwsrv = new WebSocketServer("ws://localhost:7891");

			// Add the WebSocket service for the /Echo path
			wwsrv.AddWebSocketService<Echo>("/Echo");

			// Start the WebSocket server
			wwsrv.Start();
			Console.WriteLine("WebSocket server started at ws://localhost:7890");

			// Keep the server running until the user presses Enter
			Console.ReadLine();

			// Stop the server
			wwsrv.Stop();
		}
	}
}