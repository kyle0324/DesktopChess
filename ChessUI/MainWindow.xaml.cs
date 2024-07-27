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
                }
            }
        }
    }
}