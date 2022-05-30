using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfParadojaGemelos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

          
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            double tObservador = 0;
            double tViajero = double.Parse(txtTiempo.Text);
            double porcentajeC = double.Parse(txtVelocidad.Text);

            tObservador = tViajero / (Math.Sqrt(1 - (Math.Pow(porcentajeC,2) / 10000)));

            lblTiempo2.Visibility = Visibility.Visible;
            lblResultado.Content = tObservador.ToString();
        }

    }
}
