﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_3
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public string minutes;
		public string seconds;

		public Grid grid;

		private Board gameboard;

		private bool gameover;
		private bool gamewon;

		private void DrawGrid()
		{
			myArea.Children.Remove(grid);

			grid = new Grid();
			grid.Name = "grid";
			grid.Width = 320;
			grid.Height = 320;
			grid.HorizontalAlignment = HorizontalAlignment.Center;
			grid.VerticalAlignment = VerticalAlignment.Center;

			for (int i = 0; i < 10; i++)
			{
				ColumnDefinition col = new ColumnDefinition();
				grid.ColumnDefinitions.Add(col);
			}

			for (int i = 0; i < 10; i++)
			{
				RowDefinition row = new RowDefinition();
				grid.RowDefinitions.Add(row);
			}

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					Button button = new Button();

					button.Click += LeftClick;
					button.MouseRightButtonUp += RightClick;

					if (gameboard.grid[i, j].isFloodFillMarked && !gameboard.grid[i, j].isBomb)
					{
						button.Background = Brushes.DarkGray;

						switch(gameboard.grid[i, j].adjacentBombs)
						{
							case 1:
								button.Foreground = Brushes.Blue;
								break;

							case 2:
								button.Foreground = Brushes.Green;
								break;

							case 3:
								button.Foreground = Brushes.Red;
								break;

							case 4:
								button.Foreground = Brushes.Purple;
								break;

							case 5:
								button.Foreground = Brushes.Maroon;
								break;

							case 6:
								button.Foreground = Brushes.Teal;
								break;

							case 7:
								button.Foreground = Brushes.Magenta;
								break;

							case 8:
								button.Foreground = Brushes.Yellow;
								break;
						}

						if (gameboard.grid[i, j].adjacentBombs > 0)
						{
							button.Content = gameboard.grid[i, j].adjacentBombs;
						}
					}

					if (gameboard.grid[i, j].isFloodFillMarked && gameboard.grid[i, j].isBomb)
					{
						button.Background = Brushes.Red;
						button.Content = "\uD83D\uDCA3";
						gameover = true;
					}

					if (!gameboard.grid[i, j].isFloodFillMarked && gameboard.grid[i, j].isHint)
					{
						button.Background = Brushes.Green;
					}

					if (gameboard.grid[i, j].isBombFlagged)
					{
						button.Content = "\u2691";
					}

					Grid.SetColumn(button, i);
					Grid.SetRow(button, j);

					grid.Children.Add(button);
				}
			}

			myArea.Children.Add(grid);

			remainingBombs.Content = "Bombs: " + (gameboard.numberOfBombs - gameboard.markedBombs).ToString();
		}

		private void LeftClick(object sender, RoutedEventArgs e)
		{
			if (!gameover && !gamewon)
			{
				int x = Grid.GetColumn((Button)sender);
				int y = Grid.GetRow((Button)sender);

				if (!gameboard.grid[x, y].isBombFlagged)
				{
					gameboard.Click(x, y);
				}

				DrawGrid();
			}
		}

		private void RightClick(object sender, RoutedEventArgs e)
		{
			if (!gameover && !gamewon)
			{
				int x = Grid.GetColumn((Button)sender);
				int y = Grid.GetRow((Button)sender);

				gameboard.RightClick(x, y);

				DrawGrid();

				CheckWin();
			}
		}

		private void CheckWin()
		{
			if (gameboard.accuratelyMarkedBombs == gameboard.numberOfBombs && gameboard.accuratelyMarkedBombs == gameboard.markedBombs)
			{
				gamewon = true;

				System.Windows.MessageBox.Show("You've won with a score of " + minutes);
			}
		}

		public MainWindow()
		{
			InitializeComponent();

			NewGame();
		}

		private void NewGame()
		{
			gameboard = new Board(10, 10, 15, 20);
			DrawGrid();

			minutes = "00";
			seconds = "00";
			gameover = false;
			gamewon = false;

			timer.Content = minutes + ":" + seconds;
			difficulty.Content = "3BV Difficulty: " + gameboard.count3BV;
		}

		private void reset_Click(object sender, RoutedEventArgs e)
		{
			NewGame();
		}

		private void hint_Click(object sender, RoutedEventArgs e)
		{
			if (!gameover)
			{
				if (seconds.Equals("00"))
				{
					seconds = "30";
				}
				else
				{
					seconds = "00";
					int intminutes = int.Parse(minutes);

					intminutes++;
					minutes = intminutes.ToString();
				}

				timer.Content = minutes + ":" + seconds;

				gameboard.MarkHint();
				DrawGrid();
			}
		}
	}
}
