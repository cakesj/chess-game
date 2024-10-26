using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements.PawnMoves
{
    internal class PawnSingleMovement : DirectionalMovement
    {
        protected int[][] movement;

        static int[][] directions = new int[][]
        {
            new int[] { 0, -1 }
        };
        static internal PawnSingleMovement MoveWHT = new PawnSingleMovement(PieceColor.WHT);
        static internal PawnSingleMovement MoveBLK = new PawnSingleMovement(PieceColor.BLK);
        public PawnSingleMovement(PieceColor pieceColor) : base(directions, pieceColor) { }

        internal override List<int[]> GetMoves(int[] origin, int repeat)
        {
            List<int[]> positions = base.GetMoves(origin, repeat);
            int LastItemIndex = positions.Count - 1;
            if (LastItemIndex >= 0 && !IsSquareFree(positions[LastItemIndex]))
            {
                positions.Remove(positions[LastItemIndex]);
            }
            return positions;
        }

    }
}


