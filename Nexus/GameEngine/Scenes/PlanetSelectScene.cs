using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Nexus.Engine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nexus.GameEngine {

	public class PlanetData {

		public string name;
		public string sprite;
		public string worldID;
		public byte diff;
		public byte head;
		public byte suit;
		public byte hat;
		public List<MoonData> moons;
		public Vector2 textSize;

		public PlanetData(string name, string worldID, byte planetID, byte diff, byte head = 0, byte suit = 0, byte hat = 0) {
			this.name = name;
			this.worldID = worldID;
			this.sprite = PlanetInfo.Planets[(byte) planetID];
			this.diff = diff;
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
			this.posX = (short) CalcRandom.IntBetween(-65, 65);
			this.posY = lat;
			this.front = true;
		}
	}

	public class PlanetSelectScene : Scene {

		// References, Component
		public Atlas atlas;
		public readonly PlayerInput playerInput;
		public readonly MenuUI menuUI;
		public readonly PagingSystem paging;

		// Planets
		public Dictionary<short, PlanetData> planets = new Dictionary<short, PlanetData>();

		public PlanetSelectScene() : base() {

			// Prepare Components
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];
			this.menuUI = new MenuUI(this, MenuUI.MenuUIOption.PlanetSelect);

			// Retrieve Planet Path + JSON Content
			string listsDir = Path.Combine(Systems.filesLocal.localDir, "Lists");
			string planetPath = Path.Combine(listsDir, "Planets.json");
			string json = File.ReadAllText(planetPath);
			WorldListFormat planetData = JsonConvert.DeserializeObject<WorldListFormat>(json);

			// Load Planet Data
			short listID = 0;

			foreach(PlanetFormat planet in planetData.planets) {

				// Add Planet
				this.planets.Add(listID, new PlanetData(planet.name, planet.worldID, planet.planetID, planet.difficulty, planet.head, planet.suit, planet.hat));

				// Attach Moons to Planet
				foreach(byte moonID in planet.moons) {
					this.planets[listID].AddMoon(moonID);
				}

				listID++;
			}

			// Prepare Paging System
			this.paging = new PagingSystem(7, 3, (short) this.planets.Count);
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

			// Paging Input
			if(this.paging.PagingInput(playerInput)) {
				Systems.sounds.click2.Play(0.5f, 0, 0.5f);
			}

			// Open Menu
			if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
				this.uiState = UIState.MainMenu;
			}

			// Activate Node
			else if(playerInput.isPressed(IKey.AButton) == true) {
				short curVal = this.paging.CurrentSelectionVal;
				string worldID = this.planets[curVal].worldID;
				SceneTransition.ToWorld(worldID);
				return;
			}

			// Update Moon Positions
			for(short i = this.paging.MinVal; i < this.paging.MaxVal; i++) {
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
			short highlightX = (short)(this.paging.selectX * 200 + 100);
			short highlightY = (short)(this.paging.selectY * 200 + 100);

			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 60, highlightY - 60, 155, 170), Color.DarkRed);
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 50, highlightY - 50, 135, 150), Color.DarkSlateGray);

			// Draw Planets
			for(short i = this.paging.MinVal; i < this.paging.MaxVal; i++) {
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
