using System;
using System.Windows.Controls;
using Img = System.Windows.Media.Imaging;

namespace FridgeManagerWPF.Modules
{
    public class Images
    {
        public static Image add {  get; set; }
        public static Image export {  get; set; }
        public static Image remove {  get; set; }
        public static Image save {  get; set; }
        public static Image search {  get; set; }

        private static string path = "Icons/";

        static Images()
        {
            add = new Image();
            export = new Image();
            remove = new Image();
            save = new Image();
            search = new Image();

            add.Source = new Img.BitmapImage(new Uri("pack://application:,,,/Icons/add.png"));
            export.Source = new Img.BitmapImage(new Uri("pack://application:,,,/Icons/export.png"));
            remove.Source = new Img.BitmapImage(new Uri("pack://application:,,,/Icons/remove.png"));
            save.Source = new Img.BitmapImage(new Uri("pack://application:,,,/Icons/save.png"));
            search.Source = new Img.BitmapImage(new Uri("pack://application:,,,/Icons/search.png"));
        }
    }
}
