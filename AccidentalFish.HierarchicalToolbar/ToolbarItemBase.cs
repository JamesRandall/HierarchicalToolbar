using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AccidentalFish.HierarchicalToolbar.Annotations;

namespace AccidentalFish.HierarchicalToolbar
{
    public abstract class ToolbarItemBase : INotifyPropertyChanged
    {
        private bool _enabled;
        private RGBColor _backgroundColor;
        private string _id;

        protected ToolbarItemBase()
        {
            BackgroundColor = new RGBColor(0xf45f00);
            PrimaryItems = new List<ToolbarItem>();
            SecondaryItems = new List<ToolbarItem>();
			Enabled = true;
        }

		public bool Enabled
		{
		    get { return _enabled; }
		    set
		    {
		        _enabled = value;
                RaisePropertyChanged("Enabled");
		    }
		}

        public RGBColor BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                RaisePropertyChanged("BackgroundColor");
            }
        }

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        public IEnumerable<ToolbarItem> PrimaryItems { get; set; }

        public IEnumerable<ToolbarItem> SecondaryItems { get; set; }

        public bool HasChildren
        {
            get { return PrimaryItems.Any() || SecondaryItems.Any(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
