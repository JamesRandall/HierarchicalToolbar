using System.Drawing;

namespace AccidentalFish.HierarchicalToolbar.Items
{
    public class TextToggleButtonItem : ToggleItemBase
    {
        private string _text;
        private float _cornerRadius;
        private float _strokeWidth;
        private SizeF _size;

        public TextToggleButtonItem()
        {
            CornerRadius = 8.0f;
            StrokeWidth = 3.0f;
            Size = new SizeF(56.0f, 32.0f);
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        public float CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                _cornerRadius = value;
                RaisePropertyChanged("CornerRadius");
            }
        }

        public float StrokeWidth
        {
            get { return _strokeWidth; }
            set
            {
                _strokeWidth = value;
                RaisePropertyChanged("StrokeWidth");
            }
        }

        public SizeF Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged("Size");
            }
        }
    }
}
