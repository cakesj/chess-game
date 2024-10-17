using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements.PawnMoves
{
    internal class PawnSingleMovement : PawnMovement
    {
        static int[][] directions = new int[][]
        {
            new int[] { 0, -1 }
        };
        static internal PawnSingleMovement MoveWHT = new PawnSingleMovement(PieceColor.WHT);
        static internal PawnSingleMovement MoveBLK = new PawnSingleMovement(PieceColor.BLK);
        public PawnSingleMovement(PieceColor pieceColor) : base(directions, pieceColor) { }

        internal override List<int[]> GetMoves(int[] origin, bool Useless)
        {
            int[] target = AddCoordinates(origin, movement[0]);
            return base.GetMoves(
                target, 
                IsSquareFree(target)
                );
        }

    }
}
