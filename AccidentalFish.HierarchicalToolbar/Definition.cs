namespace AccidentalFish.HierarchicalToolbar
{
    public class Definition : ToolbarItemBase
    {
        private bool _isVisible;

        public enum ToolbarAlignmentEnum
        {
            Top,
            Left,
            Right,
            Bottom,
            Middle
        };

        public Definition()
        {
            _isVisible = true;
            Alignment = ToolbarAlignmentEnum.Top;
            AnimationDuration = 0.25f;
            BackgroundColor = new RGBColor(0xf45f00);
            ItemSpacing = 12.0f;
            Breadth = 44.0f + ItemSpacing*2;
            PrimaryItemAlignment = ToolbarAlignmentEnum.Left;
        }

        public ToolbarAlignmentEnum Alignment { get; set; }

        public float AnimationDuration { get; set; }

        public float ItemSpacing { get; set; }

        public float Breadth { get; set; }

        public ToolbarAlignmentEnum PrimaryItemAlignment { get; set; }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }
    }
}
