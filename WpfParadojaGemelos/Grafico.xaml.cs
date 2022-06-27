﻿using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfParadojaGemelos
{
    /// <summary>
    /// Lógica de interacción para Grafico.xaml
    /// </summary>
    public partial class Grafico : Window
    {
        public Grafico(ObservableCollection<Dato> valores)
        {
            InitializeComponent();
            //DGDatos1.ItemsSource = valores;
            Graficar(valores);
        }


        private void Graficar(ObservableCollection<Dato> valores)
        {
            var plotModel1 = new PlotModel();
            plotModel1.Title = "% Vel Luz vs Tiempo Observador ";

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
                linea.Points.Add(new DataPoint(dato.Porcentaje_C, dato.Tiempo_Observador));
            }

            linea.Title = "Valores 1";
            plotModel1.Series.Add(linea);
            Grafica.Model = plotModel1;
        }
    }
}