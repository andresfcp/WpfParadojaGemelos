using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            btnGrafico.Visibility = Visibility.Hidden;
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            calcular();
            //MoverCohete();
        }


        private async void MoverCohete()
        {
            int y = 0;
            TranslateTransform mover = new TranslateTransform();
            for (int i = 0; i < 81; i++)
            {
                y = y - 4;
                mover.Y = y;
                imgCohete.RenderTransform = mover;
                await Task.Delay(1);
            }
            for (int i = 0; i < 81; i++)
            {                
                y = y + 4;
                mover.Y = y;
                imgCohete.RenderTransform = mover;
                await Task.Delay(1);
            }
        }


        private void calcular()
        {
            //double tViajero = double.Parse(txtTiempo.Text);
            //double porcentajeC = double.Parse(txtVelocidad.Text,CultureInfo.InvariantCulture);

            double tViajero = sldTiempoV.Value;
            double porcentajeC = sldPorcentajeC.Value;
            double masa = Convert.ToDouble(txtMasaReposo.Text);

            double tObservador = tViajero / (Math.Sqrt(1 - (Math.Pow(porcentajeC, 2) / 10000)));
            double masaRelativa = masa / (Math.Sqrt(1-(Math.Pow(porcentajeC/100,2))));

            //lblTiempo2.Visibility = Visibility.Visible;
            lblTiempo2.Content = "Tiempo del Observador";
            lblResultado.Content = tObservador.ToString();
            if (masa != 0)
            {
                txtMasaRelativa.Text = masaRelativa.ToString();
            }

            valores.Add(new Dato() { Tiempo_Viajero = tViajero, Porcentaje_C = porcentajeC, Tiempo_Observador = tObservador, Masa_Relativa = masaRelativa });
            DGDatos.ItemsSource = valores;
        }

        private void autoCalcular()
        {
            double tViajero = sldTiempoV.Value;
            double tObservador;
			double masa = Convert.ToDouble(txtMasaReposo.Text);
            double porcentajeC;
			double masaRelativa;

            for (int i = 0; i < 100; i++)
            {
                tObservador = sldTiempoV.Value / (Math.Sqrt(1 - (Math.Pow(i, 2) / 10000)));
                porcentajeC = Convert.ToDouble(i) / 100;
				masaRelativa = masa / (Math.Sqrt(1 - (Math.Pow(porcentajeC, 2))));
				lblTiempo2.Content = "Tiempo del Observador";
                lblResultado.Content = tObservador.ToString();

                valores.Add(new Dato() { Tiempo_Viajero = tViajero, Porcentaje_C = i, Tiempo_Observador = tObservador, Masa_Relativa = masaRelativa });
            }
            DGDatos.ItemsSource = valores;
        }


        #region EVENTOS
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
            MessageBoxResult resultado;
            resultado = MessageBox.Show("Está seguro de limpiar los datos de la pantalla", "Paradoja de los Gemelos",MessageBoxButton.YesNo,MessageBoxImage.Exclamation);
            if (resultado == MessageBoxResult.Yes)
            {
                valores.Clear();
                DGDatos.ItemsSource = valores;
                sldPorcentajeC.Value = 0;
                txtTiempo.Focusable = true;
                chkGraficar.IsChecked = false;
                sldTiempoV.Visibility = Visibility.Visible;
                btnGrafico.Visibility = Visibility.Hidden;
                lblTiempo2.Content = "";
                lblResultado.Content = "";
                txtMasaRelativa.Text = "";
            }
        }

        private void btnEliminarElemento_Click(object sender, RoutedEventArgs e)
        {
			//Object obj = DGDatos.SelectedValue;
			//Dato? dato = obj as Dato;
			//if (dato != null)
			//{
			//    //MessageBox.Show(dato.Porcentaje_C.ToString());
			//    valores.Remove(dato);
			//    DGDatos.ItemsSource = valores;
			//}
			//else
			//    MessageBox.Show("Seleccione un elemento de la lista","Advertencia");

			if (DGDatos.SelectedItem is Dato datoSeleccionado)
			{
				var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este elemento?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

				if (resultado == MessageBoxResult.Yes)
				{
					valores.Remove(datoSeleccionado);
					DGDatos.ItemsSource = null;
					DGDatos.ItemsSource = valores;
				}
			}
			else
			{
				MessageBox.Show("Por favor, seleccione un elemento de la lista.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}


        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();          
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            Grafico frm = new Grafico(valores, Convert.ToInt32(sldTiempoV.Value));
            frm.ShowDialog();
        }

        private void chkGraficar_Checked(object sender, RoutedEventArgs e)
        {
            btnGrafico.Visibility = Visibility.Visible;
            valores.Clear();
            DGDatos.ItemsSource = valores;
            txtTiempo.Focusable = false;
            sldTiempoV.Visibility = Visibility.Hidden;

            autoCalcular();
            
            //muestra en el datagrid el último registro
            DGDatos.SelectedItems.Clear();
            object item = DGDatos.Items[DGDatos.Items.Count - 1];
            DGDatos.SelectedItem = item;
            DGDatos.ScrollIntoView(item);
        }


        private void btnGrafico_Click(object sender, RoutedEventArgs e)
        {
            Grafico frm = new Grafico(valores, Convert.ToInt32(sldTiempoV.Value));
            frm.ShowDialog();
        }


        private void chkGraficar_Unchecked(object sender, RoutedEventArgs e)
        {
            btnGrafico.Visibility = Visibility.Hidden;
            txtTiempo.Focusable = true;
            sldTiempoV.Visibility = Visibility.Visible;
        }
        #endregion
    }
}
