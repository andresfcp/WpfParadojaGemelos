using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfParadojaGemelos.Models;

namespace WpfParadojaGemelos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard _coheteStoryboard;
        private DoubleAnimation _animacionCohete;
        //private readonly DispatcherTimer _resizeTimer = new DispatcherTimer();

        //public ObservableCollection<Dato> valores = new ObservableCollection<Dato>();
        public ObservableCollection<Dato> Valores { get; } = new ObservableCollection<Dato>();


        public MainWindow()
        {
            InitializeComponent();
            btnGrafico.Visibility = Visibility.Hidden;

            //Asignación única del ItemsSource
            DGDatos.ItemsSource = Valores;
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            Calcular();
            //MoverCohete();
        }

        /*
        private async void MoverCohete()
        {
            int y = 0;
            TranslateTransform mover = new TranslateTransform();
            for (int i = 0; i < 81; i++)
            {
                y = y - 4;
                mover.Y = y;
                imgCohete.RenderTransform = mover;
                await Task.Delay(1).ConfigureAwait(true);
            }
            for (int i = 0; i < 81; i++)
            {
                y = y + 4;
                mover.Y = y;
                imgCohete.RenderTransform = mover;
                await Task.Delay(1).ConfigureAwait(true);
            }
        }
        */

        private void UpdateCoheteAnimation()
        {
            if (imgCohete == null) return;

            // Asegurarse de que el RenderTransform es un TranslateTransform
            if (!(imgCohete.RenderTransform is TranslateTransform tt))
            {
                tt = new TranslateTransform();
                imgCohete.RenderTransform = tt;
            }

            // Calcular el desplazamiento necesario, el top deseado es 16 píxeles
            var topPoint = imgCohete.TranslatePoint(new System.Windows.Point(0, 0), this);
            double desiredTop = 16.0;
            double toValue = desiredTop - topPoint.Y;

            if (double.IsNaN(toValue) || double.IsInfinity(toValue)) return;
            if (Math.Abs(toValue) < 20) toValue = Math.Sign(toValue) * 20;

            // Parar y limpiar storyboard anterior
            try
            {
                _coheteStoryboard?.Stop(imgCohete);
                _coheteStoryboard?.Remove(imgCohete);
            }
            catch { }

            // Crear nueva animación con el ToValue calculado
            _animacionCohete = new DoubleAnimation
            {
                From = 0,
                To = toValue,
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            _coheteStoryboard = new Storyboard();
            _coheteStoryboard.Children.Add(_animacionCohete);
            //Apuntar la animación al objeto y propiedad correctos
            Storyboard.SetTarget(_animacionCohete, imgCohete);
            Storyboard.SetTargetProperty(_animacionCohete, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

            //iniciar la animación
            _coheteStoryboard.Begin(imgCohete, true);
        }


        private void Calcular()
        {
            double tViajero = sldTiempoV.Value;
            double porcentajeC = sldPorcentajeC.Value;

            // Validar y parsear masa de forma segura
            if (!double.TryParse(txtMasaReposo.Text,
                                 System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands,
                                 System.Globalization.CultureInfo.CurrentCulture,
                                 out double masa))
            {
                MessageBox.Show("Masa inválida. Introduzca un número válido.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMasaReposo.Focus();
                txtMasaReposo.SelectAll();
                return;
            }

            // Caso límite: 100% de la velocidad de la luz => división por cero en las fórmulas
            if (Math.Abs(porcentajeC - 100.0) < double.Epsilon)
            {
                lblTiempo2.Content = "Tiempo del Observador";
                lblResultado.Content = "∞"; // o usa "Infinito" si prefieres texto
                txtMasaRelativa.Text = masa == 0 ? "0" : "∞";

                Valores.Add(new Dato()
                {
                    Tiempo_Viajero = tViajero,
                    Porcentaje_C = porcentajeC,
                    Tiempo_Observador = double.PositiveInfinity,
                    Masa_Relativa = double.PositiveInfinity
                });

                return;
            }

            // Cálculos normales (porcentajeC está en 0..100 y distinto de 100)
            double tObservador = tViajero / Math.Sqrt(1 - (Math.Pow(porcentajeC, 2) / 10000.0));
            double masaRelativa = masa;
            if (masa != 0)
            {
                masaRelativa = masa / Math.Sqrt(1 - Math.Pow(porcentajeC / 100.0, 2));
            }

            lblTiempo2.Content = "Tiempo del Observador";
            lblResultado.Content = tObservador.ToString("G", System.Globalization.CultureInfo.CurrentCulture);
            if (masa != 0)
            {
                txtMasaRelativa.Text = masaRelativa.ToString("F6", System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                txtMasaRelativa.Text = "0";
            }

            Valores.Add(new Dato()
            {
                Tiempo_Viajero = tViajero,
                Porcentaje_C = porcentajeC,
                Tiempo_Observador = tObservador,
                Masa_Relativa = masaRelativa
            });
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

                Valores.Add(new Dato() 
                { 
                    Tiempo_Viajero = tViajero, 
                    Porcentaje_C = i, 
                    Tiempo_Observador = 
                    tObservador, 
                    Masa_Relativa = masaRelativa 
                });
            }
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
                Valores.Clear();
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
					Valores.Remove(datoSeleccionado);
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
            Grafico frm = new Grafico(Valores, Convert.ToInt32(sldTiempoV.Value));
            frm.ShowDialog();
        }

        private void chkGraficar_Checked(object sender, RoutedEventArgs e)
        {
            btnGrafico.Visibility = Visibility.Visible;
            Valores.Clear();

            txtTiempo.Focusable = false;
            sldTiempoV.Visibility = Visibility.Hidden;

            autoCalcular();
            
            //muestra en el datagrid el último registro
            object item = DGDatos.Items[DGDatos.Items.Count - 1];
            DGDatos.SelectedItem = item;
            DGDatos.ScrollIntoView(item);
        }


        private void btnGrafico_Click(object sender, RoutedEventArgs e)
        {
            Grafico frm = new Grafico(Valores, Convert.ToInt32(sldTiempoV.Value));
            frm.ShowDialog();
        }


        private void chkGraficar_Unchecked(object sender, RoutedEventArgs e)
        {
            btnGrafico.Visibility = Visibility.Hidden;
            txtTiempo.Focusable = true;
            sldTiempoV.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCoheteAnimation();
            //Dispatcher.BeginInvoke((Action)(() => UpdateCoheteAnimation()), DispatcherPriority.Loaded);
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateCoheteAnimation();
            //_resizeTimer.Stop();
            //_resizeTimer.Start();
        }
        #endregion
    }
}
