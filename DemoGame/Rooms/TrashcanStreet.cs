﻿using System;
using API;
using Engine;

namespace DemoGame
{
	public class TrashcanStreet
	{
		private readonly IRoom _room;
		private readonly IPlayer _player;
		private const string _baseFolder = "../../Assets/Rooms/TrashcanStreet/";

		public TrashcanStreet(IPlayer player, IViewport viewport, IGameEvents gameEvents)
		{
			_player = player;
			AGSEdges edges = new AGSEdges (new AGSEdge { Value = 20f },
				new AGSEdge { Value = 310f }, new AGSEdge { Value = 190f },
				new AGSEdge { Value = 10f });
			edges.Left.OnEdgeCrossed.Subscribe(onLeftEdgeCrossed);
			edges.Right.OnEdgeCrossed.Subscribe(onRightEdgeCrossed);
			_room = new AGSRoom ("Trashcan Street", player, viewport, edges, gameEvents);
		}

		public IRoom Load(IGraphicsFactory factory)
		{
			AGSObject bg = new AGSObject (new AGSSprite ());
			bg.Image = factory.LoadImage(_baseFolder + "bg.png");
			_room.Background = bg;

			AGSMaskLoader maskLoader = new AGSMaskLoader (factory);
			_room.WalkableAreas.Add(new AGSArea { Mask = maskLoader.Load(_baseFolder + "walkable.png") });

			return _room;
		}

		private void onLeftEdgeCrossed(object sender, EventArgs args)
		{
			_player.Character.ChangeRoom(Rooms.DarsStreet, 490);
		}

		private void onRightEdgeCrossed(object sender, EventArgs args)
		{
			_player.Character.ChangeRoom(Rooms.EmptyStreet, 30);
		}
	}
}
