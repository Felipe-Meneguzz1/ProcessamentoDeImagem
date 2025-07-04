﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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

        //Aumentar Brilho Imagem
        private void IncreaseBrightnessRGB() {
            if (pictureBox1.Image == null) {
                MessageBox.Show("Nenhuma imagem carregada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int up = (int)numericUpDownBrilhoMais.Value;
            if (up < 0) {
                MessageBox.Show("O valor de brilho deve ser positivo.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try {
                Bitmap image = (Bitmap)pictureBox1.Image;
                img2 = new Bitmap(image.Width, image.Height);

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
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao processar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Diminuir Brilho Imagem
        private void DecreaseBrightnessRGB() {
            if (pictureBox1.Image == null) {
                MessageBox.Show("Nenhuma imagem carregada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int down = (int)numericUpDownBrilhoMenos.Value;
            if (down < 0) {
                MessageBox.Show("O valor de brilho deve ser positivo.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try {
                Bitmap image = (Bitmap)pictureBox1.Image;
                img2 = new Bitmap(image.Width, image.Height);

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
            } catch (Exception ex) {
                MessageBox.Show($"Erro ao processar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Limpar pictureBox
        private void ClearPictureBox(PictureBox pictureBox) {
            if (pictureBox.Image != null) {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e) {

        }

        //Carregar Imagem
        private Boolean updateImage() {
            // Configurações iniciais da OpenFileDialogBox
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Users\\lipem\\OneDrive\\Documentos\\MatLab\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 5;
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

        //Salvar Imagem
        private void SalvarImagem(System.Drawing.Image imagemParaSalvar) {
            if (imagemParaSalvar != null) {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                    saveFileDialog.Title = "Salvar Imagem";
                    saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                    saveFileDialog.DefaultExt = "png";
                    saveFileDialog.FileName = "imagem";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                        System.Drawing.Imaging.ImageFormat formato = System.Drawing.Imaging.ImageFormat.Png;

                        if (saveFileDialog.FileName.EndsWith(".jpg"))
                            formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                        else if (saveFileDialog.FileName.EndsWith(".bmp"))
                            formato = System.Drawing.Imaging.ImageFormat.Bmp;

                        try {
                            imagemParaSalvar.Save(saveFileDialog.FileName, formato);
                            MessageBox.Show("Imagem salva com sucesso em:\n" + saveFileDialog.FileName, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } catch (Exception ex) {
                            MessageBox.Show("Erro ao salvar a imagem:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            } else {
                MessageBox.Show("Nenhuma imagem para salvar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Horizontal
        private void fliplr() {
            if (pictureBox7.Image == null) {
                MessageBox.Show("Nenhuma imagem carregada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox7.Image;
            Bitmap image2 = new Bitmap(image1.Width, image1.Height);
            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color pixel = image1.GetPixel(i, j);
                    image2.SetPixel(image1.Width - 1 - i, j, pixel);
                }
            }
            pictureBox8.Image = image2;
        }

        //Vertical
        private void flipud() {
            if (pictureBox7.Image == null) {
                MessageBox.Show("Nenhuma imagem carregada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox7.Image;
            Bitmap image2 = new Bitmap(image1.Width, image1.Height);

            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color pixel = image1.GetPixel(i, j);
                    image2.SetPixel(i, image1.Height - 1 - j, pixel);
                }
            }
            pictureBox8.Image = image2;
        }

        //Diferença
        private void subplot() {
            if (pictureBox9.Image == null || pictureBox10.Image == null) {
                MessageBox.Show("Carregue as duas imagens antes de executar o subplot.",
                                "Imagens faltando", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox9.Image;
            Bitmap image2 = (Bitmap)pictureBox10.Image;
            int width = Math.Min(image1.Width, image2.Width);
            int height = Math.Min(image1.Height, image2.Height);
            Bitmap C = new Bitmap(width, height);
            Bitmap D = new Bitmap(width, height);
            Bitmap E = new Bitmap(width, height);

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);
                    int R1 = Math.Max(pixel1.R - pixel2.R, 0);
                    int G1 = Math.Max(pixel1.G - pixel2.G, 0);
                    int B1 = Math.Max(pixel1.B - pixel2.B, 0);
                    int R2 = Math.Max(pixel2.R - pixel1.R, 0);
                    int G2 = Math.Max(pixel2.G - pixel1.G, 0);
                    int B2 = Math.Max(pixel2.B - pixel1.B, 0);

                    Color newColorC = Color.FromArgb(R1, G1, B1);
                    Color newColorD = Color.FromArgb(R2, G2, B2);
                    Color newColorE = Color.FromArgb(
                        Math.Min(R1 + R2, 255),
                        Math.Min(G1 + G2, 255),
                        Math.Min(B1 + B2, 255)
                    );

                    C.SetPixel(i, j, newColorC);
                    D.SetPixel(i, j, newColorD);
                    E.SetPixel(i, j, newColorE);
                }
            }
            pictureBox11.Image?.Dispose();
            pictureBox12.Image?.Dispose();
            pictureBox13.Image?.Dispose();
            pictureBox11.Image = C;
            pictureBox12.Image = D;
            pictureBox13.Image = E;
        }

        //Limiarização
        private void limiarizacao() {
            int threshold = (int)numericUpDownlimirizacao.Value;
            if (radioButton3.Checked && pictureBox14.Image == null) {
                MessageBox.Show("Imagem 1 não carregada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (radioButton4.Checked && pictureBox22.Image == null) {
                MessageBox.Show("Imagem 2 não carregada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap selectedImage = null;
            if (radioButton3.Checked)
                selectedImage = (Bitmap)pictureBox14.Image;
            else if (radioButton4.Checked)
                selectedImage = (Bitmap)pictureBox22.Image;
            else {
                MessageBox.Show("Selecione uma imagem usando os botões de opção.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int width = selectedImage.Width;
            int height = selectedImage.Height;
            Bitmap processedImage = new Bitmap(width, height);
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Color pixel = selectedImage.GetPixel(i, j);
                    byte intensity = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    Color newColor = (intensity >= threshold) ? Color.White : Color.Black;

                    processedImage.SetPixel(i, j, newColor);
                }
            }
            pictureBox16.Image = processedImage;
        }

        //Operações Logicas
        private void logic() {
            if (pictureBox17.Image == null || pictureBox18.Image == null) {
                MessageBox.Show("Carregue as duas imagens antes de aplicar a operação lógica.",
                                "Imagens faltando", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox17.Image;
            Bitmap image2 = (Bitmap)pictureBox18.Image;
            if (image1.Width != image2.Width || image1.Height != image2.Height) {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho.",
                                "Dimensões diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string value = comboBox1.Text;
            int width = image1.Width;
            int height = image1.Height;
            Bitmap result = new Bitmap(width, height);
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);
                    Color newColor = Color.Black;
                    switch (value) {
                        case "AND":
                        newColor = Color.FromArgb(
                            pixel1.R & pixel2.R,
                            pixel1.G & pixel2.G,
                            pixel1.B & pixel2.B);
                        break;

                        case "OR":
                        newColor = Color.FromArgb(
                            pixel1.R | pixel2.R,
                            pixel1.G | pixel2.G,
                            pixel1.B | pixel2.B);
                        break;

                        case "XOR":
                        newColor = Color.FromArgb(
                            pixel1.R ^ pixel2.R,
                            pixel1.G ^ pixel2.G,
                            pixel1.B ^ pixel2.B);
                        break;

                        case "NOT":
                        if (radioButton1.Checked) {
                            newColor = Color.FromArgb(
                                pixel1.R ^ 255,
                                pixel1.G ^ 255,
                                pixel1.B ^ 255);
                        } else if (radioButton2.Checked) {
                            newColor = Color.FromArgb(
                                pixel2.R ^ 255,
                                pixel2.G ^ 255,
                                pixel2.B ^ 255);
                        } else {
                            MessageBox.Show("Selecione qual imagem aplicar o NOT.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;

                        default:
                        MessageBox.Show("Selecione uma operação lógica no ComboBox.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result.SetPixel(i, j, newColor);
                }
            }
            pictureBox19.Image?.Dispose();
            pictureBox19.Image = result;
        }

        //Mediana
        private void mediana() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int imgsize = 3;
            string value = RealceCombobox.Text;
            switch (value) {
                case "3X3":
                imgsize = 3;
                break;
                case "5X5":
                imgsize = 5;
                break;
                case "7X7":
                imgsize = 7;
                break;
            }

            Bitmap original = new Bitmap(pictureBox20.Image);
            Bitmap result = new Bitmap(original.Width, original.Height);

            int offset = imgsize / 2;

            for (int y = offset; y < original.Height - offset; y++) {
                for (int x = offset; x < original.Width - offset; x++) {
                    List<int> r = new List<int>();
                    List<int> g = new List<int>();
                    List<int> b = new List<int>();
                    for (int fy = -offset; fy <= offset; fy++) {
                        for (int fx = -offset; fx <= offset; fx++) {
                            Color pixel = original.GetPixel(x + fx, y + fy);
                            r.Add(pixel.R);
                            g.Add(pixel.G);
                            b.Add(pixel.B);
                        }
                    }
                    r.Sort();
                    g.Sort();
                    b.Sort();
                    Color mediana = Color.FromArgb(
                        r[r.Count / 2],
                        g[g.Count / 2],
                        b[b.Count / 2]
                    );
                    result.SetPixel(x, y, mediana);
                }
            }
            pictureBox21.Image?.Dispose();
            pictureBox21.Image = result;
        }

        //Media
        private void media() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int imgsize = 3;

            string value = RealceCombobox.Text;
            switch (value) {
                case "3X3":
                imgsize = 3;
                break;
                case "5X5":
                imgsize = 5;
                break;
                case "7X7":
                imgsize = 7;
                break;
            }

            Bitmap image = new Bitmap(pictureBox20.Image);
            Bitmap image2 = new Bitmap(image.Width, image.Height);

            int offset = imgsize / 2;

            for (int y = offset; y < image.Height - offset; y++) {
                for (int x = offset; x < image.Width - offset; x++) {
                    List<int> rValues = new List<int>();
                    List<int> gValues = new List<int>();
                    List<int> bValues = new List<int>();

                    for (int fy = -offset; fy <= offset; fy++) {
                        for (int fx = -offset; fx <= offset; fx++) {
                            Color pixel = image.GetPixel(x + fx, y + fy);
                            rValues.Add(pixel.R);
                            gValues.Add(pixel.G);
                            bValues.Add(pixel.B);
                        }
                    }

                    int rMedia = (int)rValues.Average();
                    int gMedia = (int)gValues.Average();
                    int bMedia = (int)bValues.Average();

                    Color avgColor = Color.FromArgb(rMedia, gMedia, bMedia);
                    image2.SetPixel(x, y, avgColor);

                }
            }
            pictureBox21.Image = image2;
        }

        //Minimo
        private void minimo() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int imgsize = 3;

            string value = RealceCombobox.Text;
            switch (value) {
                case "3X3":
                imgsize = 3;
                break;
                case "5X5":
                imgsize = 5;
                break;
                case "7X7":
                imgsize = 7;
                break;
            }

            Bitmap image = new Bitmap(pictureBox20.Image);
            Bitmap image2 = new Bitmap(image.Width, image.Height);

            int offset = imgsize / 2;

            for (int y = offset; y < image.Height - offset; y++) {
                for (int x = offset; x < image.Width - offset; x++) {
                    List<int> rValues = new List<int>();
                    List<int> gValues = new List<int>();
                    List<int> bValues = new List<int>();

                    for (int fy = -offset; fy <= offset; fy++) {
                        for (int fx = -offset; fx <= offset; fx++) {
                            Color pixel = image.GetPixel(x + fx, y + fy);
                            rValues.Add(pixel.R);
                            gValues.Add(pixel.G);
                            bValues.Add(pixel.B);
                        }
                    }

                    Color minColor = Color.FromArgb(
                        rValues.Min(),
                        gValues.Min(),
                        bValues.Min()
                    );
                    image2.SetPixel(x, y, minColor);
                }
            }
            pictureBox21.Image = image2;
        }

        //Maxima
        private void maxima() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int imgsize = 3;

            string value = RealceCombobox.Text;
            switch (value) {
                case "3X3":
                imgsize = 3;
                break;
                case "5X5":
                imgsize = 5;
                break;
                case "7X7":
                imgsize = 7;
                break;
            }

            Bitmap image = new Bitmap(pictureBox20.Image);
            Bitmap image2 = new Bitmap(image.Width, image.Height);

            int offset = imgsize / 2;

            for (int y = offset; y < image.Height - offset; y++) {
                for (int x = offset; x < image.Width - offset; x++) {
                    List<int> rValues = new List<int>();
                    List<int> gValues = new List<int>();
                    List<int> bValues = new List<int>();

                    for (int fy = -offset; fy <= offset; fy++) {
                        for (int fx = -offset; fx <= offset; fx++) {
                            Color pixel = image.GetPixel(x + fx, y + fy);
                            rValues.Add(pixel.R);
                            gValues.Add(pixel.G);
                            bValues.Add(pixel.B);
                        }
                    }

                    Color maxColor = Color.FromArgb(
                        rValues.Max(),
                        gValues.Max(),
                        bValues.Max()
                    );

                    image2.SetPixel(x, y, maxColor);
                }
            }
            pictureBox21.Image = image2;
        }

        //Linear
        private void mediaLinear() {
            Bitmap image1 = (Bitmap)pictureBox14.Image;
            Bitmap image2 = (Bitmap)pictureBox22.Image;

            if (image1 == null || image2 == null) {
                MessageBox.Show("As duas imagens devem estar carregadas.");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height) {
                MessageBox.Show("As imagens devem ter o mesmo tamanho.");
                return;
            }

            Bitmap mediaImage = new Bitmap(image1.Width, image1.Height);

            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);

                    int r = (pixel1.R + pixel2.R) / 2;
                    int g = (pixel1.G + pixel2.G) / 2;
                    int b = (pixel1.B + pixel2.B) / 2;

                    mediaImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            pictureBox16.Image = mediaImage;
        }

        //Blending
        private void blending() {
            Bitmap image1 = (Bitmap)pictureBox14.Image;
            Bitmap image2 = (Bitmap)pictureBox22.Image;
            float blendingFactor = (float)numericUpDownBlending.Value / 100f;

            if (image1 == null || image2 == null) {
                MessageBox.Show("As duas imagens devem estar carregadas.");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height) {
                MessageBox.Show("As imagens devem ter o mesmo tamanho.");
                return;
            }

            Bitmap blendedImage = new Bitmap(image1.Width, image1.Height);

            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);

                    int r = (int)(blendingFactor * pixel1.R + (1 - blendingFactor) * pixel2.R);
                    int g = (int)(blendingFactor * pixel1.G + (1 - blendingFactor) * pixel2.G);
                    int b = (int)(blendingFactor * pixel1.B + (1 - blendingFactor) * pixel2.B);

                    // Clamping para garantir que os valores fiquem entre 0 e 255
                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));
                    b = Math.Min(255, Math.Max(0, b));

                    blendedImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            pictureBox16.Image = blendedImage;
        }

        //Ordem
        private void Ordem() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int imgsize = 3;

            string value = RealceCombobox.Text;
            switch (value) {
                case "3X3":
                imgsize = 3;
                break;
                case "5X5":
                imgsize = 5;
                break;
                case "7X7":
                imgsize = 7;
                break;
            }

            int ordem = (int)numericUpDownOrdem.Value;

            Bitmap image = new Bitmap(pictureBox20.Image);
            Bitmap output = new Bitmap(image.Width, image.Height);
            int offset = imgsize / 2;

            for (int x = offset; x < image.Width - offset; x++) {
                for (int y = offset; y < image.Height - offset; y++) {
                    List<byte> intensidades = new List<byte>();

                    for (int i = -offset; i <= offset; i++) {
                        for (int j = -offset; j <= offset; j++) {
                            Color pixel = image.GetPixel(x + i, y + j);
                            byte intensidade = (byte)((pixel.R + pixel.G + pixel.B) / 3); // Escala de cinza
                            intensidades.Add(intensidade);
                        }
                    }

                    intensidades.Sort();

                    // Clamping da ordem
                    int k = Math.Min(Math.Max(ordem - 1, 0), intensidades.Count - 1);
                    byte valor = intensidades[k];

                    Color novoPixel = Color.FromArgb(valor, valor, valor);
                    output.SetPixel(x, y, novoPixel);
                }
            }

            pictureBox21.Image = output;
        }

        //Parte Visual -- IGNORAR --
        private void Form1_Load(object sender, EventArgs e) {
            // Configura o tipo de gráfico como colunas
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            // Ajustes de visualização do eixo X e barras
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.Series[0]["PointWidth"] = "1";
            chart1.Series[0].IsVisibleInLegend = true;
            chart1.Series[0].LegendText = "Histograma Original";

            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 255;
            chart2.Series[0]["PointWidth"] = "1";
            chart2.Series[0].IsVisibleInLegend = true;
            chart2.Series[0].LegendText = "Histograma Equalizado";
        }

        //Histograma
        private void histograma() {
            if (pictureBox23.Image == null) {
                MessageBox.Show("Carregue uma imagem primeiro.");
                return;
            }

            Bitmap imagem = new Bitmap(pictureBox23.Image);

            int largura = imagem.Width;
            int altura = imagem.Height;
            int[] histograma = new int[256];
            int[] histogramaEqualizado = new int[256];

            for (int y = 0; y < altura; y++) {
                for (int x = 0; x < largura; x++) {
                    Color cor = imagem.GetPixel(x, y);
                    int tomCinza = (int)(0.299 * cor.R + 0.587 * cor.G + 0.114 * cor.B);
                    histograma[tomCinza]++;
                }
            }

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < 256; i++) {
                chart1.Series[0].Points.AddXY(i, histograma[i]);
            }

            int[] distribuicaoCumulativa = new int[256];
            distribuicaoCumulativa[0] = histograma[0];
            for (int i = 1; i < 256; i++) {
                distribuicaoCumulativa[i] = distribuicaoCumulativa[i - 1] + histograma[i];
            }

            int totalPixels = largura * altura;
            int L = 256;

            for (int i = 0; i < 256; i++) {
                histogramaEqualizado[i] = (distribuicaoCumulativa[i] * (L - 1)) / totalPixels;
            }

            Bitmap imagemEqualizada = new Bitmap(largura, altura);
            int[] novoHistograma = new int[256];

            for (int y = 0; y < altura; y++) {
                for (int x = 0; x < largura; x++) {
                    Color cor = imagem.GetPixel(x, y);
                    int tomCinza = (int)(0.299 * cor.R + 0.587 * cor.G + 0.114 * cor.B);
                    int novoTom = histogramaEqualizado[tomCinza];
                    Color novaCor = Color.FromArgb(novoTom, novoTom, novoTom);
                    imagemEqualizada.SetPixel(x, y, novaCor);
                    novoHistograma[novoTom]++;
                }
            }

            pictureBox24.Image = imagemEqualizada;

            chart2.Series[0].Points.Clear();
            for (int i = 0; i < 256; i++) {
                chart2.Series[0].Points.AddXY(i, novoHistograma[i]);
            }
        }

        //Limpar Histograma
        private void LimparChart() {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.Titles.Clear();
        }

        //Suavização
        private void suavizacao() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int imgsize = 3;

            string value = RealceCombobox.Text;
            switch (value) {
                case "3X3":
                imgsize = 3;
                break;
                case "5X5":
                imgsize = 5;
                break;
                case "7X7":
                imgsize = 7;
                break;
            }

            Bitmap imagemOriginal = new Bitmap(pictureBox20.Image);
            Bitmap imagemResultado = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            int offset = imgsize / 2;

            for (int y = offset; y < imagemOriginal.Height - offset; y++) {
                for (int x = offset; x < imagemOriginal.Width - offset; x++) {
                    List<int> vizinhos = new List<int>();
                    for (int j = -offset; j <= offset; j++) {
                        for (int i = -offset; i <= offset; i++) {
                            if (i == 0 && j == 0) continue; 

                            Color cor = imagemOriginal.GetPixel(x + i, y + j);
                            int tom = (int)(0.299 * cor.R + 0.587 * cor.G + 0.114 * cor.B);
                            vizinhos.Add(tom);
                        }
                    }
                    Color corCentral = imagemOriginal.GetPixel(x, y);
                    int tomCentral = (int)(0.299 * corCentral.R + 0.587 * corCentral.G + 0.114 * corCentral.B);

                    int min = vizinhos.Min();
                    int max = vizinhos.Max();

                    int novoTom = tomCentral;
                    if (tomCentral < min)
                        novoTom = min;
                    else if (tomCentral > max)
                        novoTom = max;

                    Color novaCor = Color.FromArgb(novoTom, novoTom, novoTom);
                    imagemResultado.SetPixel(x, y, novaCor);
                }
            }

            pictureBox21.Image = imagemResultado;
        }

        //Gaussiano
        private void filtroGaussiano() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Bitmap imagemOriginal = new Bitmap(pictureBox20.Image);
            Bitmap imagemSuavizada = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            int tamanhoMascara = 3;
            double sigma = 1.0;

            string value = RealceCombobox.Text;
            switch (value) {
                case "3X3":
                tamanhoMascara = 3;
                break;
                case "5X5":
                tamanhoMascara = 5;
                break;
                case "7X7":
                tamanhoMascara = 7;
                break;
            }

            if (!double.TryParse(SigmaTextBox.Text, out sigma)) {
                MessageBox.Show("Valor de σ inválido. Usando 1.0 como padrão.");
                sigma = 1.0;
            }

            double[,] kernel = GerarKernelGaussiano(tamanhoMascara, sigma);
            int offset = tamanhoMascara / 2;

            for (int y = offset; y < imagemOriginal.Height - offset; y++) {
                for (int x = offset; x < imagemOriginal.Width - offset; x++) {
                    double somaR = 0, somaG = 0, somaB = 0;

                    for (int i = -offset; i <= offset; i++) {
                        for (int j = -offset; j <= offset; j++) {
                            Color corVizinho = imagemOriginal.GetPixel(x + j, y + i);
                            double peso = kernel[i + offset, j + offset];

                            somaR += corVizinho.R * peso;
                            somaG += corVizinho.G * peso;
                            somaB += corVizinho.B * peso;
                        }
                    }

                    int r = Math.Min(255, Math.Max(0, (int)Math.Round(somaR)));
                    int g = Math.Min(255, Math.Max(0, (int)Math.Round(somaG)));
                    int b = Math.Min(255, Math.Max(0, (int)Math.Round(somaB)));

                    imagemSuavizada.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox21.Image = imagemSuavizada;
        }

        //kernerl -- Aplicação Visual não funcionou
        private double[,] GerarKernelGaussiano(int tamanho, double sigma) {
            double[,] kernel = new double[tamanho, tamanho];
            int centro = tamanho / 2;
            double soma = 0;

            for (int y = -centro; y <= centro; y++) {
                for (int x = -centro; x <= centro; x++) {
                    double valor = (1.0 / (2 * Math.PI * sigma * sigma)) *
                                   Math.Exp(-(x * x + y * y) / (2 * sigma * sigma));
                    kernel[y + centro, x + centro] = valor;
                    soma += valor;
                }
            }

            for (int y = 0; y < tamanho; y++) {
                for (int x = 0; x < tamanho; x++) {
                    kernel[y, x] /= soma;
                }
            }

            return kernel;
        }

        //Prewitt
        private void prewitt() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox20.Image;
            Bitmap image2 = new Bitmap(image1.Width, image1.Height);

            // Máscaras de Prewitt
            int[,] Gx = new int[,]
            {
                { -1, 0, 1 },
                { -1, 0, 1 },
                { -1, 0, 1 }
            };

            int[,] Gy = new int[,]
            {
                { -1, -1, -1 },
                {  0,  0,  0 },
                {  1,  1,  1 }
            };

            for (int x = 1; x < image1.Width - 1; x++) {
                for (int y = 1; y < image1.Height - 1; y++) {
                    int sumX = 0;
                    int sumY = 0;

                    for (int i = -1; i <= 1; i++) {
                        for (int j = -1; j <= 1; j++) {
                            Color pixel = image1.GetPixel(x + i, y + j);
                            int gray = (pixel.R + pixel.G + pixel.B) / 3;

                            sumX += gray * Gx[i + 1, j + 1];
                            sumY += gray * Gy[i + 1, j + 1];
                        }
                    }

                    int magnitude = (int)Math.Sqrt(sumX * sumX + sumY * sumY);
                    magnitude = Math.Min(255, Math.Max(0, magnitude)); // Limita entre 0 e 255

                    Color newColor = Color.FromArgb(magnitude, magnitude, magnitude);
                    image2.SetPixel(x, y, newColor);
                }
            }
            pictureBox21.Image = image2;
        }

        //Sobel
        private void sobel() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox20.Image;
            Bitmap image2 = new Bitmap(image1.Width, image1.Height);

            int[,] Gx = new int[,]
            {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };

            int[,] Gy = new int[,]
            {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
            };

            for (int x = 1; x < image1.Width - 1; x++) {
                for (int y = 1; y < image1.Height - 1; y++) {
                    int sumX = 0;
                    int sumY = 0;

                    for (int i = -1; i <= 1; i++) {
                        for (int j = -1; j <= 1; j++) {
                            Color pixel = image1.GetPixel(x + i, y + j);
                            int gray = (pixel.R + pixel.G + pixel.B) / 3;

                            sumX += gray * Gx[i + 1, j + 1];
                            sumY += gray * Gy[i + 1, j + 1];
                        }
                    }

                    int magnitude = (int)Math.Sqrt(sumX * sumX + sumY * sumY);
                    magnitude = Math.Min(255, Math.Max(0, magnitude));

                    Color newColor = Color.FromArgb(magnitude, magnitude, magnitude);
                    image2.SetPixel(x, y, newColor);
                }
            }

            pictureBox21.Image = image2;
        }

        //Laplaciano
        private void laplaciano() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox20.Image;
            Bitmap image2 = new Bitmap(image1.Width, image1.Height);

            int[,] kernel = null;

            string value = comboBox2.Text;
            switch (value) {
                case "3x3, padrao":
                kernel = new int[,]
                {
                {  0, -1,  0 },
                { -1,  4, -1 },
                {  0, -1,  0 }
                };
                break;

                case "3x3, 8 viz":
                kernel = new int[,]
                {
                { -1, -1, -1 },
                { -1,  8, -1 },
                { -1, -1, -1 }
                };
                break;

                default:
                MessageBox.Show("Selecione uma opção válida para a Limiarização.");
                return;
            }

            for (int x = 1; x < image1.Width - 1; x++) {
                for (int y = 1; y < image1.Height - 1; y++) {
                    int sum = 0;

                    for (int i = -1; i <= 1; i++) {
                        for (int j = -1; j <= 1; j++) {
                            Color pixel = image1.GetPixel(x + i, y + j);
                            int gray = (pixel.R + pixel.G + pixel.B) / 3;

                            sum += gray * kernel[i + 1, j + 1];
                        }
                    }

                    sum = Math.Min(255, Math.Max(0, sum));
                    Color newColor = Color.FromArgb(sum, sum, sum);
                    image2.SetPixel(x, y, newColor);
                }
            }

            pictureBox21.Image = image2;
        }

        //Botão para carregar IMAGEM
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


        //Kernel Padrão Morfologicos
        private int[,] ObterElementoEstruturante() {
            string tipo = comboBox3.Text;
            switch (tipo) {
                case "Elemento estruturante quadrado":
                return new int[,]
                {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 }
                };

                case "Quadrado maior":
                return new int[,]
                {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
                };

                case "Elemento estruturante linear vertical":
                return new int[,]
                {
                { 1 },
                { 1 },
                { 1 },
                { 1 },
                { 1 }
                };

                case "Elemento estruturante retangular":
                return new int[,]
                {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
                };

                default:
                MessageBox.Show("Selecione um elemento estruturante.");
                return null;
            }
        }

        private void erosao() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox20.Image;
            Bitmap image2 = new Bitmap(image1.Width, image1.Height);

            int[,] elemento = ObterElementoEstruturante();
            if (elemento == null) return;

            int w = elemento.GetLength(0);
            int h = elemento.GetLength(1);
            int offsetX = w / 2;
            int offsetY = h / 2;

            for (int x = offsetX; x < image1.Width - offsetX; x++) {
                for (int y = offsetY; y < image1.Height - offsetY; y++) {
                    bool erodir = false;

                    for (int i = 0; i < w && !erodir; i++) {
                        for (int j = 0; j < h && !erodir; j++) {
                            if (elemento[i, j] == 1) {
                                Color pixel = image1.GetPixel(x + i - offsetX, y + j - offsetY);
                                int gray = (pixel.R + pixel.G + pixel.B) / 3;
                                if (gray < 128)
                                    erodir = true;
                            }
                        }
                    }

                    image2.SetPixel(x, y, erodir ? Color.Black : Color.White);
                }
            }

            pictureBox21.Image = image2;
        }

        private void dilatacao() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox20.Image;
            Bitmap image2 = new Bitmap(image1.Width, image1.Height);

            int[,] elemento = ObterElementoEstruturante();
            if (elemento == null) return;

            int w = elemento.GetLength(0);
            int h = elemento.GetLength(1);
            int offsetX = w / 2;
            int offsetY = h / 2;

            for (int x = offsetX; x < image1.Width - offsetX; x++) {
                for (int y = offsetY; y < image1.Height - offsetY; y++) {
                    bool dilatar = false;

                    for (int i = 0; i < w && !dilatar; i++) {
                        for (int j = 0; j < h && !dilatar; j++) {
                            if (elemento[i, j] == 1) {
                                Color pixel = image1.GetPixel(x + i - offsetX, y + j - offsetY);
                                int gray = (pixel.R + pixel.G + pixel.B) / 3;
                                if (gray >= 128)
                                    dilatar = true;
                            }
                        }
                    }

                    image2.SetPixel(x, y, dilatar ? Color.White : Color.Black);
                }
            }

            pictureBox21.Image = image2;
        }

        private void abertura() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap original = (Bitmap)pictureBox20.Image;
            Bitmap erodida = new Bitmap(original);
            Bitmap dilatada = new Bitmap(original);

            pictureBox20.Image = original;
            erosao();
            erodida = (Bitmap)pictureBox21.Image;

            pictureBox20.Image = erodida;
            dilatacao();
            dilatada = (Bitmap)pictureBox21.Image;

            pictureBox20.Image = original;
            pictureBox21.Image = dilatada;
        }

        private void fechamento() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap original = (Bitmap)pictureBox20.Image;
            Bitmap dilatada = new Bitmap(original);
            Bitmap erodida = new Bitmap(original);

            pictureBox20.Image = original;
            dilatacao();
            dilatada = (Bitmap)pictureBox21.Image;

            pictureBox20.Image = dilatada;
            erosao();
            erodida = (Bitmap)pictureBox21.Image;

            pictureBox20.Image = original;
            pictureBox21.Image = erodida;
        }

        private void contorno() {
            if (pictureBox20.Image == null) {
                MessageBox.Show("Carregue uma imagem antes de aplicar o filtro.", "Imagem não carregada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap original = (Bitmap)pictureBox20.Image;
            erosao();
            Bitmap erodida = (Bitmap)pictureBox21.Image;
            Bitmap resultado = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++) {
                for (int y = 0; y < original.Height; y++) {
                    int originalGray = (original.GetPixel(x, y).R + original.GetPixel(x, y).G + original.GetPixel(x, y).B) / 3;
                    int erodidaGray = (erodida.GetPixel(x, y).R + erodida.GetPixel(x, y).G + erodida.GetPixel(x, y).B) / 3;

                    int contorno = originalGray - erodidaGray;
                    contorno = Math.Max(0, Math.Min(255, contorno));
                    resultado.SetPixel(x, y, Color.FromArgb(contorno, contorno, contorno));
                }
            }
            pictureBox21.Image = resultado;
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

        //Adição
        private void Page2_btn_union_Click(object sender, EventArgs e) {
            if (pictureBox4.Image == null || pictureBox5.Image == null) {
                MessageBox.Show("Carregue as duas imagens antes de somar.",
                                "Imagens faltando", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox4.Image;
            Bitmap image2 = (Bitmap)pictureBox5.Image;
            if (image1.Width != image2.Width || image1.Height != image2.Height) {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho.",
                                "Dimensões diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap result = new Bitmap(image1.Width, image1.Height);
            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);

                    int R = Math.Min(pixel1.R + pixel2.R, 255);
                    int G = Math.Min(pixel1.G + pixel2.G, 255);
                    int B = Math.Min(pixel1.B + pixel2.B, 255);

                    result.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            }
            pictureBox6.Image = result;
        }

        //Subtração
        private void page2_btn_subtracao_Click(object sender, EventArgs e) {
            if (pictureBox4.Image == null || pictureBox5.Image == null) {
                MessageBox.Show("Carregue as duas imagens antes de subtrair.",
                                "Imagens faltando", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox4.Image;
            Bitmap image2 = (Bitmap)pictureBox5.Image;

            if (image1.Width != image2.Width || image1.Height != image2.Height) {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho.",
                                "Dimensões diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap result = new Bitmap(image1.Width, image1.Height);
            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color pixel1 = image1.GetPixel(i, j);
                    Color pixel2 = image2.GetPixel(i, j);

                    int R = Math.Max(pixel1.R - pixel2.R, 0);
                    int G = Math.Max(pixel1.G - pixel2.G, 0);
                    int B = Math.Max(pixel1.B - pixel2.B, 0);

                    result.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            }
            pictureBox6.Image = result;
        }

        private void pg3_bnt_img_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox7.Image = img1;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            fliplr();
        }

        private void pg3_bnt_img_down_Click(object sender, EventArgs e) {
            flipud();
        }

        private void label8_Click(object sender, EventArgs e) {

        }

        private void pg4_bnt_img1_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox9.Image = img1;
            }
        }

        private void pg4_bnt_img2_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox10.Image = img1;
            }
        }

        private void pg4_bnt_img_dif_Click(object sender, EventArgs e) {
            subplot();
        }

        private void pg2_bt_img_save_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox6.Image);
        }

        private void pg5_btn_carregar_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox14.Image = img1;
            }

            if (bLoadImgOK == true) {
                // Adiciona imagem na PictureBox
                pictureBox14.Image = img1;
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
                pictureBox15.Image = img2;
            }
        }

        private void button1_Click_1(object sender, EventArgs e) {
            limiarizacao();
        }

        private void pg6_btn_carregar_1_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox17.Image = img1;
            }
        }

        private void pg6_btn_carregar_2_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox18.Image = img1;
            }
        }

        private void button2_Click_1(object sender, EventArgs e) {
            logic();
        }

        private void pg_bnt_carregar_realce_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox20.Image = img1;
            }
        }

        private void Pg_bnt_Mean_Click(object sender, EventArgs e) {
            media();
        }

        private void Pg_bnt_Min_Click(object sender, EventArgs e) {
            minimo();
        }

        private void Pg_bnt_Max_Click(object sender, EventArgs e) {
            maxima();
        }

        private void pg_bnt_remove_Click(object sender, EventArgs e) {
            mediana();
        }

        //Multiplicação
        private void btn_pg2_Multiplicacao_Click(object sender, EventArgs e) {
            if (pictureBox4.Image == null || pictureBox5.Image == null) {
                MessageBox.Show("Carregue as duas imagens antes de multiplicar.",
                                "Imagens faltando", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox4.Image;
            Bitmap image2 = (Bitmap)pictureBox5.Image;
            if (image1.Width != image2.Width || image1.Height != image2.Height) {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho.",
                                "Dimensões diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap result = new Bitmap(image1.Width, image1.Height);
            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color p1 = image1.GetPixel(i, j);
                    Color p2 = image2.GetPixel(i, j);

                    int R = Math.Min((p1.R * p2.R) / 255, 255);
                    int G = Math.Min((p1.G * p2.G) / 255, 255);
                    int B = Math.Min((p1.B * p2.B) / 255, 255);

                    result.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            }
            pictureBox6.Image = result;
        }

        //Divisão
        private void bnt_pg2_divisao_Click(object sender, EventArgs e) {
            if (pictureBox4.Image == null || pictureBox5.Image == null) {
                MessageBox.Show("Carregue as duas imagens antes de dividir.",
                                "Imagens faltando", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap image1 = (Bitmap)pictureBox4.Image;
            Bitmap image2 = (Bitmap)pictureBox5.Image;
            if (image1.Width != image2.Width || image1.Height != image2.Height) {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho.",
                                "Dimensões diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Bitmap result = new Bitmap(image1.Width, image1.Height);
            for (int i = 0; i < image1.Width; i++) {
                for (int j = 0; j < image1.Height; j++) {
                    Color p1 = image1.GetPixel(i, j);
                    Color p2 = image2.GetPixel(i, j);
                    int R = (p2.R == 0) ? 255 : Math.Min((p1.R * 255) / p2.R, 255);
                    int G = (p2.G == 0) ? 255 : Math.Min((p1.G * 255) / p2.G, 255);
                    int B = (p2.B == 0) ? 255 : Math.Min((p1.B * 255) / p2.B, 255);

                    result.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            }
            pictureBox6.Image = result;
        }

        private void button3_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox22.Image = img1;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {

        }

        private void button4_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox16.Image);
        }

        private void label12_Click(object sender, EventArgs e) {

        }

        private void bnt_pg5_blending_Click(object sender, EventArgs e) {
            blending();
        }

        private void button5_Click(object sender, EventArgs e) {
            mediaLinear();
        }

        private void bnt_Ordem_Click(object sender, EventArgs e) {
            Ordem();
        }

        private void button6_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = updateImage();

            if (imagemCarregada == true) {
                pictureBox23.Image = img1;
            }
        }

        private void button7_Click(object sender, EventArgs e) {
            histograma();
        }

        private void button8_Click(object sender, EventArgs e) {
            suavizacao();
        }

        private void button9_Click(object sender, EventArgs e) {
            filtroGaussiano();
        }

        private void btn_prewitt_Click(object sender, EventArgs e) {
            prewitt();
        }

        private void button10_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox21.Image);
        }

        private void bnt_sobel_Click(object sender, EventArgs e) {
            sobel();
        }

        private void bnt_laplaciano_Click(object sender, EventArgs e) {
            laplaciano();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void button11_Click(object sender, EventArgs e) {
            dilatacao();
        }

        private void button13_Click(object sender, EventArgs e) {
            erosao();
        }

        private void button15_Click(object sender, EventArgs e) {
            fechamento();
        }

        private void button14_Click(object sender, EventArgs e) {
            abertura();
        }

        private void button12_Click(object sender, EventArgs e) {
            contorno();
        }

        private void button16_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox3);
        }

        private void button17_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox1);
            ClearPictureBox(pictureBox2);
        }

        private void button19_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox2.Image);
        }

        private void button18_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox3.Image);
        }

        private void button20_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox8.Image);
        }

        private void button22_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox6);
        }

        private void button21_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox4);
            ClearPictureBox(pictureBox5);
        }

        private void button23_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox7);
        }

        private void button24_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox8);
        }

        private void pictureBox13_Click(object sender, EventArgs e) {

        }

        private void button25_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox9);
            ClearPictureBox(pictureBox10);
        }

        private void button26_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox11);
            ClearPictureBox(pictureBox12);
            ClearPictureBox(pictureBox13);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {

        }

        private void button28_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox17);
            ClearPictureBox(pictureBox18);
        }

        private void button29_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox19);

        }

        private void button27_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox19.Image);
        }

        private void button30_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox20);
        }

        private void button31_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox21);
        }

        private void button32_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox14);
            ClearPictureBox(pictureBox16);
        }

        private void button33_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox22);
        }

        private void button34_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox23);
        }

        private void button35_Click(object sender, EventArgs e) {
            ClearPictureBox(pictureBox24);
            LimparChart();
        }

        private void button36_Click(object sender, EventArgs e) {
            SalvarImagem(pictureBox24.Image);
        }

        private void chart1_Click(object sender, EventArgs e) {

        }
    }
}
