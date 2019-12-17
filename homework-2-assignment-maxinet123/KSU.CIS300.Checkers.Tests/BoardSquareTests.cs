using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace KSU.CIS300.Checkers.Tests
{
    class BoardSquareTests
    {

        [Test]
        public void BoardSquareTest_King()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("King"));
        }

        [Test]
        public void BoardSquareTest_Color()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Color"));
        }

        [Test]
        public void BoardSquareTest_Selected()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Selected"));
        }

        [Test]
        public void BoardSquareTest_Row()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Row"));
        }

        [Test]
        public void BoardSquareTest_Column()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Column"));
        }

        [Test]
        public void BoardSquareTest_Default()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.AreEqual(1, sq.Row);
            Assert.AreEqual(1, sq.Column);
            Assert.IsFalse(sq.King);
            Assert.IsFalse(sq.Selected);
        }
    }
}
