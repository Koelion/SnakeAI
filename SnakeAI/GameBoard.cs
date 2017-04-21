using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeAI
{
    public enum GAME_ITEMS
    {
        NONE,
        FOOD,
        HEAD,
        BODY,
        WALL
    }
    public class GameBoard
    {
        private Canvas canvas;
        private const int gridSize = 30;
        private int[,] grid = new int[gridSize, gridSize];
        private Rectangle[,] canvasGrid = new Rectangle[gridSize, gridSize];
        Random random = new Random((int)DateTime.Now.Ticks);

        public GameBoard(Canvas canvas)
        {
            this.canvas = canvas;

            OnFoodSummon += CanvasOnFoodSummon;
            CreateBoard();
        }

        private void CreateBoard()
        {
            //init grid
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    grid[y, x] = (int)GAME_ITEMS.NONE;
                }
            }
            //inint cavas grid
            double rectWidth = canvas.Width / canvasGrid.GetLength(1);
            double rectHeight = canvas.Height / canvasGrid.GetLength(0);
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    canvasGrid[y,x] = new Rectangle();
                    //canvasGrid[y, x].Stroke = new SolidColorBrush(Colors.Black);
                    canvasGrid[y, x].StrokeThickness = 0;
                    canvasGrid[y, x].Fill = new SolidColorBrush(Colors.Green);
                    canvasGrid[y, x].Width = rectWidth;
                    canvasGrid[y, x].Height = rectHeight;
                    Canvas.SetLeft(canvasGrid[y, x], x* rectWidth);
                    Canvas.SetTop(canvasGrid[y, x], y* rectHeight);
                    canvas.Children.Add(canvasGrid[y, x]);
                }
            }

            SummonFood();
        }

        private void CanvasOnFoodSummon(int x, int y)
        {
            canvasGrid[y, x].Fill = new SolidColorBrush(Colors.Red);
        }

        public delegate void OnSummonFoodHandler(int x, int y);
        event OnSummonFoodHandler OnFoodSummon;
        private void SummonFood()
        {
            int x ;
            int y;
            do
            {
                x = random.Next(grid.GetLength(1));
                y = random.Next(grid.GetLength(0));
            } while (grid[x, y] != (int)GAME_ITEMS.NONE);
            grid[x, y] = (int)GAME_ITEMS.FOOD;
            OnFoodSummon?.Invoke(x, y);
        }

    }
}
