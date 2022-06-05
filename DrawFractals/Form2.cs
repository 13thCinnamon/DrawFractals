using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawFractals
{
    public partial class Form2 : Form
    {
        Bitmap image;

        public Form2()
        {
            InitializeComponent();
            InitImage();
            DrawTree(pictureBox1.Width / 2, 0, trackBar4.Value, trackBar2.Value);
        }

        public void InitImage()
        {
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
        public void DrawTree(int x, int y, double len, double angle)
        {
            pictureBox1.Image = image;
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            double x1, y1;
            x1 = x + len * Math.Sin(angle * Math.PI * 2 / 360);
            y1 = y + len * Math.Cos(angle * Math.PI * 2 / 360);
            double k = trackBar3.Value / 10.0;
            g.DrawLine(new Pen(Color.Black), x, pictureBox1.Height - y, (int)x1, pictureBox1.Height - (int)y1);
            if (len > trackBar5.Value)
            {
                DrawTree((int)x1, (int)y1, len / k , angle + trackBar1.Value + trackBar2.Value);
                DrawTree((int)x1, (int)y1, len / k, angle - trackBar1.Value + trackBar2.Value);
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            InitImage();
            DrawTree(pictureBox1.Width / 2, 0, trackBar4.Value, trackBar2.Value);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            InitImage();
            DrawTree(pictureBox1.Width / 2, 0, trackBar4.Value, trackBar2.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            InitImage();
            DrawTree(pictureBox1.Width / 2, 0, trackBar4.Value, trackBar2.Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            InitImage();
            DrawTree(pictureBox1.Width / 2, 0, trackBar4.Value, trackBar2.Value);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            InitImage();
            DrawTree(pictureBox1.Width / 2, 0, trackBar4.Value, trackBar2.Value);
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            InitImage();
            DrawTree(pictureBox1.Width / 2, 0, trackBar4.Value, trackBar2.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm mainform = new MainForm();
            mainform.Show();
        }
    }
}
