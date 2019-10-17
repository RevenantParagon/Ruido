using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DIPLi;

namespace Ruido
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Imagem image;
        Imagem Salt_Pepper;
        public bool Abrir()
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Selecionar Imagem";
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            openFileDialog1.FileName = "";

            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string file = openFileDialog1.FileName.ToString();
                image = new Imagem(file);
                return true;
            }
            else
                return false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(Abrir())
            {
                pictureBox1.Image = image.ToBitmap();
                image = image.ToGrayscale();
            }

            double media = 0;

            for (int i = 0; i < image.Altura; i++)
            {
                for (int j = 0; j < image.Largura; j++)
                {
                    media = media + image[i, j];
                }
            }
            media = media / (image.Largura * image.Altura);

            label1.Text = media.ToString();
        }

        public Imagem SaltPepper(Imagem image)
        {
            Random random = new Random();

            Imagem resultante = new Imagem(image.Largura, image.Altura, image.Tipo);

            for (int i = 0; i < image.Altura; i++)
            {
                for (int j = 0; j < image.Largura; j++)
                {
                    if (random.Next(1, 100) < 2)
                    {
                        resultante[i, j] = 255;
                    }
                    else if (random.Next(1, 100) < 2)
                    {
                        resultante[i, j] = 0;
                    }
                    else
                    {
                        resultante[i, j] = image[i, j];
                    }
                }
            }
            return resultante;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Salt_Pepper = SaltPepper(image);
            pictureBox2.Image = Salt_Pepper.ToBitmap();
        }

        public Imagem Mediana(Imagem imagem)
        {
            Imagem resultante = new Imagem(imagem.Largura, imagem.Altura, imagem.Tipo);
            double[] mediana = new double[9];
            for (int i = 0; i < imagem.Altura; i++)
            {
                for (int j = 0; j < imagem.Largura; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        mediana.SetValue(imagem[i, j], 0);
                        mediana.SetValue(imagem[i, j], 1);
                        mediana.SetValue(imagem[i, j + 1], 2);
                        mediana.SetValue(imagem[i, j], 3);
                        mediana.SetValue(imagem[i, j], 4);
                        mediana.SetValue(imagem[i, j + 1], 5);
                        mediana.SetValue(imagem[i + 1, j], 6);
                        mediana.SetValue(imagem[i + 1, j], 7);
                        mediana.SetValue(imagem[i + 1, j + 1], 8);
                    }
                    else if (i == 0)
                    {
                        mediana.SetValue(imagem[i, j - 1], 0);
                        mediana.SetValue(imagem[i, j], 1);
                        mediana.SetValue(imagem[i, j + 1], 2);
                        mediana.SetValue(imagem[i, j - 1], 3);
                        mediana.SetValue(imagem[i, j], 4);
                        mediana.SetValue(imagem[i, j + 1], 5);
                        mediana.SetValue(imagem[i + 1, j - 1], 6);
                        mediana.SetValue(imagem[i + 1, j], 7);
                        mediana.SetValue(imagem[i + 1, j + 1], 8);
                    }
                    else if (j == 0)
                    {
                        mediana.SetValue(imagem[i - 1, j], 0);
                        mediana.SetValue(imagem[i - 1, j], 1);
                        mediana.SetValue(imagem[i - 1, j + 1], 2);
                        mediana.SetValue(imagem[i, j], 3);
                        mediana.SetValue(imagem[i, j], 4);
                        mediana.SetValue(imagem[i, j + 1], 5);
                        mediana.SetValue(imagem[i + 1, j], 6);
                        mediana.SetValue(imagem[i + 1, j], 7);
                        mediana.SetValue(imagem[i + 1, j + 1], 8);
                    }
                    else if (i == imagem.Altura && j == imagem.Largura)
                    {
                        mediana.SetValue(imagem[i - 1, j - 1], 0);
                        mediana.SetValue(imagem[i - 1, j], 1);
                        mediana.SetValue(imagem[i - 1, j], 2);
                        mediana.SetValue(imagem[i, j - 1], 3);
                        mediana.SetValue(imagem[i, j], 4);
                        mediana.SetValue(imagem[i, j], 5);
                        mediana.SetValue(imagem[i, j - 1], 6);
                        mediana.SetValue(imagem[i, j], 7);
                        mediana.SetValue(imagem[i, j], 8);
                    }
                    else if (i == imagem.Altura)
                    {
                        mediana.SetValue(imagem[i - 1, j - 1], 0);
                        mediana.SetValue(imagem[i - 1, j], 1);
                        mediana.SetValue(imagem[i - 1, j + 1], 2);
                        mediana.SetValue(imagem[i, j], 3);
                        mediana.SetValue(imagem[i, j], 0);
                        mediana.SetValue(imagem[i, j], 0);
                        mediana.SetValue(imagem[i, j], 0);
                        mediana.SetValue(imagem[i, j], 0);
                        mediana.SetValue(imagem[i, j], 0);
                    }
                    else if (i == imagem.Largura)
                    {
                        mediana.SetValue(imagem[i, j], 0);
                    }
                }
            }

            return resultante;
        }
    }
}
