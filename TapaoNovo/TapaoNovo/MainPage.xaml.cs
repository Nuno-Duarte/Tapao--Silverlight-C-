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
        double cronometroTempoE;
        double cronTempoEspM = 0;
        double cronometroAparecer;
        double tempoAparecer;
        double tempoAparecerMin = 0.5;
        double tempoAparecerMax = 0.8;

        bool jogando;
        bool podeApertarTecla = false;
        bool esperandoApertarTecla = false;

        KeyManager keyManager = new KeyManager();

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
        void Start()
        {
            RndTempoAparecer();
            baralho.CriarCartas();
            //lCartaC.Visibility = Visibility.Collapsed;
        }

        void RndTempoAparecer()
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
                            //lCartaC.Visibility = Visibility.Visible;
                            lCartaC.Opacity = 1;
                            lCartaC.Text = "" + baralho.valor[cartaContagem];
                            iCartaMeio.Opacity = 1;
                            if (cartaContagem == baralho.CartaAtual().valorN)
                            {
                                Esperar(3);
                                /*if (j == true)
                                {
                                    jogador2.pontos++;
                                    j = false;
                                }
                                if (a == true)
                                {
                                    jogador1.pontos++;
                                    a = false;
                                }*/
                                //tecla J é do player 2 
                                // tecla a é do player 1
                                //podeApertarTecla = true;
                                cartaContagem = 0;
                            }
                            else
                            {
                                /*if (j == true)
                                {
                                    jogador2.pontos++;
                                        j = false;
                                }
                                if (a == true)
                                {
                                    jogador1.pontos++;
                                        a = false;
                                }*/
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
                /*if (tempoEsperar >= 0)
                {
                    cronometroTempoE += deltaTime;
                    if (cronometroTempoE >= 1)
                    {
                        cronometroTempoE = 0;
                        tempoEsperar--;
                        lEsperar.Text = "Espere " + tempoEsperar + " segundos";
                        if (tempoEsperar <= -1)
                        {
                            jogando = true;
                            lEsperar.Visibility = Visibility.Collapsed;
                        }
                    }
                }*/
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
            /*if (keyManager.isPressed(Key.A))
            {
                a = true;
            }

            if (keyManager.isPressed(Key.J))
            {
                j = true;
            }*/

        }

        void EsperarMenu(int tempo)
        {
            jogando = false;
            cronTempoEspM = tempo;
        }

        void Esperar(int tempo)
        {
            jogando = false;
            tempoEsperar = tempo;
            cronometroTempoE = 0;
            //lEsperar.Visibility = Visibility.Visible;
            //lEsperar.Text = "Espere " + tempoEsperar + " segundos";
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
                //LevelJogo.KeyDown += new KeyEventHandler(OnKeyDownHandler);
                //LevelJogo.KeyUp += new KeyEventHandler(OnKeyUp);
                //Esperar(3);
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
            /*if (sender == teclaA)
            {
                a = true;
            }
            if (sender == teclaJ)
            {
                j = true;
            }*/

        }
        /*private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.J)
            {
                j = true;
            }
            if (e.Key == Key.A)
            {
                a = true;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //Util.Debug.Log("keydown=" + e.Key);
            keyManager.Add(e.Key);

            if (e.Key.Equals(Key.A))
            {
                if (jogando)
                {
                    if (podeApertarTecla)
                    {
                        podeApertarTecla = false;
                        jogador1.pontos += qntCartaPassou;
                        lPontosJ1.Text = "Pontos: " + jogador1.pontos;
                    }
                    else
                    {
                        jogador2.pontos += qntCartaPassou;
                        lPontosJ2.Text = "Pontos: " + jogador2.pontos;
                    }
                    qntCartaPassou = 0;
                    cartaContagem = 0;
                    Esperar(1);
                }
            }
            if (e.Key.Equals(Key.L))
            {
                if (jogando)
                {
                    if (podeApertarTecla)
                    {
                        podeApertarTecla = false;
                        jogador2.pontos += qntCartaPassou;
                        lPontosJ2.Text = "Pontos: " + jogador2.pontos;
                    }
                    else
                    {
                        jogador1.pontos += qntCartaPassou;
                        lPontosJ1.Text = "Pontos: " + jogador1.pontos;
                    }
                    qntCartaPassou = 0;
                    cartaContagem = 0;
                    Esperar(1);
                }
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            //Util.Debug.Log("keyup=" + e.Key);
            //keyManager.Remove(e.Key);
        }*/

        /*private void fundoJogo_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }*/
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            //Util.Debug.Log("keydown=" + e.Key);
            keyManager.Add(e.Key);

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
            /*if (keyManager.isPressed(Key.A))
            {
                Esperar(10);
                //a = true;
            }
            if (keyManager.isPressed(Key.J))
            {
                j = true;
            }*/
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            //Util.Debug.Log("keyup=" + e.Key);
            keyManager.Remove(e.Key);
        }
    }
}
