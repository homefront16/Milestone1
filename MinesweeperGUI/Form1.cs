using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method handlers radio button click events. 
        /// The method will set the size of the board to 8, 12, or 15
        /// based on the difficulty level. A greater difficulty will equate 
        /// to a larger board. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            int sizeOfBoard = 0;
            if (radioEasy.Checked)
            {
                sizeOfBoard = 8;
            }
            else if (radioModerate.Checked)
            {
                sizeOfBoard = 12;
            }
            else
            {
                sizeOfBoard = 15;
            }
            Form2 newForm = new Form2(sizeOfBoard);
            newForm.ShowDialog();
        }
    }
}
