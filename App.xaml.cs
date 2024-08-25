using System.Configuration;
using System.Data;
using System.Windows;

namespace SuperUltraMegaClick
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static event EventHandler? IsClickOn;

        public static void ClickOn()
        {
            IsClickOn?.Invoke(null,EventArgs.Empty);
        }


    }

}
