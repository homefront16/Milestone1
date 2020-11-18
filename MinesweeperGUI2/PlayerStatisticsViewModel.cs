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

        /*private ObservableCollection<PlayerStatistics> GetPlayerStatisticsList()
        {
            return 
        }
*/
        private String _Name;
        /// <summary>
        /// Time elapsed is formatted in the Dt_Tick event handler. 
        /// If the value of TimeElapsed is changed it will use the PropertyChanged event
        /// activate the property value changed source trigger of the text box holding this value. 
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
        /// Time elapsed is formatted in the Dt_Tick event handler. 
        /// If the value of TimeElapsed is changed it will use the PropertyChanged event
        /// activate the property value changed source trigger of the text box holding this value. 
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



        private void InsertStatistics(object obj)
        {
            _dataHandling = new DataHandling();

            string fileName = @"C:\Users\Raymond\Source\Repos\homefront16\Milestone1\MinesweeperGUI2\Data\PlayerStats.json";
            _playerStats = new List<PlayerStats>();         
            _playerStats = _dataHandling.ReadJSONFile(fileName);
           
            foreach(PlayerStats playerStat in _playerStats)
            {
                playerStatisticsList.Add(playerStat);
            /*    Name = playerStat.Name;
                Score = playerStat.Score;
                GameLevel = playerStat.Difficulty;
                TimeElapsed = playerStat.Time;*/
            }
        }



    }
}
