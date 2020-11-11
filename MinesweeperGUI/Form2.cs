using MinesweeperClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    public partial class Form2 : Form
    {
       
        // Stores a 2d Array
        public Button[,] gridOfButtons;
        public int clicks = 0;
        Board board;
        BindingSource bs = new BindingSource();

        public Form2(int sizeOfGrid)
        {

            InitializeComponent();
            board = new Board(sizeOfGrid);
            board.SetupLiveNeighbors(board);
            board.CalculateLiveNeighbors(board);

            
            bs.DataSource = board;


            gridOfButtons = new Button[sizeOfGrid, sizeOfGrid];
            
            placeButtonsOnGrid(sizeOfGrid);
        }

        /// <summary>
        /// Method creates a grid of buttons based on the size of the board. 
        /// The size of the board is received from form 1. The size of the buttons
        /// are created based on the size of the panel and amount of buttons created. 
        /// </summary>
        /// <param name="sizeOfBoard"></param>
        public void placeButtonsOnGrid(int sizeOfBoard)
        {
            // Calculate the width of each button using available button
            int sizeOfButton = panelButtonHolder.Width / sizeOfBoard;

            // Making the panel square
            panelButtonHolder.Height = panelButtonHolder.Width;

            // Created new buttons and places then in the panel
            for (int row = 0; row < sizeOfBoard; row++)
            {
                for (int col = 0; col < sizeOfBoard; col++)
                {
                    gridOfButtons[row, col] = new Button();
                    // Define the width and height of each button
                    gridOfButtons[row, col].Width = sizeOfButton;
                    gridOfButtons[row, col].Height = sizeOfButton;

                    // gridOfButtons[row, col].DataBindings.Add("bool", bs, board.GetCell(row, col).IsCellLive);
                    // gridOfButtons[row, col].DataBindings.Add("button", board, board.GetCell(row, col));
                    // Place the button on the panel
                    panelButtonHolder.Controls.Add(gridOfButtons[row, col]);
                    // Locate the button where it needs to live
                    gridOfButtons[row, col].Location = new Point(sizeOfButton * row, sizeOfButton * col);

                    // Creates an event handler for each button for a click event. 
                    gridOfButtons[row, col].Click += new EventHandler(this.GridButton_Click);
                    

                }
            }

            
        }

        /// <summary>
        /// Method is an event handler for when a button is clicked on the grid of buttons. 
        /// When a button is clicked it will display the total number of clicks on all buttons. 
        /// The background color of the button will change. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridButton_Click(object sender, EventArgs e)
        {

            clicks++;
            Button clickedButton = (Button)sender;
        
            clickedButton.Text = clicks.ToString();
            clickedButton.BackColor = Color.Aquamarine;
            
            //clickedButton.Enabled = false;


        }

        private void groupBoxButtonHolder_Enter(object sender, EventArgs e)
        {
        }

        private void panelButtonHolder_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
