using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AppClockNosferatu
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int MaximoNoite = 8;
        private int NoiteJogo = 0;
        private Stack<int> Cartas = new Stack<int>();
        private int Jogador=1;


        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void BtnRodar_Click(object sender, RoutedEventArgs e)
        {

            ImgRelogioDia.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ImgRelogioNoite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            int selecionado = this.Cartas.Pop();
            if (selecionado == 0) {
                ImgRelogioNoite.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                ImgRelogioDia.Visibility = Windows.UI.Xaml.Visibility.Visible;
                BtnRodar.IsEnabled = false;
            }
            this.Jogador++;

            this.ExibeJogador();
        }

        private void ExibeJogador()
        {
            TxtRodada.Text = "Jogador " + this.Jogador;
        }

        private void BtnDelNoite_Click(object sender, RoutedEventArgs e)
        {
            this.NoiteJogo -= 1;
            this.MontarCartas();
        }

        private void BtnAddNoite_Click(object sender, RoutedEventArgs e)
        {
            this.NoiteJogo += 1;
            this.MontarCartas();

            if (NoiteJogo == MaximoNoite)
                BtnAddNoite.IsEnabled = false;
        }

        private void TxtNumeroJogadores_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.NoiteJogo = Convert.ToInt32(TxtNumeroJogadores.Text);
            ((TextBox)sender).IsEnabled = false;

            this.MontarCartas();

        }

        private void MontarCartas()
        {
            List<int> n = new List<int>();
            for (int i = 0; i < this.NoiteJogo; i++)
            {
                n.Add(0);
            }
            n.Add(1);
            n.Shuffle();

            foreach (var item in n)
            {
                Cartas.Push(item);
            }

        }

        private void BtnZerar_Click(object sender, RoutedEventArgs e)
        {
            Cartas = new Stack<int>();
            ImgRelogioDia.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ImgRelogioNoite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            BtnAddNoite.IsEnabled = true;
            BtnDelNoite.IsEnabled = true;
            BtnRodar.IsEnabled = true;
            this.Jogador = 1;
            this.ExibeJogador();

            this.MontarCartas();
        }

    }
}
