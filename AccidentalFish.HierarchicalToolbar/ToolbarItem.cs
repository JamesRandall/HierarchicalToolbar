using System;

namespace AccidentalFish.HierarchicalToolbar
{
   public class ToolbarItem : ToolbarItemBase
    {
        private bool _animatesNavigation;
        private bool _isBackButton;

        public ToolbarItem()
        {
            BackgroundColor = RGBColor.Clear;
            IsBackButton = false;
            AnimatesNavigation = true;
        }

        public bool AnimatesNavigation
        {
            get { return _animatesNavigation; }
            set
            {
                _animatesNavigation = value;
                RaisePropertyChanged("AnimatesNavigation");
            }
        }

        public bool IsBackButton
        {
            get { return _isBackButton; }
            set
            {
                _isBackButton = value;
                RaisePropertyChanged("IsBackButton");
            }
        }

        public Action<IToolbar, ToolbarItem> Tapped { get; set; }
    }
}
