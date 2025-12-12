using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using WpfParadojaGemelos.Models;
using WpfParadojaGemelos.Views;

namespace WpfParadojaGemelos.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<Dato> Valores { get; } = new ObservableCollection<Dato>();

        [ObservableProperty]
        private double tiempoV = 1.0;

        [ObservableProperty]
        private double porcentajeC = 0.0;

        [ObservableProperty]
        private string masaReposo = "0";

        [ObservableProperty]
        private string masaRelativa = "";

        [ObservableProperty]
        private string resultadoLabel = "";

        [ObservableProperty]
        private string tiempoLabel = "";

        [ObservableProperty]
        private bool autoGenerate = false;

        [ObservableProperty]
        private bool showGraficoButton = false;

        [ObservableProperty]
        private bool isSliderVisible = true;

        public MainViewModel()
        {
        }

        partial void OnAutoGenerateChanged(bool value)
        {
            ShowGraficoButton = value;
            IsSliderVisible = !value;

            Valores.Clear();

            if (value)
            {
                AutoCalcularCommand.Execute(null);
            }
        }

        [RelayCommand]
        public void Calcular()
        {
            double tViajero = TiempoV;
            double porcentaje = PorcentajeC;

            if (!double.TryParse(MasaReposo,
                                 NumberStyles.Float | NumberStyles.AllowThousands,
                                 CultureInfo.CurrentCulture,
                                 out double masa))
            {
                MessageBox.Show("Masa inválida. Introduzca un número válido.", "Entrada inválida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Math.Abs(porcentaje - 100.0) < double.Epsilon)
            {
                TiempoLabel = "Tiempo del Observador";
                ResultadoLabel = "∞";
                MasaRelativa = masa == 0 ? "0" : "∞";

                Valores.Add(new Dato()
                {
                    Tiempo_Viajero = tViajero,
                    Porcentaje_C = porcentaje,
                    Tiempo_Observador = double.PositiveInfinity,
                    Masa_Relativa = double.PositiveInfinity
                });

                return;
            }

            double tObservador = tViajero / Math.Sqrt(1 - (Math.Pow(porcentaje, 2) / 10000.0));
            double masaRel = masa;
            if (masa != 0)
            {
                masaRel = masa / Math.Sqrt(1 - Math.Pow(porcentaje / 100.0, 2));
            }

            TiempoLabel = "Tiempo del Observador";
            ResultadoLabel = tObservador.ToString("G", CultureInfo.CurrentCulture);
            MasaRelativa = masa != 0 ? masaRel.ToString("F6", CultureInfo.CurrentCulture) : "0";

            Valores.Add(new Dato()
            {
                Tiempo_Viajero = tViajero,
                Porcentaje_C = porcentaje,
                Tiempo_Observador = tObservador,
                Masa_Relativa = masaRel
            });
        }

        [RelayCommand]
        public void AutoCalcular()
        {
            double tViajero = TiempoV;
            if (!double.TryParse(MasaReposo, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double masa))
            {
                masa = 0;
            }

            for (int i = 0; i < 100; i++)
            {
                double tObservador = TiempoV / (Math.Sqrt(1 - (Math.Pow(i, 2) / 10000.0)));
                double porcentaje = Convert.ToDouble(i);
                double porcentajeUnit = Convert.ToDouble(i) / 100.0;
                double masaRelativaCalc = masa;
                if (masa != 0)
                {
                    masaRelativaCalc = masa / (Math.Sqrt(1 - (Math.Pow(porcentajeUnit, 2))));
                }

                TiempoLabel = "Tiempo del Observador";
                ResultadoLabel = tObservador.ToString(CultureInfo.CurrentCulture);

                Valores.Add(new Dato()
                {
                    Tiempo_Viajero = tViajero,
                    Porcentaje_C = porcentaje,
                    Tiempo_Observador = tObservador,
                    Masa_Relativa = masaRelativaCalc
                });
            }
        }

        [RelayCommand]
        public void LimpiarGrid()
        {
            Valores.Clear();
            PorcentajeC = 0;
            TiempoV = 1.0;
            TiempoLabel = "";
            ResultadoLabel = "";
            MasaRelativa = "";
        }

        [RelayCommand]
        public void EliminarElemento(Dato? seleccionado)
        {
            if (seleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un elemento de la lista.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este elemento?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                Valores.Remove(seleccionado);
            }
        }

        [RelayCommand]
        public void OpenAbout()
        {
            var about = new About();
            about.ShowDialog();
        }

        [RelayCommand]
        public void OpenGrafico()
        {
            var graf = new Grafico(Valores, Convert.ToInt32(TiempoV));
            graf.ShowDialog();
        }
    }
}
