using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Snake snake = null;
        public int posX = 25;
        public int posY = 0;
        private const int _width = 500;
        private const int _height = 500;
        public Panel GameArea = null;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            GameCreate();
        }

        private void GameCreate()
        {
            GameArea = new Panel()
            {
                Width = _width,
                Height = _height,
                BackColor = Color.Black
            };
            Controls.Add(GameArea);
            snake = new Snake();
            foreach (PictureBox x in snake.body)
                GameArea.Controls.Add(x);

            GameArea.Controls.Add(snake.bait.apple);
            start.Interval = 125;
            start.Enabled = true;
        }
        private void start_Tick(object sender, EventArgs e)
        {
            snake.GoMove(posX, posY);
            snake.CrashControl(_width, _height);
            if (!snake.status)
            {
                this.Controls.Clear();
                
                GameCreate();
            }
            foreach (PictureBox x in snake.body)
            {
                GameArea.Controls.Add(x);
            }
            GameArea.Controls.Add(snake.bait.apple);
            start.Enabled = snake.status;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && snake.direction != 's')
            {
                posX = 0; posY = -25;
                snake.direction = 'w';
            }
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && snake.direction != 'd') 
            {
                posX = -25; posY = 0;
                snake.direction = 'a';
            }
            if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && snake.direction != 'a')
            {
                posX = 25; posY = 0;
                snake.direction = 'd';
            }
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && snake.direction != 'w')
            {
                posX = 0; posY = 25;
                snake.direction = 's';
            }
            if (e.KeyCode == Keys.Space) start.Enabled ^= true; // pause - continue 
            if (e.KeyCode == Keys.Escape) this.Dispose(); // Exit
        }
    }
}
