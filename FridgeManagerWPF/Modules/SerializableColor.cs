using System;
using System.Windows.Media;

namespace FridgeManagerWPF.Modules
{
    using System.Windows.Media;

    [Serializable]
    public class SerializableColor
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public SerializableColor() { }

        public SerializableColor(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }

        public SolidColorBrush ToSolidColorBrush()
        {
            return new SolidColorBrush(ToColor());
        }
    }



}
