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
        public ObservableCollection<Dato> valores = new ObservableCollection<Dato>();

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

            //lblTiempo2.Visibility = Visibility.Visible;
            lblTiempo2.Content = "Tiempo del Observador";
            lblResultado.Content = tObservador.ToString();

            valores.Add(new Dato() { Tiempo_Viajero = tViajero, Porcentaje_C = porcentajeC, Tiempo_Observador = tObservador });
            DGDatos.ItemsSource = valores;
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
            valores.Clear();
            DGDatos.ItemsSource = valores;
        }

        private void btnEliminarElemento_Click(object sender, RoutedEventArgs e)
        {
            Object obj = DGDatos.SelectedValue;
            Dato? dato = obj as Dato;
            if (dato != null)
            {
                //MessageBox.Show(dato.Porcentaje_C.ToString());
                valores.Remove(dato);
                DGDatos.ItemsSource = valores;
            }
            else
                MessageBox.Show("Seleccione un elemento de la lista","Advertencia");
        }

        public class Dato
        {
            public double Tiempo_Viajero { get; set; }
            public double Porcentaje_C { get; set; }
            public double Tiempo_Observador { get; set; }
        }


        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();          
        }
    }

}
