
using Nexus.GameEngine;
using System;

namespace Nexus.Engine {

	public class PagingSystem {

		// Paging Rules
		public readonly short NumberOfItems;
		public readonly byte PerRow;
		public readonly byte PerPage;

		// Paging Selections
		public short page = 0;
		public byte numOnPage = 0;
		public byte selectX = 0;
		public byte selectY = 0;

		public PagingSystem(byte numRows, byte numCols, short numItems) {
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

		// Optional Input Process
		public bool PagingInput(PlayerInput playerInput) {
			
			// Selector Movement
			if(playerInput.isPressed(IKey.Up)) { this.MoveSelector(0, -1); return true; }
			if(playerInput.isPressed(IKey.Down)) { this.MoveSelector(0, 1); return true; }
			if(playerInput.isPressed(IKey.Left)) { this.MoveSelector(-1, 0); return true; }
			if(playerInput.isPressed(IKey.Right)) { this.MoveSelector(1, 0); return true; }

			// Page Changing
			if(playerInput.isPressed(IKey.L1) || playerInput.isPressed(IKey.L2)) { this.ToPage(Math.Max((short) 0, (short)(this.page - 1))); return true; }
			if(playerInput.isPressed(IKey.R1) || playerInput.isPressed(IKey.R2)) { this.ToPage((short)(this.page + 1)); return true; }

			return false;
		}

		public void ToPage(short pageNum) {

			// Cannot reduce below 0.
			if(pageNum < 0) { pageNum = 0; }

			// Cannot exceed max page:
			else if(pageNum * this.PerPage > this.NumberOfItems) {
				pageNum = (short)Math.Floor((float)this.NumberOfItems / (float)this.PerPage);
			}

			// Update Page
			this.page = pageNum;
			this.numOnPage = (byte)Math.Min(this.PerPage, (this.NumberOfItems - (pageNum * this.PerPage)));

			// Reposition to Safe Selector Location:
			this.selectX = 0;
			this.selectY = 0;
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

				// Wrap around, if applicable.
				if(toCol >= cols) { this.selectY = 0; }

				// Standard Move
				else if(toCol < cols - 1) { this.selectY++; }

				// Standard Move to the final row.
				else {
					if(toRow >= finalRow) { this.selectY = 0; } else { this.selectY = (byte)(cols - 1); }
				}

				return;
			}

			// Moving Up
			if(slotY == -1) {

				// Wrap around, if applicable.
				// This wrap is more complicated, since you have to handle the finalRow issue.
				if(toCol < 0) {
					if(toRow < finalRow) { this.selectY = (byte)(cols - 1); } else { this.selectY = (byte)(cols - 2); }
				}

				// Standard Move
				else {
					this.selectY--;
				}

				return;
			}

			// If we're not on the final row, or the final row is full:
			if(toCol != (byte)(cols - 1) || finalRow == this.PerRow) {
				if(toRow < 0) { this.selectX = (byte)(this.PerRow - 1); } else if(toRow >= this.PerRow) { this.selectX = 0; } else { this.selectX = (byte)toRow; }
				return;
			}

			// Otherwise, if we're on the final row:
			if(toRow < 0) { this.selectX = (byte)(finalRow - 1); } else if(toRow >= finalRow) { this.selectX = 0; } else { this.selectX = (byte)toRow; }
		}

	}
}
