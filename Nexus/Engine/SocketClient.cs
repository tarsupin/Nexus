using System;
using WebSocketSharp;

namespace Nexus.Engine {

	public class SocketClient {

		public WebSocket ws;
		public const string socketDomain = "127.0.0.1";
		public const short port = 8000;

		public SocketClient() {

			// Using 'wss' scheme WebSocket URL will use secure connection to server.
			this.ws = new WebSocket("ws://" + socketDomain + ":" + port);
			//this.ws = new WebSocket("ws://echo.websocket.org");
			//using(var ws = new WebSocket("ws://echo.websocket.org"))

			this.ws.OnOpen += (sender, e) => {
				this.ws.Send("Hi, there!");
			};

			this.ws.OnMessage += (sender, e) => {
				if(e.IsText) {
					Console.WriteLine("Received String: " + e.Data);
				} else if(e.IsBinary) {
					int frame = BitConverter.ToInt32(e.RawData, 1);
					Console.WriteLine("On frame " + frame.ToString() + ", received 1st instruction of " + e.RawData[0].ToString());
					//Console.WriteLine("Received Binary Data: " + BitConverter.ToString(e.RawData));
					//Console.WriteLine("Received Binary Data: " + System.Text.Encoding.UTF8.GetString(e.RawData, 0, e.RawData.Length));
				}
			};

			this.ws.OnError += (sender, e) => {
				Console.WriteLine("Socket Client Error: " + e.Message);
			};

			this.ws.OnClose += (sender, e) => {
				Console.WriteLine("Socket Client OnClose: " + e.Code + "; " + e.Reason);
			};

#if DEBUG
			// Debug Settings
			this.ws.Log.Level = LogLevel.Debug; // To change the logging level.
			// this.ws.WaitTime = TimeSpan.FromSeconds (10); // To change the wait time for the response to the Ping or Close.
			// this.ws.EmitOnPing = true;	// To emit a WebSocket.OnMessage event when receives a ping.
#endif

			// To enable the Per-message Compression extension.
			// this.ws.Compression = CompressionMethod.Deflate;

			// To validate the server certificate.
			/*
			this.ws.SslConfiguration.ServerCertificateValidationCallback =
				(sender, certificate, chain, sslPolicyErrors) => {
				this.ws.Log.Debug (
					String.Format (
					"Certificate:\n- Issuer: {0}\n- Subject: {1}",
					certificate.Issuer,
					certificate.Subject
					)
				);
				return true; // If the server certificate is valid.
				};
				*/

			// To send the credentials for the HTTP Authentication (Basic/Digest).
			// this.ws.SetCredentials ("nobita", "password", false);

			// To send the Origin header.
			// this.ws.Origin = "http://localhost:4649";

			// To send the cookies.
			// this.ws.SetCookie (new Cookie ("name", "nobita"));
			// this.ws.SetCookie (new Cookie ("roles", "\"idiot, gunfighter\""));

			// To connect through the HTTP Proxy server.
			// this.ws.SetProxy ("http://localhost:3128", "nobita", "password");

			// To enable the redirection.
			// this.ws.EnableRedirection = true;

			// Connect to the server.
			// this.ws.Connect();

			// Connect to the server asynchronously.
			this.ws.ConnectAsync();
		}

		// Send a Socket Message with Text
		public void SendText( string message ) {
			this.ws.SendAsync(message, SocketClient.AsyncMessage);
		}

		// Send a Socket Message with Binary Data
		public void SendData(byte[] data) {
			this.ws.SendAsync(data, SocketClient.AsyncMessage);
		}

		public static void AsyncMessage(bool b) {
			// Console.WriteLine("Got to AsyncMessage function.");
		}
	}
}