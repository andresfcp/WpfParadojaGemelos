using System.Collections.ObjectModel;
using System.Windows;
using WpfParadojaGemelos.Models;
using WpfParadojaGemelos.ViewModels;

namespace WpfParadojaGemelos
{
    /// <summary>
    /// Lógica de interacción para Grafico.xaml
    /// </summary>
    public partial class Grafico : Window
    {
        public Grafico(ObservableCollection<Dato> valores, int tiempoV)
        {
            InitializeComponent();
            DataContext = new GraficoViewModel(valores, tiempoV);
        }
    }
}
