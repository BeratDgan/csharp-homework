using System;
using System.Drawing;
using System.Windows.Forms;

namespace TikTakToe
{
    class TikTakToe1
    {
        public TikTakToe1(Graphics gr, Panel panel)
        {
            graphics = gr;
            panel.MouseClick += Panel_MouseClick;
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            CheckRectanglesClick(e.Location);
            if (CheckWinner())
            {
                MessageBox.Show($"{Players[currentPlayerIndex]} wins!");
                ResetGame();
            }
        }

        Graphics graphics;
        Size GameAreaSize;
        public int offset { get; set; }
        private int offset2 { get; set; }
        private Size RectSize { get; set; }
        private char[] Players = new char[2] { 'X', 'O' };
        private Rectangle[,] Cells = new Rectangle[3, 3];
        private char[,] Board = new char[3, 3];
        private int currentPlayerIndex = 0;

        public void InitialiseBoard(Size panelSize)
        {
            graphics.Clear(Color.Black);
            GameAreaSize = panelSize;
            offset2 = 10;
            RectSize = new Size((panelSize.Width - 2 * offset - 6 * offset2) / 3,
                                (panelSize.Height - 2 * offset - 6 * offset2) / 3);

            DrawVerticalLines(GameAreaSize.Width, GameAreaSize.Height, offset);
            DrawHorizontalLines(GameAreaSize.Width, GameAreaSize.Height, offset);
            InitialiseRectangles(panelSize);
        }

        public void CheckRectanglesClick(Point location)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Cells[i, j].Contains(location) && Board[i, j] == '\0')
                    {
                        Board[i, j] = Players[currentPlayerIndex];
                        DrawPlayerMove(i, j);
                        currentPlayerIndex = (currentPlayerIndex + 1) % 2;
                        return;
                    }
                }
            }
        }

        private void DrawPlayerMove(int row, int col)
        {
            string playerSymbol = Players[currentPlayerIndex].ToString();
            graphics.DrawString(playerSymbol, SystemFonts.DefaultFont, Brushes.White, Cells[row, col]);
        }

        private void InitialiseRectangles(Size panelSize)
        {
            RectSize = new Size((panelSize.Width - 2 * offset - 6 * offset2) / 3,
                                (panelSize.Height - 2 * offset - 6 * offset2) / 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cells[i, j] = new Rectangle
                    {
                        Size = RectSize,
                        X = (offset + offset2) + j * RectSize.Width + 2 * j * offset2,
                        Y = (offset + offset2) + i * RectSize.Height + 2 * i * offset2
                    };
                    graphics.DrawRectangle(Pens.White, Cells[i, j]);
                    Board[i, j] = '\0';
                }
            }
        }

        private void DrawVerticalLines(int width, int height, int offset)
        {
            graphics.DrawLine(Pens.White, (width - 2 * offset) / 3 + offset, offset,
                              (width - 2 * offset) / 3 + offset, height - offset);
            graphics.DrawLine(Pens.White, 2 * (width - 2 * offset) / 3 + offset, offset,
                              2 * (width - 2 * offset) / 3 + offset, height - offset);
        }

        private void DrawHorizontalLines(int width, int height, int offset)
        {
            graphics.DrawLine(Pens.White, offset, (height - 2 * offset) / 3 + offset,
                              width - offset, (height - 2 * offset) / 3 + offset);
            graphics.DrawLine(Pens.White, offset, 2 * (height - 2 * offset) / 3 + offset,
                              width - offset, 2 * (height - 2 * offset) / 3 + offset);
        }

        public void CheckAndCorrectSize(Panel panel)
        {
            panel.Width = panel.Height;
        }

        private bool CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] != '\0' && Board[i, 0] == Board[i, 1] && Board[i, 1] == Board[i, 2])
                    return true;
                if (Board[0, i] != '\0' && Board[0, i] == Board[1, i] && Board[1, i] == Board[2, i])
                    return true;
            }
            if (Board[0, 0] != '\0' && Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
                return true;
            if (Board[0, 2] != '\0' && Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
                return true;

            return false;
        }

        private void ResetGame()
        {
            graphics.Clear(Color.Black);
            InitialiseBoard(GameAreaSize);
            currentPlayerIndex = 0;
        }
    }
}
