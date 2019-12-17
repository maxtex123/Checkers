/*
 * Game.cs
 * Author: Maxine Teixeira
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.CIS300.Checkers
{
   
    public class Game
    {
        /// <summary>
        /// Dictionary that stores the represetnation of the checker board
        /// </summary>
        private Dictionary<int, LinkedListCell<BoardSquare>> _board = null;
        /// <summary>
        /// Keeps track of how many red pieces are on the board
        /// </summary>
        public int RedCount { get; private set; }
        /// <summary>
        /// Keeps track of how many black pieces are on the board
        /// </summary>
        public int BlackCount { get; private set; }
        /// <summary>
        /// Keeps track of which pieces are selectedmby the user on the board
        /// </summary>
        public BoardSquare SelectedPiece { get; set; }
        /// <summary>
        /// Keeps track of whose turn it is
        /// </summary>
        public SquareColor Turn { get; set; }
        /// <summary>
        /// Sets up the game board. Puts all the pieces in their correct places to start.
        /// </summary>
        private void CreateBoard()
        {
            _board = new Dictionary<int, LinkedListCell<BoardSquare>>();
            for (int r = 8; r > 0; r--)
            {
                _board[r] = null;
                for (int c = 8; c > 0; c--)
                {
                    BoardSquare b  = new BoardSquare(r, c);
                    if (r < 4 && r % 2 != c % 2)
                    {
                        b.Color = SquareColor.Red;
                        RedCount++;
                    }
                    else if (r > 5 && r % 2 != c % 2)
                    {
                        b.Color = SquareColor.Black;
                        BlackCount++;
                    }
                    else
                    {
                        b.Color = SquareColor.None;
                    }
                    LinkedListCell<BoardSquare> cell = new LinkedListCell<BoardSquare>();
                    cell.Data = b;
                    cell.Next =  _board[r]; // board is null causing error
                    _board[r] = cell;
                    //cell = cell.Next;
                }
            }
        }
        /// <summary>
        /// Constructor that initializes the turn to start with the black piece.
        /// </summary>
        public Game()
        {
            Turn = SquareColor.Black;
            CreateBoard();
        }
        /// <summary>
        /// Finds the first BoardSquare if it exists
        /// </summary>
        /// <param name="row">row number</param>
        /// <returns>null unless first BoardSquare exists then returns linkedlistcell</returns>
        public LinkedListCell<BoardSquare> GetRow(int row)
        {
            LinkedListCell<BoardSquare> c;
            if (_board.TryGetValue(row, out c))
            {
                return c;
            }
            return null;
        }
        /// <summary>
        /// Finds the BoardSquare that corresponds to the row and column. If the square is the same color as the turn then the piece is stored in the SelectedPiece. If users wants to select a different valid square the originally selected one is unselected.
        /// </summary>
        /// <param name="row">row number</param>
        /// <param name="col">column number</param>
        /// <returns>The data in that LinkedListCell</returns>
        public BoardSquare SelectSquare(int row, int col) //private
        {
            LinkedListCell<BoardSquare> temp = _board[row];
            while (temp != null && temp.Data.Column != col)
            {
                temp = temp.Next;
            }
            if (temp.Data.Color != SquareColor.None && temp.Data.Color == Turn)
            {
                if (SelectedPiece != null)
                {
                    SelectedPiece.Selected = false;
                }
                SelectedPiece = temp.Data;
                SelectedPiece.Selected = true;
            }
            return temp.Data;
        }
        /// <summary>
        /// Finds the square in a certain column and checks if the color matches the one they are looking for.
        /// </summary>
        /// <param name="cell">LinkedListCell being checked</param>
        /// <param name="targetCol">Column user is aiming for</param>
        /// <param name="targetColor">Color user is aiming for</param>
        /// <param name="result">Square in that given column</param>
        /// <returns>false if target color doesnt match color in square</returns>
        public bool CheckCapture(LinkedListCell<BoardSquare> cell, int targetCol, SquareColor targetColor, out BoardSquare result) //Private
        {
            while (cell != null && cell.Data.Column != targetCol)
            {
                cell = cell.Next;
            }
            result = cell.Data;
            if (cell.Data.Color == targetColor)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Finds the square in a certain column and checks if the color matches the one they are looking for.
        /// </summary>
        /// <param name="cell">LinkedListCell being checked</param>
        /// <param name="targetCol">Column user is aiming for</param>
        /// <param name="targetColor">Color user is aiming for</param>
        /// <returns>false if target color doesnt match color in square</returns>
        public bool CheckCapture(LinkedListCell<BoardSquare> cell, int targetCol, SquareColor targetColor) //private
        {
            while (cell != null && cell.Data.Column != targetCol)
            {
                cell = cell.Next;
            }
            if (cell.Data.Color == targetColor)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemyRow">number of the row the enemy piece is located</param>
        /// <param name="targetRow">number of the row the user wants to land in</param>
        /// <param name="enemyCol">number of the column the enemy piece is located</param>
        /// <param name="targetCol">number of the column the user wants to land in</param>
        /// <param name="enemy">color of the enemies checker</param>
        /// <returns>false if there are no possible capture</returns>
        public bool CheckJump(int enemyRow, int targetRow, int enemyCol, int targetCol, SquareColor enemy) //private
        {
            if (enemyRow < 1 || enemyRow > 8 || targetRow < 1 || targetRow > 8 || enemyCol < 1 || enemyCol > 8 || targetCol < 1 || targetCol > 8)
            {
                return false;
            }
            if (CheckCapture(_board[enemyRow], enemyCol, enemy) && CheckCapture(_board[targetRow], targetCol, SquareColor.None)) //Potential error
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks for all jumps possible by kings or normal checkers
        /// </summary>
        /// <param name="current">location of all the pieces on the board</param>
        /// <param name="enemy">color of the opponents checkers</param>
        /// <returns>The result of the CheckJump method for the different oppositions available</returns>
        private bool CheckAnyJump(BoardSquare current, SquareColor enemy)
        {
            //if check king
            if (SelectedPiece.King)
            {
                return CheckJump(current.Row - 1, current.Row - 2, current.Column - 1, current.Column - 2, enemy) || CheckJump(current.Row - 1, current.Row - 2, current.Column + 1, current.Column + 2, enemy) || CheckJump(current.Row + 1, current.Row + 2, current.Column - 1, current.Column - 2, enemy) || CheckJump(current.Row + 1, current.Row + 2, current.Column + 1, current.Column + 2, enemy);
            }
            //if check if red
            if (enemy == SquareColor.Red)
            {
                return CheckJump(current.Row - 1, current.Row - 2, current.Column - 1, current.Column - 2, enemy) || CheckJump(current.Row - 1, current.Row - 2, current.Column + 1, current.Column + 2, enemy);
            }
            //else if black
            else
            {
                return CheckJump(current.Row + 1, current.Row + 2, current.Column - 1, current.Column - 2, enemy) || CheckJump(current.Row + 1, current.Row + 2, current.Column + 1, current.Column + 2, enemy);
            }
        }
        /// <summary>
        /// Gets the square that can be jumped.
        /// </summary>
        /// <param name="target">Square that the player wants to land in</param>
        /// <param name="enemy">color of the opponents checkers</param>
        /// <param name="row">row number</param>
        /// <param name="col">column number</param>
        /// <returns>true unless there is no square to jump to</returns>
        public bool GetJumpSquare(BoardSquare target, SquareColor enemy, out int row, out int col)
        {
            if (SelectedPiece.King)
            {
                if (SelectedPiece.Row - target.Row == 2)
                {
                    row = SelectedPiece.Row - 1;
                }
                else
                {
                    row = SelectedPiece.Row + 1;
                }
            }
            else if (Math.Abs(SelectedPiece.Row - target.Row) == 2)
            {
                if (enemy == SquareColor.Red)
                {
                    row = SelectedPiece.Row - 1;
                }
                else
                {
                    row = SelectedPiece.Row + 1;
                }
            }
            else
            {
                row = -1;
                col = -1;
                return false;
            }
            if (SelectedPiece.Column - target.Column == 2)
            {
                col = SelectedPiece.Column - 1;

            }
            else if (SelectedPiece.Column - target.Column == -2)
            {
                col = SelectedPiece.Column + 1;
            }
            else
            {
                row = -1;
                col = -1;
                return false;
            }
            return true;
        }
        /// <summary>
        /// This function makes the piece jump if the given target is a valid square to jump to. 
        /// </summary>
        /// <param name="current">where the piece is currently located</param>
        /// <param name="target">square where the piece will move</param>
        /// <param name="enemy">color of the opponents checkers</param>
        /// <param name="jumpMore">if there is another jump possible</param>
        /// <returns></returns>
        private bool Jump(BoardSquare current, BoardSquare target, SquareColor enemy, out bool jumpMore)
        {
            if (target.Row < 1 || target.Row > 8 || target.Column < 1|| target.Column > 8)
            {
                jumpMore = false;
                return false;
            }
            if (!GetJumpSquare(target, enemy, out int enemyRow, out int enemyCol))
            {
                jumpMore = false;
                return false;
            }
            if (enemyRow < 1 || enemyRow > 8 || enemyCol < 1 || enemyCol > 8)
            {
                jumpMore = false;
                return false;
            }
            if (CheckCapture(_board[enemyRow], enemyCol, enemy, out BoardSquare skip) && CheckCapture(_board[target.Row], target.Column, SquareColor.None, out BoardSquare land))
            {
                if (enemy == SquareColor.Red)
                {
                    RedCount--;
                }
                else
                {
                    BlackCount--;
                }
                skip.Color = SquareColor.None;
                land.King = current.King;
                jumpMore = CheckAnyJump(land, enemy);
                return true;
            }
            jumpMore = false;
            return false;
        }
        /// <summary>
        /// Based on the given target, enemy, and whether or not to force a jump, this method checks to see if a move can be made. 
        /// </summary>
        /// <param name="forceJump">forces a jump if needed</param>
        /// <param name="targetSquare">target square where the piece will move</param>
        /// <param name="enemy">color of the opponents checkers</param>
        /// <param name="jumpMore">if there is another jump possible</param>
        /// <returns></returns>
        public bool CanMove(bool forceJump, BoardSquare targetSquare, SquareColor enemy, out bool jumpMore)
        {
            if (forceJump && Jump(SelectedPiece, targetSquare, enemy, out jumpMore))
            {
                return true;
            }
            jumpMore = false;
            int dir;
            if (SelectedPiece.Color == SquareColor.Red)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            if (!forceJump && (targetSquare.Row - SelectedPiece.Row == dir))
            {
                if(Math.Abs(SelectedPiece.Column - targetSquare.Column) == 1)
                {
                    return true;
                }
            }
            if (!forceJump && SelectedPiece.King)
            {
                if (Math.Abs(targetSquare.Row - SelectedPiece.Row) == 1)
                {
                    if (Math.Abs(SelectedPiece.Column - targetSquare.Column) == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Attemps to make a legal move in the target row/column.
        /// </summary>
        /// <param name="targetRow">Row user should move to</param>
        /// <param name="targetCol">Column user should move to</param>
        /// <returns>returns true if a move is made</returns>
        public bool MoveSelectedPiece(int targetRow, int targetCol)
        {
            BoardSquare targetSquare = SelectSquare(targetRow, targetCol);
            if (targetSquare == null || SelectedPiece == null)
            {
                return false;
            }
            if (targetSquare == SelectedPiece)
            {
                return true;
            }
            if (targetSquare.Color == SquareColor.None)
            {
                SquareColor enemy;
                if (SelectedPiece.Color == SquareColor.Red)
                {
                    enemy = SquareColor.Black;
                }
                else
                {
                    enemy = SquareColor.Red;
                }
                bool forceJump = CheckAnyJump(SelectedPiece, enemy);
                if (!forceJump)
                {
                    for (int r = 1; r < 9; r++)
                    {
                        LinkedListCell<BoardSquare> temp = _board[r];
                        while (temp != null)
                        {
                            if (temp.Data.Color == Turn)
                            {

                                if (CheckAnyJump(temp.Data, enemy))
                                {
                                    return false;
                                }
                            }
                            temp = temp.Next;
                        }
                    }
                }
                if (CanMove(forceJump, targetSquare, enemy, out bool jumpMore))
                {
                    targetSquare.Color = SelectedPiece.Color;
                    if (targetSquare.Row == 1 && SelectedPiece.Color == SquareColor.Black || targetSquare.Row == 8 && SelectedPiece.Color == SquareColor.Red)
                    {
                        targetSquare.King = true;
                    }
                    else
                    {
                        targetSquare.King = SelectedPiece.King;
                    }
                    if (!jumpMore)
                    {
                        if (SelectedPiece.Color == SquareColor.Red)
                        {
                            Turn = SquareColor.Black;
                        }
                        else
                        {
                            Turn = SquareColor.Red;
                        }
                    }
                    SelectedPiece.Selected = false;
                    SelectedPiece.Color = SquareColor.None;
                    return true;
                }   
            }
            return false;
        }
    }
}