using System.Windows.Forms;
using System.IO;
namespace SuperUltraMegaClick;
using Newtonsoft.Json;

public class Config
{
    [JsonProperty("clickpersecond")]
    public int ClickPerSecond { get; set; }
    
    [JsonProperty("multiclickpersecond")]
    public int MultiClickPerSecond { get; set; }
    
    [JsonProperty("bindedkey")]
    public Keys BindedKey { get; set; }
}

public static class ConfigManager
{
    private static readonly string ConfigFilePath = "config.json";

    public static Config LoadSettings()
    {

        if (!File.Exists(ConfigFilePath))
        {
            return new Config()
            {
                ClickPerSecond = 10,
                MultiClickPerSecond = 10,
                BindedKey = Keys.RControlKey
            };
        }
        
        var json = File.ReadAllText(ConfigFilePath);
        return JsonConvert.DeserializeObject<Config>(json);
    }
    public static void SaveSettings(Config config)
    {
        var json = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(ConfigFilePath, json);
    }
}