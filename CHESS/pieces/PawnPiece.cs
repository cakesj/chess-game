using CHESS.pieces.Movements.PawnMoves;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal class PawnPiece : Piece
    {
        static int[] PromoteLines = new int[] { 0, chess.GetMaxHeight() };
        internal bool passantable = false;
        internal PawnPiece(PieceColor pieceColor, int col, int row) : base(pieceColor, col, row)
        {
            // need to somehow add attacking from the sides, first double move, and an pessant
            this.portrait = Image.FromFile($"..\\..\\..\\Resources\\{pieceColor}_pawn.png");
            this.UpdateBoard(col, row);
            if (pieceColor == PieceColor.WHT) { AddMovementsWHT(); }
            if (pieceColor == PieceColor.BLK) { AddMovementsBLK(); }
        }

        private void AddMovementsWHT()
        {
            this.movementsDict.Add(PawnSingleMovement.MoveWHT, 2);
            this.movementsDict.Add(PawnDiagonalMovement.MoveWHT, 1);
        }
        private void AddMovementsBLK()
        {
            this.movementsDict.Add(PawnSingleMovement.MoveBLK, 2);
            this.movementsDict.Add(PawnDiagonalMovement.MoveBLK, 1);
        }

        internal override void UpdateBoard(int col, int row)
        {
            // an pessant is important
            this.passantable = false;
            if (Math.Abs(this.placement[1] - row) == 2) { this.passantable = true; }
            // easier than importing math and doing absolute value

            base.UpdateBoard(col, row);

        }


        internal override void MovePiece(int col, int row)
        {
            int[] previous = (int[])this.placement.Clone();
            bool canPawnPessant = chess.GetPiece(col, row) == null;
            bool willPawnPessant = (Math.Abs(col - previous[0])) == 1 && Math.Abs(row - previous[1]) == 1;
            base.MovePiece(col, row);
            chess.Promote(col, row, PawnPiece.PromoteLines);
            NegateDoubles();
            if (!(canPawnPessant && willPawnPessant)) { return; }

            // if here it's pessanting

            chess.RemovePiece(col, previous[1]);
            chess.GetChessSquare(col, previous[1]).UpdateSquareHoldings(null, null);
            chess.ClearCalculatedMovableSpaces();
            string PessantMessage = "Pessanting!";
            //PessantMessage = "ah I see you are a man of culture as well 🤓"; // can make into a comment if it's annoying
            MessageBox.Show(PessantMessage);
        }

        private void NegateDoubles()
        {
            if (this.pieceColor == PieceColor.WHT)
            {
                this.movementsDict[PawnSingleMovement.MoveWHT] = 1;
            }
            else
            {
                this.movementsDict[PawnSingleMovement.MoveBLK] = 1;
            }

        }

        internal override bool IsEatTo(int col, int row)
        {
            int direction = -1;
            if (this.pieceColor == PieceColor.WHT) { direction *= -1; }
            bool inDirection = this.placement[1] - row == direction;
            bool inSide = Math.Abs(this.placement[0] - col) == 1;
            return inDirection && inSide;
        }

    }
}
