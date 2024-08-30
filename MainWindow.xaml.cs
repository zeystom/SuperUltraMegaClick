using System.Text;
using System.Windows;
using System;
using System.Windows.Threading;
using Gma.System.MouseKeyHook;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using System.Timers;
using WindowsInput;
using Timer = System.Timers.Timer;
namespace SuperUltraMegaClick
{
    public partial class MainWindow : Window
    {
        private Timer _timer;
        private IKeyboardMouseEvents? _globalHook;
        private InputSimulator _inputSimulator = new();
        private bool _initialized = false;
        private bool _isOn = false;
        private int _multiCounter = 1;

        public MainWindow()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsed;
            InitializeComponent();
            StartKeyHook();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            _initialized = true;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < _multiCounter; i++)
            {
                _inputSimulator.Mouse.LeftButtonClick();
            }
        }

        private void StartKeyHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHookKeyDown;
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.RControlKey)
            {
                if (!_isOn)
                {
                    _timer.Start();
                }
                else
                {
                    _timer.Stop();
                }
                _isOn = !_isOn;
            }
        }

        private void SliderCountOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Counter.Text = SliderCount.Value.ToString();
            if (_initialized)
            {
                double intervalInSeconds = 1.0 / e.NewValue;
                _timer.Interval = intervalInSeconds * 1000;
            }
        }

        private void MultiSliderCountOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MultiCount.Text = MultiSliderCount.Value.ToString();
            if (_initialized)
            {
                _multiCounter = (int)e.NewValue;
            }
        }
    }
}
