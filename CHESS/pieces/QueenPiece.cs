using CHESS.pieces.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal class QueenPiece : Piece
    {
        internal QueenPiece(PieceColor pieceColor, int col, int row) : base(pieceColor, col, row)
        {
            this.movementsDict.Add(DiagonalMovement.Move, -1);
            this.movementsDict.Add(HorisontalMovement.Move, -1);

            this.portrait = Image.FromFile($"..\\..\\..\\Resources\\{pieceColor}_queen.png");
            this.UpdateBoard(col, row);
        }
    }
}
