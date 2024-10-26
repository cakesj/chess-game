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

        internal void SquareClick(int colIndex, int rowIndex) { SquareClick(); } // may delete cuz it's not needed
        internal void SquareClick()
        {
            SquareState CurrentState = this.state;
            chess.ClearHighlightsOnBoard();
            switch (CurrentState)
            {
                case (SquareState.Active):
                    this.state = SquareState.Inactive;
                    break;
                case (SquareState.Highlighted):
                    MovePieceFromSquare();
                    break;
                case SquareState.Inactive:
                    ActivatePieceFromSquare();
                    break;
            }
        }
        internal void MovePieceFromSquare()
        {
            chess.LastPieceMoved = chess.LastPieceClicked;
            chess.LastPieceClicked.MovePiece(this.col, this.row);
            chess.TurnColor = chess.TurnColor.Oppisite();
            chess.ClearCalculatedMovableSpaces();
        }
        internal void ActivatePieceFromSquare()
        {
            //Piece held = this.pieceHeld;
            //int[] KingCoordinates = chess.GetKing(held.pieceColor).placement;
            if (chess.IsTurn(this.pieceHeld))
            {
                chess.LastPieceClicked = pieceHeld;
                this.state = SquareState.Active;
                this.HighlightPiecesPossibilities(pieceHeld);
            }
        }

        internal void HighlightPiecesPossibilities(Piece piece)
        {
            piece.GenerateMovableSpaces();
            foreach (int[] SquareCoordinates in piece.possibleMoves)
            {
                chess.GetChessSquare(SquareCoordinates).HighLightSquare();
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

        internal Piece? TempUpdateSquareHoldings(Piece? piece)
        {
            Piece? evacuated = this.pieceHeld;
            this.pieceHeld = piece;
            return evacuated;
        }
    }
}
