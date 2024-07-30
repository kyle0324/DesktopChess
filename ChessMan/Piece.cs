namespace ChessMan
{
    public enum PieceType
    {
        king,
        pawn,
        knight,
        bishop,
        rook,
        queen,
    }

    public class Pos(int y, int x)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
    }

    public abstract class Piece
    {
        public abstract PieceType Type { get; }

        public abstract Player Color { get; }

        public Pos Position { get; set; }

        public bool HasMoved { get; set; } = false;

        public List<Pos> AvailabeMoves { get; set; } = [];

        public bool AvailabeMove(Pos pos)
        {
            foreach(var move in AvailabeMoves)
            {
                if (move.X == pos.X && move.Y == pos.Y)
                {
                    return true;
                }
            }
            return false; 
        }
        
    }

    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.pawn;

        public override Player Color { get; }

        public Pawn(Player color, Pos n)
        {
            Color = color;
            Position = n;
        }

    }

    public class Knight : Piece
    {
        public override PieceType Type => PieceType.knight;

        public override Player Color { get; }

        public Knight(Player color, Pos n)
        {
            Color = color;
            Position = n;
        }
    }

    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.bishop;
        public override Player Color { get; }

        public Bishop(Player color, Pos n)
        {
            Color = color;
            Position = n;
        }
    }

    public class Rook : Piece
    {
        public override PieceType Type => PieceType.rook;

        public override Player Color { get; }

        public Rook(Player color, Pos n)
        {
            Color = color;
            Position = n;
        }
    }

    public class Queen : Piece
    {
        public override PieceType Type => PieceType.queen;
        public override Player Color { get; }

        public Queen(Player color, Pos n)
        {
            Color = color;
            Position = n;
        }
    }

    public class King : Piece
    {
        public override PieceType Type => PieceType.king;
        public override Player Color { get; }

        public King(Player color, Pos n)
        {
            Color = color;
            Position = n;
        }
    }
}
