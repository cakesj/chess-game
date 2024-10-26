using CHESS.board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements
{
    internal abstract class Movement
    {
        internal abstract List<int[]> GetMoves(int[] origin, int repeat);

        protected bool IsSquareAvailable(int col, int row, PieceColor pieceColor)
        {
            if (!(chess.IsOnBoard(col, row))) { return false; }

            Piece? moved = chess.GetPiece(col, row);

            if (moved != null && pieceColor == moved.pieceColor) { return false; } 
            
            return true;
        }
        protected bool IsSquareAvailable(PieceColor pieceColor, int[] target)
        {
            return IsSquareAvailable(target[0], target[1], pieceColor);
        }

        protected bool IsSquareFree(int row, int col)
        {
            if (!(chess.IsOnBoard(col, row))) { return false; }
            return chess.GetPiece(row, col) == null;
        }
        protected bool IsSquareFree(int[] target)
        {
            return IsSquareFree(target[0], target[1]);
        }


        protected List<int[]> GetMoves(int[] origin, int[][] directions, int repetitions)
        {
            List<int[]> returnedList = new List<int[]>();
            if (this is KnightLikeMovement) { 
           // MessageBox.Show($"{origin[0]}a{origin[1]}\t{chess.GetPiece(origin)}");
            }
            PieceColor pieceColor = chess.GetPiece(origin).pieceColor;
            foreach (int[] direction in directions) 
            {
                int[] target = new int[] { origin[0] + direction[0], origin[1] + direction[1] };
                returnedList.AddRange(GenerateMoves(pieceColor, target, direction, repetitions));
                if (returnedList.Count > 0 ) { target = returnedList[returnedList.Count - 1]; }
                else { target = origin; }
                CallPoint(origin, target);
            }

            return returnedList;
        }

        private List<int[]> GenerateMoves(PieceColor pieceColor, int[] target, int[] direction, int repetitions)
        {
            List<int[]> ReturnedList = new List<int[]>();
            int count = repetitions;
            while (this.IsSquareAvailable(pieceColor, target) && count != 0)
            {
                ReturnedList.Add(target); 
                if (chess.GetPiece(target) != null)
                {
                    break;
                }
                target = AddCoordinates(target, direction);
                count--;
            }
            return ReturnedList;
        }

        protected int[] AddCoordinates(int[] origin, int directionCol, int directionRow)
        {
            return new int[] { origin[0] + directionCol, origin[1] + directionRow };
        }
        protected int[] AddCoordinates(int[] origin, int[] direction)
        {
            return new int[] { origin[0] + direction[0], origin[1] + direction[1] };
        }

        protected static void CallPoint(int[] origin, int[] target)
        {
            if (chess.DebugMode && chess.CanTalk)
            {
                MessageBox.Show($"({origin[0]}, {origin[1]}) -> ({target[0]}, {target[1]})");
            }
        }

    }
}
