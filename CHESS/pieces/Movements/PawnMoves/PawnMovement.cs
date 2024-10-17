using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements.PawnMoves
{
    internal abstract class PawnMovement : DirectionalMovement
    {
        static int[][] directions;

        public PawnMovement(int[][] directions, PieceColor pieceColor) : base(directions, pieceColor) { }

        internal override List<int[]> GetMoves(int[] target, bool conditional)
        {            
            // since it's a pawn we all know that repeat is false,
            // so we use further pawn movements to check conditionals
            if (conditional)
            {
                return new List<int[]>() { target };
            }
            return new List<int[]>();
        }
    
    }
}
