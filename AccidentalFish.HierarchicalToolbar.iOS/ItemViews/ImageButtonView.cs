using System.ComponentModel;
using AccidentalFish.HierarchicalToolbar.Items;
using MonoTouch.UIKit;

namespace AccidentalFish.HierarchicalToolbar.iOS.ItemViews
{
    internal class ImageButtonView : UIImageView, IToolbarView
    {
        private readonly SimpleButtonItem _item;
        private bool _isTouched;

        public ImageButtonView(SimpleButtonItem item) : base(UIImage.FromBundle(item.Image))
        {
            UserInteractionEnabled = true;
            _item = item;
            _item.PropertyChanged += ItemPropertyChanged;
            UpdateVisuals();
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateVisuals();
        }

        public override void TouchesBegan(MonoTouch.Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            if (_item.Enabled)
            {
                Alpha = Toolbar.TouchedAlpha;
                _isTouched = true;
            }
        }

        public override void TouchesEnded(MonoTouch.Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            if (_item.Enabled)
            {
                Alpha = 1.0f;
                _isTouched = false;

                if (ToolbarItemTapped != null)
                {
                    ToolbarItemTapped(this, _item);
                }
            }
        }

        public override void TouchesCancelled(MonoTouch.Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            if (_item.Enabled)
            {
                Alpha = 1.0f;
                _isTouched = false;

                if (ToolbarItemTapped != null)
                {
                    ToolbarItemTapped(this, _item);
                }
            }
        }

        private void UpdateVisuals()
        {
            Alpha = _item.Enabled && !_isTouched ? 1.0f : Toolbar.DisabledAlpha;
        }

        public event ToolbarItemTouchDelegate ToolbarItemTapped;
    }
}
