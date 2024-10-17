using CHESS.pieces.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal class KnightPiece : Piece
    {
        internal KnightPiece(PieceColor pieceColor, int col, int row) : base(pieceColor, col, row)
        {
            this.movementsDict.Add(KnightLikeMovement.Move, false);

            this.portrait = Image.FromFile($"..\\..\\..\\Resources\\{pieceColor}_knight.png");
            this.UpdateBoard(col, row);
        }
    }
}
