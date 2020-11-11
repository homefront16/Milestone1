using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WpfApp1
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            ClickMe = new ClickMeCommand();
            // Calculate the width of each button using available button
            
            Panel pt = this.
            object item = WpfApp1{ Panel.NameProperty =}

            int sizeOfButton = panelButtonHolder.Width / sizeOfBoard;

            // Making the panel square
            panelButtonHolder.Height = panelButtonHolder.Width;
            
            DynamicButton = new Button
            {
                Content = "Dynamic button",
                Command = ClickMe,
                Width = 100,
                Height = 30
            };
        }

        public ICommand ClickMe { get; set; }

        public Button DynamicButton { get; set; }
    }

    public class ClickMeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("You clicked me!");
        }
    }
}
