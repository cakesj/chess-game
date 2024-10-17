using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CHESS.pieces;

namespace CHESS.board
{
    internal class ChessSquarePictureBox : PictureBox
    {
        private int col { get; set; }
        private int row { get; set; }
        private SquareState state = SquareState.Inactive;
        internal Piece? pieceHeld;
        internal ChessSquarePictureBox(int colIndex, int rowIndex) : base()
        {
            col = colIndex;
            row = rowIndex;

            Location = new Point(50 + colIndex * 50, 100 + rowIndex * 50);
            BackColor = CalcSquareColor(colIndex, rowIndex);
            BorderStyle = BorderStyle.Fixed3D;
            Name = $"Square_{colIndex}_{rowIndex}";
            // add padding?
            Size = new Size(50, 50);
            Click += (sender, e) => SquareClick(colIndex, rowIndex);
            SizeMode = PictureBoxSizeMode.StretchImage;

        }

        static private Color CalcSquareColor(int colIndex, int rowIndex)
        {
            if (true)
            {
                if ((colIndex + rowIndex) % 2 == 0) return Color.FromArgb(255, 65, 43, 5);
                return Color.FromArgb(255, 246, 228, 196);
            }
            else
            {
                if ((colIndex + rowIndex) % 2 == 0) return Color.Black;
                return Color.White;
            }
        }

        internal void SquareClick(int colIndex, int rowIndex)
        {
            if (this.state == SquareState.Highlighted)
            {
                chess.LastPieceClicked.MovePiece(this.col, this.row);
                chess.ClearHighlightsOnBoard();
            }
            else
            {
                chess.ClearHighlightsOnBoard();
                if (this.state == SquareState.Active) { this.state = SquareState.Inactive; }
                else if (this.state == SquareState.Inactive && pieceHeld != null)
                {
                    chess.LastPieceClicked = pieceHeld;
                    this.state = SquareState.Active;
                    this.HighlightPiecesPossibilities(pieceHeld);
                }
            }




        }

        internal void HighlightPiecesPossibilities(Piece piece)
        {
            piece.GenerateMovableSpaces();
            foreach (int[] SquareCoordinates in piece.possibleMoves)
            {
                chess.boardSquares[SquareCoordinates[0]][SquareCoordinates[1]].HighLightSquare();
            }
        }
        internal void HighLightSquare()
        {
            this.state = SquareState.Highlighted;
            this.BackColor = Color.Blue;
        }
        internal void UnhighLightSquare()
        {
            this.state = SquareState.Inactive;
            this.BackColor = CalcSquareColor(this.col, this.row);
        }

        internal void UpdateSquareHoldings(Piece? piece, Image? portrait)
        {
            this.pieceHeld = piece;
            this.Image = portrait;
        }
    }
}
