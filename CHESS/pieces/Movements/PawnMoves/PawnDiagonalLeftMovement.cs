namespace CHESS.pieces.Movements.PawnMoves
{
    internal class PawnDiagonalLeftMovement : PawnMovement
    {
        static int[][] directions = new int[][]
        {
            new int[] { 1, -1 },
            new int[] { -1, -1 }
        };
        static internal PawnDiagonalLeftMovement MoveWHT = new PawnDiagonalLeftMovement(PieceColor.WHT);
        static internal PawnDiagonalLeftMovement MoveBLK = new PawnDiagonalLeftMovement(PieceColor.BLK);
        public PawnDiagonalLeftMovement(PieceColor pieceColor) : base(directions, pieceColor) { }

        internal override List<int[]> GetMoves(int[] origin, bool Useless)
        {
            return new List<int[]> {
                this.MoveToTheRight(origin),
                this.MoveToTheLeft(origin)
            }.Where(item => item != null).ToList();
        }

        internal int[]? MoveToTheRight(int[] origin)
        {
            int[] target = AddCoordinates(origin, movement[0]);
            PieceColor color = chess.GetPiece(origin).pieceColor;
            Piece? onTheRight = chess.GetPiece(target[0], origin[1]);
            if (CanPessant(onTheRight, color))
            {
                return target;
            }
            if (!IsSquareFree(target) && IsSquareAvailable(color, target)) {
                return target;
            }
            return null;
        }
        internal int[]? MoveToTheLeft(int[] origin)
        {
            int[] target = AddCoordinates(origin, movement[1]);
            PieceColor color = chess.GetPiece(origin).pieceColor;
            Piece? onTheLeft = chess.GetPiece(target[0], origin[1]);
            if (CanPessant(onTheLeft, color))
            {
                return target;
            }
            if (!IsSquareFree(target) && IsSquareAvailable(color, target)) {
                return target;
            }
            return null;
        }

        internal static bool CanPessant(Piece? OnTheSide, PieceColor originColor)
        {
            if (OnTheSide != null && 
                OnTheSide.GetType() == typeof(PawnPiece) && 
                OnTheSide.pieceColor != originColor)
            {
                PawnPiece Sider = (PawnPiece)OnTheSide;
                return Sider.passantable;
            }
            return false;
        }
    }
}
