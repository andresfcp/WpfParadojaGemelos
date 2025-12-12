using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WpfParadojaGemelos.ViewModels;

namespace WpfParadojaGemelos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard _coheteStoryboard;
        private DoubleAnimation _animacionCohete;
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            _vm = new MainViewModel();
            DataContext = _vm;

            // subscribe to collection changes to keep DataGrid selection in sync
            _vm.Valores.CollectionChanged += Valores_CollectionChanged;
        }

        private void Valores_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // select and scroll the last item added
                if (DGDatos.Items.Count > 0)
                {
                    var item = DGDatos.Items[DGDatos.Items.Count - 1];
                    DGDatos.Dispatcher.Invoke(() =>
                    {
                        DGDatos.SelectedItem = item;
                        DGDatos.ScrollIntoView(item);
                    });
                }
            }
        }

        private void UpdateCoheteAnimation()
        {
            if (imgCohete == null) return;

            if (!(imgCohete.RenderTransform is System.Windows.Media.TranslateTransform tt))
            {
                tt = new System.Windows.Media.TranslateTransform();
                imgCohete.RenderTransform = tt;
            }

            var topPoint = imgCohete.TranslatePoint(new System.Windows.Point(0, 0), this);
            double desiredTop = 16.0;
            double toValue = desiredTop - topPoint.Y;

            if (double.IsNaN(toValue) || double.IsInfinity(toValue)) return;
            if (Math.Abs(toValue) < 20) toValue = Math.Sign(toValue) * 20;

            try
            {
                _coheteStoryboard?.Stop(imgCohete);
                _coheteStoryboard?.Remove(imgCohete);
            }
            catch { }

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
            Storyboard.SetTarget(_animacionCohete, imgCohete);
            Storyboard.SetTargetProperty(_animacionCohete, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

            _coheteStoryboard.Begin(imgCohete, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCoheteAnimation();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateCoheteAnimation();
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
    }
}
