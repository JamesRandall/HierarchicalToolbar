using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace AccidentalFish.HierarchicalToolbar.iOS
{
    internal static class ColorExtensions
    {
        public static UIColor UIColor(this RGBColor value)
        {
            return MonoTouch.UIKit.UIColor.FromRGBA(value.Red, value.Green, value.Blue, value.Alpha);
        }

        public static CGColor CGColor(this RGBColor value)
        {
            return value.UIColor().CGColor;
        }

        public static UIColor UIColor(this HSLColor value)
        {
            return new RGBColor(value).UIColor();
        }

        public static CGColor CGColor(this HSLColor value)
        {
            return value.UIColor().CGColor;
        }
    }
}
