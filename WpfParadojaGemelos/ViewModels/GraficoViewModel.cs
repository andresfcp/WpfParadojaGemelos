using CommunityToolkit.Mvvm.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Linq;
using WpfParadojaGemelos.Models;

namespace WpfParadojaGemelos.ViewModels
{
    public partial class GraficoViewModel : ObservableObject
    {
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get => plotModel;
            private set => SetProperty(ref plotModel, value);
        }

        public GraficoViewModel(ObservableCollection<Dato> valores, int tiempoViajero)
        {
            ConstruirModeloGrafico(valores, tiempoViajero);
        }

        private void ConstruirModeloGrafico(ObservableCollection<Dato> valores, int tiempoViajero)
        {
            if (valores == null)
            {
                PlotModel = new PlotModel { Title = "No data" };
                return;
            }

            var valoresOrdenados = new ObservableCollection<Dato>(valores.OrderBy(x => x.Porcentaje_C));

            var model = new PlotModel { Title = "Tiempo Observador vs. % Vel Luz" };

            var ejeX = new LinearAxis
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "T. Observador"
            };
            model.Axes.Add(ejeX);

            var ejeY = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "%C"
            };
            model.Axes.Add(ejeY);

            var linea = new LineSeries();
            foreach (var dato in valoresOrdenados)
            {
                linea.Points.Add(new DataPoint(dato.Porcentaje_C, dato.Tiempo_Observador));
            }
            linea.Title = "Tiempo Viajero " + tiempoViajero;
            linea.CanTrackerInterpolatePoints = false;
            model.Series.Add(linea);

            PlotModel = model;
        }
    }
}
