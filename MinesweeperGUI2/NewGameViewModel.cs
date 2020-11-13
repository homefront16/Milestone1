using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperGUI2
{
    /// <summary>
    /// This class Opens a new game window when the user selects new game (first menu option)
    /// The game difficulty is selected and then the value is passed to the main window. 
    /// </summary>
    public class NewGameViewModel
    {
        private readonly NewGame _newGame;
        public NewGameViewModel(NewGame newGame)
        {
            _newGame = newGame;
            LevelCommand = new DelegateCommand<string>(GetLevel);
            NewGameCommand = new DelegateCommand(NewGame);
            Level = 8;
        }
        

        public int Level { get; set; }

        public ICommand LevelCommand { get; set; }

        /// <summary>
        /// This method sets the value of Level based on which radio
        /// button the user selects when starting a new game. The
        /// integer value is passed to the Main Window and used as the size of the board. 
        /// </summary>
        /// <param name="content"></param>
        private void GetLevel(string content)
        {
            if (content == "Easy")
                Level = 8;
            else if (content == "Medium")
                Level = 12;
            else if (content == "Difficult")
                Level = 15;
        }

        /// <summary>
        /// This method closes the new game window after a user has selected a 
        /// game level and clicked on the start new game button. 
        /// </summary>
        public ICommand NewGameCommand { get; set; }
        private void NewGame()
        {
            this._newGame.Close();
        }
    }
}
