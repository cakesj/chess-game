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
        internal abstract List<int[]> GetMoves(int[] origin, bool repeat);

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


        protected List<int[]> MoveRegular(int[] origin, int[][] directions, bool reapeat)
        {
            List<int[]> returnedList = new List<int[]>();
            PieceColor pieceColor = chess.GetPiece(origin).pieceColor;
            foreach (int[] direction in directions) 
            {
                int[] target = new int[] { origin[0] + direction[0], origin[1] + direction[1] };
                if (reapeat)
                {
                    returnedList.AddRange(GenerateRepeatedMovesRegular(pieceColor, target, direction));
                    if (returnedList.Count > 0 ) { target = returnedList[returnedList.Count - 1]; }
                    else { target = origin; }
                }
                else
                {
                    int[]? extension;
                    if ((extension = GenerateUnrepeatedMovesRegular(pieceColor, target)) != null) { returnedList.Add(extension); }
                }
                CallPoint(origin, target);
            }

            return returnedList;
        }

        private List<int[]> GenerateRepeatedMovesRegular(PieceColor pieceColor, int[] target, int[] direction)
        {
            List<int[]> ReturnedList = new List<int[]>();
            while (this.IsSquareAvailable(pieceColor, target))
            {
                ReturnedList.Add(target); 
                if (chess.boardSquares[target[0]][target[1]].pieceHeld != null)
                {
                    break;
                }
                target = AddCoordinates(target, direction); 

            }
            return ReturnedList;
        }

        protected int[]? GenerateUnrepeatedMovesRegular(PieceColor pieceColor, int[] target)
        {
            if (this.IsSquareAvailable(pieceColor, target))
            {
                return target;
            }
            return null;
        }

        protected int[] AddCoordinates(int[] origin, int directionCol, int directionRow)
        {
            return new int[] { origin[0] + directionCol, origin[1] + directionRow };
        }

        protected int[] AddCoordinates(int[] origin, int[] direction)
        {
            return new int[] { origin[0] + direction[0], origin[1] + direction[1] };
        }

        private static void CallPoint(int[] origin, int[] target)
        {
            if (chess.DebugMode)
            {
                MessageBox.Show($"({origin[0]}, {origin[1]}) -> ({target[0]}, {target[1]})");
            }
        }


    }
}
