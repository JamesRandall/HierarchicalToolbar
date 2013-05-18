namespace AccidentalFish.HierarchicalToolbar.Items
{
    public class ImageToggleButtonItem : ToggleItemBase
    {
        private string _selectedImage;
        private string _unselectedImage;

        public string SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                RaisePropertyChanged("SelectedImage");
            }
        }

        public string UnselectedImage
        {
            get { return _unselectedImage; }
            set
            {
                _unselectedImage = value;
                RaisePropertyChanged("UnselectedImage");
            }
        }
    }
}
