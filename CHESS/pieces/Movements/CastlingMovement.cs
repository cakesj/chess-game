﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces.Movements
{
    // everyday IM castli'n
    internal class CastlingMovement : Movement
    {
        static public CastlingMovement Move = new CastlingMovement();

        private static readonly int[][] directionLeft = new int[][] { new int[] { -2, 0 }, };
        private static readonly int[][] directionRight = new int[][] { new int[] {2, 0}, }; 

        internal CastlingMovement() : base() { }
        internal override List<int[]> GetMoves(int[] origin, bool repeat)
        {
            List<int[]> returner = new List<int[]>();
            if (chess.GetPiece(origin).hasMoved) { return returner; }
            
            Piece? leftMost = chess.GetPiece(0, origin[1]);
            Piece? rightMost = chess.GetPiece(chess.GetMaxWidth(), origin[1]);
            if (leftMost != null && !leftMost.hasMoved) { returner.AddRange(MoveRegularLeft(origin)); }
            if (rightMost != null && !rightMost.hasMoved) { returner.AddRange(MoveRegularRight(origin)); }

            return returner;
        }
        private List<int[]> MoveRegularLeft(int[] origin)
        {
            PieceColor SafeColor = chess.GetPiece(origin).pieceColor;
            for (int i = origin[0] - 1; i > 0; i--)
            {
                if (!base.IsSquareFree(i, origin[1])) { MessegeSquareDeclare("unfree"); return new List<int[]>(); }
                if (!chess.IsSafe(i, origin[1], SafeColor)) { MessegeSquareDeclare("unsafe");  return new List<int[]>(); }
            }
            return MoveRegular(origin, CastlingMovement.directionLeft, false);
        }
        private List<int[]> MoveRegularRight(int[] origin)
        {
            PieceColor SafeColor = chess.GetPiece(origin).pieceColor;
            for (int i = origin[0] + 1; i < chess.GetMaxWidth(); i++)
            {
                if (!base.IsSquareFree(i, origin[1])) { MessegeSquareDeclare("unfree"); return new List<int[]>(); }
                if (!chess.IsSafe(i, origin[1], SafeColor)) { MessegeSquareDeclare("unsafe"); return new List<int[]>(); }
            }
            return MoveRegular(origin, CastlingMovement.directionRight, false);
        }

        private static void MessegeSquareDeclare(string message)
        {
            if (chess.CanTalk)
            {
                MessageBox.Show(message);
            }
        }
    }
}