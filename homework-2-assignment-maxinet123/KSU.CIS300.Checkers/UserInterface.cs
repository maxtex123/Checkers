/*
 * UserInterface.cs
 * Author: Maxine Teixeira
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSU.CIS300.Checkers
{
    public partial class UserInterface : Form
    {
        /// <summary>
        /// initializes the game
        /// </summary>
        private Game _game;
        /// <summary>
        /// Sets the red image to the red piece
        /// </summary>
        private Image _red = Image.FromFile(@"pics\red.png");
        /// <summary>
        /// Sets the red king image to the red king piece
        /// </summary>
        private Image _redKing = Image.FromFile(@"pics\red_king.png");
        /// <summary>
        /// Sets the black image to the black piece
        /// </summary>
        private Image _black = Image.FromFile(@"pics\black.png");
        /// <summary>
        /// Sets the black king image to the black king piece
        /// </summary>
        private Image _blackKing = Image.FromFile(@"pics\black_king.png");

        /// <summary>
        /// Constructs the game and draws the board
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
            _game = new Game();
            DrawBoard();
        }
        /// <summary>
        /// Draws the game board
        /// </summary>
        private void DrawBoard()
        {
            uxToolStripStatusLabel_Turn.Text = _game.Turn.ToString() + "'s turn";
            uxFlowLayoutPanel_board.Controls.Clear();
            uxFlowLayoutPanel_board.Width = 60 * 8;
            uxFlowLayoutPanel_board.Height = uxFlowLayoutPanel_board.Width + 30;
            for (int r = 1; r < 9; r++)
            {
                int c = 1;
                LinkedListCell<BoardSquare> temp = _game.GetRow(r);
                while ( temp!= null)
                {
                    Label newLabel = new Label();
                    newLabel.Width = 60;
                    newLabel.Height = 60;
                    if ((r + c) % 2 == 0)
                    {
                        newLabel.BackColor = Color.White;
                    }
                    else
                    {
                        newLabel.BackColor = Color.Gray;
                    }
                    if (temp.Data.Color == SquareColor.Red)
                    {
                        newLabel.Image = _red;
                    }
                    else if (temp.Data.Color == SquareColor.Black)
                    {
                        newLabel.Image = _black;
                    }
                    newLabel.Margin = new Padding(0, 0, 0, 0);
                    newLabel.Text = r.ToString() + "," + c.ToString();
                    newLabel.Name = newLabel.Text;
                    newLabel.Click += new EventHandler(BoardSquare_Click);
                    uxFlowLayoutPanel_board.Controls.Add(newLabel);
                    c++;
                    temp = temp.Next;
                }
            }
        }
        /// <summary>
        /// Redraws the board everytime a piece is moved. 
        /// </summary>
        private void RedrawBoard()
        {
            uxToolStripStatusLabel_Turn.Text = _game.Turn.ToString() + "'s turn";
            for (int r = 1; r < 9; r++)
            {
                LinkedListCell<BoardSquare> temp = _game.GetRow(r);
                while (temp != null)
                {
                    Label newLabel = (Label)uxFlowLayoutPanel_board.Controls[temp.Data.Row + "," + temp.Data.Column];
                    if (temp.Data.Selected)
                    {
                        newLabel.BackColor = Color.Aqua;
                    }
                    else
                    {
                        if ((temp.Data.Row + temp.Data.Column) % 2 == 0)
                        {
                            newLabel.BackColor = Color.White;
                        }
                        else
                        {
                            newLabel.BackColor = Color.Gray;
                        }
                    }
                    if (temp.Data.Color == SquareColor.Red)
                    {
                        if (temp.Data.King)
                        {
                            newLabel.Image = _redKing;
                        }
                        else
                        {
                            newLabel.Image = _red;
                        }
                    }
                    else if (temp.Data.Color == SquareColor.Black)
                    {
                        if (temp.Data.King)
                        {
                            newLabel.Image = _blackKing;
                        }
                        else
                        {
                            newLabel.Image = _black;
                        }
                    }
                    else
                    {
                        newLabel.Image = null;
                    }
                    temp = temp.Next;
                }
            }
            uxToolStripStatusLabel_Turn.Text = _game.Turn.ToString() + "'s turn";
        }
        /// <summary>
        /// Resets the game to a new game and redraws the initial board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UxNewGame_Click(object sender, EventArgs e)
        {
            _game = new Game();
            DrawBoard();
        }
        /// <summary>
        /// Moves the piece the in the selected row and column. Determines winner withing moving the selected piece. otherwise the move is invalid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardSquare_Click(object sender, EventArgs e)
        {
            Label newLabel = (Label)sender;
            string[] pieces = newLabel.Name.Split(',');
            int row = int.Parse(pieces[0]);
            int col = int.Parse(pieces[1]);
            if (_game.MoveSelectedPiece(row, col))
            {
                RedrawBoard();
                if (_game.RedCount == 0)
                {
                    MessageBox.Show("Game over, Black wins!!!");
                }
                else if (_game.BlackCount == 0)
                {
                    MessageBox.Show("Game over, Red wins!!!");
                }
            }
            else
            {
                    MessageBox.Show("Invalid Move.");
            }
        }
    }
}