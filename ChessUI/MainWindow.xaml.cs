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
            if (board.Spaces[pos.Y, pos.X].IsOccupied())
            {
                Hilight(board.Spaces[pos.Y, pos.X].Piece);
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