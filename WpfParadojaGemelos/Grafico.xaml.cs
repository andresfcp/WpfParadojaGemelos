using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfParadojaGemelos.Models;

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
            //DGDatos1.ItemsSource = valores;
            Graficar(valores, tiempoV);
        }


        private void Graficar(ObservableCollection<Dato> valores, int tiempoViajero)
        {
            /*List<Dato> datos = new List<Dato>();
            datos = valores.ToList();
            datos.Sort(delegate (Dato x, Dato y)
            {
                return x.Porcentaje_C.CompareTo(y.Porcentaje_C);
            });*/

            //List<Dato> datos = valores.OrderBy(x => x.Porcentaje_C).ToList();
            //Hasta aquí asignamos el ObservableCollection a una lista para poderlo ordenar
            valores = new ObservableCollection<Dato>(valores.OrderBy(x => x.Porcentaje_C));

            var plotModel1 = new PlotModel();
            plotModel1.Title = "Tiempo Observador vs. % Vel Luz";

            var ejeX = new LinearAxis();
            ejeX.MajorGridlineStyle = LineStyle.Solid;
            ejeX.MinorGridlineStyle = LineStyle.Dot;
            ejeX.Title = "T. Observador";
            //ejeX.Minimum = 0;
            plotModel1.Axes.Add(ejeX);

            var ejeY = new LinearAxis();
            ejeY.Position = AxisPosition.Bottom;
            ejeY.Title = "%C";
            plotModel1.Axes.Add(ejeY);
            //Hasta aqui todo es común, ahora es asociar (el ObservableCollection) la lista con el gráfico

            var linea = new LineSeries();
            foreach (var dato in valores)
            {
                linea.Points.Add(new DataPoint(dato.Porcentaje_C, dato.Tiempo_Observador));
            }
            linea.Title = "Tiempo Viajero " + tiempoViajero;
            linea.CanTrackerInterpolatePoints = false;
            plotModel1.Series.Add(linea);
            Grafica.Model = plotModel1;
        }

    }
}
