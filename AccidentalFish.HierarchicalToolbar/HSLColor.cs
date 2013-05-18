using System;

namespace AccidentalFish.HierarchicalToolbar
{
    public class HSLColor
    {
        private const byte DefaultAlpha = 255;

        public HSLColor() : this(255, 255, 255)
        {
            
        }

		public HSLColor (byte hue, byte saturation, byte luminosity) : this(hue, saturation, luminosity, DefaultAlpha)
		{

		}

		public HSLColor (byte hue, byte saturation, byte luminosity, byte alpha)
		{
			Hue = hue;
            Saturation = saturation;
            Luminosity = luminosity;
            Alpha = alpha;
		}

		public HSLColor (float hue, float saturation, float luminosity, float alpha)
		{
            Hue = (byte)(hue * 255);
            Saturation = (byte)(saturation * 255);
            Luminosity = (byte)(luminosity * 255);
            Alpha = (byte)(alpha * 255);
		}

        public HSLColor(RGBColor rgbColor)
        {
            double red = (rgbColor.Red / 255d);
            double green = (rgbColor.Green / 255d);
            double blue = (rgbColor.Blue / 255d);

            double min = Math.Min(Math.Min(red, green), blue);
            double max = Math.Max(Math.Max(red, green), blue);
            double delta = max - min;

            double hue = 0;
            double saturation = 0;
            double luminosity = (double)((max + min) / 2.0d);

            if (luminosity > 0.0d && luminosity < 1.0d)
            {
                saturation = delta/(luminosity < 0.5f ? 2.0f*luminosity : (2.0f - 2.0f*luminosity));
            }
            if (delta > 0.0d)
            {
                if (max == red && max != green) hue += (green - blue)/delta;
                if (max == green && max != blue) hue += (2.0d + (blue - red)/delta);
                if (max == blue && max != red) hue += (4.0d + (red - green)/delta);
                hue /= 6.0d;
            }

            Hue = (byte)(hue * 255);
            Saturation = (byte)(saturation * 255);
            Luminosity = (byte)(luminosity * 255);
            Alpha = Convert.ToByte(rgbColor.Alpha);
        }

        public byte Hue { get; set; }

        public byte Saturation { get; set; }

        public byte Luminosity { get; set; }
       
        public byte Alpha { get; set; }

		public static HSLColor White { get { return new HSLColor(255, 255, 255); } }

    }
}
