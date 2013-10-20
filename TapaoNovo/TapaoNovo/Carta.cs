using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Carta
    {
        //Image imagem;
        public ValorCarta valor;
        public int valorN;
        public NaipeCarta naipe;
        public BitmapImage imagem;
        //public string caminho;

        public Carta()
        {
            
        }

        public Carta(string localImagem)
        {
            imagem = new BitmapImage(new Uri(localImagem, UriKind.Relative));
            //caminho = localImagem;
        }

        public Carta(string localImagem, ValorCarta valor, NaipeCarta naipe, int valorN)
        {
            imagem = new BitmapImage(new Uri(localImagem, UriKind.Relative));
            //caminho = localImagem;
            this.valor = valor;
            this.valorN = valorN;
            this.naipe = naipe; 
        }
    }



        /*/public Carta(Vetor2 posicao)
        {
            this.posicao.x = posicao.x;
            this.posicao.y = posicao.y;
        }/*/
    //}
}
