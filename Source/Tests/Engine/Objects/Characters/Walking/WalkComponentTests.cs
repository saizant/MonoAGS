﻿using System.Threading.Tasks;
using NUnit.Framework;
using AGS.Engine;
using AGS.API;
using Moq;
using System.Collections.Generic;
using System;

namespace Tests
{
    [TestFixture]
    public class WalkComponentTests
	{
		private AGSEvent _onRepeatedlyExecute;
		private bool _testCompleted;

		[TestFixtureSetUp]
		public void Init()
		{
			_onRepeatedlyExecute = new AGSEvent();
			startTicks();
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			_testCompleted = true;
		}

        [TestCase(0f, 0f, true, 5f, 5f, true, 3f, 3f, true)]
		[TestCase(5f, 5f, true, 0f, 0f, true, 3f, 3f, true)]
		[TestCase(0f, 0f, false, 5f, 5f, true, 3f, 3f, true)]
		[TestCase(5f, 5f, false, 0f, 0f, true, 3f, 3f, true)]
		[TestCase(0f, 0f, true, 5f, 5f, false, 3f, 3f, true)]
		[TestCase(5f, 5f, true, 0f, 0f, false, 3f, 3f, true)]
		[TestCase(0f, 0f, true, 5f, 5f, false, 3f, 3f, false)]
		[TestCase(5f, 5f, true, 0f, 0f, false, 3f, 3f, false)]
		public async Task WalkTest(float fromX, float fromY, bool fromWalkable, 
			                 float toX, float toY, bool toWalkable, 
			                 float closeToX, float closeToY, bool hasCloseToWalkable)
		{
            //Setup:
            Mock<IEntity> entity = new Mock<IEntity>();
            Mock<ITranslateComponent> translate = new Mock<ITranslateComponent>();
            Mock<IHasRoomComponent> hasRoom = new Mock<IHasRoomComponent>();
			Mock<IOutfitComponent> outfitHolder = new Mock<IOutfitComponent> ();
			Mock<IOutfit> outfit = new Mock<IOutfit> ();
            Mock<IDrawableInfoComponent> drawable = new Mock<IDrawableInfoComponent>();
			Mock<IPathFinder> pathFinder = new Mock<IPathFinder> ();
			Mock<IFaceDirectionComponent> faceDirection = new Mock<IFaceDirectionComponent> ();
			Mock<IObjectFactory> objFactory = new Mock<IObjectFactory> ();
			Mock<IRoom> room = new Mock<IRoom> ();
            Mock<IViewport> viewport = new Mock<IViewport>();
			Mock<IArea> area = new Mock<IArea> ();
            Mock<IWalkableArea> walkableArea = new Mock<IWalkableArea>();
			Mock<IMask> mask = new Mock<IMask> ();
			Mock<ICutscene> cutscene = new Mock<ICutscene> ();
			Mock<IGameState> gameState = new Mock<IGameState> ();
            Mock<IGame> game = new Mock<IGame>();
            Mock<IGameEvents> gameEvents = new Mock<IGameEvents>();
            Mock<IGLUtils> glUtils = new Mock<IGLUtils>();

			gameEvents.Setup(g => g.OnRepeatedlyExecute).Returns(_onRepeatedlyExecute);
            game.Setup(g => g.State).Returns(gameState.Object);
            game.Setup(g => g.Events).Returns(gameEvents.Object);
			gameState.Setup(s => s.Cutscene).Returns(cutscene.Object);
            gameState.Setup(r => r.Viewport).Returns(viewport.Object);
            room.Setup(r => r.Areas).Returns(new AGSBindingList<IArea>(1) { area.Object });
            walkableArea.Setup(w => w.IsWalkable).Returns(true);
			area.Setup(a => a.Enabled).Returns(true);
			area.Setup(a => a.IsInArea(It.Is<AGS.API.PointF>(p => p.X == fromX && p.Y == fromY))).Returns(fromWalkable);
			area.Setup(a => a.IsInArea(It.Is<AGS.API.PointF>(p => p.X == toX && p.Y == toY))).Returns(toWalkable);
            area.Setup(a => a.IsInArea(It.Is<AGS.API.PointF>(p => p.X == toX - 1 && p.Y == toY - 1))).Returns(toWalkable);
            area.Setup(a => a.IsInArea(It.Is<AGS.API.PointF>(p => p.X == toX + 1 && p.Y == toY + 1))).Returns(toWalkable);
			area.Setup(a => a.IsInArea(It.Is<AGS.API.PointF>(p => p.X == closeToX && p.Y == closeToY))).Returns(hasCloseToWalkable);
			area.Setup(a => a.Mask).Returns(mask.Object);
            area.Setup(a => a.GetComponent<IWalkableArea>()).Returns(walkableArea.Object);
			float distance = 1f;
			area.Setup(a => a.FindClosestPoint(It.Is<AGS.API.PointF>(p => p.X == toX && p.Y == toY), out distance)).Returns(new AGS.API.PointF (closeToX, closeToY));
			mask.Setup(m => m.Width).Returns(10);

			outfitHolder.Setup(o => o.Outfit).Returns(outfit.Object);

			float x = fromX;
			float y = fromY;
            hasRoom.Setup(o => o.Room).Returns(room.Object);
			translate.Setup(o => o.X).Returns(() => x);
			translate.Setup(o => o.Y).Returns(() => y);
			translate.Setup(o => o.Location).Returns(() => new AGSLocation (x, y));
			translate.SetupSet(o => o.X = It.IsAny<float>()).Callback<float>(f => x = f);
			translate.SetupSet(o => o.Y = It.IsAny<float>()).Callback<float>(f => y = f);

            Mocks.Bind(entity, hasRoom);
            Mocks.Bind(entity, translate);
            Mocks.Bind(entity, faceDirection);
            Mocks.Bind(entity, outfitHolder);
            Mocks.Bind(entity, drawable);

			ILocation toLocation = new AGSLocation (toX, toY);
			ILocation closeLocation = new AGSLocation (closeToX, closeToY);

			pathFinder.Setup(p => p.GetWalkPoints(It.Is<ILocation>(l => l.X == fromX && l.Y == fromY),
				It.Is<ILocation>(l => l.X == toX && l.Y == toY))).Returns(toWalkable ? new List<ILocation> {toLocation} : new List<ILocation>());

			pathFinder.Setup(p => p.GetWalkPoints(It.Is<ILocation>(l => l.X == fromX && l.Y == fromY),
				It.Is<ILocation>(l => l.X == closeToX && l.Y == closeToY))).Returns(hasCloseToWalkable ? new List<ILocation> {closeLocation} : new List<ILocation>());
			
			AGSWalkComponent walk = new AGSWalkComponent (pathFinder.Object, objFactory.Object, game.Object, 
                                                        glUtils.Object) { WalkStep = new PointF(4f, 4f), MovementLinkedToAnimation = false };

			bool walkShouldSucceed = fromWalkable && (toWalkable || hasCloseToWalkable);

            walk.Init(entity.Object);
            walk.AfterInit();

			//Act:
			bool walkSucceded = await walk.WalkAsync(toLocation);

			//Test:
			Assert.AreEqual(walkShouldSucceed, walkSucceded);

			if (walkShouldSucceed)
			{				
				Assert.AreEqual(toWalkable ? toX : closeToX, x, 0.1f);
				Assert.AreEqual(toWalkable ? toY : closeToY, y, 0.1f);
			}
		}

		private async void startTicks()
		{
			await tick();
		}

		private async Task tick()
		{
			if (_testCompleted) return;
			await Task.Delay(10);
			await _onRepeatedlyExecute.InvokeAsync();
			await tick();
		}
	}
}

