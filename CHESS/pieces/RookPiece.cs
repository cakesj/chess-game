using CHESS.pieces.Movements;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal class RookPiece : Piece
    {
        internal RookPiece(PieceColor pieceColor, int col, int row) : base(pieceColor, col, row)
        {
            this.movementsDict.Add(HorisontalMovement.Move, -1);

            this.portrait = Image.FromFile($"..\\..\\..\\Resources\\{pieceColor}_rook.png");
            this.UpdateBoard(col, row);
        }




    }
}
