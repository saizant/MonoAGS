﻿using AGS.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace AGS.Engine
{
	public class RotatingCursorScheme
	{
		private List<Cursor> _cursors = new List<Cursor>(10);
		private IGame _game;
		private bool _handlingClick;

		public const string WALK_MODE = "Walk";
		public const string LOOK_MODE = "Look";
		public const string INTERACT_MODE = "Interact";
		public const string WAIT_MODE = "Wait";
		public const string INVENTORY_MODE = "Inventory";

		private int _currentMode;
		private Cursor _inventoryCursor;

		public RotatingCursorScheme(IGame game, IObject lookCursor, IObject walkCursor,
            IObject interactCursor, IObject waitCursor)
		{
			RotatingEnabled = true;
			_game = game;
			_game.Events.OnSavedGameLoad.Subscribe(onSavedGameLoad);

			if (walkCursor != null) AddCursor(WALK_MODE, walkCursor, true);
			if (lookCursor != null) AddCursor(LOOK_MODE, lookCursor, true);
			if (interactCursor != null) AddCursor(INTERACT_MODE, interactCursor, true);
			if (waitCursor != null) AddCursor(WAIT_MODE, waitCursor, false);
			AddCursor(INVENTORY_MODE, null, true);
			_inventoryCursor = _cursors[_cursors.Count - 1];
		}

		public bool RotatingEnabled { get; set; }

		public string CurrentMode 
		{
            get => _cursors[_currentMode].Mode;
            set 
			{
				int index = _cursors.FindIndex(c => c.Mode == value);
				if (index < 0)
				{
					Debug.WriteLine("Did not find cursor mode {0}", value);
					return;
				}
				_currentMode = index;
				setCursor();
			}
		}
			
		public void AddCursor(string mode, IObject animation, bool rotating)
		{
			_cursors.Add(new Cursor (mode, animation, rotating));
		}

		public void Start()
		{
			setCursor();
			_game.Input.MouseDown.SubscribeToAsync(onMouseDown);
		}

		public void SetInventoryCursor()
		{
			_inventoryCursor.Animation = _game.State.Player.Inventory.ActiveItem.CursorGraphics;
			CurrentMode = INVENTORY_MODE;
		}

		private void setCursor()
		{
			_game.Input.Cursor = _cursors[_currentMode].Animation;
		}

		private void onSavedGameLoad()
		{
			_currentMode = 0;
			setCursor();
			RotatingEnabled = true;
		}

		private async Task onMouseDown(MouseButtonEventArgs e)
		{
			var state = _game.State;
            var hitTest = _game.HitTest;
			if (_handlingClick || !state.Player.Enabled)
				return;

			if (e.Button == MouseButton.Left)
			{
				await onLeftMouseDown(e, state, hitTest);
			}
			else if (e.Button == MouseButton.Right)
			{
				onRightMouseDown(e, state);
			}
		}

        private async Task onLeftMouseDown(MouseButtonEventArgs e, IGameState state, IHitTest hitTest)
		{
			string mode = CurrentMode;
            IObject hotspot = hitTest.ObjectAtMousePosition;

			if (_game.Input.Cursor != _inventoryCursor.Animation)
			{
				if (hotspot != null && hotspot.Room == null) 
				{
					IInventoryItem inventoryItem = state.Player.Inventory.Items.FirstOrDefault(
						i => i.Graphics == hotspot);
					if (inventoryItem != null)
					{
						if (mode != LOOK_MODE)
						{
							if (inventoryItem.ShouldInteract) mode = INTERACT_MODE;
							else
							{
								state.Player.Inventory.ActiveItem = inventoryItem;
								SetInventoryCursor();
								return;
							}
						}
					}
					else return; //Blocking clicks when hovering UI objects
				}

				if (mode == WALK_MODE)
				{
                    Vector2 xy = e.MousePosition.GetProjectedPoint(state.Viewport, state.Player);
                    AGSLocation location = new AGSLocation (xy.X, xy.Y, state.Player.Z);
					await state.Player.WalkAsync(location).ConfigureAwait(true);
				}
				else if (mode != WAIT_MODE)
				{
					_handlingClick = true;
					try
					{
						if (hotspot == null) return;

						if (mode == LOOK_MODE)
						{
                            await hotspot.Interactions.OnInteract(AGSInteractions.LOOK).InvokeAsync(new ObjectEventArgs (hotspot));
						}
						else if (mode == INTERACT_MODE)
						{
                            await hotspot.Interactions.OnInteract(AGSInteractions.INTERACT).InvokeAsync(new ObjectEventArgs (hotspot));
						}
						else
						{
                            await hotspot.Interactions.OnInteract(mode).InvokeAsync(new ObjectEventArgs (hotspot));
						}
					}
					finally
					{
						_handlingClick = false;
					}
				}
			}
			else if (hotspot != null)
			{
				if (hotspot.Room == null)
				{
					IInventoryItem inventoryItem = state.Player.Inventory.Items.FirstOrDefault(
						                              i => i.Graphics == hotspot);
					if (inventoryItem != null)
					{
						await state.Player.Inventory.OnCombination(state.Player.Inventory.ActiveItem,
							inventoryItem).InvokeAsync(new InventoryCombinationEventArgs (
							state.Player.Inventory.ActiveItem, inventoryItem));
					}
					return;
				}

                await hotspot.Interactions.OnInventoryInteract(AGSInteractions.INTERACT).InvokeAsync(new InventoryInteractEventArgs(hotspot,
					state.Player.Inventory.ActiveItem));
			}
		}

		private void onRightMouseDown(MouseButtonEventArgs e, IGameState state)
		{
			if (!RotatingEnabled) return;

			IInventory inventory = state.Player.Inventory;

			int startMode = _currentMode;
			Cursor cursor = _cursors[_currentMode];

			if (!cursor.Rotating) return;

			do
			{
				_currentMode = (_currentMode + 1) % _cursors.Count;
				cursor = _cursors[_currentMode];
			} while ((!cursor.Rotating || cursor.Animation == null) && _currentMode != startMode);

			setCursor();
		}

		private class Cursor
		{
			public Cursor(string mode, IObject animation, bool rotating)
			{
				Mode = mode;
				Animation = animation;
				Rotating = rotating;
			}

			public string Mode { get; private set; }
			public IObject Animation { get; set; }
			public bool Rotating { get; private set;}
		}
	}
}

