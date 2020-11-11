using MinesweeperClassLibrary;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace MinesweeperGUI2
{
    public class MainWindowViewModel
    {
        private Board _board;

        public MainWindowViewModel()
        {
            NewGameCommand = new DelegateCommand<object>(NewGame);
            ButtonCommand = new DelegateCommand<Button>(ExecuteButtonCommand);
            ButtonCells = new ObservableCollection<Button>();
        }

        public int GameLevel { get; set; }
        public ObservableCollection<Button> ButtonCells { get; set; }

        

        public ICommand NewGameCommand { get; set; }
        private void NewGame(object panelWidth)
        {
            var startNewGame = new NewGame();
            var startNewGameViewModel = new NewGameViewModel(startNewGame);
            startNewGame.DataContext = startNewGameViewModel;

            startNewGame.ShowDialog();

            GameLevel = startNewGameViewModel.Level;

            _board = new Board(GameLevel);
            _board.SetDifficulty(GameLevel * .2);
            _board.SetupLiveNeighbors(_board);
            _board.CalculateLiveNeighbors(_board);
            double widthOfPanel = Convert.ToDouble(panelWidth);
            SetupBoard(widthOfPanel);

            
        }

        private void SetupBoard(double panelWidth)
        {
            var sizeOfbutton = panelWidth / _board.GetSize();
            for (int row = 0; row < _board.GetSize(); row++)
            {
                for (int col = 0; col < _board.GetSize(); col++)
                {
                    Cell c = _board.GetCell(row, col);
                    var btn = new Button
                    {
                        Name = $"_{row}_{col}",
                        Width = sizeOfbutton,
                        Height = sizeOfbutton,
                        //Margin = new System.Windows.Thickness(3),
                        DataContext = c,
                        Command = ButtonCommand
                    }; // bind button to Cell object

                    btn.CommandParameter = btn;
                    
                    btn.PreviewMouseRightButtonUp += Btn_PreviewMouseRightButtonUp;
                    btn.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;
                    
                    ButtonCells.Add(btn);
                }
            }
        }

        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var btn = (Button)sender;
            var cell = btn.DataContext as Cell;

            Cell attemptedSquare = cell;

            //_board.FloodFill(attemptedSquare.GetRow(attemptedSquare), attemptedSquare.GetColumn(attemptedSquare));
            _board.FloodFill2(_board, attemptedSquare.GetRow(attemptedSquare), attemptedSquare.GetColumn(attemptedSquare));

            if (attemptedSquare.IsCellLive(attemptedSquare))
            {
                btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\Raymond\\Source\\Repos\\homefront16\\Milestone1\\MinesweeperGUI2\\Pictures\\mine.jpg")),
                    VerticalAlignment = VerticalAlignment.Center
                };
                MessageBox.Show("You Lost!");
            }

           // attemptedSquare.SetCellToVisited(attemptedSquare);

            if (_board.CheckVisitedSquaresLeft(_board))
            {
                MessageBox.Show("You Won!");
                _board.SetGameOver(_board);
            }

            btn.Background = Brushes.Gray;
            
            foreach (var button in ButtonCells)
            {
                var cell2 = button.DataContext as Cell;
            /*    if (cell2.IsCellLive(cell2))
                {
                    button.Content = new Image
                    {
                        Source = new BitmapImage(new Uri("C:\\Users\\Raymond\\Source\\Repos\\homefront16\\Milestone1\\MinesweeperGUI2\\Pictures\\mine.jpg")),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                }*/
                
                if (cell2.GetCellVisited(cell2))
                {
                    button.Background = Brushes.Gray;
                    
                    button.Content = "";
                 
                    ShowLiveNeighbors(button);
                }
                /*if(cell2.GetCellVisited(cell2) == false && cell2.IsCellLive(cell2) == false)
                {
                    button.Content = cell2.GetNeighbors(cell2).ToString();
                }*/
                
            }
        }

        // right-click
        private void Btn_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var btn = (Button)sender;
            var cell = btn.DataContext as Cell;

            btn.Content = new Image
            {
                Source = new BitmapImage(new Uri("C:\\Users\\Raymond\\Source\\Repos\\homefront16\\Milestone1\\MinesweeperGUI2\\Pictures\\flag.jpg")),
                VerticalAlignment = VerticalAlignment.Center
            };
            
        }
        public void ShowLiveNeighbors(Button btn)
        {
            /*these arrays hold the x and y values for adjacent positions
          around the selected grid location. They will act as "square" around
          the selected grid location to check for mines*/
            int[] offSetX = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] offSetY = { -1, -1, 0, 1, 1, 1, 0, -1 };

            for (int x = 0; x < _board.GetSize(); x++)
            {
                for (int y = 0; y < _board.GetSize(); y++)
                {
                    if (_board.GetCell(x, y).GetCellVisited(_board.GetCell(x, y)))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            int nx = x + offSetX[i];
                            int ny = y + offSetY[i];

                            // The first if statement catches times where the current
                            // for loop is checking a location that is out of bounds
                            if (nx < 0 || nx > _board.GetSize() - 1 || ny < 0 || ny > _board.GetSize() - 1)
                            {
                                continue;
                            }
                            /*This if statment checks to see if the current grid location
                            is holding a mine and then increments the Neighbor property if true.
                            True = mine is present.*/
                            if (_board.GetCell(nx, ny).GetCellVisited(_board.GetCell(nx, ny)) == true)
                            {
                                var cell = btn.DataContext as Cell;
                                foreach(var button in ButtonCells)
                                {
                                    if(button.Name == $"_{nx}_{ny}")
                                    {
                                        var correctCell = button.DataContext as Cell;
                                        if(button.Content == null)
                                        {
                                            button.Content = correctCell.GetNeighbors(correctCell).ToString();
                                        }
                                        
                                    }
                                }    
                                
                            }

                        }
                    }
                }
            }
        }

        public ICommand ButtonCommand { get; set; }

        // left-click
        private void ExecuteButtonCommand(Button btn)
        {
          

           

            /*for (int row = 0; row < _board.GetSize(); row++)
            {
                for (int col = 0; col < _board.GetSize(); col++)
                {
                    Cell cellCheck = _board.GetCell(row, col);
                    if (cellCheck.GetCellVisited(cellCheck))
                    {
                        ButtonCells.GetEnumerator
                    }
                }
            }*/
            //_board.PrintBoardDuringGame();
            /*  if (cell.GetCellVisited(cell))
              {
                  btn.Background = Brushes.DarkGray;
              }*/
            //if (MouseAction.LeftClick)
            //{
            //    cell.SetCellToVisited(cell);
            //}


        }

       
    }
}
