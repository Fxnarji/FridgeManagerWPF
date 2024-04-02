using System.Collections.Generic;

namespace FridgeManagerWPF.Modules
{
    public class AppSettings
    {
        public int maxDays;
        public List<string> categories { get; set; } = new List<string>();
        public List<string> searchSpecific { get; set; } = new List<string>();


    }
}
