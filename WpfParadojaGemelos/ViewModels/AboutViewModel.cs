using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfParadojaGemelos.ViewModels
{
    public partial class AboutViewModel : ObservableObject
    {
        [ObservableProperty]
        string version = "1.0.1b";

        [ObservableProperty]
        private string url = "https://acortes.co";

    }
}
