using System.IO;
using System.Collections.Generic;
using System.Windows;
using FridgeManagerWPF.Modules;
using Newtonsoft.Json;


namespace FridgeManagerWPF.Repositories
{
    public class Settings
    {
        public static string SettingsFilePath = "Data/settings.json";

        public static AppSettings LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                return JsonConvert.DeserializeObject<AppSettings>(json);
            }
            return new AppSettings();
        }

        public static void save(AppSettings settings)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, json);
        }
    }
}
