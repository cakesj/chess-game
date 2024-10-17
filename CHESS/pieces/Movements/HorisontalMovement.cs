using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements
{
    internal sealed class HorisontalMovement : Movement
    {
        static public HorisontalMovement Move = new HorisontalMovement();

        private static readonly int[][] directions = new int[][]
            {
                new int[] {0, 1},
                new int[] {0, -1},
                new int[] {1, 0},
                new int[] {-1, 0}
            };

        internal HorisontalMovement() : base() { }
        internal override List<int[]> GetMoves(int[] origin, bool repeat)
        {
            return MoveRegular(origin, HorisontalMovement.directions, repeat);
        }

    }
}
