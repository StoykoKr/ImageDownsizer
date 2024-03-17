using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using static System.Windows.Forms.LinkLabel;

namespace ImageDownsizer
{
    public partial class Form1 : Form
    {
        CustomBitmap originalImage;
        CustomBitmap newImage;
        string a;
        double newSizePercent = 100;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    a = ofd.FileName;
                    originalImage = LoadImageAsCustomBitmap(ofd.FileName);
                    btnSingleThread.Enabled = true;
                    btnMultiThread.Enabled = true;
                }
            }
        }      
        private static bool AlmostEqualTo(double value1, double value2)
        {
            return Math.Abs(value1 - value2) < 0.000001;
        }
        private void Quarter(int startX, int startY, int endX, int endY)
        {
            float scaleX = originalImage.Width / newImage.Width;
            float scaleY = originalImage.Height / newImage.Height;
            for (int y = startY; y < endY; y++)
            {
                for (int x = startX; x < endX; x++)
                {
                    int j = 0;
                    List<(Color, float)> listVertical = new List<(Color, float)>();
                    for (float smallY = scaleY; smallY > 0;)
                    {
                        List<(Color, float)> listHorizontal = new List<(Color, float)>();
                        int i = 0;
                        float indexY = (y * scaleY) + j;
                        for (float smallX = scaleX; !AlmostEqualTo(smallX, 0);)
                        {
                            float indexX = (x * scaleX) + i;
                            float remnant = (float)(indexX % Math.Floor(indexX));
                            listHorizontal.Add(
                                (originalImage.GetPixel((int)Math.Floor(indexX), (int)indexY),
                                (float)(AlmostEqualTo(indexX, 0) ? 1
                                : (AlmostEqualTo(remnant, 0) ? 1 : (smallX > 1 ? 1 - (remnant) : remnant)))));
                            smallX -= (float)(AlmostEqualTo(indexX, 0) ? 1 : AlmostEqualTo(remnant, 0) ? 1 : remnant);
                            i++;
                        }
                        float sumR = 0;
                        float sumG = 0;
                        float sumB = 0;
                        for (int iterator = 0; iterator < listHorizontal.Count; iterator++)
                        {
                            sumR += listHorizontal[iterator].Item1.R * listHorizontal[iterator].Item2;
                            sumG += listHorizontal[iterator].Item1.G * listHorizontal[iterator].Item2;
                            sumB += listHorizontal[iterator].Item1.B * listHorizontal[iterator].Item2;
                        }
                        float remainingFromY = (float)(indexY % Math.Floor(indexY));
                        listVertical.Add(
                            (Color.FromArgb(0, (int)double.Round(sumR / scaleX), (int)double.Round(sumG / scaleX), (int)double.Round(sumB / scaleX)),
                            (float)(AlmostEqualTo(indexY, 0) ? 1 : AlmostEqualTo(remainingFromY, 0) ? 1
                            : (smallY > 1 ? 1 - remainingFromY : remainingFromY))));
                        smallY -= (float)(AlmostEqualTo(indexY, 0) ? 1 : AlmostEqualTo(remainingFromY, 0) ? 1 : remainingFromY);
                        j++;

                    }

                    float sumVertR = 0;
                    float sumVertG = 0;
                    float sumVertB = 0;
                    for (int iterator = 0; iterator < listVertical.Count; iterator++)
                    {
                        sumVertR += listVertical[iterator].Item1.R * listVertical[iterator].Item2;
                        sumVertG += listVertical[iterator].Item1.G * listVertical[iterator].Item2;
                        sumVertB += listVertical[iterator].Item1.B * listVertical[iterator].Item2;
                    }

                    float finalR = (int)double.Ceiling(sumVertR / scaleY);
                    float finalG = (int)double.Ceiling(sumVertG / scaleY);
                    float finalB = (int)double.Ceiling(sumVertB / scaleY);

                    newImage.SetPixel(x, y, Color.FromArgb(0, (int)Math.Round(finalR), (int)Math.Round(finalG), (int)Math.Round(finalB)));

                }
            }
        }



        private CustomBitmap LoadImageAsCustomBitmap(string filepath)
        {
            Bitmap bmp = new Bitmap(filepath);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =
            bmp.LockBits(rect, ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            CustomBitmap customBitmap = new CustomBitmap(bmp.Width, bmp.Height);

            bmp.UnlockBits(bmpData);


            for (int counter = 0; counter < bytes; counter += 4)
            {
                customBitmap.WriteToBits(counter / 4, Color.FromArgb(0, rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]));

            }
            lblSelected.Text = "Image selected success";
            return customBitmap;

        }

        private void btnSingleThread_Click(object sender, EventArgs e)
        {
            newSizePercent = int.Parse(txtBox.Text);
            newImage = new CustomBitmap((int)(originalImage.Width * (newSizePercent / 100)), (int)(originalImage.Height * (newSizePercent / 100)));

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Quarter(0, 0, newImage.Width, newImage.Height);
            stopWatch.Stop();
            using (newImage)
            {
                newImage.Bitmap.Save(a + newSizePercent + ".jpg", ImageFormat.Jpeg);
                MessageBox.Show("Success|Time elapsed: " + stopWatch.Elapsed);
            }
        }

        private void btnMultiThread_Click(object sender, EventArgs e)
        {
            newSizePercent = int.Parse(txtBox.Text);
            newImage = new CustomBitmap((int)(originalImage.Width * (newSizePercent / 100)), (int)(originalImage.Height * (newSizePercent / 100)));
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread q1 = new Thread(() =>
            {
                Quarter(0, 0, newImage.Width / 2, newImage.Height / 2);
            });
            Thread q2 = new Thread(() =>
            {
                Quarter(newImage.Width / 2, 0, newImage.Width, newImage.Height / 2);

            });
            Thread q3 = new Thread(() =>
            {
                Quarter(0, newImage.Height / 2, newImage.Width / 2, newImage.Height);

            });
            Thread q4 = new Thread(() =>
            {
                Quarter(newImage.Width / 2, newImage.Height / 2, newImage.Width, newImage.Height);

            });
            q1.Start();
            q2.Start();
            q3.Start();
            q4.Start();
            q1.Join();
            q2.Join();
            q3.Join();
            q4.Join();
            stopWatch.Stop();
            using (newImage)
            {
                newImage.Bitmap.Save(a + newSizePercent + ".jpg", ImageFormat.Jpeg);
                MessageBox.Show("Success|Time elapsed: " + stopWatch.Elapsed);
            }
        }
    }
}

