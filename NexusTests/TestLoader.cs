using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus;
using Nexus.Engine;
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Diagnostics;

namespace NexusTests {

	[TestClass]
	public class TestLoader {
		public static GameClient game;

		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context) {
			
			Action gameLoadInstructions = () => {
				SceneTransition.ToLevel("", "TEST_DEBUG_01");
				Systems.camera.CenterAtPosition(1200, 0);
			};

			TestLoader.game = new GameClient(gameLoadInstructions);
			TestLoader.game.RunOneFrame();
		}

		[TestMethod]
		public void CheckSystemComponentsLoaded() {

			// Mapper Components
			Debug.Assert(Systems.mapper != null);
			Debug.Assert(Systems.mapper.atlas.Length > 0);
			Debug.Assert(Systems.mapper.MetaList.Keys.Count > 0);
			Debug.Assert(Systems.mapper.ObjectMetaData.Keys.Count > 0);
			Debug.Assert(Systems.mapper.ObjectTypeDict.Keys.Count > 0);
			Debug.Assert(Systems.mapper.TileDict.Keys.Count > 0);

			// State Components
			Debug.Assert(Systems.handler.campaignState is CampaignState);
			Debug.Assert(Systems.handler.levelContent is LevelContent);
			Debug.Assert(Systems.handler.levelState is LevelState);
		}
	}
}
