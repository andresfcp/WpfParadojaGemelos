using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;


namespace WpfParadojaGemelos
{
    /// <summary>
    /// Lógica de interacción para About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                var psi = new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el navegador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            e.Handled = true;
        }
    }
}
