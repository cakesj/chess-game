using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements.PawnMoves
{
    internal class MayBeUseless : PawnMovement
    {
        static int[][] directions = new int[][]
        {
            new int[] { -1, -1 }
        };
        static internal MayBeUseless MoveWHT = new MayBeUseless(PieceColor.WHT);
        static internal MayBeUseless MoveBLK = new MayBeUseless(PieceColor.BLK);
        public MayBeUseless(PieceColor pieceColor) : base(directions, pieceColor) { }

        internal override List<int[]> GetMoves(int[] origin, bool Useless)
        {
            int[] target = AddCoordinates(origin, movement[0]);
            PieceColor color = chess.GetPiece(origin).pieceColor;
            return base.GetMoves(
                target,
                !IsSquareFree(target) && IsSquareAvailable(color, target) || IsAnPessant(origin)
                );
        }
        private bool IsAnPessant(int[] origin)
        {
            return false;
        }
    }
}

