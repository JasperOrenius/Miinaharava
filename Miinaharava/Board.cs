using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miinaharava
{
    public class Board
    {
        public static GameManager gameManager;
        public Tile[,] gameBoard;

        public void GenerateBoard(int boardSizeX, int boardSizeY, int mineAmount)
        {
            gameBoard = new Tile[boardSizeY, boardSizeX];
            int positionX = 0;
            int positionY = 0;
            for (int i = 0; i < boardSizeY; i++)
            {
                for (int j = 0; j < boardSizeX; j++)
                {
                    Tile tile = new Tile();
                    GameManager.instance.Controls.Add(tile);
                    tile.Location = new Point(15 + positionX, 50 + positionY);
                    tile.Size = new Size(30, 30);
                    tile.position = $"{i},{j}";
                    positionX += 30;
                    tile.Click += GameManager.instance.TileClick;
                    tile.MouseDown += GameManager.instance.PlaceFlag;
                    tile.FlatStyle = FlatStyle.Flat;
                    tile.BackColor = SystemColors.ControlLight;
                    tile.FlatAppearance.BorderColor = SystemColors.ControlDark;
                    gameBoard[i, j] = tile;
                }
                positionY += 30;
                positionX = 0;
            }
            GenerateMines(boardSizeX, boardSizeY, mineAmount);
            SetTilePositionValues(boardSizeX, boardSizeY);
        }

        void GenerateMines(int boardSizeX, int boardSizeY, int mineAmount)
        {
            Random random = new Random();
            for (int i = 0; i < mineAmount; i++)
            {
                int positionX = random.Next(0, boardSizeY);
                int positionY = random.Next(0, boardSizeX);
                if (!gameBoard[positionX, positionY].isMine)
                {
                    gameBoard[positionX, positionY].isMine = true;
                }
                else
                {
                    i--;
                }
            }
        }

        void SetTilePositionValues(int boardSizeX, int boardSizeY)
        {
            for (int i = 0; i < boardSizeY; i++)
            {
                for (int j = 0; j < boardSizeX; j++)
                {
                    if (gameBoard[i, j].isMine)
                    {
                        continue;
                    }
                    int mineCount = 0;
                    for (int x = -1; x < 2; x++)
                    {
                        int row = i + x;
                        for (int y = -1; y < 2; y++)
                        {
                            int col = j + y;
                            if (row == -1 || col == -1 || row > boardSizeY - 1 || col > boardSizeX - 1)
                            {
                                continue;
                            }
                            if (row == i && col == j)
                            {
                                continue;
                            }
                            if (gameBoard[row, col].isMine)
                            {
                                mineCount++;
                            }
                        }
                    }
                    if (mineCount == 0)
                    {
                        gameBoard[i, j].adjacentMines = 0;
                    }
                    else
                    {
                        gameBoard[i, j].adjacentMines = mineCount;
                    }
                }
            }
        }

        public void FloodFill(Tile tile, int boardSizeX, int boardSizeY)
        {
            int x = Convert.ToInt32(tile.position.ToString().Split(',').GetValue(0));
            int y = Convert.ToInt32(tile.position.ToString().Split(',').GetValue(1));
            List<Tile> emptyTiles = new List<Tile>();
            for (int i = -1; i < 2; i++)
            {
                int row = x + i;
                for (int j = -1; j < 2; j++)
                {
                    int col = y + j;
                    if (row == -1 || col == -1 || row > boardSizeY - 1 || col > boardSizeX - 1)
                    {
                        continue;
                    }
                    if (row == x && col == y)
                    {
                        continue;
                    }
                    Tile tile_ = gameBoard[row, col];
                    int xNeighbour = Convert.ToInt32(tile_.position.ToString().Split(',').GetValue(0));
                    int yNeighbour = Convert.ToInt32(tile_.position.ToString().Split(',').GetValue(1));
                    if (gameBoard[xNeighbour, yNeighbour].adjacentMines == 0)
                    {
                        if (tile_.BackColor != SystemColors.ControlLightLight)
                        {
                            tile_.BackColor = SystemColors.ControlLightLight;
                            emptyTiles.Add(tile_);
                            if (tile_.isFlagged)
                            {
                                tile_.BackgroundImage = null;
                                GameManager.instance.flagAmount++;
                                GameManager.instance.flagLabel.Text = GameManager.instance.flagAmount.ToString();
                            }
                        }
                    }
                    else if (!gameBoard[xNeighbour, yNeighbour].isMine)
                    {
                        tile_.PerformClick();
                    }
                }
            }
            foreach (var emptyTile in emptyTiles)
            {
                FloodFill(emptyTile, boardSizeX, boardSizeY);
            }
        }
    }
}
