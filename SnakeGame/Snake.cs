using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{

    public class Snake
    {
        public int width = 25;
        public int height = 25;
        public bool status = true;
        public char direction;
        public PictureBox[] body = new PictureBox[1];
        public int queue = 0;
        public int head= 0;
        public int length = 1;
        public Bait bait;
        public Snake()
        {
            bait = new Bait();
            bait.Create(500, 500);
            Create();
        }

        /// <summary>
        /// Yılan gövdelerinin tutulduğu dizinin 
        /// boyutunu yeniden düzenler
        /// </summary>
        public void addBody(PictureBox p)
        {
            Array.Resize(ref body, ++length);
            body[length -1] = p;
        }

        public void Create()
        {
            for (int i = 0; i < length; i++)
            {
                body[i] = new PictureBox()
                {
                    Width = width,
                    Height = height,
                    Left = 225,
                    Top = 225,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White
                };
                head= length - 1;
            }
        }

        // Hareket et
        public void GoMove(int left, int top)
        {
            // son gövde bir önceki gövdenin kordinatlarına eşitlenir.
            body[queue].Left = body[head].Left + left;
            body[queue].Top = body[head].Top + top;

            if (queue == length - 1)
            {
                queue = 0;
                head = length - 1;
            }
            else head = ++queue - 1;

            //  Yemek yediyse 
            if (DidEating())
            {
                // bellekten yemi sil
                bait.Delete();
                // yeni yem oluştur
                bait.Create(500, 500);
                // yeni gövde ekle
                addBody(new PictureBox() {
                    Width = width,
                    Height = height,
                    Left = body[queue].Left,
                    Top = body[queue].Top,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White
                });
            }

        }

        // Yemek yedi mi ? 
        public bool DidEating() 
        {
            // kafa ve yem kordinatları kontrol eder
            return body[head].Left == bait.posX && body[head].Top == bait.posY;
        }
        public void CrashControl(int wall_width, int wall_height)
        {
            if (length > 3)
            {
                for (int i = 3; i < length - 1; i++)
                {
                    if (i != head && body[head].Left == body[i].Left && body[head].Top == body[i].Top)
                    {
                        status = false;
                        break;
                    }
                }
            }

            if (0 > body[head].Left) status = false;
            else if (wall_width < body[head].Left + 25) status = false;
            else if (0 > body[head].Top) status = false;
            else if (wall_height < body[head].Top + 25) status = false;
        }

 
    }
}
