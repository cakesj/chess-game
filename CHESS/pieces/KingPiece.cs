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
            this.movementsDict.Add(DiagonalMovement.Move, 1);
            this.movementsDict.Add(HorisontalMovement.Move, 1);
            this.movementsDict.Add(CastlingMovement.Move, 1);
            this.portrait = Image.FromFile($"..\\..\\..\\Resources\\{pieceColor}_king.png");
            this.UpdateBoard(col, row);
        }


        internal override void MovePiece(int col, int row)
        {
            if (!hasMoved) { hasMoved = true; }
            int colDistance = this.placement[0] - col;
            int SoonToBePrevious = this.placement[0];
            base.MovePiece(col, row);
            if (Math.Abs(colDistance) <= 1) { return; }

            // if here it's castling

            Piece Castle;
            if (colDistance > 0) { Castle = chess.GetPiece(0, row); }
            else { Castle = chess.GetPiece(chess.GetMaxWidth(), row); }
            Castle.UpdateBoard((SoonToBePrevious + this.placement[0]) / 2, row);
            chess.ClearCalculatedMovableSpaces();
            string Castlemessage = "Castling!";
            //Castlemessage = "everyday im castlin😎"; // can make into a comment if it's annoying
            MessageBox.Show(Castlemessage);
        }

        internal override bool IsEatTo(int col, int row)
        {
            if (Math.Abs(this.placement[0] - col) > 1) { return false; }
            if (Math.Abs(this.placement[1] - row) > 1) { return false; }
            return true;
        }
    }
}
