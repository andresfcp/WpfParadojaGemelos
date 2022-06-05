using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WpfParadojaGemelos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Dato> infoTabla = new ObservableCollection<Dato>();

        public MainWindow()
        {
            InitializeComponent();


        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            calcular();
        }


        private void calcular()
        {
            //double tViajero = double.Parse(txtTiempo.Text);
            //double porcentajeC = double.Parse(txtVelocidad.Text,CultureInfo.InvariantCulture);

            double tViajero = sldTiempoV.Value;
            double porcentajeC = sldPorcentajeC.Value;

            double tObservador = tViajero / (Math.Sqrt(1 - (Math.Pow(porcentajeC, 2) / 10000)));

            lblTiempo2.Visibility = Visibility.Visible;
            lblResultado.Content = tObservador.ToString();

            infoTabla.Add(new Dato() { Tiempo_Viajero = tViajero, Porcentaje_C = porcentajeC, Tiempo_Observador = tObservador });
            DGDatos.ItemsSource = infoTabla;
        }

        private void txtTiempo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
                txtVelocidad.Focus();

        }

        private void txtVelocidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
                btnCalcular.Focus();
        }

        private void btnLimpiarGrid_Click(object sender, RoutedEventArgs e)
        {
            infoTabla.Clear();
            DGDatos.ItemsSource = infoTabla;
        }

        public class Dato
        {
            public double Tiempo_Viajero { get; set; }
            public double Porcentaje_C { get; set; }
            public double Tiempo_Observador { get; set; }
        }

        private void btnEliminarElemento_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
