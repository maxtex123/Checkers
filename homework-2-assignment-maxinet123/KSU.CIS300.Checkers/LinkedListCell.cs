/* LinkedListCell.cs
 * Josh Weese
 */
using System;
using System.Collections.Generic;

namespace KSU.CIS300.Checkers
{
    /// <summary>
    /// Represents a cell in a linked list
    /// </summary>
    /// <typeparam name="T">The type of data in the list</typeparam>
    public class LinkedListCell<T>
    {
        /// <summary>
        /// Gets or sets the data in the cell
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the next cell in the list
        /// </summary>
        public LinkedListCell<T> Next { get; set; }

    }
}