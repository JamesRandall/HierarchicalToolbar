using System.ComponentModel;
using AccidentalFish.HierarchicalToolbar.Items;
using MonoTouch.UIKit;

namespace AccidentalFish.HierarchicalToolbar.iOS.ItemViews
{
    internal class ImageToggleButtonView : UIImageView, IToolbarView
    {
        private readonly ImageToggleButtonItem _item;
        private readonly UIImage _selectedImage;
        private readonly UIImage _unselectedImage;
        private bool _isTouched;
        
		public ImageToggleButtonView(ImageToggleButtonItem item) : base(UIImage.FromBundle(item.Selected ? item.SelectedImage : item.UnselectedImage))
        {
            UserInteractionEnabled = true;
            _item = item;
            _selectedImage = item.Selected ? Image : UIImage.FromBundle(item.SelectedImage);
            _unselectedImage = item.Selected ? UIImage.FromBundle(item.UnselectedImage) : Image;

			UpdateVisuals ();
			_item.PropertyChanged += ItemPropertyChanged;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _item.PropertyChanged -= ItemPropertyChanged;
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateVisuals();
        }

        public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
            if (_item.Enabled)
            {
                Alpha = Toolbar.TouchedAlpha;
                _isTouched = true;
            }
		}
		
		public override void TouchesEnded (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches,evt);
			if (_item.Enabled)
			{
				Alpha = 1.0f;
			    _isTouched = false;
                _item.Selected = !_item.Selected;

				if (ToolbarItemTapped != null) ToolbarItemTapped(this, _item);
			}
		}
		
		public override void TouchesCancelled (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			if (_item.Enabled)
			{
				Alpha = 1.0f;
			    _isTouched = false;
			}
		}

        private void UpdateVisuals()
        {
            Alpha = _item.Enabled && !_isTouched ? 1.0f : Toolbar.DisabledAlpha;
            Image = _item.Selected ? _selectedImage : _unselectedImage;
        }

        public event ToolbarItemTouchDelegate ToolbarItemTapped;
    }
}
