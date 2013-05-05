using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AgileMind.TestWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tictactoe.ITicTacToeservice client = new tictactoe.ITicTacToeservice();
            int result = client.NewGame();
            client.MakeMove(result, 1, 1);
            int isWinner = client.IsWinner(1);
            int move = client.NextMove(result, 2);
        }
    }
}
