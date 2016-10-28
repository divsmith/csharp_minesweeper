using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
	class Cell
	{
		public bool isBomb;
		public int adjacentBombs;
		public bool isFloodFillMarked;
		public bool isQuestionFlagged;
		public bool isBombFlagged;
		public bool is3BVMarked;

		public Cell()
		{
			isBomb = false;
			adjacentBombs = 0;
			isFloodFillMarked = false;
			isQuestionFlagged = false;
			isBombFlagged = false;
			is3BVMarked = false;
		}
	}
}
