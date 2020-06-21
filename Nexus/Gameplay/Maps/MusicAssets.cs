using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Nexus.Engine;
using System.Collections.Generic;

// https://www.gamefromscratch.com/post/2015/07/25/MonoGame-Tutorial-Audio.aspx

namespace Nexus.Gameplay {

	public class MusicAssets {

		public enum MusicTrack : byte {
			None = 0,

			// Peaceful
			Happy1 = 1,
			Happy2 = 2,
			Peaceful = 3,
			PleasantDay1 = 4,
			PleasantDay2 = 5,

			// Themes
			CasualDesert = 21,
			CasualDungeon = 22,
			CasualForest = 23,
			CasualGrassland = 24,
			CasualIceland = 25,

			// Journey
			EpicTask = 41,
			Journey = 42,

			// Intense
			Intensity1 = 61,
			Intensity2 = 62,
			Intensity3 = 63,
			Intensity4 = 64,

			// Challenges
			BossFight1 = 81,
			BossFight2 = 82,
			Challenge1 = 83,
			Challenge2 = 84,
			Challenge3 = 85,
		}

		ContentManager content;

		private Song currentTrack;

		private Dictionary<byte, string> trackNames = new Dictionary<byte, string>() {

			// Peaceful
			{ (byte) MusicTrack.Happy1, "Music/Happy1" },
			{ (byte) MusicTrack.Happy2, "Music/Happy2" },
			{ (byte) MusicTrack.Peaceful, "Music/Peaceful" },
			{ (byte) MusicTrack.PleasantDay1, "Music/PleasantDay1" },
			{ (byte) MusicTrack.PleasantDay2, "Music/PleasantDay2" },
			
			// Themes
			{ (byte) MusicTrack.CasualDesert, "Music/CasualDesert" },
			{ (byte) MusicTrack.CasualDungeon, "Music/CasualDungeon" },
			{ (byte) MusicTrack.CasualForest, "Music/CasualForest" },
			{ (byte) MusicTrack.CasualGrassland, "Music/CasualGrassland" },
			{ (byte) MusicTrack.CasualIceland, "Music/CasualIceland" },

			// Journey
			{ (byte) MusicTrack.EpicTask, "Music/EpicTask" },
			{ (byte) MusicTrack.Journey, "Music/Journey" },

			// Intense
			{ (byte) MusicTrack.Intensity1, "Music/Intensity1" },
			{ (byte) MusicTrack.Intensity2, "Music/Intensity2" },
			{ (byte) MusicTrack.Intensity3, "Music/Intensity3" },
			{ (byte) MusicTrack.Intensity4, "Music/Intensity4" },
			
			// Challenges
			{ (byte) MusicTrack.BossFight1, "Music/BossFight1" },
			{ (byte) MusicTrack.BossFight2, "Music/BossFight2" },
			{ (byte) MusicTrack.Challenge1, "Music/Challenge1" },
			{ (byte) MusicTrack.Challenge2, "Music/Challenge2" },
			{ (byte) MusicTrack.Challenge3, "Music/Challenge3" },
		};

		public MusicAssets(GameClient game) {
			this.content = game.Content;
		}

		public void Play(MusicTrack track) {

			// Make sure the track exists:
			if(!this.trackNames.ContainsKey((byte) track)) { return; }

			// Load the Music Track
			this.currentTrack = this.content.Load<Song>(this.trackNames[(byte) track]);

			MediaPlayer.Volume = Systems.settings.audio.MusicVolume;
			MediaPlayer.Play(this.currentTrack);
			MediaPlayer.IsRepeating = true;

			// See this link for more details: https://www.gamefromscratch.com/post/2015/07/25/MonoGame-Tutorial-Audio.aspx
			// MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
		}
	}
}
