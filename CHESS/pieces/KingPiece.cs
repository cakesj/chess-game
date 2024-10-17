using CHESS.pieces.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal class KingPiece : Piece
    {
        internal KingPiece(PieceColor pieceColor, int col, int row) : base(pieceColor, col, row)
        {
            this.movementsDict.Add(DiagonalMovement.Move, false);
            this.movementsDict.Add(HorisontalMovement.Move, false);
            this.movementsDict.Add(CastlingMovement.Move, false);
            // need to add castling
            this.portrait = Image.FromFile($"..\\..\\..\\Resources\\{pieceColor}_king.png");
            this.UpdateBoard(col, row);
        }
        internal override void GenerateMovableSpaces()
        {
            foreach (Movement movement in movementsDict.Keys)
            {
                foreach (int[] target in movement.GetMoves(placement, movementsDict[movement]))
                {
                    if (chess.IsSafe(target, this.pieceColor)) { possibleMoves.Add(target); }
                }
                
            }
        }

        internal override void MovePiece(int col, int row)
        {
            if (!hasMoved) { hasMoved = true; }
            int colDistance = this.placement[0] - col;
            int SoonToBePrevious = this.placement[0];
            base.MovePiece(col, row);
            if (Math.Abs(colDistance) <= 1) { return; }
            if (!(this.placement[0] == col) || !(this.placement[1] == row)) { return; }

            // if here it's castling

            Piece Castle;
            if (colDistance > 0) { Castle = chess.GetPiece(0, row); }
            else { Castle = chess.GetPiece(chess.GetMaxWidth(), row); }
            Castle.UpdateBoard((SoonToBePrevious + this.placement[0]) / 2, row);
            string CastleMessege = "Castling!";
            CastleMessege = "everyday im castlin😎"; // can make into a comment if it's annoying
            MessageBox.Show(CastleMessege);
        }
        internal override bool IsEatTo(int col, int row)
        {
            if (Math.Abs(this.placement[0] - col) > 1) { return false; }
            if (Math.Abs(this.placement[1] - row) > 1) { return false; }
            return true;
        }
    }
}
