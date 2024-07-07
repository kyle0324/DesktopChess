using System;
using System.Collections.Generic;

namespace ChessMan
{

    public class Space
    {
        public Piece Piece { get; set; } = null;
        public bool IsOccupied()
        {
            return Piece != null;
        }

        public List<Piece> UnderThreat { get; set; } = [];

        public Space() { }



    }
    public class Board
    {
        public Space[,] Spaces { get; set; }

        private void AddThreatsKing(Piece piece)
        {

        }

        private void AddThreatsQueen(Piece piece)
        {
        }

        private void AddThreatsBishop(Piece piece) { }

        private void AddThreatsKnight(Piece piece) { }

        private void AddThreatsRook(Piece piece) { }

        private void AddThreatsPawn(Piece piece) { }


        //update the peices available moves with this. Threats include occupied ally spaces
        private void AddThreats(Piece piece)
        {
            //use this to check what piece it is

        }

        private void RemoveThreatsKing(Piece piece) { }
        private void RemoveThreatsQueen(Piece piece) { }
        private void RemoveThreatsBishop(Piece piece)
        {

        }

        private void RemoveThreatsKnight(Piece piece)
        {

        }

        private void RemoveThreatsRook(Piece piece)
        {

        }
        private void RemoveThreatsPawn(Piece piece) { }

        private void RemoveThreats(Piece piece) //remove availabe moves as well
        {
            //use this to check what piece it is

        }

        private void SetBoard()
        {
            //initalize pieces
            Spaces[0, 0].Piece = new Rook(Player.Black, new Pos(0, 0));
            Spaces[0, 1].Piece = new Knight(Player.Black, new Pos(0, 1));
            Spaces[0, 2].Piece = new Bishop(Player.Black, new Pos(0, 2));
            Spaces[0, 3].Piece = new King(Player.Black, new Pos(0,3));
            Spaces[0, 4].Piece = new Queen(Player.Black, new Pos(0, 4));
            Spaces[0, 5].Piece = new Bishop(Player.Black, new Pos(0, 5));
            Spaces[0, 6].Piece = new Knight(Player.Black, new Pos(0, 6));
            Spaces[0, 7].Piece = new Rook(Player.Black, new Pos(0, 7));
            Spaces[1, 0].Piece = new Pawn(Player.Black, new Pos(1, 0));
            Spaces[1, 1].Piece = new Pawn(Player.Black, new Pos(1, 1));
            Spaces[1, 2].Piece = new Pawn(Player.Black, new Pos(1, 2));
            Spaces[1, 3].Piece = new Pawn(Player.Black, new Pos(1, 3));
            Spaces[1, 4].Piece = new Pawn(Player.Black, new Pos(1, 4));
            Spaces[1, 5].Piece = new Pawn(Player.Black, new Pos(1, 5));
            Spaces[1, 6].Piece = new Pawn(Player.Black, new Pos(1, 6));
            Spaces[1, 7].Piece = new Pawn(Player.Black, new Pos(1, 7));

            Spaces[6, 0].Piece = new Pawn(Player.White, new Pos(6, 0));
            Spaces[6, 1].Piece = new Pawn(Player.White, new Pos(6, 1));
            Spaces[6, 2].Piece = new Pawn(Player.White, new Pos(6, 2));
            Spaces[6, 3].Piece = new Pawn(Player.White, new Pos(6, 3));
            Spaces[6, 4].Piece = new Pawn(Player.White, new Pos(6, 4));
            Spaces[6, 5].Piece = new Pawn(Player.White, new Pos(6, 5));
            Spaces[6, 6].Piece = new Pawn(Player.White, new Pos(6, 6));
            Spaces[6, 7].Piece = new Pawn(Player.White, new Pos(6, 7));
            Spaces[7, 0].Piece = new Rook(Player.White, new Pos(7, 0));
            Spaces[7, 1].Piece = new Knight(Player.White, new Pos(7, 1));
            Spaces[7, 2].Piece = new Bishop(Player.White, new Pos(7, 2));
            Spaces[7, 3].Piece = new King(Player.White, new Pos(7, 3));
            Spaces[7, 4].Piece = new Queen(Player.White, new Pos(7, 4));
            Spaces[7, 5].Piece = new Bishop(Player.White, new Pos(7, 5));
            Spaces[7, 6].Piece = new Knight(Player.White, new Pos(0, 6));
            Spaces[7, 7].Piece = new Rook(Player.White, new Pos(0, 7));

            //set up spaces with their canMove lists

            //first pawns
            AddThreats(Spaces[1, 0].Piece);
            AddThreats(Spaces[1, 1].Piece);
            AddThreats(Spaces[1, 2].Piece);
            AddThreats(Spaces[1, 3].Piece);
            AddThreats(Spaces[1, 4].Piece);
            AddThreats(Spaces[1, 5].Piece);
            AddThreats(Spaces[1, 6].Piece);
            AddThreats(Spaces[1, 7].Piece);
            AddThreats(Spaces[6, 0].Piece);
            AddThreats(Spaces[6, 1].Piece);
            AddThreats(Spaces[6, 2].Piece);
            AddThreats(Spaces[6, 3].Piece);
            AddThreats(Spaces[6, 4].Piece);
            AddThreats(Spaces[6, 5].Piece);
            AddThreats(Spaces[6, 6].Piece);
            AddThreats(Spaces[6, 7].Piece);

            //now knights
            AddThreats(Spaces[0, 1].Piece);
            AddThreats(Spaces[0, 6].Piece);
            AddThreats(Spaces[7, 1].Piece);
            AddThreats(Spaces[7, 6].Piece);

            //now rooks
            AddThreats(Spaces[0, 0].Piece);
            AddThreats(Spaces[0, 7].Piece);
            AddThreats(Spaces[7, 0].Piece);
            AddThreats(Spaces[7, 7].Piece);

            //now Bishops
            AddThreats(Spaces[0, 2].Piece);
            AddThreats(Spaces[0, 5].Piece);
            AddThreats(Spaces[7, 2].Piece);
            AddThreats(Spaces[7, 5].Piece);

            //now kings
            AddThreats(Spaces[0, 3].Piece);           
            AddThreats(Spaces[7, 3].Piece);

            //now queens
            AddThreats(Spaces[0, 4].Piece);
            AddThreats(Spaces[7, 4].Piece);



        }

        public Board()
        {
            Spaces = new Space[8, 8];
            SetBoard();
        }


    }
}
