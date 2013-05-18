namespace AccidentalFish.HierarchicalToolbar.Items
{
    public class FixedSpacer : ToolbarItem
    {
        private float _spacing;
        public float Spacing
        {
            get { return _spacing; }
            set
            {
                _spacing = value;
                RaisePropertyChanged("Spacing");
            }
        }
    }
}
