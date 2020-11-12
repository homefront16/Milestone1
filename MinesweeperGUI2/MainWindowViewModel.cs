using Microsoft.Win32;
using MinesweeperClassLibrary;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MinesweeperGUI2
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
   
        private Board _board;
        private DispatcherTimer dispatchTime;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            NewGameCommand = new DelegateCommand<object>(NewGame);
            ButtonCommand = new DelegateCommand<Button>(ExecuteButtonCommand);
            ButtonCells = new ObservableCollection<Button>();
            Stopwatch = new Stopwatch();
            dispatchTime = new DispatcherTimer();
           
            
        }

        public int GameLevel { get; set; }
        public ObservableCollection<Button> ButtonCells { get; set; }
        private String _timeElapsed;
        public String TimeElapsed
        {
            get { return _timeElapsed; }
            set
            {
                if (value != this.TimeElapsed)
                {
                    _timeElapsed = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("TimeElapsed"));
                    }
                }
            }
        }

        public Stopwatch Stopwatch { get; set; }

        public ICommand NewGameCommand { get; set; }
        private void NewGame(object panelWidth)
        {
            var startNewGame = new NewGame();
            var startNewGameViewModel = new NewGameViewModel(startNewGame);
            startNewGame.DataContext = startNewGameViewModel;

            startNewGame.ShowDialog();

            GameLevel = startNewGameViewModel.Level;

            _board = new Board(GameLevel);
            _board.SetDifficulty(GameLevel * .05);
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
                        DataContext = c, // bind button to Cell object
                        Command = ButtonCommand
                    }; 

                    btn.CommandParameter = btn;

                    btn.PreviewMouseRightButtonUp += Btn_PreviewMouseRightButtonUp;
                    btn.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;

                    ButtonCells.Add(btn);
                }
            }
            TimeSpan interval = TimeSpan.FromSeconds(1);
            dispatchTime.Interval = interval;
            dispatchTime.Tick += new EventHandler(Dt_Tick);



        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = Stopwatch.Elapsed;
            string timeFormatted = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            TimeElapsed = timeFormatted;

        }

        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!Stopwatch.IsRunning)
            {
                dispatchTime.Start();
                Stopwatch.Start();

                
            }

            var btn = (Button)sender;
            var cell = btn.DataContext as Cell;

            Cell attemptedSquare = cell;

            _board.FloodFill2(_board, attemptedSquare.GetRow(attemptedSquare), attemptedSquare.GetColumn(attemptedSquare));

            if (attemptedSquare.IsCellLive(attemptedSquare))
            {
                btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\Raymond\\Source\\Repos\\homefront16\\Milestone1\\MinesweeperGUI2\\Pictures\\mine.jpg")),
                    VerticalAlignment = VerticalAlignment.Center
                };
                dispatchTime.Stop();
                Stopwatch.Stop();
                MessageBox.Show("You Lost!" + TimeElapsed + " seconds");
            }

            if (_board.CheckVisitedSquaresLeft(_board))
            {
                
                foreach (var button in ButtonCells)
                {
                    var cellHolder = button.DataContext as Cell;
                   // cellHolder.SetCellToVisited(cellHolder);

                    if (cellHolder.IsCellLive(cellHolder))
                    {
                        button.Content = new Image
                        {
                            Source = new BitmapImage(new Uri("C:\\Users\\Raymond\\Source\\Repos\\homefront16\\Milestone1\\MinesweeperGUI2\\Pictures\\flag.jpg")),
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    }
                }
                dispatchTime.Stop();
                Stopwatch.Stop();
                MessageBox.Show("You Won in " + TimeElapsed + " seconds");
                
                
            }

            btn.Background = Brushes.Gray;

            foreach (var button in ButtonCells)
            {
                var cell2 = button.DataContext as Cell;


                if (cell2.GetCellVisited(cell2))
                {
                    button.Background = Brushes.Gray;
                }
            }
            ShowLiveNeighbors(btn);
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
                             /*This if statment checks to see if adjacent grid locations
                             are visted. If there are cells not visted adjacent to the given cell than
                             it will display the amount of neighboring mines for the given cell. */
                             if (_board.GetCell(nx, ny).GetCellVisited(_board.GetCell(nx, ny)) == false)
                             {
                                 var cell = btn.DataContext as Cell;
                                 foreach(var button in ButtonCells)
                                 {
                                     if(button.Name == $"_{x}_{y}")
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

        }

    }

   
}
