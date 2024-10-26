using CHESS.board;
using CHESS.pieces.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHESS.pieces
{
    internal abstract class Piece
    {
        internal Image portrait;
        internal int[] placement = new int[2];
        internal PieceColor pieceColor;
        protected Dictionary<Movement, int> movementsDict = new Dictionary<Movement, int>();
        internal List<int[]> possibleMoves = new List<int[]>();
        internal bool hasMoved = false;


        internal Piece(PieceColor piececolor) : this(piececolor, chess.FindValidPlaceOnBoard()) { }
        internal Piece(PieceColor piececolor, int[] placement) : this(piececolor, placement[0], placement[1]) { }
        internal Piece(PieceColor Piececolor, int col, int row)
        {
            if (chess.GetPiece(col, row) != null)
            {
                int[] newPlace = chess.FindValidPlaceOnBoard();
                col = newPlace[0];
                row = newPlace[1];
                chess.MessageAbout($"place unviable, moving piece to ({this.placement[0]}, {this.placement[0]})");
            }
            this.placement[0] = col;
            this.placement[1] = row;
            this.pieceColor = Piececolor;
            chess.boardPieces.Add(this);
        }


        internal virtual void MovePiece(int col, int row)
        {
            if (!hasMoved) { hasMoved = true; }
            bool canMove = IsMoveTo(col, row);
            string message = "Can't move there";
            if (canMove)
            {
                chess.RemovePiece(col, row);
                this.UpdateBoard(col, row);
                chess.ClearCalculatedMovableSpaces();
                message = "Move played succesfuly!";
            }
            chess.MessageAbout(message);
            this.CheckChecks();
        }

        internal void CheckChecks()
        {
            PieceColor enemy = this.pieceColor.Oppisite();
            if (chess.IsCheckmate(enemy))
            {
                if (chess.IsCheck(enemy))
                {
                    MessageBox.Show("CHECKMATE!!!");
                    return;
                }
                MessageBox.Show("stalemate");
                return;
            }
            if (chess.IsCheck(enemy))
            {
                MessageBox.Show("check!");
            }
        } // could it be rearranged for better?


        internal virtual void UpdateBoard(int col, int row)
        {
            chess.GetChessSquare(this.placement).UpdateSquareHoldings(null, null);
            placement[0] = col;
            placement[1] = row;
            chess.GetChessSquare(this.placement).UpdateSquareHoldings(this, this.portrait);
        }


        internal virtual bool IsMoveTo(int col, int row)
        {
            if (this.possibleMoves.Count == 0)
            {
                this.GenerateMovableSpaces();
            }
            return this.IsInPossibleMoveList(new int[] { col, row });
        }
        internal virtual bool IsEatTo(int col, int row)
        {
            this.ClearMovableSpaces();
            this.GenerateMovableSpaces(false);
            return this.IsInPossibleMoveList(new int[] { col, row });
        }

        internal virtual void GenerateMovableSpaces() { GenerateMovableSpaces(true); }
        internal virtual void GenerateMovableSpaces(bool secondary)
        {
            foreach (Movement movement in movementsDict.Keys)
            {
                List<int[]> movements = movement.GetMoves(placement, movementsDict[movement]);
                if (secondary) { this.AddOnlyValids(movements); }
                else  { this.possibleMoves.AddRange(movements); }
            }
        }
        internal void ClearMovableSpaces()
        {
            this.possibleMoves.Clear();
        }
        protected void AddOnlyValids(List<int[]> movements) // need to simplefy
        {
            int[] origin = (int[])this.placement.Clone();
            chess.GetChessSquare(origin).TempUpdateSquareHoldings(null);

            foreach (int[] movement in new List<int[]>(movements)) { AddIfValid(movement); }

            chess.GetChessSquare(origin).TempUpdateSquareHoldings(this);
            this.placement = origin;
        }

        protected void AddIfValid(int[] movement)
        {
            this.placement = movement;
            Piece? taken = chess.GetChessSquare(movement).TempUpdateSquareHoldings(this);
            if (!chess.IsCheck(this.pieceColor)) { this.possibleMoves.Add(movement); }
            chess.GetChessSquare(movement).TempUpdateSquareHoldings(taken);
        }
        

        private bool IsInPossibleMoveList(int[] target)
        {
            foreach (int[] place in this.possibleMoves)
            {
                if (place[0] == target[0] && place[1] == target[1]) { return true; }
            }
            return false;
        }



    }
}
