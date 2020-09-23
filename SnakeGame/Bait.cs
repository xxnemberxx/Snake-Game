using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class Bait
    {
        public int width = 25;
        public int height = 25;
        public PictureBox apple = null;
        public int posX = 25;
        public int posY = 25;
        Random rand;

        public void Create(int max_x, int max_y)
        {
            rand = new Random();
            bool newBait = true;
            while (newBait)
            {
                posX = rand.Next(0, max_x);
                posY = rand.Next(0, max_y);
                if (posX % 25 == 0 && posY % 25 == 0)
                    newBait = false;
            }

            apple = new PictureBox()
            {
                Left = posX,
                Top = posY,
                Width = 25,
                Height = 25,
                BackColor = Color.Red
            };
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, apple.Width, apple.Height);
            apple.Region = new Region(path);
        }

       
        public void Delete()
        {
            apple.Dispose();
        }

    }
}
