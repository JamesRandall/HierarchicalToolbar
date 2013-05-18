namespace AccidentalFish.HierarchicalToolbar.Items
{
    public abstract class ToggleItemBase : ToolbarItem
    {
        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged("Selected");
            }
        }
    }
}
