using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessMan;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] peiceImages= new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private Pos lastClicked;
        private Board board = new();


        public MainWindow()
        {
            InitializeComponent();
            InitializeImages();
            DrawBoard();
        }

        private void DrawBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = board.Spaces[i, j].Piece;
                    peiceImages[i, j].Source = Images.GetImage(piece);
                }
            }
        }

        private void InitializeImages()
        {
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0;j < 8; j++)
                {
                    Image image = new();
                    peiceImages[i, j] = image;
                    PeiceGrid.Children.Add(image);
                    highlights[i, j] = new();
                    HighlightGrid.Children.Add(highlights[i, j]);
                }
            }
        }

        private Pos FindSquare(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int x = (int) (point.X / squareSize);
            int y = (int) (point.Y / squareSize);
            return new Pos(y, x);
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid); // this is in pixels
            Pos pos = FindSquare(point);
            if (board.Spaces[pos.Y, pos.X].IsOccupied()) //clicked on a piece
            {
                if (lastClicked != null && board.Spaces[lastClicked.Y, lastClicked.X].IsOccupied())
                {
                    RemoveHilight(board.Spaces[lastClicked.Y, lastClicked.X].Piece);
                }
                if (lastClicked != null && board.Spaces[lastClicked.Y, lastClicked.X].IsOccupied() && 
                        board.turn == board.Spaces[lastClicked.Y, lastClicked.X].Piece.Color &&
                        board.Spaces[lastClicked.Y, lastClicked.X].Piece.AvailabeMoves.Contains(pos))
                {
                    board.Move(board.Spaces[lastClicked.Y, lastClicked.X].Piece, pos, MoveType.Norm);
                    lastClicked = pos;
                }
                else
                {    
                    Hilight(board.Spaces[pos.Y, pos.X].Piece);
                    lastClicked = pos;
                }
            }
            else //not a piece
            {
                if(lastClicked != null && board.Spaces[lastClicked.Y, lastClicked.X].IsOccupied())
                {
                    RemoveHilight(board.Spaces[lastClicked.Y, lastClicked.X].Piece);
                }
                if (lastClicked != null && board.Spaces[lastClicked.Y, lastClicked.X].IsOccupied() &&
                        board.turn == board.Spaces[lastClicked.Y, lastClicked.X].Piece.Color &&
                        board.Spaces[lastClicked.Y, lastClicked.X].Piece.AvailabeMove(pos))
                {
                    board.Move(board.Spaces[lastClicked.Y, lastClicked.X].Piece, pos, MoveType.Norm); //draw here 
                    peiceImages[pos.Y, pos.X].Source = Images.GetImage(board.Spaces[pos.Y, pos.X].Piece); //now delete prev
                    peiceImages[lastClicked.Y, lastClicked.X].Source = null;
                }
                lastClicked = pos;
                
            }



        }

        private void Hilight(Piece piece)
        {
            Color color = Color.FromArgb(150, 125, 255, 125);
            foreach(Pos move in piece.AvailabeMoves)
            {
                highlights[move.Y, move.X].Fill = new SolidColorBrush(color);
            }
        }

        private void RemoveHilight(Piece piece)
        {
            foreach(Pos move in piece.AvailabeMoves)
            {
                highlights[move.Y, move.X].Fill = Brushes.Transparent;
            }
        }
    }
}