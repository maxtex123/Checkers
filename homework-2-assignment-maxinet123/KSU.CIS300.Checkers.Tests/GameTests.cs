using System;
using System.Collections.Generic;
using System.Text;
using KSU.CIS300.Checkers;
using NUnit.Framework;

namespace KSU.CIS300.Checkers.Tests
{
    public class GameTests
    {
        Game g;


        [SetUp]
        public void Setup()
        {
            g = new Game();
        }

        [Test]
        [Category("AConstructor/CreateBoard")]
        public void GameTestA1_CreateBasicGame()
        {
            Assert.AreEqual(12, g.RedCount);
            Assert.AreEqual(12, g.BlackCount);
            Assert.AreEqual(SquareColor.Black, g.Turn);
            Assert.AreEqual(null, g.SelectedPiece);
        }

        [Test]
        [Category("BGetRow")]
        public void GameTestB1_CheckAllRows()
        {
            for (int row = 1; row <= 8; row++)
            {
                LinkedListCell<BoardSquare> boardRow = g.GetRow(row);
                Assert.IsNotNull(boardRow, "Row " + row + " did not exist.");

                int col = 1;
                while (boardRow != null)
                {
                    Assert.IsNotNull(boardRow.Data, "Board square " + row + ", " + col + " not initialized.");
                    Assert.AreEqual(row, boardRow.Data.Row, "Board row in wrong order.");
                    Assert.AreEqual(col, boardRow.Data.Column, "Board column in wrong order.");
                    if (row < 4 && col % 2 != row % 2)
                    {
                        Assert.AreEqual(SquareColor.Red, boardRow.Data.Color);
                    }
                    else if (row > 5 && col % 2 != row % 2)
                    {
                        Assert.AreEqual(SquareColor.Black, boardRow.Data.Color);
                    }
                    else
                    {
                        Assert.AreEqual(SquareColor.None, boardRow.Data.Color);
                    }
                    boardRow = boardRow.Next;
                    col++;
                }
            }
        }

        #region SelectSquare
        private void SelectHelper(int row, int col, SquareColor color)
        {
            BoardSquare square = g.SelectSquare(row, col);
            Assert.AreEqual(row, square.Row, "Wrong row selected");
            Assert.AreEqual(col, square.Column, "Wrong column selected");
            Assert.AreEqual(color, square.Color, "Wrong color selected");
            if (color == SquareColor.None)
            {
                Assert.IsFalse(square.Selected, "Empty square selected property should be false");
            }
            else
            {
                if (color == g.Turn)
                {
                    Assert.IsTrue(square.Selected, "Piece should be selected");
                    Assert.AreEqual(square, g.SelectedPiece);
                }
                else
                {
                    Assert.IsFalse(square.Selected, "Piece should not be selected (wrong turn)");
                    if (g.SelectedPiece != null)
                    {
                        Assert.IsFalse(g.SelectedPiece.Selected);
                    }
                }
            }
        }

        [Test]
        [Category("CSelectSquare")]
        public void GameTestC1_SelectSquare_FirstEmpty()
        {
            SelectHelper(1, 1, SquareColor.None);
        }

        [Test]
        [Category("CSelectSquare")]
        public void GameTestC2_SelectSquare_FirstBlack()
        {
            SelectHelper(6, 1, SquareColor.Black);
        }

        [Test]
        [Category("CSelectSquare")]
        public void GameTestC3_SelectSquare_MiddleEmpty()
        {
            SelectHelper(5, 5, SquareColor.None);
        }

        [Test]
        [Category("CSelectSquare")]
        public void GameTestC4_SelectSquare_LastRed()
        {
            SelectHelper(3, 8, SquareColor.Red);
        }

        [Test]
        [Category("CSelectSquare")]
        public void GameTestC5_SelectSquare_SwitchPiece()
        {
            SelectHelper(6, 5, SquareColor.Black);
            BoardSquare sq = g.SelectedPiece;
            SelectHelper(7, 8, SquareColor.Black);
            Assert.IsFalse(sq.Selected, "Old piece was not de-selected");
        }

        [Test]
        [Category("CSelectSquare")]
        public void GameTestC6_SelectSquare_SwitchPieceTurn()
        {
            SelectHelper(6, 5, SquareColor.Black);
            BoardSquare sq = g.SelectedPiece;
            g.Turn = SquareColor.Red;
            SelectHelper(3, 4, SquareColor.Red);
            Assert.IsFalse(sq.Selected, "Old piece was not de-selected");
        }
        #endregion

        #region CheckCapture
        [Test]
        [Category("DCheckCapture")]
        public void GameTestD1_CheckCaptureFalse()
        {
            Assert.IsFalse(g.CheckCapture(g.GetRow(5), 5, SquareColor.Red));
        }

        [Test]
        [Category("DCheckCapture")]
        public void GameTestD2_CheckCaptureTrue()
        {
            Assert.IsTrue(g.CheckCapture(g.GetRow(3), 8, SquareColor.Red));
        }

        [Test]
        [Category("DCheckCapture")]
        public void GameTestD3_CheckCaptureOutFalse()
        {
            BoardSquare sq;
            Assert.IsFalse(g.CheckCapture(g.GetRow(5), 8, SquareColor.Red, out sq));
            Assert.AreEqual(SquareColor.None, sq.Color);
            Assert.AreEqual(5, sq.Row);
            Assert.AreEqual(8, sq.Column);
        }

        [Test]
        [Category("DCheckCapture")]
        public void GameTestD4_CheckCaptureOutTrue()
        {
            BoardSquare sq;
            Assert.IsTrue(g.CheckCapture(g.GetRow(1), 8, SquareColor.Red, out sq));
            Assert.AreEqual(SquareColor.Red, sq.Color);
            Assert.AreEqual(1, sq.Row);
            Assert.AreEqual(8, sq.Column);
        }
        #endregion

        #region Check Jumps

        [Test]
        [Category("ECheckJump")]
        public void GameTestE1_CheckJump_BadEnemyRow()
        {
            Assert.IsFalse(g.CheckJump(0, 5, 5, 5, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE2_CheckJump_BadEnemyRow()
        {
            Assert.IsFalse(g.CheckJump(9, 5, 5, 5, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE3_CheckJump_BadEnemyCol()
        {
            Assert.IsFalse(g.CheckJump(5, 5, 0, 5, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE4_CheckJump_BadEnemyCol()
        {
            Assert.IsFalse(g.CheckJump(5, 5, 9, 5, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE5_CheckJump_BadTargetRow()
        {
            Assert.IsFalse(g.CheckJump(1, 0, 5, 5, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE6_CheckJump_BadTargetRow()
        {
            Assert.IsFalse(g.CheckJump(1, 9, 5, 5, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE7_CheckJump_BadTargetCol()
        {
            Assert.IsFalse(g.CheckJump(5, 5, 1, 9, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE8_CheckJump_BadTargetCol()
        {
            Assert.IsFalse(g.CheckJump(5, 5, 1, 9, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE9_CheckJump_NoEnemy()
        {
            Assert.IsFalse(g.CheckJump(5, 4, 6, 5, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestE9_CheckJump_NonEmptyTarget()
        {
            // note that this jump would be illegal anyways since its jumping too many squares
            Assert.IsFalse(g.CheckJump(3, 1, 4, 2, SquareColor.Red));
        }

        [Test]
        [Category("ECheckJump")]
        public void GameTestEE10_CheckJump()
        {
            // note that this is a mocked jump (not real)
            Assert.IsTrue(g.CheckJump(2, 1, 5, 5, SquareColor.Red));
        }

        #endregion
        
        #region GetJumpSquare

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF1_GetJumpSquare_BlackUpLeft()
        {
            int row, col;
            int tr = 4;
            int tc = 5;

            int sr = 6;
            int sc = 7;
            BoardSquare target = new BoardSquare(tr, tc);
            g.SelectSquare(sr, sc);


            Assert.IsTrue(g.GetJumpSquare(target, SquareColor.Red, out row, out col));
            Assert.AreEqual(tr + 1, row, "Wrong Row");
            Assert.AreEqual(tc + 1, col, "Wrong Column");
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF2_GetJumpSquare_BlackUpRight()
        {
            int row, col;
            int tr = 4;
            int tc = 7;

            int sr = 6;
            int sc = 5;
            BoardSquare target = new BoardSquare(tr, tc);
            g.SelectSquare(sr, sc);


            Assert.IsTrue(g.GetJumpSquare(target, SquareColor.Red, out row, out col));
            Assert.AreEqual(tr + 1, row, "Wrong Row");
            Assert.AreEqual(tc - 1, col, "Wrong Column");
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF3_GetJumpSquare_RedDownRight()
        {
            int row, col;
            int tr = 5;
            int tc = 8;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            g.Turn = SquareColor.Red;
            g.SelectSquare(sr, sc);


            Assert.IsTrue(g.GetJumpSquare(target, SquareColor.Black, out row, out col));
            Assert.AreEqual(tr - 1, row, "Wrong Row");
            Assert.AreEqual(tc - 1, col, "Wrong Column");
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF4_GetJumpSquare_RedDownLeft()
        {
            int row, col;
            int tr = 5;
            int tc = 4;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            g.Turn = SquareColor.Red;
            g.SelectSquare(sr, sc);


            Assert.IsTrue(g.GetJumpSquare(target, SquareColor.Black, out row, out col));
            Assert.AreEqual(tr - 1, row, "Wrong Row");
            Assert.AreEqual(tc + 1, col, "Wrong Column");
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF5_GetJumpSquare_InvalidRow()
        {
            int row, col;
            int tr = 6;
            int tc = 4;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            g.Turn = SquareColor.Red;
            g.SelectSquare(sr, sc);


            Assert.IsFalse(g.GetJumpSquare(target, SquareColor.Black, out row, out col));
            Assert.AreEqual(-1, row);
            Assert.AreEqual(-1, col);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF5_GetJumpSquare_InvalidCol()
        {
            int row, col;
            int tr = 5;
            int tc = 3;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            g.Turn = SquareColor.Red;
            g.SelectSquare(sr, sc);


            Assert.IsFalse(g.GetJumpSquare(target, SquareColor.Black, out row, out col));
            Assert.AreEqual(-1, row);
            Assert.AreEqual(-1, col);
        }

        #endregion

        #region CanMove


        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG1_CanMove_BlackInvalidRow()
        {
            bool jumpMore;
            g.SelectSquare(6, 5);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(7,4), SquareColor.Red, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG2_CanMove_BlackInvalidRow()
        {
            bool jumpMore;
            g.SelectSquare(6, 5);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(7, 6), SquareColor.Red, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG3_CanMove_BlackInvalidCol()
        {
            bool jumpMore;
            g.SelectSquare(6, 5);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(7, 5), SquareColor.Red, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG4_CanMove_BlackInvalidCol()
        {
            bool jumpMore;
            g.SelectSquare(6, 5);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(5, 5), SquareColor.Red, out jumpMore));
            Assert.IsFalse(jumpMore);
        }


        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG5_CanMove_RedInvalidRow()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(2, 3), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG6_CanMove_RedInvalidRow()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(2, 5), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG7_CanMove_RedInvalidCol()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(2, 4), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }


        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG8_CanMove_RedInvalidCol()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            Assert.IsFalse(g.CanMove(false, new BoardSquare(4, 4), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }


        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG9_CanMove_KingInvalidCol()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsFalse(g.CanMove(false, new BoardSquare(4, 4), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG10_CanMove_KingInvalidCol()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsFalse(g.CanMove(false, new BoardSquare(2, 4), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG11_CanMove_KingInvalidRow()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsFalse(g.CanMove(false, new BoardSquare(3, 3), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG12_CanMove_KingInvalidRow()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsFalse(g.CanMove(false, new BoardSquare(3, 5), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG13_CanMove_KingUpLeft()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsTrue(g.CanMove(false, new BoardSquare(2, 3), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG14_CanMove_KingUpRight()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsTrue(g.CanMove(false, new BoardSquare(2, 5), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG15_CanMove_KingDownLeft()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsTrue(g.CanMove(false, new BoardSquare(4, 3), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG16_CanMove_KingDownRight()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            g.SelectedPiece.King = true;
            Assert.IsTrue(g.CanMove(false, new BoardSquare(4, 5), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }        

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG16_CanMove_RedDownLeft()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            Assert.IsTrue(g.CanMove(false, new BoardSquare(4, 3), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG17_CanMove_RedDownRight()
        {
            bool jumpMore;
            g.Turn = SquareColor.Red;
            g.SelectSquare(3, 4);
            Assert.IsTrue(g.CanMove(false, new BoardSquare(4, 5), SquareColor.Black, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG18_CanMove_BlackUpLeft()
        {
            bool jumpMore;
            g.SelectSquare(6, 5);
            Assert.IsTrue(g.CanMove(false, new BoardSquare(5, 4), SquareColor.Red, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG19_CanMove_BlackUpRight()
        {
            bool jumpMore;
            g.SelectSquare(6, 5);
            Assert.IsTrue(g.CanMove(false, new BoardSquare(5, 6), SquareColor.Red, out jumpMore));
            Assert.IsFalse(jumpMore);
        }

        #endregion

    }
}
