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
using System.Reflection;

namespace TapaoNovo
{
    public class Baralho
    {
        public Carta[] carta = new Carta[52];
        public ValorCarta[] valor = new ValorCarta[13];
        public int contador;

        public Baralho()
        {
            CriarCartas();
        }

        public bool Acabou()
        {
            if (contador == carta.Length - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Carta CartaAtual()
        {
            Carta temp = carta[contador];
            return temp;
        }

        public void Embaralhar()
        {
            Random random = new Random();
            for (int i = 0; i < carta.Length - 1; i++)
            {
                int idRandom = random.Next(i + 1, carta.Length - 1);
                Carta tempCarta = carta[i];
                carta[i] = carta[idRandom];
                carta[idRandom] = tempCarta;
            }
        }

        public void CriarCartas()
        {
            /*/NaipeCarta[] naipes = new NaipeCarta[4];
            ValorCarta[] valores = new ValorCarta[13];/*/
            ValorCarta v = new ValorCarta();
            NaipeCarta n = new NaipeCarta();

            int contador = 0;

            Type tipoNaipe = n.GetType();
            Type tipoValor = v.GetType();

            FieldInfo[] naipe = tipoNaipe.GetFields();
            FieldInfo[] valor = tipoValor.GetFields();

            for (int i = 1; i < naipe.Length; i++)
            {
                for (int j = 1; j < valor.Length; j++)
                {
                    //if (naipe[i].GetType() == tipoNaipe && valor[j].GetType() == tipoValor)
                    //{
                    Carta tempCarta = new Carta("Imagens/Cartas/" + valor[j].Name + naipe[i].Name + ".jpg", (ValorCarta)Enum.Parse(typeof(ValorCarta), valor[j].Name, true), (NaipeCarta)Enum.Parse(typeof(NaipeCarta), naipe[i].Name, true), j - 1);
                    //Carta tempCarta = new Carta("/Tapao;component/Imagens/Cartas" + valor[j].Name + naipe[i].Name + "jpg", (ValorCarta)Enum.Parse(typeof(ValorCarta), valor[j].Name, true), (NaipeCarta)Enum.Parse(typeof(NaipeCarta), naipe[i].Name, true));
                    //Carta tempCarta = new Carta("Texturas/Cartas" + valor[j].Name + naipe[i].Name, ValorCarta.As, NaipeCarta.Copas);
                    carta[contador] = tempCarta;
                    contador++;
                    this.valor[j - 1] = (ValorCarta)Enum.Parse(typeof(ValorCarta), valor[j].Name, true);
                    //}
                }
            }

            /*/foreach (var naipe in n.GetType().GetFields())
            {
                foreach (var valor in v.GetType().GetFields())
                {
                        Carta tempCarta = new Carta(valor.Name + naipe.Name);
                        carta[contador] = tempCarta;
                        contador++;

                }
            }/*/
            /*/
            for (int i = 0; i < carta.Length - 1; i++)
            {
                Carta tempCarta = new Carta();
                carta[i] = tempCarta;
            }/*/
        }
    }
}
