using CHESS.board;
using CHESS.pieces;
using System.Linq.Expressions;

namespace CHESS
{
    public partial class chess : Form
    {
        internal static PickPromotionForm promotionPicker = new PickPromotionForm();
        internal static readonly int BOARDHEIGHT = 8;
        internal static readonly int BOARDWIDTH = 8;
        internal static readonly bool CanTalk = false;
        internal static readonly bool DebugMode = false;
        internal static List<Piece> boardPieces = new List<Piece>();
        internal static List<Piece> AwayPieces = new List<Piece>(); // do I need it? better be safe
        internal static ChessSquarePictureBox[][] boardSquares;
        internal static Piece? LastPieceClicked;
        internal static Piece? LastPieceMoved;
        internal static PieceColor TurnColor = PieceColor.WHT;

        public chess()
        {
            //chess._checkDesigns(); // made to check designs of the pieces


            InitializeComponents();

            chess.InitializeGame();


        }


        // priority list:
        // do a winning screen (check if everybody can't move)
        // add a game log to that?
        // add dead pieces to the outside of the board
        // add AI??? the next link seems like it could help make:
        // https://www.c-sharpcorner.com/article/C-Sharp-artificial-intelligence-ai-programming-a-basic-object/




        private static void _checkDesigns()
        {
            string strong;
            string[] stringer = { "pawn", "rook", "knight", "bishop", "queen", "king" };
            for (int x = 0; x < stringer.Length; x++)
            {
                string str = stringer[x];
                for (int i = 0; i < 4; i++)
                {
                    if (i / 2 >= 1)
                    {
                        strong = "WHT";
                    }
                    else
                    {
                        strong = "BLK";
                    }
                    chess.GetChessSquare(x, i).Image = Image.FromFile($"..\\..\\..\\Resources\\{strong}_{str}.png");
                }
            }
        }
        private static void InitializeGame()
        {
            Piece PawnWHT;
            Piece PawnBLK;
            for (int a = 0; a <= chess.GetMaxWidth(); a++)
            {
                PawnWHT = new PawnPiece(PieceColor.WHT, a, 6);
                PawnBLK = new PawnPiece(PieceColor.BLK, a, 1);
            }
            Piece WHTrook1 = new RookPiece(PieceColor.WHT, 7, 7);
            Piece WHTrook2 = new RookPiece(PieceColor.WHT, 0, 7);
            Piece BLKrook1 = new RookPiece(PieceColor.BLK, 7, 0);
            Piece BLKrook2 = new RookPiece(PieceColor.BLK, 0, 0);
            Piece WHTknight1 = new KnightPiece(PieceColor.WHT, 6, 7);
            Piece WHTknight2 = new KnightPiece(PieceColor.WHT, 1, 7);
            Piece BLKknight1 = new KnightPiece(PieceColor.BLK, 6, 0);
            Piece BLKknight2 = new KnightPiece(PieceColor.BLK, 1, 0);
            Piece WHTbishop1 = new BishopPiece(PieceColor.WHT, 5, 7);
            Piece WHTbishop2 = new BishopPiece(PieceColor.WHT, 2, 7);
            Piece BLKbishop1 = new BishopPiece(PieceColor.BLK, 5, 0);
            Piece BLKbishop2 = new BishopPiece(PieceColor.BLK, 2, 0);
            Piece WHTqueen = new QueenPiece(PieceColor.WHT, 4, 7);
            Piece BLKqueen = new QueenPiece(PieceColor.BLK, 4, 0);
            Piece WHTking = new KingPiece(PieceColor.WHT, 3, 7);
            Piece BLKking = new KingPiece(PieceColor.BLK, 3, 0);
        }

        internal static void ClearCalculatedMovableSpaces()
        {
            foreach (Piece piece in boardPieces)
            {
                piece.ClearMovableSpaces();
            }
        }
        internal static void ClearHighlightsOnBoard()
        {
            foreach (ChessSquarePictureBox[] SquareRow in chess.boardSquares)
            {
                foreach (ChessSquarePictureBox Square in SquareRow)
                {
                    Square.UnhighLightSquare();
                }
            }
        }
        internal static int[] FindValidPlaceOnBoard()
        {
            int col = 0;
            int row = 0;
            while (chess.GetPiece(col, row) != null)
            {
                col++;
                if (col > chess.GetMaxHeight())
                {
                    col = 0;
                    row++;
                }
                if (row > chess.GetMaxWidth())
                {
                    throw new Exception("there is no place on the board!");
                }
            }
            return new int[] { col, row };
        }

        internal static void Promote(int col, int row, int[] promotionFiles)
        {
            if (!promotionFiles.Contains(row)) { return; }
            PieceColor previous = chess.GetPiece(col, row).pieceColor;
            Piece newer;

            promotionPicker.ShowDialog(new string[] { "bishop", "knight", "rook", "queen" }, previous);
            string piecePick = promotionPicker.UserResponse;

            chess.RemovePiece(col, row);
            chess.GetChessSquare(col, row).UpdateSquareHoldings(null, null);
            switch (piecePick)
            {
                case "bishop":
                    newer = new BishopPiece(previous, col, row);
                    break;
                case "knight":
                    newer = new KnightPiece(previous, col, row);
                    break;
                case "rook":
                    newer = new RookPiece(previous, col, row);
                    break;
                default:
                    newer = new QueenPiece(previous, col, row); // queens are usually the most commonly chosen one so if there was an error it gives you a queen
                    break;
            }
        }
        internal static KingPiece[] GetKings()
        {
            KingPiece[] Kings = new KingPiece[] { GetKing(PieceColor.WHT), GetKing(PieceColor.BLK) };
            return Kings.Where(item => item != null).ToArray();
        }

        internal static KingPiece? GetKing(PieceColor searchedColor)
        {
            foreach (Piece piece in chess.boardPieces)
            {
                if (piece is KingPiece && piece.pieceColor == searchedColor)
                {
                    return (KingPiece)piece;
                }
            }
            return null;
        }
        internal static Piece? GetPiece(int col, int row)
        {
            if (chess.IsOnBoard(col, row)) { return chess.GetChessSquare(col, row).pieceHeld; }
            else { return null; }
        }
        internal static Piece? GetPiece(int[] directions)
        {
            return chess.GetChessSquare(directions[0], directions[1]).pieceHeld;
        }
        internal static ChessSquarePictureBox GetChessSquare(int col, int row)
        {
            return chess.boardSquares[col][row];
        }
        internal static ChessSquarePictureBox GetChessSquare(int[] directions)
        {
            return chess.boardSquares[directions[0]][directions[1]];
        }
        internal static void RemovePiece(int col, int row)
        {
            Piece piece = chess.GetPiece(col, row);
            if (!(piece != null)) { return; }
            piece.placement = new int[2];
            chess.boardPieces.Remove(piece);
            chess.AwayPieces.Add(piece);
        }

        internal static bool IsTurn(Piece? piece)
        {
            bool IsPiece = piece != null;
            if (chess.DebugMode) { return IsPiece; }
            return IsPiece && piece.pieceColor == chess.TurnColor;
        }
        internal static bool IsSafe(int row, int col, PieceColor SafeColor)
        {
            foreach (Piece piece in chess.boardPieces)
            {
                if (piece.pieceColor == SafeColor) { continue; }
                if (piece.IsEatTo(row, col)) { return false; } 
            }
            return true;
        }
        internal static bool IsSafe(int[] target, PieceColor SafeColor)
        { return chess.IsSafe(target[0], target[1], SafeColor); }


        internal static bool IsCheck(PieceColor searchedColor) // need to update
        {
            KingPiece king = chess.GetKing(searchedColor);
            return !chess.IsSafe(king.placement, king.pieceColor);
        }



        internal static bool IsCheckmate(PieceColor searchedColor)
        {
            foreach (Piece piece in chess.boardPieces)
            {
                if (piece.pieceColor != searchedColor) { continue; }
                piece.ClearMovableSpaces();
                piece.GenerateMovableSpaces();
                if (piece.possibleMoves.Count != 0) { return false; }
            }
            return true;
        }
        internal static bool IsOnBoard(int col, int row)
        {
            return 0 <= col && col <= chess.GetMaxHeight() && 0 <= row && row <= chess.GetMaxWidth();
        }
        internal static int GetMaxHeight()
        {
            return chess.BOARDHEIGHT - 1;
        }
        internal static int GetMaxWidth()
        {
            return chess.BOARDWIDTH - 1;
        }
        internal static void MessageAbout(string Message)
        {
            if (chess.CanTalk)
            {
                MessageBox.Show(Message);
                return;
            }
        }

    }
}