using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessamentoImagemTrabalhoFinal
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap img1;
        Bitmap img2;
        byte[,] vImg1Gray;

        byte[,] vImg1R;
        byte[,] vImg1G;
        byte[,] vImg1B;
        byte[,] vImg1A;
        bool bLoadImgOK = false;
        //Aumentar brilho imagem
        private void IncreaseBrightnessRGB() {
            Bitmap image = (Bitmap)pictureBox1.Image;
            img2 = new Bitmap(image.Width, image.Height);
            int up = (int)numericUpDownBrilhoMais.Value;

            for (int i = 0; i < image.Width; i++) {
                for (int j = 0; j < image.Height; j++) {
                    Color pixel = image.GetPixel(i, j);

                    int R = Math.Min(pixel.R + up, 255);
                    int G = Math.Min(pixel.G + up, 255);
                    int B = Math.Min(pixel.B + up, 255);

                    Color newColor = Color.FromArgb(R, G, B);

                    img2.SetPixel(i, j, newColor);
                }
            }
            pictureBox3.Image = img2;
        }

        private void DecreaseBrightnessRGB() {
            Bitmap image = (Bitmap)pictureBox1.Image;
            img2 = new Bitmap(image.Width, image.Height);
            int down = (int)numericUpDownBrilhoMenos.Value;

            for (int i = 0; i < image.Width; i++) {
                for (int j = 0; j < image.Height; j++) {
                    Color pixel = image.GetPixel(i, j);

                    int R = Math.Max(pixel.R - down, 0);
                    int G = Math.Max(pixel.G - down, 0);
                    int B = Math.Max(pixel.B - down, 0);

                    Color newColor = Color.FromArgb(R, G, B);

                    img2.SetPixel(i, j, newColor);
                }
            }

            pictureBox3.Image = img2;
        }
        private void tabPage1_Click(object sender, EventArgs e) {

        }

        private Boolean updateImage() {
            // Configurações iniciais da OpenFileDialogBox
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Users\\lipem\\OneDrive\\Documentos\\MatLab\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
           

            // Se um arquivo foi localizado com sucesso...
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                // Armnazena o path do arquivo de imagem
                filePath = openFileDialog1.FileName;


                
                try {
                    img1 = new Bitmap(filePath);
                    img2 = new Bitmap(img1.Width, img1.Height);
                    bLoadImgOK = true;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Erro ao abrir imagem...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bLoadImgOK = false;
                }
            }
            return bLoadImgOK;
        }

        private void btImg1_Click_1(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if(imagemCarregada == true) {
                pictureBox1.Image = img1;
            }

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                // Adiciona imagem na PictureBox
                pictureBox1.Image = img1;
                vImg1Gray = new byte[img1.Width, img1.Height];
                vImg1R = new byte[img1.Width, img1.Height];
                vImg1G = new byte[img1.Width, img1.Height];
                vImg1B = new byte[img1.Width, img1.Height];
                vImg1A = new byte[img1.Width, img1.Height];

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < img1.Width; i++) {
                    for (int j = 0; j < img1.Height; j++) {
                        Color pixel = img1.GetPixel(i, j);

                        // Para imagens em escala de cinza, extrair o valor do pixel com...
                        //byte pixelIntensity = Convert.ToByte((pixel.R + pixel.G + pixel.B) / 3);
                        byte pixelIntensity = Convert.ToByte((pixel.R + pixel.G + pixel.B) / 3);
                        vImg1Gray[i, j] = pixelIntensity;

                        // Para imagens RGB, extrair o valor do pixel com...
                        byte R = pixel.R;
                        byte G = pixel.G;
                        byte B = pixel.B;
                        byte A = pixel.A;

                        vImg1R[i, j] = R;
                        vImg1G[i, j] = G;
                        vImg1B[i, j] = B;
                        vImg1A[i, j] = A;

                        Color cor = Color.FromArgb(
                            255,
                            vImg1Gray[i, j],
                            vImg1Gray[i, j],
                            vImg1Gray[i, j]);

                        img2.SetPixel(i, j, cor);
                    }
                }
                pictureBox2.Image = img2;
            }
        }

        private void bntPlus_Click(object sender, EventArgs e) {
            IncreaseBrightnessRGB();
        }

        private void bntDown_Click(object sender, EventArgs e) {
            DecreaseBrightnessRGB();
        }

        private void button1_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox4.Image = img1;
            }

        }

        private void Page2_btn_image2_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox5.Image = img1;
            }

           
        }

        private void Page2_btn_union_Click(object sender, EventArgs e) {
            Bitmap image1 = (Bitmap)pictureBox4.Image;
            Bitmap image2 = (Bitmap)pictureBox5.Image;

            // Percorre todos os pixels da imagem...
            for (int i = 0; i < img1.Width; i++) {
                for (int j = 0; j < img1.Height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);

                    int R = Math.Min(pixel1.R + pixel2.R, 255);
                    int G = Math.Min(pixel1.G + pixel2.G, 255);
                    int B = Math.Min(pixel1.B + pixel2.B, 255);

                    Color newColor = Color.FromArgb(R, G, B);

                    img2.SetPixel(i, j, newColor);
                }
            }
            pictureBox6.Image = img2;
        }

        private void page2_btn_subtracao_Click(object sender, EventArgs e) {
            Bitmap image1 = (Bitmap)pictureBox4.Image;
            Bitmap image2 = (Bitmap)pictureBox5.Image;

            // Percorre todos os pixels da imagem...
            for (int i = 0; i < img1.Width; i++) {
                for (int j = 0; j < img1.Height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);

                    int R = Math.Max(pixel1.R - pixel2.R, 0);
                    int G = Math.Max(pixel1.G - pixel2.G, 0);
                    int B = Math.Max(pixel1.B - pixel2.B, 0);

                    Color newColor = Color.FromArgb(R, G, B);

                    img2.SetPixel(i, j, newColor);
                }
            }
            pictureBox6.Image = img2;
        }
    }
}
