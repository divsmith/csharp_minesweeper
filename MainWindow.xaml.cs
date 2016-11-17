using System;
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
		public string minutes = "00";
		public string seconds = "00";

		public Grid grid;

		private Board gameboard;

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

					if (gameboard.grid[i, j].isFloodFillMarked)
					{
						button.Content = gameboard.grid[i, j].adjacentBombs;
					}

					if (gameboard.grid[i, j].is3BVMarked)
					{
						button.Content += "*";
					}

					Grid.SetColumn(button, i);
					Grid.SetRow(button, j);

					grid.Children.Add(button);
				}
			}

			myArea.Children.Add(grid);
		}

		private void LeftClick(object sender, RoutedEventArgs e)
		{
			int x = Grid.GetColumn((Button)sender);
			int y = Grid.GetRow((Button)sender);

			gameboard.Click(x, y);

			DrawGrid();
		}

		public MainWindow()
		{
			InitializeComponent();

			gameboard = new Board(10, 10, 20, 20);

			DrawGrid();

			timer.Content = minutes + ":" + seconds;
			difficulty.Content = "3BV Difficulty: " + gameboard.count3BV;
			remainingBombs.Content = "Bombs: " + (gameboard.numberOfBombs - gameboard.markedBombs).ToString();
		}

		private void hint_Click(object sender, RoutedEventArgs e)
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
		}
	}
}
