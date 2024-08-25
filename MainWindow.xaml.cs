using System.Text;
using System.Windows;
using System.Windows.Threading;
using Gma.System.MouseKeyHook;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

using WindowsInput;

namespace SuperUltraMegaClick
{

    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private IKeyboardMouseEvents? m_globalhook;

        bool isOn = false;
        public MainWindow()
        {
            InitializeComponent();

            timer =  new DispatcherTimer();
            timer.Interval += TimeSpan.FromSeconds(0.0001);
            StartKeyHook();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var sim = new InputSimulator();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();
            sim.Mouse.LeftButtonClick();


        }


        private void StartKeyHook()
        {
            m_globalhook = Hook.GlobalEvents();
            m_globalhook.KeyDown += GlobalHookKeyDown;
        }
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == System.Windows.Forms.Keys.RControlKey) 
            {
                if (!isOn)
                {
                    timer.Tick += Timer_Tick;
                    timer.Start();

                }
                else
                {
                    timer.Tick -= Timer_Tick;
                    timer.Stop();

                }
                isOn = !isOn;
            }


        }


    }
}