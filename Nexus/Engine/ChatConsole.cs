﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Nexus.Engine {

	public class ChatLine {

		public string[] message; // The message that the user wrote, divided into new lines as needed.
		public Color color; // The color of the text.

		public ChatLine( string text, Color color, string username = "" ) {
			this.color = color;

			string messagePrep = (username.Length > 0 ? "[" + username + "] " : "") + text;

			// Cut the text into segments based on width allowed (with roughly 10px padding on each side).
			this.message = TextHelper.WrapTextSplit(Systems.fonts.console.font, messagePrep, ChatConsole.width - 18);
		}

		public void Draw( int posX, int posY, byte lineHeight = 15 ) {
			byte len = (byte) message.Length;
			for(byte i = 0; i < len; i++) {
				Systems.fonts.console.Draw(message[i], posX, posY + i * lineHeight, this.color);
			}
		}
	}

	public static class ChatConsole {

		// Visuals
		public static bool showBackground = true;
		public static short fromBottom = 100;
		public static short height = 400;
		public static short width = 400;

		public static bool isEnabled = true;	// If console is not enabled, no commands can be triggered (even through macros).
		public static bool isVisible = true;	// Whether or not console is currently visible.

		public static LinkedList<ChatLine> chatLines = new LinkedList<ChatLine>(); // Tracks the last twenty lines that have been typed.

		public static void SetEnabled(bool enable) { ChatConsole.isEnabled = enable; if(!enable) { ChatConsole.isVisible = false; } }
		public static void SetVisible(bool visible) { if(ChatConsole.isEnabled) { ChatConsole.isVisible = visible; } else { ChatConsole.isVisible = false; } }

		public static void SendMessage(string message, Color color, string username = "") {
			if(!ChatConsole.isEnabled) { return; }
			ChatConsole.chatLines.AddFirst(new ChatLine(message, color, username));

			// If there are over 20 lines, remove the last one.
			if(ChatConsole.chatLines.Count > 20) {
				ChatConsole.chatLines.RemoveLast();
			}
		}

		public static void Clear() {
			while(ChatConsole.chatLines.Count > 0) {
				ChatConsole.chatLines.RemoveFirst();
			}
		}

		public static void Draw() {
			if(!ChatConsole.isVisible) { return; }

			short bottomPos = (short) (Systems.screen.viewHeight - ChatConsole.fromBottom);

			// Draw Console Background
			if(ChatConsole.showBackground) {
				Systems.spriteBatch.Draw(Systems.tex2dBlack, new Rectangle(0, bottomPos - ChatConsole.height, ChatConsole.width, ChatConsole.height), Color.Black * 0.65f);
			}

			// Draw Console Lines
			byte numLines = 0;

			foreach(ChatLine line in chatLines) {
				numLines += (byte) line.message.Length;
				line.Draw(10, bottomPos - 15 * numLines, 15);
			}
		}
	}
}
