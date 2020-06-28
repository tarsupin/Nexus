using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.GameEngine {

	public class PlanetData {

		public string name;
		public string sprite;
		public byte head;
		public byte suit;
		public byte hat;
		public List<MoonData> moons;
		public Vector2 textSize;

		public PlanetData(string name, byte planetID, byte head = 0, byte suit = 0, byte hat = 0) {
			this.name = name;
			this.sprite = PlanetInfo.Planets[(byte) planetID];
			this.head = head;
			this.suit = suit;
			this.hat = hat;
			this.moons = new List<MoonData>();
			this.textSize = Systems.fonts.baseText.font.MeasureString(name);
		}

		public void AddMoon(byte moonID) {
			this.moons.Add(new MoonData(moonID));
		}
	}

	public class MoonData {
		public string sprite;
		public short lat;
		public float speed;
		public float posX;
		public float posY;
		public bool front;

		public MoonData(byte moonID) {
			this.sprite = PlanetInfo.Planets[(byte) moonID];
			this.lat = (short) CalcRandom.IntBetween(-10, 19);
			this.speed = (float) CalcRandom.FloatBetween(0.15f, 0.8f);
			this.posX = (short) CalcRandom.IntBetween(-50, 50);
			this.posY = lat;
			this.front = true;
		}
	}

	public class PlanetSelectScene : Scene {

		// References
		public Atlas atlas;
		public readonly PlayerInput playerInput;
		public readonly MenuUI menuUI;

		// Paging & Selection
		public short page = 0;
		public byte selectX = 0;
		public byte selectY = 0;

		// Planets
		public Dictionary<short, PlanetData> planets = new Dictionary<short, PlanetData>();

		public PlanetSelectScene() : base() {

			// Prepare Components
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];
			this.menuUI = new MenuUI(this, MenuUI.MenuUIOption.PlanetSelect);

			// Prepare Temporary Planet(s)
			this.planets.Add(0, new PlanetData("Astaria", (byte) PlanetInfo.PlanetID.Grassland));
			this.planets.Add(1, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));

			// TEMP
			this.planets.Add(2, new PlanetData("Tutorial", (byte) PlanetInfo.PlanetID.Ice));
			this.planets.Add(3, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(4, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(5, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(6, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(7, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(8, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(9, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(10, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(11, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(12, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(13, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(14, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(15, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(16, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));
			this.planets.Add(17, new PlanetData("Grimland", (byte) PlanetInfo.PlanetID.Toxic));

			// Astaria
			this.planets[0].AddMoon((byte)PlanetInfo.PlanetID.MoonBrown);

			// Grimland
			this.planets[1].AddMoon((byte)PlanetInfo.PlanetID.MoonGreen);
			this.planets[1].AddMoon((byte)PlanetInfo.PlanetID.MoonRed);
		}

		public override void StartScene() {

			// Reset Timer
			Systems.timer.ResetTimer();
		}

		public override void EndScene() {
			//if(Systems.music.whatever) { Systems.music.SomeTrack.Stop(); }
		}

		public override void RunTick() {

			// Update Timer
			Systems.timer.RunTick();

			// Loop through every player and update inputs for this frame tick:
			foreach(var player in Systems.localServer.players) {
				//player.Value.input.UpdateKeyStates(Systems.timer.Frame);
				player.Value.input.UpdateKeyStates(0); // TODO: Update LocalServer so frames are interpreted and assigned here.
			}

			// Menu UI is active:
			if(this.uiState == UIState.SubMenu || this.uiState == UIState.MainMenu) {
				this.menuUI.RunTick();
				return;
			}

			// Play UI is active:
			InputClient input = Systems.input;

			// Open Menu
			if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
				this.uiState = UIState.MainMenu;
			}

			// Movement
			else if(playerInput.isPressed(IKey.Up)) {
				if(this.selectY > 0) { this.selectY--; }
			}
			else if(playerInput.isPressed(IKey.Down)) {
				if(this.selectY < 2) { this.selectY++; }
			}
			else if(playerInput.isPressed(IKey.Left)) {
				if(this.selectX > 0) { this.selectX--; }
			}
			else if(playerInput.isPressed(IKey.Right)) {
				if(this.selectX < 6) { this.selectX++; }
			}

			// Activate Node
			else if(playerInput.isPressed(IKey.AButton) == true) {
				// TODO: Do a thing here. Load Planet.
			}

			// Update Moon Positions
			for(short i = 0; i < this.planets.Count; i++) {
				PlanetData planet = this.planets[i];

				foreach(MoonData moon in planet.moons) {
					moon.posX += moon.speed;

					if(moon.speed > 0 && moon.posX > 70) {
						moon.posX = 70;
						moon.speed = (float)-Math.Abs(moon.speed);
						moon.front = false;
					}
					
					else if(moon.posX < -70) {
						moon.posX = -70;
						moon.speed = (float)Math.Abs(moon.speed);
						moon.front = true;
					}

					if(moon.speed > 0) {
						moon.posY = moon.lat - (float)Math.Sqrt(70 - Math.Abs(moon.posX)) * -3.2f;
					}
					else if(moon.speed < 0) {
						moon.posY = moon.lat + (float)Math.Sqrt(70 - Math.Abs(moon.posX)) * -3.2f;
					}
				}
			}
		}

		public override void Draw() {

			// SamplerState.PointClamp will force Sprites to draw without blurring effect. Without this, all scaling upward is blurry.
			Systems.spriteBatch.End();
			Systems.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, Systems.screen.windowWidth, Systems.screen.windowHeight), Color.DarkSlateGray);

			short posX = 100;
			short posY = 100;

			// Draw Current Selection
			short highlightX = (short)(this.selectX * 200 + 100);
			short highlightY = (short)(this.selectY * 200 + 100);

			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 60, highlightY - 60, 155, 170), Color.DarkRed);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 50, highlightY - 50, 135, 150), Color.DarkSlateGray);

			// Draw Planets
			for(short i = 0; i < this.planets.Count; i++) {
				PlanetData planet = this.planets[i];
				this.DrawPlanet(planet, posX, posY);

				// Update Next Position
				posX += 200;
				if(posX >= 1500) { posY += 200; posX = 100; }
			}
		}

		public void DrawPlanet( PlanetData planetData, short posX, short posY ) {

			foreach(MoonData moon in planetData.moons) {
				if(!moon.front) { this.atlas.Draw(moon.sprite, posX + (short) moon.posX, posY + (short) moon.posY); }
			}

			this.atlas.DrawAdvanced(planetData.sprite, posX + 2, posY, Color.White, 0f, 4);

			Systems.fonts.baseText.Draw(planetData.name, posX + 16 - (byte)Math.Floor(planetData.textSize.X * 0.5f), posY + 75, Color.White);

			foreach(MoonData moon in planetData.moons) {
				if(moon.front) { this.atlas.Draw(moon.sprite, posX + (short) moon.posX, posY + (short) moon.posY); }
			}
		}
	}
}
