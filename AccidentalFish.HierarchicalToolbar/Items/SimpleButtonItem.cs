namespace AccidentalFish.HierarchicalToolbar.Items
{
    public class SimpleButtonItem : ToolbarItem
    {
        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
            }
        }
    }
}
