﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ChessMan
{
    public enum Gamestate
    {
        White,
        Black,
        Finish
    }

    public enum MoveType
    {
        Norm,
        CastleQ,
        Castle,
        Possante
    }

    public class Space
    {
        public Piece Piece { get; set; }
        public bool IsOccupied()
        {
            return Piece != null;
        }

        public List<Piece> UnderThreat { get; set; } = [];

        public bool ThreatByOppColor(Player player)
        {
            foreach (Piece piece in UnderThreat)
            {
                if (piece.Color != player)
                {
                    return true;
                }
            }
            return false;
        }

        public Space() { }

    }
    public class Board
    {
        public Space[,] Spaces { get; set; } = new Space[8, 8];

        public bool Wcheck = false;
        public bool Bcheck = false;
        public int WMovesNum = 0;
        public int BMovesNum = 0;
        public Player turn = Player.White;

        public Pos Wking { get; set; } = new Pos(7, 3);
        public Pos Bking { get; set; } = new Pos(0, 3);

        public void MarkAvailableMove(Piece piece, int y, int x)
        {
            if (Spaces[y, x].IsOccupied() && Spaces[y, x].Piece.Color == piece.Color) { }
            else if (piece.Type == PieceType.pawn)
            {
                if (Math.Abs(y - piece.Position.Y) == 2 && Spaces[y, x].Piece == null) //2 spaces foward
                {
                    piece.AvailabeMoves.Add(new Pos(y, x));
                }
                else if (Spaces[y, x].Piece != null && (Math.Abs(y - piece.Position.Y) + (Math.Abs(x - piece.Position.X)) == 2) && Math.Abs(y - piece.Position.Y) == 1) //diagnal take
                {
                    piece.AvailabeMoves.Add(new Pos(y, x));
                }
                else if (Spaces[y, x].Piece == null && (Math.Abs(y - piece.Position.Y) + (Math.Abs(x - piece.Position.X)) == 1))
                {
                    piece.AvailabeMoves.Add(new Pos(y, x));
                }
                else { } //dont do anything
            }
            else if (piece.Type == PieceType.king && Spaces[y, x].ThreatByOppColor(piece.Color))
            {
                //nothing because we can't put ourselves in check
            }
            else
            {
                piece.AvailabeMoves.Add(new Pos(y, x));
                if (Spaces[y, x].IsOccupied() && Spaces[y, x].Piece.Type == PieceType.king) //check for check
                {
                    Wcheck = Spaces[y, x].Piece.Color == Player.White;
                    Bcheck = Spaces[y, x].Piece.Color == Player.Black;
                }
            }
        }

        private void AddThreatsKing(Piece piece)
        {
            int x = piece.Position.X;
            int y = piece.Position.Y;
            if (x > 0)
            {
                if(y > 0)
                {
                    Spaces[y-1, x-1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, y - 1, x - 1);
                }
                if(y < 7)
                {
                    Spaces[y+1, x-1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, y + 1, x - 1);
                }
                Spaces[y, x-1].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y, x - 1);
            }
            if(x < 7)
            {
                if(y>0)
                {
                    Spaces[y-1, x+1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, y - 1, x + 1);
                }
                if(y < 7)
                {
                    Spaces[y + 1, x + 1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, y+1, x+1);
                }
                Spaces[y, x + 1].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y, x + 1);
            }
            if(y < 7)
            {
                Spaces[y + 1, x].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y + 1, x);
            }
            if(y > 0)
            {
                Spaces[y-1, x].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y-1, x);
            }
            
        }
        private void AddThreatsDiag(Piece piece)
        {
            int x = piece.Position.X;
            int y = piece.Position.Y;
            int tempx = 0;
            int tempy = 0;

            if (x < 7 && y < 7) //check down right
            {
                tempx = x + 1;
                tempy = y + 1;
                while (tempx < 8 && tempy < 8)
                {
                    Spaces[tempy, tempx].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, tempy, tempx);

                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx++;
                    tempy++;
                }
            }
            if (x < 7 && y > 0) //check up right
            {
                tempx = x + 1;
                tempy = y - 1;
                while (tempx < 8 && tempy >= 0)
                {
                    Spaces[tempy, tempx].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, tempy, tempx);

                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx++;
                    tempy--;
                }
            }
            if (x > 0 && y > 0) //check up left
            {
                tempx = x - 1;
                tempy = y - 1;
                while (tempx >= 0 && tempy >= 0)
                {
                    Spaces[tempy, tempx].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, tempy, tempx);

                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx--;
                    tempy--;
                }
            }
            if (x > 0 && y < 7)//check down left
            {
                tempx = x - 1;
                tempy = y + 1;
                while (tempx >= 0 && tempy < 8)
                {
                    Spaces[tempy, tempx].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, tempy, tempx);

                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx--;
                    tempy++;
                }
            }
        }

        private void AddThreatsDpad(Piece piece)
        {
            int x = piece.Position.X;
            int y = piece.Position.Y;
            int tempx = 0;
            int tempy = 0;

            if (x > 0) //check left
            {
                tempx = x - 1;
                while (tempx >= 0)
                {
                    Spaces[y, tempx].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, y, tempx);
                    if (Spaces[y, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx--;
                }
            }
            if (x < 7) //check right
            {
                tempx = x + 1;
                while (tempx < 8)
                {
                    Spaces[y, tempx].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, y, tempx);
                    if (Spaces[y, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx++;
                }
            }
            if (y > 0) // check up
            {
                tempy = y - 1;
                while (tempy >= 0)
                {
                    Spaces[tempy, x].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, tempy, x);
                    if (Spaces[tempy, x].IsOccupied())
                    {
                        break;
                    }
                    tempy--;
                }
            }
            if (y < 7) //check down
            {
                tempy = y + 1;
                while (tempy < 8)
                {
                    Spaces[tempy, x].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, tempy, x);
                    if (Spaces[tempy, x].IsOccupied())
                    {
                        break;
                    }
                    tempy++;
                }
            }
        }
        private void AddThreatsQueen(Piece piece)
        {
            AddThreatsDiag(piece);
            AddThreatsDpad(piece);
        }

        private void AddThreatsBishop(Piece piece) 
        {
            AddThreatsDiag(piece);
        }

        private void AddThreatsKnight(Piece piece) 
        { 
            int x = piece.Position.X;
            int y = piece.Position.Y;

            if(x > 1 && y > 0)
            {
                Spaces[y-1, x-2].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y - 1, x - 2);
            }
            if(x > 1 && y < 7)
            {
                Spaces[y+1, x-2].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y+1, x - 2);
            }
            if(x > 0 && y > 1)
            {
                Spaces[y - 2, x - 1].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y-2, x - 1);
            }
            if(x>0 && y < 6)
            {
                Spaces[y+2, x-1].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y+2, x-1);
            }
            if(x < 6 && y > 0)
            {
                Spaces[y - 1, x + 2].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y - 1, x + 2);
            }
            if(x < 6 && y < 7)
            {
                Spaces[y + 1, x + 2].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y + 1, x + 2);
            }
            if(x < 7 && y > 1)
            {
                Spaces[y-2, x+1].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y-2,x+1);
            }
            if(x < 7 && y < 6)
            {
                Spaces[y + 2, x + 1].UnderThreat.Add(piece);
                MarkAvailableMove(piece, y+2, x+1);
            }
        }

        private void AddThreatsRook(Piece piece) 
        {
            AddThreatsDpad(piece);
        }

        private void AddThreatsPawn(Piece piece)
        {
            if(piece.Color == Player.White)
            {
                if(piece.Position.Y == 0)
                {
                    return;
                }
                Spaces[piece.Position.Y - 1, piece.Position.X].UnderThreat.Add(piece);
                MarkAvailableMove(piece, piece.Position.Y -1, piece.Position.X);

                if (!piece.HasMoved)
                {
                    Spaces[piece.Position.Y - 2, piece.Position.X].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, piece.Position.Y-2, piece.Position.X);
                }
                if (piece.Position.X > 0)
                {
                    Spaces[piece.Position.Y - 1, piece.Position.X - 1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, piece.Position.Y - 1, piece.Position.X - 1);
                }
                
                if (piece.Position.X < 7)
                {
                    Spaces[piece.Position.Y - 1, piece.Position.X + 1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, piece.Position.Y - 1, piece.Position.X + 1);
                }


            }
            else //black peice
            {
                if(piece.Position.Y == 7)
                {
                    return;
                }
                Spaces[piece.Position.Y + 1, piece.Position.X].UnderThreat.Add(piece);
                MarkAvailableMove(piece, piece.Position.Y + 1, piece.Position.X);
                if (!piece.HasMoved)
                {
                    Spaces[piece.Position.Y + 2, piece.Position.X].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, piece.Position.Y + 2, piece.Position.X);
                }
                if (piece.Position.X > 0)
                {
                    Spaces[piece.Position.Y + 1, piece.Position.X - 1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, piece.Position.Y + 1, piece.Position.X - 1);
                }
                if (piece.Position.X < 7)
                {
                    Spaces[piece.Position.Y + 1, piece.Position.X + 1].UnderThreat.Add(piece);
                    MarkAvailableMove(piece, piece.Position.Y + 1, piece.Position.X + 1);
                }
            }
        }


        //update the peices available moves with this. Threats include occupied ally spaces
        private void AddThreats(Piece piece)
        {
            //use this to check what piece it is
            switch (piece.Type)
            {
                case PieceType.king: AddThreatsKing(piece); break;
                case PieceType.pawn: AddThreatsPawn(piece); break;
                case PieceType.knight: AddThreatsKnight(piece); break;
                case PieceType.bishop: AddThreatsBishop(piece); break;
                case PieceType.rook: AddThreatsRook(piece); break;
                case PieceType.queen: AddThreatsQueen(piece); break;
                default: break;
            }

        }

        private void RemoveThreatsKing(Piece piece)
        {
            int x = piece.Position.X;
            int y = piece.Position.Y;
            if (x > 0)
            {
                if (y > 0)
                {
                    Spaces[y - 1, x - 1].UnderThreat.Remove(piece);
                }
                if (y < 7)
                {
                    Spaces[y + 1, x - 1].UnderThreat.Remove(piece);
                }
                Spaces[y, x - 1].UnderThreat.Remove(piece);
            }
            if (x < 7)
            {
                if (y > 0)
                {
                    Spaces[y - 1, x + 1].UnderThreat.Remove(piece);
                }
                if (y < 7)
                {
                    Spaces[y + 1, x + 1].UnderThreat.Remove(piece);
                }
                Spaces[y, x + 1].UnderThreat.Remove(piece);
            }
            if (y < 7)
            {
                Spaces[y + 1, x].UnderThreat.Remove(piece);
            }
            if (y > 0)
            {
                Spaces[y - 1, x].UnderThreat.Remove(piece);
            }
        }

        private void RemoveThreatsDiag(Piece piece)
        {
            int x = piece.Position.X;
            int y = piece.Position.Y;
            int tempx = 0;
            int tempy = 0;

            if (x < 7 && y < 7) //check down right
            {
                tempx = x + 1;
                tempy = y + 1;
                while (tempx > 8 && tempy > 8)
                {
                    Spaces[tempy, tempx].UnderThreat.Remove(piece);
                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx++;
                    tempy++;
                }
            }
            if (x < 7 && y > 0) //check up right
            {
                tempx = x + 1;
                tempy = y - 1;
                while (tempx > 8 && tempy >= 0)
                {
                    Spaces[tempy, tempx].UnderThreat.Remove(piece);
                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx++;
                    tempy--;
                }
            }
            if (x > 0 && y > 0) //check up left
            {
                tempx = x - 1;
                tempy = y - 1;
                while (tempx >= 0 && tempy >= 0)
                {
                    Spaces[tempy, tempx].UnderThreat.Remove(piece);
                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx--;
                    tempy--;
                }
            }
            if (x > 0 && y < 7)//check down left
            {
                tempx = x - 1;
                tempy = y + 1;
                while (tempx >= 0 && tempy < 8)
                {
                    Spaces[tempy, tempx].UnderThreat.Remove(piece);
                    if (Spaces[tempy, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx--;
                    tempy++;
                }
            }
        }

        private void RemoveThreatsDpad(Piece piece) 
        //can make more efficient by just having a temp instead of tempy and tempx
        {
            int x = piece.Position.X;
            int y = piece.Position.Y;
            int tempx = 0;
            int tempy = 0;

            if (x > 0) //check left
            {
                tempx = x - 1;
                while (tempx >= 0)
                {
                    Spaces[y, tempx].UnderThreat.Remove(piece);
                    if (Spaces[y, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx--;
                }
            }
            if (x < 7) //check right
            {
                tempx = x + 1;
                while (tempx < 8)
                {
                    Spaces[y, tempx].UnderThreat.Remove(piece);
                    if (Spaces[y, tempx].IsOccupied())
                    {
                        break;
                    }
                    tempx++;
                }
            }
            if (y > 0) // check up
            {
                tempy = y - 1;
                while (tempy >= 0)
                {
                    Spaces[tempy, x].UnderThreat.Remove(piece);
                    if (Spaces[tempy, x].IsOccupied())
                    {
                        break;
                    }
                    tempy--;
                }
            }
            if (y < 7) //check down
            {
                tempy = y + 1;
                while (tempy < 8)
                {
                    Spaces[tempy, x].UnderThreat.Remove(piece);
                    if (Spaces[tempy, x].IsOccupied())
                    {
                        break;
                    }
                    tempy++;
                }
            }
        }
        private void RemoveThreatsQueen(Piece piece)
        {
            RemoveThreatsDiag(piece);
            RemoveThreatsDpad(piece);
        }
        private void RemoveThreatsBishop(Piece piece)
        {
            RemoveThreatsDiag(piece);
        }

        private void RemoveThreatsKnight(Piece piece)
        {
            int x = piece.Position.X;
            int y = piece.Position.Y;

            if (x > 1 && y > 0)
            {
                Spaces[y - 1, x - 2].UnderThreat.Remove(piece);
            }
            if (x > 1 && y < 7)
            {
                Spaces[y + 1, x - 2].UnderThreat.Remove(piece);
            }
            if (x > 0 && y > 1)
            {
                Spaces[y - 2, x - 1].UnderThreat.Remove(piece);
            }
            if (x > 0 && y < 6)
            {
                Spaces[y + 2, x - 1].UnderThreat.Remove(piece);
            }
            if (x < 6 && y > 0)
            {
                Spaces[y - 1, x + 2].UnderThreat.Remove(piece);
            }
            if (x < 6 && y < 7)
            {
                Spaces[y + 1, x + 2].UnderThreat.Remove(piece);
            }
            if (x < 7 && y > 1)
            {
                Spaces[y - 2, x + 1].UnderThreat.Remove(piece);
            }
            if (x < 7 && y < 6)
            {
                Spaces[y + 2, x + 1].UnderThreat.Remove(piece);
            }
        }

        private void RemoveThreatsRook(Piece piece)
        {
            RemoveThreatsDpad(piece);
        }
        private void RemoveThreatsPawn(Piece piece)
        {
            if (piece.Color == Player.White)
            {
                Spaces[piece.Position.Y - 1, piece.Position.X].UnderThreat.Remove(piece);

                if (!piece.HasMoved)
                {
                    Spaces[piece.Position.Y - 2, piece.Position.X].UnderThreat.Remove(piece);
                }
                if (piece.Position.X > 0 && Spaces[piece.Position.Y - 1, piece.Position.X - 1].Piece != null
                    && Spaces[piece.Position.Y - 1, piece.Position.X - 1].Piece.Color != piece.Color)
                {
                    Spaces[piece.Position.Y - 1, piece.Position.X - 1].UnderThreat.Remove(piece);
                }
                if (piece.Position.X < 7 && Spaces[piece.Position.Y - 1, piece.Position.X + 1].Piece != null
                    && Spaces[piece.Position.Y - 1, piece.Position.X + 1].Piece.Color != piece.Color)
                {
                    Spaces[piece.Position.Y - 1, piece.Position.X + 1].UnderThreat.Remove(piece);
                }
            }
            else
            {
                Spaces[piece.Position.Y + 1, piece.Position.X].UnderThreat.Remove(piece);
                if (!piece.HasMoved)
                {
                    Spaces[piece.Position.Y + 2, piece.Position.X].UnderThreat.Remove(piece);
                }
                if (piece.Position.X > 0 && Spaces[piece.Position.Y + 1, piece.Position.X - 1].Piece != null
                    && Spaces[piece.Position.Y + 1, piece.Position.X - 1].Piece.Color != piece.Color)
                {
                    Spaces[piece.Position.Y + 1, piece.Position.X - 1].UnderThreat.Remove(piece);
                }
                if (piece.Position.X < 7 && Spaces[piece.Position.Y + 1, piece.Position.X + 1].Piece != null
                    && Spaces[piece.Position.Y + 1, piece.Position.X + 1].Piece.Color != piece.Color)
                {
                    Spaces[piece.Position.Y + 1, piece.Position.X + 1].UnderThreat.Remove(piece);
                }
            }
        }

        private void RemoveThreats(Piece piece) //remove availabe moves as well
        {
            //use this to check what piece it is
            piece.AvailabeMoves.Clear();

            switch(piece.Type)
            {
                case PieceType.king: RemoveThreatsKing(piece); break;
                case PieceType.pawn: RemoveThreatsPawn(piece); break;
                case PieceType.knight: RemoveThreatsKnight(piece); break;
                case PieceType.bishop: RemoveThreatsBishop(piece); break;
                case PieceType.rook: RemoveThreatsRook(piece); break;
                case PieceType.queen: RemoveThreatsQueen(piece); break;
                default: break;
            }
        }

        private void NormMove(Piece piece, Pos pos)
        {
            //move peice, clear threatens, add new threats
            Pos temp = piece.Position;
            RemoveThreats(piece);

            piece.Position = pos;
            if (Spaces[pos.Y, pos.X].IsOccupied())
            {
                RemoveThreats(Spaces[pos.Y, pos.X].Piece);
            }
            Spaces[pos.Y, pos.X].Piece = piece;
            Spaces[temp.Y, temp.X].Piece = null;

            piece.HasMoved = true;

            AddThreats(piece);
            UpdateBoardSpace(temp);
            if (piece.Type == PieceType.king)
            {
                if (piece.Color == Player.White)
                {
                    Wking = pos;
                }
                else
                {
                    Bking = pos;
                }

            }
        }

        private void CastleQMove(Piece piece, Pos pos) //move king left
        {
            Piece other;

            if(piece.Type == PieceType.king)
            {
                other = Spaces[piece.Position.Y, 0].Piece;
            }
            else
            {
                other = Spaces[piece.Position.Y, 4].Piece;
            }
            RemoveThreats(piece);
            RemoveThreats(other);

            Spaces[pos.Y, pos.X].Piece = piece;
            Spaces[piece.Position.Y, piece.Position.X].Piece = null;
            piece.Position = pos;

            if (other.Type == PieceType.king)
            {
                Spaces[piece.Position.Y, piece.Position.X - 1].Piece = other;
                Spaces[other.Position.Y, other.Position.X].Piece = null;
                other.Position.X = piece.Position.X - 1;
            }
            else //the rook
            {
                Spaces[piece.Position.Y, piece.Position.X + 1].Piece = other;
                Spaces[other.Position.Y, other.Position.X].Piece = null;
                other.Position.X = other.Position.X + 1;
            }

            AddThreats(other);
            AddThreats(piece);


        }

        private void CastleMove(Piece piece, Pos pos) //move king right
        {
            Piece other;

            if (piece.Type == PieceType.king)
            {
                other = Spaces[piece.Position.Y, 7].Piece;
            }
            else
            {
                other = Spaces[piece.Position.Y, 4].Piece;
            }
            RemoveThreats(piece);
            RemoveThreats(other);

            Spaces[pos.Y, pos.X].Piece = piece;
            Spaces[piece.Position.Y, piece.Position.X].Piece = null;
            piece.Position = pos;

            if (other.Type == PieceType.king)
            {
                Spaces[piece.Position.Y, piece.Position.X + 1].Piece = other;
                Spaces[other.Position.Y, other.Position.X].Piece = null;
                other.Position.X = piece.Position.X + 1;
            }
            else //the rook
            {
                Spaces[piece.Position.Y, piece.Position.X - 1].Piece = other;
                Spaces[other.Position.Y, other.Position.X].Piece = null;
                other.Position.X = other.Position.X - 1;
            }

            AddThreats(other);
            AddThreats(piece);

        }

        private void PossanteMove(Piece piece, Pos pos)
        {
            Pos temp = piece.Position;

            RemoveThreats(Spaces[temp.Y, pos.X].Piece);
            Spaces[temp.Y, pos.X].Piece = null;
            foreach (Piece pieces in Spaces[temp.Y, pos.X].UnderThreat)
            {
                RemoveThreats(pieces);
                AddThreats(pieces);
            }


        }

        public void UpdateBoardSpace(Pos pos)
        {
            if (Spaces[pos.Y, pos.X].UnderThreat.Count > 0)
            {
                List<Piece> pieces = new List<Piece>();
                foreach (Piece p in Spaces[pos.Y, pos.X].UnderThreat)
                {
                    pieces.Add(p);
                }
                foreach (Piece p in pieces)
                {
                    RemoveThreats(p);
                    AddThreats(p);
                }
            }
        }

        public void Move(Piece piece, Pos pos, MoveType move)
        {
            MoveType moveType = MoveType.Norm;
            if(piece.Type == PieceType.pawn && pos.X != piece.Position.X && !Spaces[pos.Y, pos.X].IsOccupied())
            {
                moveType = MoveType.Possante;
            }
            else
            {
                moveType = MoveType.Norm;
            }
            switch (move)
            {
                case MoveType.Norm:
                    NormMove(piece, pos);
                    break;
                case MoveType.CastleQ:
                    CastleQMove(piece, pos);
                    break;
                case MoveType.Castle:
                    CastleMove(piece, pos);
                    break;
                case MoveType.Possante:
                    PossanteMove(piece, pos);
                    break;
                default:
                    break;
            }
            UpdateBoardSpace(pos);
            turn = PlayerExt.Opponent(turn);
        }


        private void SetBoard()
        {
            //initalize pieces
            Piece RW = new Rook(Player.Black, new Pos(0, 0));
            Spaces[0, 0].Piece = RW;
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
            Spaces[7, 6].Piece = new Knight(Player.White, new Pos(7, 6));
            Spaces[7, 7].Piece = new Rook(Player.White, new Pos(7, 7));

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

        private void InitializeSpaces()
        {
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Spaces[i, j] = new Space();
                }
            }
        }

        public Board()
        {
            InitializeSpaces();
            SetBoard();
        }


    }
}
