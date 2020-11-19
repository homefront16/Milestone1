using MinesweeperClassLibrary;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MinesweeperGUI2
{
    public class PlayerStatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PlayerStats> playerStatisticsList { get; private set; }

        private List<PlayerStats> _playerStats;
        private DataHandling _dataHandling;

        public ICommand InsertStatisticsCommand { get; set; }

        private readonly PlayerStatistics _playerStatistics;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlayerStatisticsViewModel(PlayerStatistics playerStatistics)
        {
            _playerStatistics = playerStatistics;
            InsertStatisticsCommand = new DelegateCommand<object>(InsertStatistics);
            this.playerStatisticsList = new ObservableCollection<PlayerStats>();
        }

        private String _Name;
        /// <summary>
        /// Property holds the name value Typically taken from a textbox. It also utilizes
        /// the PropertyChanged event which will update whenever the value is changed. 
        /// </summary>
        public String Name
        {
            get { return _Name; }
            set
            {
                if (value != this.Name)
                {
                    _Name = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("Name")); // Uses the 
                    }
                }
            }
        }
        private int _Score;
        /// <summary>
        /// Property holds the Score value taken from the json file. It also utilizes
        /// the PropertyChanged event which will update whenever the value is changed. 
        /// </summary>
        public int Score
        {
            get { return _Score; }
            set
            {
                if (value != this.Score)
                {
                    _Score = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("Score")); // Uses the 
                    }
                }
            }
        }
        private int _TimeElapsed;
        /// <summary>
        /// Property holds the Time Elapsed value taken from the json file. It also utilizes
        /// the PropertyChanged event which will update whenever the value is changed. 
        /// </summary>
        public int TimeElapsed
        {
            get { return _Score; }
            set
            {
                if (value != this.TimeElapsed)
                {
                    _TimeElapsed = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("TimeElapsed")); // Uses the 
                    }
                }
            }
        }
        private int _GameLevel;

        /// <summary>
        /// Property holds the GameLevel value taken from the json file. It also utilizes
        /// the PropertyChanged event which will update whenever the value is changed. 
        /// </summary>
        public int GameLevel
        {
            get { return _GameLevel; }
            set
            {
                if (value != this.GameLevel)
                {
                    _GameLevel = value;

                    var handler = this.PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("GameLevel")); // Uses the 
                    }
                }
            }
        }


        /// <summary>
        /// This method reads a json file. That file is deserialized in to a list of 
        /// PlayerStat objects. Those objects are sorted to the top 5 scores via a
        /// LINQ query. The display data can be changed with other LINQ querys if requested. 
        /// </summary>
        /// <param name="obj"></param>
        private void InsertStatistics(object obj)
        {
            _dataHandling = new DataHandling();

            string fileName = @"C:\Users\Raymond\Source\Repos\homefront16\Milestone1\MinesweeperGUI2\Data\PlayerStats.json";
            _playerStats = new List<PlayerStats>();         
            _playerStats = _dataHandling.ReadJSONFile(fileName); // Deserializing file in to list of PlayerStats objects

            var topFivePlayers = (from playerStat in _playerStats
                                 orderby playerStat.Score descending
                                  select playerStat).Take(5); // LINQ query for TOP 5 Scores. 

            foreach(PlayerStats playerStat in topFivePlayers)
            {
                playerStatisticsList.Add(playerStat); // Adding to display list of PlayerStats objects. 
            }
        }



    }
}
