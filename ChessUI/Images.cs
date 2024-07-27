using System;
using ChessMan;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessUI
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.king, LoadImage("assets/KingW.png") },
            {PieceType.pawn, LoadImage("assets/PawnW.png") },
            {PieceType.knight, LoadImage("assets/KnightW.png") },
            {PieceType.bishop, LoadImage("assets/BishopW.png") },
            {PieceType.rook, LoadImage("assets/RookW.png") },
            {PieceType.queen, LoadImage("assets/QueenW.png") }
        };

        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            {PieceType.king, LoadImage("assets/KingB.png") },
            {PieceType.pawn, LoadImage("assets/PawnB.png") },
            {PieceType.knight, LoadImage("assets/KnightB.png") },
            {PieceType.bishop, LoadImage("assets/BishopB.png") },
            {PieceType.rook, LoadImage("assets/RookB.png") },
            {PieceType.queen, LoadImage("assets/QueenB.png") }
        };
        private static ImageSource LoadImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => whiteSources[type],
                Player.Black => blackSources[type],
                _ => null
            };
        }

        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null || piece.Color == Player.None)
            {
                return null;
            }
            if (piece.Color == Player.White)
            {
                return whiteSources[piece.Type];
            }
            return blackSources[piece.Type];
        }
            

    }
}
