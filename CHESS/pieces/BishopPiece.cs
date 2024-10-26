using CHESS.pieces.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal class BishopPiece : Piece
    {
        internal BishopPiece(PieceColor pieceColor, int col, int row) : base(pieceColor, col, row)
        {
            this.movementsDict.Add(DiagonalMovement.Move, -1);

            this.portrait = Image.FromFile($"..\\..\\..\\Resources\\{pieceColor}_bishop.png");
            this.UpdateBoard(col, row);
        }
    }
}
