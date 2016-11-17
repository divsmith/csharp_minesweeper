using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
	class Board
	{
		public int numberOfBombs;
		public int markedBombs;
		public Cell[,] grid;
		public int count3BV = 0;
		public int max3BV;
		public int boardCreationTries;

		public Board(int rows, int columns, int bombs, int max3BV)
		{
			numberOfBombs = bombs;
			grid = new Cell[rows, columns];
			this.max3BV = max3BV;

			do
			{
				Console.WriteLine("******** Creating New Board ************\n\n");
				count3BV = 0;
				boardCreationTries++;
				for (int x = 0; x < grid.GetLength(0); x++)
				{
					for (int y = 0; y < grid.GetLength(1); y++)
					{
						grid[x, y] = new Cell();
					}
				}

				PlaceBombs();
				CalculateAdjacentBombs();
				Calculate3BV();
			} while (count3BV > max3BV && boardCreationTries < 10);
		}

		private void Increment3BV()
		{
			Console.Write("\n");
			PrintGrid();
			count3BV++;
			Console.Write("\n");
		}

		private void Calculate3BV()
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = 0; y < grid.GetLength(1); y++)
				{
					if (!grid[x, y].isBomb && grid[x, y].adjacentBombs == 0)
					{
						if (!grid[x, y].isFloodFillMarked)
						{
							Increment3BV();

							grid[x, y].is3BVMarked = true;
							grid[x, y].isFloodFillMarked = true;

							FloodFillMark(x, y);
						}
					}
				}
			}

			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = 0; y < grid.GetLength(1); y++)
				{
					if (!grid[x, y].isBomb && !grid[x, y].isFloodFillMarked)
					{
						Increment3BV();
						grid[x, y].is3BVMarked = true;
					}
				}
			}

			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = 0; y < grid.GetLength(1); y++)
				{
					grid[x, y].isFloodFillMarked = false;
				}
			}
		}

		private void FloodFillMarkGrid(int x, int y)
		{
			if (!grid[x, y].isBomb && !grid[x, y].isFloodFillMarked)
			{
				grid[x, y].isFloodFillMarked = true;

				if (grid[x, y].adjacentBombs == 0)
				{
					FloodFillMark(x, y);
				}
			}
		}

		private void CalculateCenterFloodFillMark(int x, int y)
		{
			for (int x2 = x - 1; x2 < x + 2; x2++)
			{
				for (int y2 = y - 1; y2 < y + 2; y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateLeftEdgeFloodFillMark(int x)
		{
			for (int x2 = x - 1; x2 < x + 2; x2++)
			{
				for (int y2 = 0; y2 < 2; y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateRightEdgeFloodFillMark(int x)
		{
			for (int x2 = x - 1; x2 < x + 2; x2++)
			{
				for (int y2 = grid.GetLength(1) - 2; y2 < grid.GetLength(1); y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateTopEdgeFloodFillMark(int y)
		{
			for (int x2 = 0; x2 < 2; x2++)
			{
				for (int y2 = y - 1; y2 < y + 2; y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateTopLeftCornerFloodFillMark()
		{
			for (int x2 = 0; x2 < 2; x2++)
			{
				for (int y2 = 0; y2 < 2; y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateTopRightCornerFloodFillMark()
		{
			for (int x2 = 0; x2 < 2; x2++)
			{
				for (int y2 = grid.GetLength(1) - 2; y2 < grid.GetLength(1); y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateBottomEdgeFloodFillMark(int y)
		{
			for (int x2 = grid.GetLength(0) - 2; x2 < grid.GetLength(0); x2++)
			{
				for (int y2 = y - 1; y2 < y + 2; y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateBottomLeftCornerFloodFillMark()
		{
			for (int x2 = grid.GetLength(0) - 2; x2 < grid.GetLength(0); x2++)
			{
				for (int y2 = 0; y2 < 2; y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void CalculateBottomRightCornerFloodFillMark()
		{
			for (int x2 = grid.GetLength(0) - 2; x2 < grid.GetLength(0); x2++)
			{
				for (int y2 = grid.GetLength(1) - 2; y2 < grid.GetLength(1); y2++)
				{
					FloodFillMarkGrid(x2, y2);
				}
			}
		}

		private void FloodFillMark(int x, int y)
		{
			if (x != 0 && x != grid.GetLength(0) - 1)
			{
				if (y != 0 && y != grid.GetLength(1) - 1)
				{
					CalculateCenterFloodFillMark(x, y);
				}
				else if (y == 0)
				{
					CalculateLeftEdgeFloodFillMark(x);
				}
				else
				{
					CalculateRightEdgeFloodFillMark(x);
				}
			}
			else if (x == 0)
			{
				if (y != 0 && y != grid.GetLength(1) - 1)
				{
					CalculateTopEdgeFloodFillMark(y);
				}
				else if (y == 0)
				{
					CalculateTopLeftCornerFloodFillMark();
				}
				else
				{
					CalculateTopRightCornerFloodFillMark();
				}
			}
			else
			{
				if (y != 0 && y != grid.GetLength(1) - 1)
				{
					CalculateBottomEdgeFloodFillMark(y);
				}
				else if (y == 0)
				{
					CalculateBottomLeftCornerFloodFillMark();
				}
				else
				{
					CalculateBottomRightCornerFloodFillMark();
				}
			}
		}

		private void PlaceBombs()
		{
			Random bombPlacer = new Random();

			for (int bombs = 0; bombs < numberOfBombs; bombs++)
			{
				int x = bombPlacer.Next(0, grid.GetLength(0));
				int y = bombPlacer.Next(0, grid.GetLength(1));

				if (!grid[x, y].isBomb)
				{
					grid[x, y].isBomb = true;
				}
				else
				{
					bombs--;
				}
			}
		}

		private int CalculateCenterBombs(int x, int y)
		{
			int adjacentBombs = 0;

			for (int x2 = x - 1; x2 < x + 2; x2++)
			{
				for (int y2 = y - 1; y2 < y + 2; y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateLeftEdgeBombs(int x)
		{
			int adjacentBombs = 0;

			for (int x2 = x - 1; x2 < x + 2; x2++)
			{
				for (int y2 = 0; y2 < 2; y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateRightEdgeBombs(int x)
		{
			int adjacentBombs = 0;

			for (int x2 = x - 1; x2 < x + 2; x2++)
			{
				for (int y2 = grid.GetLength(1) - 2; y2 < grid.GetLength(1); y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateTopEdgeBombs(int y)
		{
			int adjacentBombs = 0;

			for (int x2 = 0; x2 < 2; x2++)
			{
				for (int y2 = y - 1; y2 < y + 2; y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateTopLeftCornerBombs()
		{
			int adjacentBombs = 0;

			for (int x2 = 0; x2 < 2; x2++)
			{
				for (int y2 = 0; y2 < 2; y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateTopRightCornerBombs()
		{
			int adjacentBombs = 0;

			for (int x2 = 0; x2 < 2; x2++)
			{
				for (int y2 = grid.GetLength(1) - 2; y2 < grid.GetLength(1); y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateBottomEdgeBombs(int y)
		{
			int adjacentBombs = 0;

			for (int x2 = grid.GetLength(0) - 2; x2 < grid.GetLength(0); x2++)
			{
				for (int y2 = y - 1; y2 < y + 2; y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateBottomLeftCornerBombs()
		{
			int adjacentBombs = 0;

			for (int x2 = grid.GetLength(0) - 2; x2 < grid.GetLength(0); x2++)
			{
				for (int y2 = 0; y2 < 2; y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateBottomRightCornerBombs()
		{
			int adjacentBombs = 0;

			for (int x2 = grid.GetLength(0) - 2; x2 < grid.GetLength(0); x2++)
			{
				for (int y2 = grid.GetLength(1) - 2; y2 < grid.GetLength(1); y2++)
				{
					if (grid[x2, y2].isBomb)
					{
						adjacentBombs++;
					}
				}
			}

			return adjacentBombs;
		}

		private int CalculateCellBombs(int x, int y)
		{
			int adjacentBombs = 0;

			if (x != 0 && x != grid.GetLength(0) - 1)
			{
				if (y != 0 && y != grid.GetLength(1) - 1)
				{
					adjacentBombs = CalculateCenterBombs(x, y);
				}
				else if (y == 0)
				{
					adjacentBombs = CalculateLeftEdgeBombs(x);
				}
				else
				{
					adjacentBombs = CalculateRightEdgeBombs(x);
				}
			}
			else if (x == 0)
			{
				if (y != 0 && y != grid.GetLength(1) - 1)
				{
					adjacentBombs = CalculateTopEdgeBombs(y);
				}
				else if (y == 0)
				{
					adjacentBombs = CalculateTopLeftCornerBombs();
				}
				else
				{
					adjacentBombs = CalculateTopRightCornerBombs();
				}
			}
			else
			{
				if (y != 0 && y != grid.GetLength(1) - 1)
				{
					adjacentBombs = CalculateBottomEdgeBombs(y);
				}
				else if (y == 0)
				{
					adjacentBombs = CalculateBottomLeftCornerBombs();
				}
				else
				{
					adjacentBombs = CalculateBottomRightCornerBombs();
				}
			}

			return adjacentBombs;
		}

		private void CalculateAdjacentBombs()
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = 0; y < grid.GetLength(1); y++)
				{
					if (!grid[x, y].isBomb)
					{
						grid[x, y].adjacentBombs = CalculateCellBombs(x, y);
					}
				}
			}
		}

		public void PrintGrid()
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = 0; y < grid.GetLength(1); y++)
				{
					if (grid[x, y].is3BVMarked)
					{
						Console.Write(" ^ ");
					}
					else if (grid[x, y].isBomb)
					{
						Console.Write(" X ");
					}
					else if (grid[x, y].adjacentBombs != 0)
					{
						Console.Write(" " + grid[x, y].adjacentBombs + " ");
					}
					else if (grid[x, y].isFloodFillMarked)
					{
						Console.Write(" - ");
					}
					else
					{
						Console.Write(" O ");
					}
				}

				Console.Write("\n");
			}
		}
	}
}
