using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;


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

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                if (sender.GetType() != typeof(Hyperlink)) 
                    return; 
                string link = ((Hyperlink)sender).NavigateUri.ToString(); 
                Process.Start(link);
                //Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
