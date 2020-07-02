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
using static Nexus.Engine.UIHandler;

namespace Nexus.GameEngine {

	public class PlanetData {

		public string name;
		public string sprite;
		public string worldID;
		public byte diff;
		public byte[] icon; // HeadSubType, SuitSubType, HatSubType
		public List<MoonData> moons;
		public Vector2 textSize;

		public PlanetData(string name, string worldID, byte planetID, byte diff, byte[] icon) {
			this.name = name;
			this.worldID = worldID;
			this.sprite = PlanetInfo.Planets[(byte) planetID];
			this.diff = diff;
			this.icon = icon;
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
		public PagingSystem paging;
		public PagingSystem pagingFeatured;

		// Planets
		public Dictionary<short, PlanetData> featured = new Dictionary<short, PlanetData>();
		public Dictionary<short, PlanetData> planets = new Dictionary<short, PlanetData>();

		public PlanetSelectScene() : base() {

			// UI State
			UIHandler.SetUIOptions(true, true);
			UIHandler.SetMenu(null, true);

			// Prepare Components
			this.playerInput = Systems.localServer.MyPlayer.input;
			this.atlas = Systems.mapper.atlas[(byte)AtlasGroup.World];

			// Prepare Default Featured Planets (Choose World, My World)
			this.featured.Add(0, new PlanetData("My World", "__World", 0, 0, new byte[] { 0, 0, 0 }));
			this.featured.Add(1, new PlanetData("Load World", "__Load", 0, 0, new byte[] { 0, 0, 0 }));

			// Load Planets and Featured Planets
			this.LoadPlanets("FeaturedPlanets", this.featured, 2);
			this.LoadPlanets("Planets", this.planets);

			// Prepare Featured Options Paging System (top section, one line, where it remembers your favorite / featured choices)
			this.pagingFeatured = new PagingSystem(7, 1, (short)this.featured.Count);
			this.pagingFeatured.SetExitRule(DirCardinal.Down, PagingSystem.PagingExitRule.LeaveArea);
			this.pagingFeatured.SetExitRule(DirCardinal.Up, PagingSystem.PagingExitRule.NoWrap);

			// Prepare Planet Paging System (full paging system)
			this.paging = new PagingSystem(7, 2, (short)this.planets.Count);
			this.paging.SetExitRule(DirCardinal.Up, PagingSystem.PagingExitRule.LeaveArea);
			this.paging.SetExitRule(DirCardinal.Down, PagingSystem.PagingExitRule.NoWrap);
			this.paging.exitDir = DirCardinal.Up;
		}

		public void LoadPlanets(string filename, Dictionary<short, PlanetData> planetDict, short listID = 0) {

			// Retrieve Planet Path + JSON Content
			string listsDir = Path.Combine(Systems.filesLocal.localDir, "Lists");
			string planetPath = Path.Combine(listsDir, filename + ".json");
			string json = File.ReadAllText(planetPath);
			WorldListFormat planetData = JsonConvert.DeserializeObject<WorldListFormat>(json);

			// Load Planet Data
			foreach(PlanetFormat planet in planetData.planets) {

				// Add Planet
				planetDict.Add(listID, new PlanetData(planet.name, planet.worldID, planet.planetID, planet.difficulty, planet.icon));

				// Attach Moons to Planet
				foreach(byte moonID in planet.moons) {
					planetDict[listID].AddMoon(moonID);
				}

				listID++;
			}
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

			// Update UI
			UIComponent.ComponentWithFocus = null;
			Cursor.UpdateMouseState();
			UIHandler.cornerMenu.RunTick();

			// Playing State
			if(UIHandler.uiState == UIState.Playing) {

				// Paging Input (only when in the paging area)
				if(this.paging.exitDir == DirCardinal.None) {
					if(this.paging.PagingInput(playerInput) != PagingSystem.PagingPress.None) {
						Systems.sounds.click2.Play(0.5f, 0, 0.5f);

						// If Planet Paging is exited, go to the Featured Planets.
						if(this.paging.exitDir == DirCardinal.Up) {
							this.pagingFeatured.ReturnToPagingArea();

							if(this.pagingFeatured.NumberOfItems > this.paging.selectX) {
								this.pagingFeatured.selectX = this.paging.selectX;
							}
						}
					}
				}

				// Featured Paging Input (when in the featured paging area)
				else {
					if(this.pagingFeatured.PagingInput(playerInput) != PagingSystem.PagingPress.None) {
						Systems.sounds.click2.Play(0.5f, 0, 0.5f);

						// If Featured Planet Paging is exited, go to Planet Paging.
						if(this.pagingFeatured.exitDir == DirCardinal.Down) {
							this.paging.ReturnToPagingArea();

							if(this.paging.NumberOfItems > this.pagingFeatured.selectX) {
								this.paging.selectX = this.pagingFeatured.selectX;
							}
						}
					}
				}

				// Activate Planet / World
				if(playerInput.isPressed(IKey.AButton) == true) {
					string worldID;

					// If the featured list is active, load its worldID:
					if(this.pagingFeatured.exitDir == DirCardinal.None) {
						short curVal = this.pagingFeatured.CurrentSelectionVal;
						worldID = this.featured[curVal].worldID;
					}

					// Otherwise, load the worldID from the planet list.
					else {
						short curVal = this.paging.CurrentSelectionVal;
						worldID = this.planets[curVal].worldID;
					}

					// If WorldID is "__World", go to your personal world.
					if(worldID == "__World") {
						SceneTransition.ToWorldEditor("__World");
						return;
					}

					// If WorldID is "__Load", allow loading of a user generated world.
					else if(worldID == "__Load") {
						// TODO: DO THING
						return;
					}

					SceneTransition.ToWorld(worldID);
					return;
				}

				InputClient input = Systems.input;

				if(input.LocalKeyPressed(Keys.Tab) || input.LocalKeyPressed(Keys.Escape) || playerInput.isPressed(IKey.Start) || playerInput.isPressed(IKey.Select)) {
					UIHandler.SetMenu(UIHandler.mainMenu, true);
				}
			}
			
			// Menu State
			else {
				UIHandler.menu.RunTick();
			}

			// Update Moon Positions
			this.UpdateMoonPositions(this.pagingFeatured, this.featured);
			this.UpdateMoonPositions(this.paging, this.planets);
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

		public override void Draw() {

			// SamplerState.PointClamp will force Sprites to draw without blurring effect. Without this, all scaling upward is blurry.
			Systems.spriteBatch.End();
			Systems.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

			// Draw Background
			Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(0, 0, Systems.screen.windowWidth, Systems.screen.windowHeight), Color.DarkSlateGray);

			// Draw Featured Paging Selection (if applicable)
			if(this.pagingFeatured.exitDir == DirCardinal.None) {

				short highlightX = (short)(this.pagingFeatured.selectX * 200 + 100);
				short highlightY = (short)(this.pagingFeatured.selectY * 200 + 200);

				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 60, highlightY - 60, 155, 195), Color.DarkRed);
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 50, highlightY - 50, 135, 175), Color.DarkSlateGray);
			}

			// Draw Featured Planets
			for(short i = this.pagingFeatured.MinVal; i < this.pagingFeatured.MaxVal; i++) {
				PlanetData planet = this.featured[i];
				this.DrawPlanet(planet, (short)(100 + i * 200), 200);
			}

			short posX = 100;
			short posY = 450;

			// Draw Current Paging Selection
			if(this.paging.exitDir == DirCardinal.None) {

				short highlightX = (short)(this.paging.selectX * 200 + posX);
				short highlightY = (short)(this.paging.selectY * 250 + posY);

				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 60, highlightY - 60, 155, 195), Color.DarkRed);
				Systems.spriteBatch.Draw(Systems.tex2dWhite, new Rectangle(highlightX - 50, highlightY - 50, 135, 175), Color.DarkSlateGray);
			}

			// Draw Planets
			for(short i = this.paging.MinVal; i < this.paging.MaxVal; i++) {
				PlanetData planet = this.planets[i];
				this.DrawPlanet(planet, posX, posY);

				// Update Next Position
				posX += 200;
				if(posX >= 1500) { posY += 250; posX = 100; }
			}

			// Draw UI
			UIHandler.cornerMenu.Draw();
			UIHandler.menu.Draw();
		}

		public void DrawPlanet( PlanetData planetData, short posX, short posY ) {

			// Draw Moons behind Planet:
			foreach(MoonData moon in planetData.moons) {
				if(!moon.front) { this.atlas.Draw(moon.sprite, posX + (short) moon.posX, posY + (short) moon.posY); }
			}

			// Draw Planet
			this.atlas.DrawAdvanced(planetData.sprite, posX + 2, posY, Color.White, 0f, 4);

			// Display Name
			Systems.fonts.baseText.Draw(planetData.name, posX + 16 - (byte)Math.Floor(planetData.textSize.X * 0.5f), posY + 78, Color.White);

			// Display Difficulty
			short diffSize = (short) Systems.fonts.console.font.MeasureString(GameplayTypes.DiffName[(byte)planetData.diff]).X;
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
				if(moon.front) { this.atlas.Draw(moon.sprite, posX + (short) moon.posX, posY + (short) moon.posY); }
			}
		}
	}
}
