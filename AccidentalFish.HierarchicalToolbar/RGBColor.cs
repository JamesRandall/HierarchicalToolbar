using System;

namespace AccidentalFish.HierarchicalToolbar
{
	/// <summary>
	/// Cross platform color class
	/// </summary>
	public class RGBColor
	{
		private const byte DefaultAlpha = 255;

        public RGBColor() : this(0xFFFFFF)
        {
            
        }

		public RGBColor (byte red, byte green, byte blue) : this(red, green, blue, DefaultAlpha)
		{

		}

		public RGBColor (byte red, byte green, byte blue, byte alpha)
		{
			Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
		}

		public RGBColor (float red, float green, float blue, float alpha)
		{
            Red = (byte)(red * 255);
            Green = (byte)(green * 255);
            Blue = (byte)(blue * 255);
            Alpha = (byte)(alpha * 255);
		}

		// Hex color of the form 0xFFEEDD
		public RGBColor (int webcolor) 
		{
            Red = (byte)((webcolor & 0xFF0000) >> 16);
            Green = (byte)((webcolor & 0x00FF00) >> 8);
            Blue = (byte)(webcolor & 0x0000FF);
            Alpha = DefaultAlpha;
		}

        public RGBColor(HSLColor hslColor)
        {
            double luminosity = hslColor.Luminosity/255.0d;
            double hue = hslColor.Hue/255.0d;
            double saturation = hslColor.Saturation/255.0d;

            double r = luminosity;
            double g = luminosity;
            double b = luminosity;
            double v = (luminosity <= 0.5) ? (luminosity * (1.0 + saturation)) : (luminosity + saturation - luminosity * saturation);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = luminosity + luminosity - v;
                sv = (v - m) / v;
                hue *= 6.0;
                sextant = (int)hue;
                fract = hue - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            
            Red = Convert.ToByte(r * 255.0f);
            Green = Convert.ToByte(g * 255.0f);
            Blue = Convert.ToByte(b * 255.0f);
            Alpha = hslColor.Alpha;
        }

        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }

        public byte Alpha { get; set; }

		public static RGBColor White { get { return new RGBColor(0xFFFFFF); } }

	    public static RGBColor Clear { get { return new RGBColor(0.0f, 0.0f ,0.0f, 0.0f);}}
	}
}

