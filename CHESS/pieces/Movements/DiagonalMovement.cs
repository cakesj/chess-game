using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements
{
    internal class DiagonalMovement : Movement
    {
        static public DiagonalMovement Move = new DiagonalMovement();

        private static readonly int[][] directions = new int[][]
            {
                new int[] {1, 1},
                new int[] {1, -1},
                new int[] {-1, 1},
                new int[] {-1, -1}
            };

        internal DiagonalMovement() : base() { }
        internal override List<int[]> GetMoves(int[] origin, int repeat)
        {
            return GetMoves(origin, DiagonalMovement.directions, repeat);
        }

    }
}
