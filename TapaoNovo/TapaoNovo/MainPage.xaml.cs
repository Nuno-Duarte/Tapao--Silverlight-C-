using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TapaoNovo
{
    public partial class MainPage : UserControl
    {
#region fields
        private const string TIRO = "tiro";
        private const string ALVO = "alvo";
        DateTime start;
        Baralho baralho = new Baralho();
        Jogador jogador1 = new Jogador();
        Jogador jogador2 = new Jogador();
        private bool j;
        private bool a;
        Level level = Level.Menu;
        int tempoEsperar = 3;
        int cartaContagem = 0;
        int qntCartaPassou = 0;
        double cronTempoEspM = 0;
        double cronometroAparecer;
        double tempoAparecer;
        double tempoAparecerMin = 0.5;
        double tempoAparecerMax = 0.8;

        bool jogando;
        bool podeApertarTecla = false;
#endregion

        /**
         * Construtor 
         */
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PageLoaded);
        }

        /**
         * Pagina carregada
         */
        protected void PageLoaded(object sender, EventArgs e)
        {
            Application.Current.Host.Settings.MaxFrameRate = 25;
            Start();
            CompositionTarget.Rendering += Update;
        }

        /**
         * Configuracoes iniciais do jogo
         */
        private void Start()
        {
            RndTempoAparecer();
            baralho.CriarCartas();
        }

        private void RndTempoAparecer()
        {
            Random random = new Random();
            tempoAparecer = (random.NextDouble() * tempoAparecerMax) + tempoAparecerMin;
            cronometroAparecer = tempoAparecer;
        }

        /**
         * Loop principal
         */
        protected void Update(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - start).Milliseconds / 1000.0;
            start = now;

            if (level == Level.Jogo)
            {
                a = false;
                j = false;
                if (jogando)
                {
                    if (!podeApertarTecla)
                    {
                        cronometroAparecer -= deltaTime;
                        iCartaMeio.Opacity = (tempoAparecer - (tempoAparecer - cronometroAparecer)) / tempoAparecer + 0.2;
                        lCartaC.Opacity = (tempoAparecer - (tempoAparecer - cronometroAparecer)) / tempoAparecer + 0.2;
                        if (cronometroAparecer <= 0)
                        {
                            iCartaMeio.Source = baralho.CartaAtual().imagem;
                            lCartaC.Opacity = 1;
                            lCartaC.Text = "" + baralho.valor[cartaContagem];
                            iCartaMeio.Opacity = 1;
                            if (cartaContagem == baralho.CartaAtual().valorN)
                            {
                                Esperar(3);
                                cartaContagem = 0;
                            }
                            else
                            {
                                cartaContagem++;
                                if (cartaContagem > 12)
                                {
                                    cartaContagem = 0;
                                }
                            }
                            qntCartaPassou++;
                            baralho.contador++;
                            if (baralho.contador >= 52)
                            {
                                jogando = false;
                                baralho.contador = cartaContagem = 0;
                                EsperarMenu(3);
                                lCartaC.Text = "Fim de Jogo";
                                if (jogador1.pontos == jogador2.pontos)
                                {
                                    lEsperar.Text = "Empatou!";
                                    teclaInicial.Visibility = Visibility.Collapsed;
                                }
                                else if (jogador1.pontos > jogador2.pontos)
                                {
                                    lEsperar.Text = "Jogador 1 Venceu Jogador 2";
                                    teclaInicial.Visibility = Visibility.Collapsed;
                                }
                                else if (jogador1.pontos < jogador2.pontos)
                                {
                                    lEsperar.Text = "Jogador 2 Venceu Jogador 1";
                                    teclaInicial.Visibility = Visibility.Collapsed;
                                }
                                lEsperar.Visibility = Visibility.Visible;
                                lCartaC.Visibility = Visibility.Visible;
                                lCartaC.Opacity = 1;
                            }
                            RndTempoAparecer();
                        }
                    }
                    lPontosJ1.Text = "" + jogador1.pontos;
                    lPontosJ2.Text = "" +jogador2.pontos;
                }
                if (cronTempoEspM > 0)
                {
                    cronTempoEspM -= deltaTime;
                    if (cronTempoEspM < 0)
                    {
                        level = Level.Menu;
                        lCartaC.Text = "";
                        lPontosJ1.Text = lPontosJ2.Text = "Pontos: ";
                        LevelJogo.Visibility = Visibility.Collapsed;
                        LevelMenu.Visibility = Visibility.Visible;
                        teclaInicial.Visibility = Visibility.Visible;
                        lEsperar.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void EsperarMenu(int tempo)
        {
            jogando = false;
            cronTempoEspM = tempo;
        }

        private void Esperar(int tempo)
        {
            jogando = false;
            tempoEsperar = tempo;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender == bVoltarCreditos)
            {
                LevelCreditos.Visibility = Visibility.Collapsed;
                LevelMenu.Visibility = Visibility.Visible;
                level = Level.Menu;
            }
            if (sender == bVoltarInstrucoes)
            {
                LevelInstrucoes.Visibility = Visibility.Collapsed;
                LevelMenu.Visibility = Visibility.Visible;
                level = Level.Menu;
            }
            if (sender == bJogarMenu)
            {
                LevelMenu.Visibility = Visibility.Collapsed;
                LevelJogo.Visibility = Visibility.Visible;
                level = Level.Jogo;
                iCartaMeio.Source = null;
                baralho.Embaralhar();
                jogador1.pontos = jogador2.pontos = 0;
            }
            if (sender == bCreditosMenu)
            {
                LevelMenu.Visibility = Visibility.Collapsed;
                LevelCreditos.Visibility = Visibility.Visible;
                level = Level.Creditos;
            }
            if (sender == bInstruçõesMenu)
            {
                LevelMenu.Visibility = Visibility.Collapsed;
                LevelInstrucoes.Visibility = Visibility.Visible;
                level = Level.Instrucoes;
            }
            if (sender == teclaInicial)
            {
                if(!jogando)
                jogando = true;
            }

        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {

            if (e.Key.Equals(Key.A))
            {
                if (!jogando && j == false)
                {
                    a = true;
                    jogador1.pontos++;
                    tempoEsperar = 0;
                    jogando = true;
                }
            }
            if (e.Key.Equals(Key.J))
            {
                if (!jogando && a == false)
                {
                    j = true;
                    jogador2.pontos++;
                    tempoEsperar = 0;
                    jogando = true;
                }
            }
        }
    }
}
