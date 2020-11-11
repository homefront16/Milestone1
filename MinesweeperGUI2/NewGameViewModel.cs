using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperGUI2
{
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
        // Easy = 8
        // Medium = 12
        // Difficult = 15

        public int Level { get; set; }

        public ICommand LevelCommand { get; set; }
        private void GetLevel(string content)
        {
            if (content == "Easy")
                Level = 8;
            else if (content == "Medium")
                Level = 12;
            else if (content == "Difficult")
                Level = 15;
        }

        public ICommand NewGameCommand { get; set; }
        private void NewGame()
        {
            this._newGame.Close();
        }
    }
}
