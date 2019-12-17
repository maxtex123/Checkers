/*
 * BoardSquare.cs
 * Author: Maxine Teixeira
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.CIS300.Checkers
{
    public class BoardSquare
    {
        /// <summary>
        /// Passes row and col into the the setter and getter
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public BoardSquare(int row, int col)
        {
            Row = row;
            Column = col;
        }
        /// <summary>
        /// Indicates if the square piece has been promoted to king. It contains a default getter/setter and is initialized to false.
        /// </summary>
        public bool King { get; set; } = false;
        /// <summary>
        /// Indicates if this square contains a colored piece or if it has none. It contains a default getter/setter.
        /// </summary>
        public SquareColor Color { get; set; }
        /// <summary>
        /// Indicates if this square is selected. It contains a default getter/setter and should be initialized to false.
        /// </summary>
        public bool Selected { get; set; } = false;
        /// <summary>
        /// The row location on the board. It contains a default getter but no setter
        /// </summary>
        public int Row { get; }
        /// <summary>
        /// The column location on the board. It contains a default getter but no setter.
        /// </summary>
        public int Column { get; }
    }
    /// <summary>
    /// Used to indicated the color of a piece.
    /// </summary>
    public enum SquareColor
    {
        Red,
        Black,
        None
    }
}