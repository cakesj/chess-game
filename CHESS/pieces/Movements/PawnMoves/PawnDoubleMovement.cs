using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements.PawnMoves
{
    internal class PawnDoubleMovement : PawnMovement
    {
        static int[][] directions = new int[][]
        {
            new int[] { 0, -2 }
        };

        static internal PawnDoubleMovement MoveWHT = new PawnDoubleMovement(PieceColor.WHT);
        static internal PawnDoubleMovement MoveBLK = new PawnDoubleMovement(PieceColor.BLK);
        public PawnDoubleMovement(PieceColor pieceColor) : base(directions, pieceColor) { }

      internal override List<int[]> GetMoves(int[] origin, bool Useless)
        {
            int[] target = AddCoordinates(origin, movement[0]);
            int[] theSquareBefore = AddCoordinates(origin, 0, movement[0][1] / 2);
            return base.GetMoves(
                target, 
                IsSquareFree(target) && IsSquareFree(theSquareBefore)
                );
        }
    
    }
}
