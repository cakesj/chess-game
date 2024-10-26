using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements
{
    internal class KnightLikeMovement : Movement
    {
        static internal Movement Move = new KnightLikeMovement();

        private static readonly int[][] directions = new int[][]
            {
                new int[] {2, 1},
                new int[] {2, -1},
                new int[] {-2, 1},
                new int[] {-2, -1},
                new int[] {1, 2},
                new int[] {1, -2},
                new int[] {-1, 2},
                new int[] {-1, -2}
            };

        internal KnightLikeMovement() : base() { }

        internal override List<int[]> GetMoves(int[] origin, int repeat)
        {
            return GetMoves(origin, KnightLikeMovement.directions, repeat);
        }

    }
}

