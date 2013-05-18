/*using System;
using System.Drawing;
using AccidentalFish.HierarchicalToolbar;
using AccidentalFish.HierarchicalToolbar.iOS;
using AccidentalFish.Mono.ApplicationFramework.CrossPlatform;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace AccidentalFish.Mono.ApplicationFramework.UIKit.SlidingToolbar.ItemViews
{
    
    public class TextToggleButtonView : UIView, IToolbarItemView
    {
        private readonly float _cornerRadius;
        private readonly float _strokeWidth;
        private readonly Action _toggleAction;
        private readonly UILabel _titleLabel;

        private readonly UIColor _enabledColor = UIColor.White;
        private readonly UIColor _disabledColor = new RGBColor(0x7fd6f7).UIColor();

        private bool _enabled;

        public TextToggleButtonView(RectangleF frame, string text, float cornerRadius, float strokeWidth, bool selected, Action toggleAction)
        {
            Frame = frame;
            BackgroundColor = UIColor.Clear;
            _enabled = true;
            _titleLabel = new UILabel(Bounds);
            _titleLabel.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
            _titleLabel.TextColor = UIColor.White;
            _titleLabel.Text = text;
            _titleLabel.BackgroundColor = UIColor.Clear;
            _titleLabel.TextAlignment = UITextAlignment.Center;
            _titleLabel.ContentMode = UIViewContentMode.Center;
            _cornerRadius = cornerRadius;
            _strokeWidth = strokeWidth;
            _toggleAction = toggleAction;
            Selected = selected;

            AddSubview(_titleLabel);
        }

        public UILabel TitleLabel { get { return _titleLabel; } }

        public UIFont Font
        {
            get { return TitleLabel.Font; }
            set { TitleLabel.Font = value; }
        }

        public void Toggle()
        {
            if (Enabled)
            {
                Selected = !Selected;
                SetNeedsDisplay();
                _toggleAction();
            }
        }

        public bool Selected { get; set; }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                Alpha = _enabled ? 1.0f : Toolbar.DisabledAlpha;
            }
        }

        public override void Draw(RectangleF rect)
        {
            CGContext context = UIGraphics.GetCurrentContext();
            if (Selected)
            {
                RectangleF innerRect = Bounds;
                innerRect.Inflate(-_strokeWidth/2, -_strokeWidth/2);
                _titleLabel.TextColor.SetStroke();
                context.SetLineWidth(_strokeWidth);
                context.StrokeRoundedRect(innerRect, _cornerRadius);
            }
        }

		public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			Alpha = 0.5f;
		}
		
		public override void TouchesEnded (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches,evt);
			if (Enabled)
			{
				Alpha = 1.0f;
			}
		}
		
		public override void TouchesCancelled (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			if (Enabled)
			{
				Alpha = 1.0f;
			}
		}
    }
}*/
