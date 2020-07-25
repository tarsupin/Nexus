using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Nexus.Engine;
using Nexus.Gameplay;
using Nexus.ObjectComponents;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nexus.GameEngine {

	public class PlanetData {

		public string title;
		public string sprite;
		public string worldID;
		public byte diff;
		public byte[] icon; // HeadSubType, SuitSubType, HatSubType
		public List<MoonData> moons;
		public Vector2 textSize;

		public PlanetData(string title, string worldID, byte planetID, byte diff, byte[] icon) {
			this.title = title;
			this.worldID = worldID;
			this.sprite = PlanetInfo.Planets[(byte)planetID];
			this.diff = diff;
			this.icon = icon;
			this.moons = new List<MoonData>();
			this.textSize = Systems.fonts.baseText.font.MeasureString(title);
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
			this.sprite = PlanetInfo.Planets[(byte)moonID];
			this.lat = (short)CalcRandom.IntBetween(-10, 19);
			this.speed = (float)CalcRandom.FloatBetween(0.15f, 0.8f);
			this.posX = (short)CalcRandom.IntBetween(-65, 65);
			this.posY = lat;
			this.front = true;
		}
	}

	public class StarData {
		public readonly short posX;
		public readonly short posY;
		public readonly bool bright;
		public StarData(short posX, short posY, bool isBright) {
			this.posX = posX;
			this.posY = posY;
			this.bright = isBright;
		}
	}

	public class PlanetSelectScene : Scene {

		// References, Component
		public Atlas atlas;
		private readonly PlayerInput playerInput;
		public PagingSystem paging;

		// Screen UI
		private Texture2D logo;

		private const string versionBlurb = "Early Alpha Release";
		private readonly short versionBlurbHalf;

		private const string openMenuBlurb = "Press Start (or Enter) to open the Main Menu.";
		private readonly short openMenuHalf;

		// Mouse Highlight
		private int mouseHighX = 0;
		private int mouseHighY = 0;

		// Planets + Stars
		public Dictionary<short, PlanetData> planets = new Dictionary<short, PlanetData>();
		public List<StarData> stars = new List<StarData>();

		public PlanetSelectScene() : base() {

			// UI State
			UIHandler.SetUIOptions(true, true);
			UIHandler.SetMenu(null, true);

			// Prepare Components
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];

			// Screen UI
			this.logo = Systems.game.Content.Load<Texture2D>("Images/creo-logo");
			this.versionBlurbHalf = (short)(Systems.fonts.console.font.MeasureString(PlanetSelectScene.versionBlurb).X * 0.5f);
			this.openMenuHalf = (short)(Systems.fonts.baseText.font.MeasureString(PlanetSelectScene.openMenuBlurb).X * 0.5f);

			// Prepare Space
			this.LoadPlanets("Planets", this.planets);
			this.GenerateStars();

			// Prepare Planet Paging System
			this.paging = new PagingSystem(5, 2, (short)this.planets.Count);
		}

		public void LoadPlanets(string filename, Dictionary<short, PlanetData> planetDict, short listID = 0) {

			// Retrieve Planet Path + JSON Content
			string planetPath = Path.Combine(Systems.filesLocal.localDir, filename + ".json");
			string json = File.ReadAllText(planetPath);
			WorldListFormat planetData = JsonConvert.DeserializeObject<WorldListFormat>(json);

			// Load Planet Data
			foreach(PlanetFormat planet in planetData.planets) {

				// Add Planet
				planetDict.Add(listID, new PlanetData(planet.title, planet.worldID, planet.planetID, planet.difficulty, planet.icon));

				// Attach Moons to Planet
				foreach(byte moonID in planet.moons) {
					planetDict[listID].AddMoon(moonID);
				}

				listID++;
			}
		}

		public void GenerateStars() {

			// Generate Random Stars
			for(byte starIndex = 0; starIndex < 200; starIndex++) {
				short posX = CalcRandom.ShortBetween(0, Systems.screen.screenWidth);
				short posY = CalcRandom.ShortBetween(0, Systems.screen.screenHeight);
				StarData star = new StarData(posX, posY, false);
				this.stars.Add(star);
			}

			// Generate Clusters
			for(byte clusterIndex = 0; clusterIndex < 3; clusterIndex++) {
				short clusX = CalcRandom.ShortBetween(50, (short)(Systems.screen.screenWidth - 50));
				short clusY = CalcRandom.ShortBetween(50, (short)(Systems.screen.screenHeight - 50));

				// Generate Random Stars
				for(byte starIndex = 0; starIndex < 35; starIndex++) {
					short posX = CalcRandom.ShortBetween((short)(clusX - 140), (short)(clusX + 140));
					short posY = CalcRandom.ShortBetween((short)(clusY - 140), (short)(clusY + 140));

					if(posX > 0 && posY > 0 && posX < Systems.screen.screenWidth && posY < Systems.screen.screenHeight) {
						StarData star = new StarData(posX, posY, false);
						this.stars.Add(star);
					}
				}
			}
		}

		public override void StartScene() {

			// Reset Timer
			Systems.timer.ResetTimer();

			// Play or Stop Music
			Systems.music.Play((byte)MusicAssets.MusicTrack.Journey);
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

			// Update UI
			UIComponent.ComponentWithFocus = null;
			Cursor.UpdateMouseState();
			UIHandler.cornerMenu.RunTick();

			// Playing State
			if(UIHandler.uiState == UIState.Playing) {

				// Paging Input (only when in the paging area)
				if(this.paging.PagingInput(playerInput) != PagingSystem.PagingPress.None) {
					Systems.sounds.click2.Play(0.5f, 0, 0.5f);
				}

				// Check if the mouse is hovering over a planet (and draw accordingly if so)
				this.CheckPlanetHover();

				// Activate Planet / World
				if(playerInput.isPressed(IKey.AButton) == true) {
					this.LoadWorldById(this.planets[this.paging.CurrentSelectionVal].worldID);
					return;
				}

				// Open Menu
				if(Systems.input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
					UIHandler.SetMenu(UIHandler.mainMenu, true);
				}
			}
			
			// Menu State
			else {
				UIHandler.menu.RunTick();
			}

			// Update Moon Positions
			this.UpdateMoonPositions(this.paging, this.planets);
		}

		public void CheckPlanetHover() {

			// Reset Mouse Highlight Positions
			this.mouseHighX = 0;
			this.mouseHighY = 0;

			short posX = 280 - 200;
			short posY = 350;

			int mouseX = Cursor.MouseX;
			int mouseY = Cursor.MouseY;

			// Draw Planets
			for(short i = this.paging.MinVal; i < this.paging.MaxVal; i++) {

				// Update Next Position
				posX += 200;
				if(posX >= 1180) { posY += 275; posX = 280; }

				if(mouseX < posX - 60 || mouseX > posX + 95 || mouseY < posY - 60 || mouseY > posY + 135) {
					continue;
				}

				this.mouseHighX = posX;
				this.mouseHighY = posY;

				// Activate Planet / World
				if(Cursor.LeftMouseState == Cursor.MouseDownState.Clicked) {
					this.LoadWorldById(this.planets[i].worldID);
					return;
				}
			}
		}

		private void LoadWorldById(string worldID) {
			
			// Load the Special "Users World" menu.
			if(worldID == "_users") {
				UIHandler.SetMenu(UIHandler.loadWorldMenu, false);
				return;
			}

			SceneTransition.ToWorld(worldID);
			return;
		}

		public void UpdateMoonPositions(PagingSystem paging, Dictionary<short, PlanetData> planets) {
			for(short i = paging.MinVal; i < paging.MaxVal; i++) {
				PlanetData planet = planets[i];

				foreach(MoonData moon in planet.moons) {
					moon.posX += moon.speed;

					if(moon.speed > 0 && moon.posX > 70) {
						moon.posX = 70;
						moon.speed = (float)-Math.Abs(moon.speed);
						moon.front = false;
					} else if(moon.posX < -70) {
						moon.posX = -70;
						moon.speed = (float)Math.Abs(moon.speed);
						moon.front = true;
					}

					if(moon.speed > 0) {
						moon.posY = moon.lat - (float)Math.Sqrt(70 - Math.Abs(moon.posX)) * -3.2f;
					} else if(moon.speed < 0) {
						moon.posY = moon.lat + (float)Math.Sqrt(70 - Math.Abs(moon.posX)) * -3.2f;
					}
				}
			}
		}
		
		public void DrawStar(StarData star) {
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(star.posX, star.posY, 3, 3), UIHandler.starColor);
		}

		public override void Draw() {

			// SamplerState.PointClamp will force Sprites to draw without blurring effect. Without this, all scaling upward is blurry.
			Systems.spriteBatch.End();
			Systems.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, Systems.screen.windowWidth, Systems.screen.windowHeight), UIHandler.spaceBG);

			// Draw Stars
			for(short starIndex = 0; starIndex < this.stars.Count; starIndex++) {
				this.DrawStar(this.stars[starIndex]);
			}

			short posX = 280;
			short posY = 350;

			// Draw Scene UI
			Systems.spriteBatch.Draw(this.logo, new Vector2(Systems.screen.windowHalfWidth - 298, 50), Color.White);
			Systems.fonts.console.Draw(PlanetSelectScene.versionBlurb, Systems.screen.windowHalfWidth - this.versionBlurbHalf, 150, Color.White);
			Systems.fonts.baseText.Draw(PlanetSelectScene.openMenuBlurb, Systems.screen.windowHalfWidth - this.openMenuHalf, 210, Color.White);

			// Draw Paging Selection
			short highlightX = (short)(this.paging.selectX * 200 + posX);
			short highlightY = (short)(this.paging.selectY * 275 + posY);

			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 60, highlightY - 60, 155, 195), UIHandler.selector * (this.mouseHighY > 0 ? 0.45f : 1));
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 50, highlightY - 50, 135, 175), UIHandler.spaceBG);

			if(this.mouseHighY > 0) {
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.mouseHighX - 60, this.mouseHighY - 60, 155, 195), UIHandler.mouseSelect);
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(this.mouseHighX - 50, this.mouseHighY - 50, 135, 175), UIHandler.spaceBG);
			}

			// Draw Planets
			for(short i = this.paging.MinVal; i < this.paging.MaxVal; i++) {
				PlanetData planet = this.planets[i];
				this.DrawPlanet(planet, posX, posY);

				// Update Next Position
				posX += 200;
				if(posX >= 1180) { posY += 275; posX = 280; }
			}

			// Draw Indicators
			if(this.paging.MinVal > 0) {
				UIHandler.atlas.Draw("Arrow/Left", 70, 500);
			}

			if(this.paging.MaxVal < this.paging.NumberOfItems) {
				UIHandler.atlas.Draw("Arrow/Right", 1280, 500);
			}

			// Draw UI
			UIHandler.cornerMenu.Draw();
			UIHandler.menu.Draw();
		}

		public void DrawPlanet(PlanetData planetData, short posX, short posY) {

			// Draw Moons behind Planet:
			foreach(MoonData moon in planetData.moons) {
				if(!moon.front) { this.atlas.Draw(moon.sprite, posX + (short)moon.posX, posY + (short)moon.posY); }
			}

			// Draw Planet
			this.atlas.DrawAdvanced(planetData.sprite, posX + 2, posY, Color.White, 0f, 4);

			// Display Name
			Systems.fonts.baseText.Draw(planetData.title, posX + 16 - (byte)Math.Floor(planetData.textSize.X * 0.5f), posY + 78, Color.White);

			// Display Difficulty
			short diffSize = (short)Systems.fonts.console.font.MeasureString(GameplayTypes.DiffName[(byte)planetData.diff]).X;
			Systems.fonts.console.Draw(GameplayTypes.DiffName[(byte)planetData.diff], posX + 16 - (byte)Math.Floor(diffSize * 0.5f), posY + 103, Color.White);

			// Display Character
			if(planetData.icon != null && planetData.icon[0] > 0 && planetData.icon[1] > 0) {
				Head.GetHeadBySubType(planetData.icon[0]).Draw(false, posX - 30, posY - 45, 0, 0);
				Suit.GetSuitBySubType(planetData.icon[1]).Draw("StandLeft", posX - 30, posY - 45, 0, 0);

				if(planetData.icon[2] > 0) {
					Hat.GetHatBySubType(planetData.icon[2]).Draw(false, posX - 30, posY - 45, 0, 0);
				} else if(planetData.icon[0] == (byte)HeadSubType.PooHead) {
					Hat.GetHatBySubType((byte)HatSubType.PooHat).Draw(false, posX - 30, posY - 45, 0, 0);
				}
			}

			// Draw Moons in front of planet:
			foreach(MoonData moon in planetData.moons) {
				if(moon.front) { this.atlas.Draw(moon.sprite, posX + (short)moon.posX, posY + (short)moon.posY); }
			}
		}
	}
}
