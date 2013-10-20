using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace TapaoNovo
{
    public class Imagem
    {
        private Image imagem = new Image();
        private double _largura;
        private double _altura;
        private double _opacidade;

        public Image getImagem()
        {
            return imagem;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="local"></param>
        public void setImagem(string local)
        {
            imagem.Source = CriarImagem(local);
            imagem.Width = _largura;
            imagem.Height = _altura;
        }

       /*/ public void posicaoImagem(Vetor2 posicao)
        {
            imagem.SetValue(Canvas.TopProperty, posicao.y);
            imagem.SetValue(Canvas.LeftProperty, posicao.x);
        }/*/

        /// <summary>
        /// Retorna ou Atribiu a largura da imagem
        /// </summary>
        public double largura
        {
            get{ return _largura;}
            set
            {
                _largura = value;
                imagem.Width = value;
            }
        }
        /// <summary>
        /// Retorna ou Atribiu a altura da imagem
        /// </summary>
        public double altura
        {
            get { return _altura; }
            set
            {
                _altura = value;
                imagem.Height = value;
            }
        }

        /// <summary>
        /// Retorna ou Atribiu a opacidade da imagem
        /// </summary>
        public double opacidade
        {
            get { return _opacidade; }
            set
            {
                _opacidade = value;
                imagem.Opacity = value;
            }
        }

        public Imagem(string local)
        {
            //../Texturas/Coringa.jpg
            imagem.Source = CriarImagem(local);
            //imagem.Width = 30;
            //imagem.Height = 30;
        }

        public Imagem(string local, double opacidade)
        {
            imagem.Source = CriarImagem(local);
            imagem.Opacity = opacidade;
        }

        public Imagem(string local, double largura, double altura)
        {
            imagem.Source = CriarImagem(local);
            imagem.Width = largura;
            imagem.Height = altura;
        }

        public Imagem(string local, double largura, double altura, double opacidade)
        {
            imagem.Source = CriarImagem(local);
            imagem.Width = largura;
            imagem.Height = altura;
            imagem.Opacity = opacidade;
        }

        private BitmapImage CriarImagem(string local)
        {
            return new BitmapImage(new Uri("../" + local, UriKind.Relative));
        }
    }
}
