
using Nexus.GameEngine;
using Nexus.Gameplay;
using System;
using System.Collections.Generic;

namespace Nexus.Engine {

	// By default, the paging sytem will wrap between a given number of rows and columns.
	// You can assign Exit Rules to each Cardinal Direction [SetExitRule()], such as if you want the "up" direction to leave the paging area.
	// If an exit direction leaves the paging area, it will change "exitDir" to the direction that was left.
	public class PagingSystem {

		// Paging Rules
		public readonly short NumberOfItems;
		public readonly byte PerRow;
		public readonly byte PerPage;

		public enum PagingExitRule : byte {
			Wrap,
			NoWrap,
			LeaveArea,
		}

		public enum PagingPress : byte {
			None,
			SelectionMove,
			PageChange,
		}

		// Paging Selections
		public short page = 0;
		public byte numOnPage = 0;
		public byte selectX = 0;
		public byte selectY = 0;
		public DirCardinal exitDir;	// NONE if currently inside the paging area. A direction means it exited paging area in that direction.

		// Exit Controls - These define what happens when you exit the paging section.
		// The default is to wrap to the other side of the paging section.
		public Dictionary<DirCardinal, PagingExitRule> exits;

		public PagingSystem(byte numRows, byte numCols, short numItems) {
			
			this.exits = new Dictionary<DirCardinal, PagingExitRule>() {
				{ DirCardinal.Left, PagingExitRule.Wrap },
				{ DirCardinal.Right, PagingExitRule.Wrap },
				{ DirCardinal.Up, PagingExitRule.Wrap },
				{ DirCardinal.Down, PagingExitRule.Wrap },
			};

			this.exitDir = DirCardinal.None;

			this.PerRow = numRows;
			this.PerPage = (byte)(numCols * numRows);
			this.NumberOfItems = numItems;
			this.ToPage(0);
		}

		// Get the Current Page's Minimum and Maximum values that you can iterate through.
		public short MinVal { get { return (short)(this.page * this.PerPage); } }
		public short MaxVal { get { return Math.Min((short)(this.page * this.PerPage + this.PerPage), (short)this.NumberOfItems); } }

		// Return the ID of the Current Selection:
		public short CurrentSelectionVal {
			get { return (short)(this.page * this.PerPage + this.selectY * this.PerRow + this.selectX); }
		}

		// Assigns an exit rule to a particular direction.
		public void SetExitRule(DirCardinal dir, PagingExitRule exitRule) {
			this.exits[dir] = exitRule;
		}

		// This clears the exit direction. Use it when you've returned to the paging area.
		public void ReturnToPagingArea() { this.exitDir = DirCardinal.None; }

		// Optional Input Process
		public PagingPress PagingInput(PlayerInput playerInput) {
			
			// Selector Movement
			if(playerInput.isPressed(IKey.Up)) { this.MoveSelector(0, -1); return PagingPress.SelectionMove; }
			if(playerInput.isPressed(IKey.Down)) { this.MoveSelector(0, 1); return PagingPress.SelectionMove; }
			if(playerInput.isPressed(IKey.Left)) { this.MoveSelector(-1, 0); return PagingPress.SelectionMove; }
			if(playerInput.isPressed(IKey.Right)) { this.MoveSelector(1, 0); return PagingPress.SelectionMove; }

			// Page Changing
			if(playerInput.isPressed(IKey.L1) || playerInput.isPressed(IKey.L2)) { this.ToPage(Math.Max((short) 0, (short)(this.page - 1))); return PagingPress.PageChange; }
			if(playerInput.isPressed(IKey.R1) || playerInput.isPressed(IKey.R2)) { this.ToPage((short)(this.page + 1)); return PagingPress.PageChange; }

			return PagingPress.None;
		}

		public void ToPage(short pageNum) {

			// Cannot reduce below 0.
			if(pageNum < 0) { pageNum = 0; }

			// Cannot exceed max page:
			else if(pageNum * this.PerPage >= this.NumberOfItems) {
				pageNum = (short) Math.Floor((float)(this.NumberOfItems - 1) / (float)this.PerPage);
			}

			// Update Page
			this.page = pageNum;
			this.numOnPage = (byte)Math.Min(this.PerPage, (this.NumberOfItems - (pageNum * this.PerPage)));

			// Reposition to Safe Selector Location:
			if(this.numOnPage <= this.selectX) { this.selectX = (byte)(this.numOnPage - 1); }
			if(this.numOnPage <= this.selectY * this.PerRow + this.selectX) { this.selectY = 0; }
		}

		// One of these values gets set as 0, the other is set to -1 or 1 to represent the direction of movement.
		public void MoveSelector(sbyte slotX, sbyte slotY) {

			// Determine Sizes
			byte cols = (byte)Math.Ceiling((float)this.numOnPage / (float)this.PerRow);
			byte finalRow = (byte)(this.numOnPage - ((cols - 1) * this.PerRow));

			sbyte toRow = (sbyte)(this.selectX + slotX);
			sbyte toCol = (sbyte)(this.selectY + slotY);

			// Moving Down
			if(slotY == 1) {

				// Leaves Paging Section
				if(toCol >= cols) {
					PagingExitRule exitRule = this.exits[DirCardinal.Down];
					if(exitRule == PagingExitRule.Wrap) { this.selectY = 0; }
					else if(exitRule == PagingExitRule.NoWrap) { /* No Change. Resists the Movement. */ }
					else if(exitRule == PagingExitRule.LeaveArea) { this.exitDir = DirCardinal.Down; }
				}

				// Standard Move
				else if(toCol < cols - 1) { this.selectY++; }

				// Standard Move to the final row.
				else {
					if(toRow >= finalRow) {
						PagingExitRule exitRule = this.exits[DirCardinal.Down];
						if(exitRule == PagingExitRule.Wrap) { this.selectY = 0; }
						else if(exitRule == PagingExitRule.NoWrap) { this.selectX = (byte)(finalRow - 1); this.selectY = (byte)toCol; }
						else if(exitRule == PagingExitRule.LeaveArea) { this.exitDir = DirCardinal.Down; }
					} else {
						this.selectY = (byte)(cols - 1);
					}
				}

				return;
			}

			// Moving Up
			if(slotY == -1) {

				// Wrap around, if applicable.
				// This wrap is more complicated, since you have to handle the finalRow issue.
				if(toCol < 0) {
					PagingExitRule exitRule = this.exits[DirCardinal.Up];
					if(exitRule == PagingExitRule.Wrap) {
						if(toRow < finalRow) { this.selectY = (byte)(cols - 1); } else { this.selectY = (byte)(cols - 2); }
					}
					else if(exitRule == PagingExitRule.NoWrap) { /* No Change. Resists the Movement. */ }
					else if(exitRule == PagingExitRule.LeaveArea) { this.exitDir = DirCardinal.Up; }
				}

				// Standard Move
				else {
					this.selectY--;
				}

				return;
			}

			// If we're not on the final row, or the final row is full:
			if(toCol != (byte)(cols - 1) || finalRow == this.PerRow) {
				if(toRow < 0) {
					PagingExitRule exitRule = this.exits[DirCardinal.Left];
					if(exitRule == PagingExitRule.Wrap) { this.selectX = (byte)(this.PerRow - 1); }
					else if(exitRule == PagingExitRule.NoWrap) { /* Do nothing. No wrap. */ }
					else if(exitRule == PagingExitRule.LeaveArea) { this.exitDir = DirCardinal.Left; }
				}
				else if(toRow >= this.PerRow) {
					PagingExitRule exitRule = this.exits[DirCardinal.Right];
					if(exitRule == PagingExitRule.Wrap) { this.selectX = 0; }
					else if(exitRule == PagingExitRule.NoWrap) { /* Do nothing. No wrap. */ }
					else if(exitRule == PagingExitRule.LeaveArea) { this.exitDir = DirCardinal.Right; }
				} else { this.selectX = (byte)toRow; }
				return;
			}

			// Otherwise, if we're on the final row:
			if(toRow < 0) {
				PagingExitRule exitRule = this.exits[DirCardinal.Left];
				if(exitRule == PagingExitRule.Wrap) { this.selectX = (byte)(finalRow - 1); }
				else if(exitRule == PagingExitRule.NoWrap) { /* Do nothing. No wrap. */ }
				else if(exitRule == PagingExitRule.LeaveArea) { this.exitDir = DirCardinal.Left; }
			} else if(toRow >= finalRow) {
				PagingExitRule exitRule = this.exits[DirCardinal.Right];
				if(exitRule == PagingExitRule.Wrap) { this.selectX = 0; }
				else if(exitRule == PagingExitRule.NoWrap) { /* Do nothing. No wrap. */ }
				else if(exitRule == PagingExitRule.LeaveArea) { this.exitDir = DirCardinal.Right; }
			} else { this.selectX = (byte)toRow; }
		}
	}
}
