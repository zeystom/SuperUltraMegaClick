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
        private readonly InputSimulator _inputSimulator = new();
        private bool _initialized = false;
        private bool _isOn = false;
        private int _multiCounter = 1;
        private Config _config;

        public MainWindow()
        {
            _config = ConfigManager.LoadSettings(); 
            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsed;
            InitializeComponent();
            InitializeSettings();
            StartKeyHook();
            Window_Loaded();
        }
        private void Window_Loaded()
        {
            this.Closing += MainWindow_Closing;
        }

        private void InitializeSettings()
        {
            Counter.Text = _config.ClickPerSecond.ToString();
            SliderCount.Value = _config.ClickPerSecond;
            MultiCount.Text = _config.MultiClickPerSecond.ToString();
            MultiSliderCount.Value = _config.MultiClickPerSecond;
            _multiCounter = _config.MultiClickPerSecond;
            CurrentKps.Text = (_config.ClickPerSecond * _config.MultiClickPerSecond).ToString();
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
            if (_initialized)
            {
                Counter.Text = SliderCount.Value.ToString();
             
                CurrentKps.Text = (MultiSliderCount.Value * SliderCount.Value).ToString();
            }
            else
            {
                double intervalInSeconds = 1.0 / e.NewValue;
                _timer.Interval = intervalInSeconds * 1000;
            }
        }

        private void MultiSliderCountOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_initialized)
            {
                MultiCount.Text = MultiSliderCount.Value.ToString();
                _multiCounter = (int)e.NewValue;
                CurrentKps.Text = (MultiSliderCount.Value * SliderCount.Value).ToString();

            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _config = ConfigManager.LoadSettings(); 
            _config.ClickPerSecond = (int)SliderCount.Value;
            _config.MultiClickPerSecond = (int)MultiSliderCount.Value;
            ConfigManager.SaveSettings(_config);
        }
    }
}
