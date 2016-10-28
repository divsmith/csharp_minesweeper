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
		public MainWindow()
		{
			InitializeComponent();

			Board gameboard = new Board(10, 10, 20, 20);

			Grid grid = new Grid();
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
					Label text = new Label();
					if (gameboard.grid[i, j].isBomb)
					{
						text.Content = "X";
					}
					else
					{
						text.Content = gameboard.grid[i, j].adjacentBombs;
					}

					text.BorderThickness = new Thickness(1);
					text.BorderBrush = Brushes.Black;

					Grid.SetColumn(text, i);
					Grid.SetRow(text, j);

					grid.Children.Add(text);
				}
			}

			myArea.Children.Add(grid);

			difficulty.Content = "Difficulty: Hard";
			remainingBombs.Content = "Bombs: " + (gameboard.numberOfBombs - gameboard.markedBombs).ToString();
		}
	}
}
