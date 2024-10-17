using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements
{
    internal abstract class DirectionalMovement : Movement
    {
        protected int[][] movement;

        static internal DirectionalMovement MoveWHT;
        static internal DirectionalMovement MoveBLK;

        protected DirectionalMovement(int[][] directions, PieceColor pieceColor) : base()
        {
            SetDirection(directions, pieceColor);
        }

        private void SetDirection(int[][] directions, PieceColor pieceColor)
        {
            this.movement = directions;
            if (pieceColor == PieceColor.BLK) { this.movement = Oppisite(directions); }
        }

        protected static int[][] Oppisite(int[][] originalDirection)
        {
            int[][] oppisiteDirection = new int[originalDirection.Length][];
            for (int i = 0; i < originalDirection.Length; i++)
            {
                oppisiteDirection[i] = new int[2] { originalDirection[i][0] * -1, originalDirection[i][1] * -1 };
            }
            return oppisiteDirection;
        }
        internal override List<int[]> GetMoves(int[] origin, bool repeat)
        {
            return MoveRegular(origin, this.movement, repeat);
        }


    }
}
