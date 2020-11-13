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
    /// <summary>
    /// This class displays the minesweeper game. The INotifyPropertyChanged is used
    /// to Update the TimeElapsed String which is binded to a TextBox (subject to change). 
    /// </summary>
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
        /// <summary>
        /// Time elapsed is formatted in the Dt_Tick event handler. 
        /// If the value of TimeElapsed is changed it will use the PropertyChanged event
        /// activate the property value changed source trigger of the text box holding this value. 
        /// </summary>
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
                        handler(this, new PropertyChangedEventArgs("TimeElapsed")); // Uses the 
                    }
                }
            }
        }

        public Stopwatch Stopwatch { get; set; }

        public ICommand NewGameCommand { get; set; }

        /// <summary>
        /// This method opens the new game window. The board is set with mines, 
        /// based on the difficulty chosen. The number of mines neighboring each square
        /// is calculated. Lastly, the buttons are placed on the board and are binded
        /// to Cells. 
        /// </summary>
        /// <param name="panelWidth"></param>
        private void NewGame(object panelWidth)
        {
            var startNewGame = new NewGame();
            var startNewGameViewModel = new NewGameViewModel(startNewGame);
            startNewGame.DataContext = startNewGameViewModel;

            startNewGame.ShowDialog();

            GameLevel = startNewGameViewModel.Level;

            _board = new Board(GameLevel);
            _board.SetDifficulty(GameLevel * .1);
            _board.SetupLiveNeighbors(_board);
            _board.CalculateLiveNeighbors(_board);
            double widthOfPanel = Convert.ToDouble(panelWidth);
            SetupBoard(widthOfPanel);
        }

        /// <summary>
        /// This method places the buttons within a panel. The size of the buttons
        /// are made based on the panel width divided by the size of the board. 
        /// Each button is given a name attribute with its respective row and column. The buttons
        /// are then binded to Cell objects. A left mouse click and right mouse click event are also
        /// added to each button. Lastly the dispatch time interval is set for 1 second and an event 
        /// handler is set for Tick events. 
        /// </summary>
        /// <param name="panelWidth"></param>
        private void SetupBoard(double panelWidth)
        {
            
            var sizeOfbutton = panelWidth / _board.GetSize();
            // 2 for loops to iterate through all rows and columns of the _board. 
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

                    ButtonCells.Add(btn); // Button is added to the ButtonCells Collection
                }
            }
            TimeSpan interval = TimeSpan.FromSeconds(1);
            dispatchTime.Interval = interval;
            dispatchTime.Tick += new EventHandler(Dt_Tick);



        }

        /// <summary>
        /// This method handles Tick events occuring for dispatchTime. 
        /// The elapsed time is formatted and then set as the value for 
        /// Time elapsed.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dt_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = Stopwatch.Elapsed;
            string timeFormatted = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            TimeElapsed = timeFormatted;

        }

        /// <summary>
        /// This method handles the a left mouse click by the User on a button. 
        /// It contains the game play logic. The stopwatch is started. The Floodfill method is called to 
        /// set the value of Visted to true for cells that have 0 neighboring mines and were next to the clicked
        /// button. The IsCellLive method is then called to see if a user clicked on a mine (if they did a mine will be displayed 
        /// and the game is over. The CheckVistedSquaresLeft method is called to see if there are any cells left that have not been
        /// visted. If the user finds all squares without a mine the user wins and is shown all mine locations with a flag showing. 
        /// Lastly, the ShowLiveNeighbors method is called to display the number of neighboring mines next to the recently revealed Cells. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Starts the dispatch and stopwatch if time hasn't already started.
            if (!Stopwatch.IsRunning)
            {
                dispatchTime.Start();
                Stopwatch.Start();
            }

            var btn = (Button)sender; 
            var cell = btn.DataContext as Cell; //Binds the clicked button to a Cell

            Cell attemptedSquare = cell;

            //Floodfill method called recursively to show all squares without neighboring mines. 
            _board.FloodFillUpgraded(_board, attemptedSquare.GetRow(attemptedSquare), attemptedSquare.GetColumn(attemptedSquare));

            //Checks to see if the user clicked on a mine. If they do click on a mine than a mine will be shown
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

            //Checks if there are any squares left for the user to click on that are not mines. User wins if they find all squares without mines. 
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

            btn.Background = Brushes.Gray; // Setting the clicked buttons background as grey symbolized visted square. 

            foreach (var button in ButtonCells)
            {
                var cell2 = button.DataContext as Cell;


                if (cell2.GetCellVisited(cell2))
                {
                    button.Background = Brushes.Gray; // all visited squares set to a grey background. 
                }
            }
            ShowLiveNeighbors(btn);
        }

       /// <summary>
       /// This method handles right mouse click events. If a user clicks a button with a right
       /// mouse click a flag will be displayed on that button. This typically symbolizes a cell location
       /// that the user believes is a mine. 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
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

        /// <summary>
        /// This method is used to show the amount of mines adjacent to the Visted square. 
        /// It is set to only show the cells bordering unvisted squares. 
        /// </summary>
        /// <param name="btn"></param>
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
                    /*Iterating through the entire board and checking for visibile cells. When
                      a visible cell is found it then checks its adjacent cells to see if there 
                      if there are any unvisted cells.*/
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

        /// <summary>
        /// To be continued....
        /// </summary>
        /// <param name="btn"></param>
        private void ExecuteButtonCommand(Button btn)
        {

        }

    }

   
}
