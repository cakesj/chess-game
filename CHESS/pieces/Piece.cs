using CHESS.pieces.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS.pieces
{
    internal abstract class Piece
    {
        internal Image portrait;
        internal int[] placement = new int[2];
        internal PieceColor pieceColor;
        protected Dictionary<Movement, bool> movementsDict = new Dictionary<Movement, bool>();
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
                if (chess.CanTalk)
                {
                    MessageBox.Show($"place unviable, moving piece to ({this.placement[0]}, {this.placement[0]})");
                }
            }
            this.placement[0] = col;
            this.placement[1] = row;
            this.pieceColor = Piececolor;
            chess.boardPieces.Add(this);
        }


        internal virtual void MovePiece(int col, int row)
        {
            if (!hasMoved) { hasMoved = true; }
            chess.RemovePiece(col, row);
            bool canMove = IsMoveTo(col, row);
            if (canMove)
            {
                this.UpdateBoard(col, row);
                chess.ClearCalculatedMovableSpaces();
            }
            MessageAbout(canMove);
        }


        internal virtual void UpdateBoard(int col, int row)
        {
            chess.boardSquares[placement[0]][placement[1]].UpdateSquareHoldings(null, null);
            placement[0] = col;
            placement[1] = row;
            chess.boardSquares[placement[0]][placement[1]].UpdateSquareHoldings(this, this.portrait);
        }
        protected void MessageAbout(bool canMove)
        {
            if (chess.CanTalk)
            {
                if (canMove) { MessageBox.Show("Move played succesfuly!"); }
                else { MessageBox.Show("Can't move there"); }
            }
        }
        internal virtual void GenerateMovableSpaces()
        {
            foreach (Movement movement in movementsDict.Keys)
            {
                possibleMoves.AddRange(movement.GetMoves(placement, movementsDict[movement]));
            }
        }

        internal void ClearMovableSpaces()
        {
            this.possibleMoves.Clear();
        }

        internal virtual bool IsMoveTo(int col, int row)
        {
            if (possibleMoves.Count == 0)
            {
                this.GenerateMovableSpaces();
            }
            return this.IsInPossibleMoveList(new int[] { col, row });
        }

        internal virtual bool IsEatTo(int col, int row)
        { return IsMoveTo(col, row); }

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
