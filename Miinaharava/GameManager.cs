using System.Drawing.Imaging;

namespace Miinaharava
{
    public partial class GameManager : Form
    {
        public static GameManager instance { get; private set; }

        int boardSizeX = 0;
        int boardSizeY = 0;
        int mineAmount = 0;
        public int flagAmount = 0;
        int timePast = 0;
        int revealedCells = 0;
        Board board;
        ComboBox comboBox;
        Button startButton;
        Label timeLabel;
        public Label flagLabel;
        PictureBox pictureBox;
        System.Windows.Forms.Timer timer;

        public GameManager()
        {
            InitializeComponent();
            instance = this;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += GameTimerTick;
        }

        private void GameManager_Load(object sender, EventArgs e)
        {
            board = new Board();
            LoadStartMenu();
        }

        public void LoadStartMenu()
        {
            comboBox = new ComboBox();
            startButton = new Button();
            timeLabel = new Label();
            pictureBox = new PictureBox();
            flagLabel = new Label();
            Controls.Add(comboBox);
            Controls.Add(startButton);
            Controls.Add(timeLabel);
            Controls.Add(pictureBox);
            Controls.Add(flagLabel);
            comboBox.Location = new Point(15, 15);
            comboBox.Items.Add("Easy");
            comboBox.Items.Add("Normal");
            comboBox.Items.Add("Hard");
            comboBox.SelectedIndex = 0;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            startButton.Text = "Play";
            startButton.Location = new Point(20 + comboBox.Bounds.Width, 15);
            startButton.Size = new Size(comboBox.Bounds.Width, comboBox.Bounds.Height);
            startButton.Click += StartButtonClick;
            timeLabel.Text = "Time: 0 s";
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.Location = new Point(50 + comboBox.Bounds.Width + startButton.Bounds.Width, 15);
            timeLabel.Size = new Size(comboBox.Bounds.Width, comboBox.Bounds.Height);
            pictureBox.BackgroundImage = Properties.Resources.MineFlag;
            pictureBox.Location = new Point(500, 15);
            pictureBox.Size = new Size(30, 30);
            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            flagLabel.Text = flagAmount.ToString();
            flagLabel.Location = new Point(500 + pictureBox.Bounds.Width, 20);
            flagLabel.Size = new Size(comboBox.Bounds.Width, comboBox.Bounds.Height);
        }

        public void StartButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            switch (comboBox.SelectedIndex)
            {
                default:
                case (0):
                    boardSizeX = 9;
                    boardSizeY = 9;
                    mineAmount = 10;
                    flagAmount = mineAmount;
                    break;
                case (1):
                    boardSizeX = 16;
                    boardSizeY = 16;
                    mineAmount = 40;
                    flagAmount = mineAmount;
                    break;
                case (2):
                    boardSizeX = 30;
                    boardSizeY = 16;
                    mineAmount = 99;
                    flagAmount = mineAmount;
                    break;
            }
            board.GenerateBoard(boardSizeX, boardSizeY, mineAmount);
            timer.Start();
            flagLabel.Text = flagAmount.ToString();
            button.Click -= StartButtonClick;
        }

        public void TileClick(object sender, EventArgs e)
        {
            Tile tile = sender as Tile;
            int x = Convert.ToInt32(tile.position.ToString().Split(',').GetValue(0));
            int y = Convert.ToInt32(tile.position.ToString().Split(',').GetValue(1));
            List<Button> mineList = new List<Button>();
            if (tile.isFlagged)
            {
                return;
            }
            if (tile.isMine)
            {
                tile.BackgroundImage = Properties.Resources.mine;
                foreach (Tile tile_ in board.gameBoard)
                {
                    int x_ = Convert.ToInt32(tile_.position.ToString().Split(',').GetValue(0));
                    int y_ = Convert.ToInt32(tile_.position.ToString().Split(',').GetValue(1));
                    if (board.gameBoard[x_, y_].isMine)
                    {
                        tile_.BackgroundImage = Properties.Resources.mine;
                        tile_.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                tile.BackgroundImageLayout = ImageLayout.Stretch;
                GameOver();
                tile.Click -= TileClick;
                return;
            }
            else if (tile.adjacentMines == 0)
            {
                tile.BackColor = SystemColors.ControlLightLight;
                board.FloodFill(tile, boardSizeX, boardSizeY);
            }
            else
            {
                tile.BackColor = SystemColors.ControlLightLight;
                tile.Text = tile.adjacentMines.ToString();
            }
            comboBox.Focus();
            CheckForWin();
            tile.Click -= TileClick;
        }

        void GameTimerTick(object sender, EventArgs e)
        {
            timePast += 1;
            timeLabel.Text = "Time: " + timePast + " s";
        }

        public void PlaceFlag(object sender, MouseEventArgs e)
        {
            Tile tile = sender as Tile;
            Image flag = Properties.Resources.MineFlag;
            if (e.Button == MouseButtons.Right && tile.BackColor == SystemColors.ControlLight)
            {
                if (!tile.isFlagged && flagAmount > 0)
                {
                    tile.isFlagged = true;
                    tile.BackgroundImage = flag;
                    tile.BackgroundImageLayout = ImageLayout.Stretch;
                    flagAmount--;
                }
                else if(tile.isFlagged)
                {
                    tile.isFlagged = false;
                    tile.BackgroundImage = null;
                    flagAmount++;
                }
                flagLabel.Text = flagAmount.ToString();
            }
        }

        void CheckForWin()
        {
            revealedCells = 0;
            foreach (Tile tile in board.gameBoard)
            {
                if (tile.BackColor == SystemColors.ControlLightLight)
                {
                    revealedCells++;
                }
            }
            if (revealedCells == board.gameBoard.Length - mineAmount)
            {
                timer.Stop();
                MessageBox.Show("You won! Your time: " + timePast + " s");
                NewGame();
            }
            else
            {
                revealedCells = 0;
            }
        }

        void GameOver()
        {
            timer.Stop();
            MessageBox.Show("You lost");
            NewGame();
        }

        void NewGame()
        {
            foreach (Button button in board.gameBoard)
            {
                this.Controls.Remove(button);
            }
            this.Controls.Remove(comboBox);
            this.Controls.Remove(startButton);
            this.Controls.Remove(timeLabel);
            this.Controls.Remove(flagLabel);
            Array.Clear(board.gameBoard);
            mineAmount = 0;
            timePast = 0;
            flagAmount = 0;
            LoadStartMenu();
        }
    }
}