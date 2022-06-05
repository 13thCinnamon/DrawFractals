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
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
            InitImage();
            Draw();

            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) scale = scale * 1.5;
            if (e.Delta < 0) scale = scale / 1.5;
            Draw();
        }

        public static double cX = 0.0;
        public static double cY = 0.0;

        double scale = 100;

        public double ConvertX(double x)
        {
            x = (x - (double)(image.Width / 2)) / scale + cX;
            return x;
        }

        public double ConvertY(double y)
        {
            y = - ((y - (double)(image.Height / 2)) / scale - cY);
            return y;
        }

        public double ConvertX(int x)
        {
            double x1 = (x - (double)(image.Width / 2)) / scale + cX;
            return x1;
        }

        public double ConvertY(int y)
        {
            double y1 = - ((y - (double)(image.Height / 2)) / scale - cY);
            return y1;
        }

        public void InitImage()
        {
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = image;
        }

        public void FormReset()
        {
            InitImage();
        }

        public void Draw()
        {
            int n = trackBar1.Value;
            int p1 = trackBar2.Value;
            int p2 = trackBar3.Value;
            int p3 = trackBar4.Value;

            var imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
            );

            IntPtr ptr = imageData.Scan0;
            int bytes = Math.Abs(imageData.Stride) * image.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for(int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int p = (y * image.Width + x) * 4;

                    var x0 = ConvertX(x);
                    var y0 = ConvertY(y);
                    var xn = x0;
                    var yn = y0;
                    double xn1, yn1;
                    bool inter = true;
                    int i;
                    for (i = 0; i < n; i++)
                    {
                        xn1 = Math.Pow(xn, p2) - Math.Pow(yn, p3) + x0;
                        yn1 = p1 * xn * yn + y0;
                        
                        if(xn1 * xn1 + yn1 * yn1 > 4)
                        {
                            inter = false;
                            break;
                        }
                        xn = xn1;
                        yn = yn1;
                    }

                    if (inter)
                    {
                        rgbValues[p] = 19;
                        rgbValues[p + 1] = 8;
                        rgbValues[p + 2] = 77;
                        rgbValues[p + 3] = 255;//alpha
                    }
                    else
                    {
                        double k = 1 - (double)i / n;

                        rgbValues[p] = (byte)((1 - k) * 245 + k  * 0);
                        rgbValues[p + 1] = (byte)((1 - k) * 66 + k * 0);
                        rgbValues[p + 2] = (byte)((1 - k) * 144 + k * 0);
                        rgbValues[p + 3] = 255;//alpha
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            image.UnlockBits(imageData);
            pictureBox1.Invalidate();

            label3.Text = $"Приближение: {scale}";
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            InitImage();
            Draw();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.Location;
            var convertPointX = ConvertX(point.X);
            var convertPointY = ConvertY(point.Y);


            label1.Text = $"X: {convertPointX} Y: {convertPointY}"; 
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var point = e.Location;
            var convertPointX = ConvertX(point.X);
            var convertPointY = ConvertY(point.Y);
            cX = convertPointX;
            cY = convertPointY;
            Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
