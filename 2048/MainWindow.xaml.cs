using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly SolidColorBrush Tile = new SolidColorBrush(Color.FromRgb(238, 228, 218));
        public readonly SolidColorBrush Tile2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEE4DA"));
        public readonly SolidColorBrush Tile4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDE0C8"));
        public readonly SolidColorBrush Tile8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2B179"));
        public readonly SolidColorBrush Tile16 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F59563"));
        public readonly SolidColorBrush Tile32 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F67C5F"));
        public readonly SolidColorBrush Tile64 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F65E3B"));
        public readonly SolidColorBrush Tile128 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDCF72"));
        public readonly SolidColorBrush Tile256 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDCC61"));
        public readonly SolidColorBrush Tile512 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC850"));
        public readonly SolidColorBrush Tile1024 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC53F"));
        public readonly SolidColorBrush Tile2048 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC22E"));
        public readonly SolidColorBrush TileSuper = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3A32"));

        public readonly SolidColorBrush Below4 = new SolidColorBrush(Colors.Black);
        public readonly SolidColorBrush Above4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f6f2"));

        public const int Size = 4;
        public GameManager GameManager { get; set; }

        public TextBlock[,] TextBlocks;
        public Border[,] Borders;

        public MainWindow()
        {
            InitializeComponent();

            GameManager = new GameManager(Size, 2);

            TextBlocks = new TextBlock[Size,Size];
            Borders = new Border[Size,Size];

            for(int i = 0; i < Size; i++)
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        Width = new GridLength(60)
                    });
            for (int i = 0; i < Size; i++)
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(60)
                });
            for(int y = 0; y < Size; y++)
                for(int x = 0; x < Size; x++)
                {
                    TextBlock textBlock = new TextBlock
                        {
                            FontWeight = FontWeights.Bold,
                            FontSize = 14,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    Border border = new Border
                        {
                            Background = Tile,
                            BorderBrush = new SolidColorBrush(Colors.Black),
                            BorderThickness = new Thickness(2),
                            Margin = new Thickness(1),
                            Child = textBlock
                        };
                    System.Windows.Controls.Grid.SetRow(border, y);
                    System.Windows.Controls.Grid.SetColumn(border, x);
                    MainGrid.Children.Add(border);

                    Borders[x, y] = border;
                    TextBlocks[x, y] = textBlock;
                }
        }

        private void Refresh()
        {
            if (GameManager == null || GameManager.Grid == null)
                return;
            for (int y = 0; y < Size; y++)
                for (int x = 0; x < Size; x++)
                {
                    int value = GameManager.Grid.Cells[x, y];
                    TextBlocks[x, y].Text = value > 0 ? value.ToString(CultureInfo.InvariantCulture) : null;
                    switch(value)
                    {
                        case 0:
                            Borders[x, y].Background = Tile;
                            break;
                        case 2:
                            Borders[x, y].Background = Tile2;
                            TextBlocks[x, y].Foreground = Below4;
                            break;
                        case 4:
                            Borders[x, y].Background = Tile4;
                            TextBlocks[x, y].Foreground = Below4;
                            break;
                        case 8:
                            Borders[x, y].Background = Tile8;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 16:
                            Borders[x, y].Background = Tile16;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 32:
                            Borders[x, y].Background = Tile32;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 64:
                            Borders[x, y].Background = Tile64;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 128:
                            Borders[x, y].Background = Tile128;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 256:
                            Borders[x, y].Background = Tile256;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 512:
                            Borders[x, y].Background = Tile512;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 1024:
                            Borders[x, y].Background = Tile1024;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        case 2048:
                            Borders[x, y].Background = Tile2048;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                        default:
                            Borders[x, y].Background = TileSuper;
                            TextBlocks[x, y].Foreground = Above4;
                            break;
                    }
                }
            ScoreText.Text = GameManager.Grid.Score.ToString(CultureInfo.InvariantCulture);
            MovesText.Text = GameManager.Moves.ToString(CultureInfo.InvariantCulture);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Down:
                    GameManager.Move(Directions.Down);
                    break;
                case Key.Left:
                    GameManager.Move(Directions.Left);
                    break;
                case Key.Right:
                    GameManager.Move(Directions.Right);
                    break;
                case Key.Up:
                    GameManager.Move(Directions.Up);
                    break;
            }
            Refresh();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                    System.Diagnostics.Debug.Write(GameManager.Grid.Cells[x,y].ToString(CultureInfo.InvariantCulture).PadLeft(4));
                System.Diagnostics.Debug.WriteLine(String.Empty);
            }
            System.Diagnostics.Debug.WriteLine("===============================");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Start();
            Refresh();
        }
    }
}
