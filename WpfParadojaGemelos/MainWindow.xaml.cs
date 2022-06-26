using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            MoverCohete();
        }


        private async void MoverCohete()
        {
            int x = 0;
            TranslateTransform mover = new TranslateTransform();
            for (int i = 0; i < 300; i++)
            {
                if (i % 10 == 0)
                {
                    x = x + 10;
                    mover.X = x;
                    imgCohete.RenderTransform = mover;
                    await Task.Delay(25);
                }
            }
            for (int i = 0; i < 300; i++)
            {
                if (i % 10 == 0)
                {
                    x = x - 10;
                    mover.X = x;
                    imgCohete.RenderTransform = mover;
                    await Task.Delay(25);
                }
            }
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
        
        private void Graficar()
        {
            var plotModel1 = new PlotModel();
            plotModel1.Title = "% Vel Luz v Tiempo Observador ";

            var ejeX = new LinearAxis();
            ejeX.MajorGridlineStyle = LineStyle.Solid;
            ejeX.MinorGridlineStyle = LineStyle.Dot;
            //ejeX.Minimum = 0;
            plotModel1.Axes.Add(ejeX);

            var ejeY = new LinearAxis();
            ejeY.Position = AxisPosition.Bottom;
            plotModel1.Axes.Add(ejeY);
            //Hasta aqui todo es común, ahora es asociar el ObservableCollection con el gráfico

            //var lista = new Modelo(); //esto seria valores
            var linea = new LineSeries();
            foreach (var dato in valores)
            {
                linea.Points.Add(new DataPoint(dato.Porcentaje_C,dato.Tiempo_Observador));
            }
            
            linea.Title = "Valores 1";
            plotModel1.Series.Add(linea);
            Grafica.Model = plotModel1;
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



        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();          
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            Graficar();
        }

    }

}
