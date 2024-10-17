using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal enum PieceColor
    {
        BLK,
        WHT
    }

    internal static class PieceColorExtensions
    {
        public static PieceColor Oppisite(this PieceColor pieceColor)
        {
            return pieceColor switch
            {
                PieceColor.BLK => PieceColor.WHT,
                PieceColor.WHT => PieceColor.BLK,
            };
        }
    }
}
