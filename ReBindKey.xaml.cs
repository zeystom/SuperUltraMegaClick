using Gma.System.MouseKeyHook;
using System.Windows;
using System.Windows.Forms;
namespace SuperUltraMegaClick;

public partial class ReBindKey : Window
{
    private Config _config;
    private IKeyboardMouseEvents? m_Hook;
    public ReBindKey()
    {
        _config = ConfigManager.LoadSettings();
        InitializeComponent();
        ShowSettings();
        StartKeyHook();
    }
    private void StartKeyHook()
    {
        m_Hook = Hook.GlobalEvents();
        m_Hook.KeyDown += OnKeyDown;

    }
    private void ShowSettings()
    {
        BindedKeyCaption.Text = $"Выбрана клавиша: {_config.BindedKey}";
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        BindedKeyCaption.Text = $"Выбрана клавиша: {e.KeyCode}";
        _config.BindedKey = e.KeyCode;

    }

    private void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        if (m_Hook != null)
        {
            m_Hook.KeyDown -= OnKeyDown;
            m_Hook.Dispose();
            m_Hook = null;
        }

        ConfigManager.SaveSettings(_config);
        this.Close();
    }
}