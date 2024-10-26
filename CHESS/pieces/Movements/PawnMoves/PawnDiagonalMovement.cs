namespace CHESS.pieces.Movements.PawnMoves
{
    internal class PawnDiagonalMovement : DirectionalMovement
    {
        static int[][] directions = new int[][]
        {
            new int[] { 1, -1 },
            new int[] { -1, -1 }
        };
        static internal PawnDiagonalMovement MoveWHT = new PawnDiagonalMovement(PieceColor.WHT);
        static internal PawnDiagonalMovement MoveBLK = new PawnDiagonalMovement(PieceColor.BLK);
        public PawnDiagonalMovement(PieceColor pieceColor) : base(directions, pieceColor) { }

        internal override List<int[]> GetMoves(int[] origin, int repeat) // in diagonal pawn movement it should always be 1
        {
            PieceColor color = chess.GetPiece(origin).pieceColor;
            List<int[]> positions = base.GetMoves(origin, repeat);
            foreach (int[] position in new List<int[]>(positions))
            {
                Piece? sider = chess.GetPiece(position[0], origin[1]);
                if (!IsPossibleMove(position, color, sider)) { positions.Remove(position); }
            }
            return positions;
        }

        internal bool IsPossibleMove(int[] target, PieceColor color, Piece? sider)
        {
            if (CanPessant(sider, color)) 
            { return true; }
            if (!IsSquareFree(target) && IsSquareAvailable(color, target))
            { return true; }
            return false;
        }

        internal static bool CanPessant(Piece? onTheSide, PieceColor originColor)
        {
            if (onTheSide is PawnPiece &&
                onTheSide == chess.LastPieceMoved &&
                onTheSide.pieceColor != originColor)
            {
                PawnPiece sider = (PawnPiece)onTheSide;
                return sider.passantable;
            }
            return false;
        }
    }
}
