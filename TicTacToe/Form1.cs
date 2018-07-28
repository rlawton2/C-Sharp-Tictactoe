using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
/*
 * This project was developed following a tutorial online. I made many adjustments to the project, you can run the project and 
 * then visit the Youtube url to see the difference. One of the adjustments is I made the game a single player game. Also, the
 * game tracks wins/losses, background images, and draws a line through the winning combination among other things.
 * 
 * Credit Chris Merritt on Youtube https://www.youtube.com/watch?v=p3gYVcggQOU
 */
namespace TicTacToe
{
    public partial class Form1 : Form
    {
        bool turn = true;   // When true x turn, false y turn
        int turnCount = 0, p1Wins, p2Wins, p1Losses, p2Losses;  // Counts turns of players
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            label1.Text = "Player One's turn.";
        }
       
        private void buttonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
            {
                b.Text = "X";
                label1.Text = "Player Two's turn.";
            }
            else
            {
                b.Text = "O";
                label1.Text = "Player One's turn.";
            }
            turn = !turn;
            b.Enabled = false;
            turnCount++;


            checkForWinner();
        }

        private void checkForWinner()
        {
                bool winner = false;

                // Horizontal check
                if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && !A1.Enabled)
                    winner = true;
                else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && !B1.Enabled)
                    winner = true;
                else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && !C1.Enabled)
                    winner = true;

                // Horizontal check
                else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && !A1.Enabled)
                    winner = true;
                else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && !A2.Enabled)
                    winner = true;
                else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && !A3.Enabled)
                    winner = true;

                // Diagonal check
                else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && !A1.Enabled)
                    winner = true;
                else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && !A3.Enabled)
                    winner = true;
 
                if(winner)
                {
                    disableButtons();
                    
                    string theWinner = "";
                    if (turn)
                    {
                        theWinner = "Player Two (O)";
                        p2WinsLabel.Text = (p2Wins += 1).ToString();
                        p1LossesLabel.Text = (p1Losses += 1).ToString();
                    }
                    else
                    {
                        theWinner = "Player One (X)";
                        p1WinsLabel.Text = (p1Wins += 1).ToString();
                        p2LossesLabel.Text = (p2Losses += 1).ToString();
                    }
                    label1.Text = (theWinner + " wins! Game Over");
                }
                else
                {
                    if(turnCount == 9)
                    label1.Text = ("Draw! No Winners. Game Over");
                }
            }
            
        private void disableButtons()
        {
            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
                catch { }
            }
        }

        private void changeBackground()
            {
                var FD = new System.Windows.Forms.OpenFileDialog();
                var path = @"\Images\";
                var imagePath = (Path.GetDirectoryName(Application.ExecutablePath));

                string background = (rand.Next(1, 5)).ToString();
                FD.FileName =  imagePath + path + background + ".jpg";
                Console.WriteLine(FD.FileName);
                try
                {
                    BackgroundImage = Image.FromFile(FD.FileName);
                } catch (FileNotFoundException ffe) {
                    Console.WriteLine("Not working");
                }

            }
            // New form load
            private void Form1_Load(object sender, EventArgs e)
            {
                label1.BackColor = System.Drawing.Color.Transparent;

                changeBackground();
            }
            // Menu "About" click
            private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("For Bibi, the best BFF in the whole world!", "Bibi's Tic Tac Toe");
            }
            // Menu "Exit" click
            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

        // Menu "New Game" click
        private void newgameToolStripMenuItem_Click(object sender, EventArgs e)
            {
                turn = true;
                turnCount = 0;

                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                        b.Text = "";
                        b.Enabled = true;
                    }
                    catch { }
                }
                label1.Text = "Player One's turn.";
            }
            // Menu "Change Background" click
            private void changeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
            {
                changeBackground();
            }
    }
}
