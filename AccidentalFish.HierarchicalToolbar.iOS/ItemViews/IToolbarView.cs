namespace AccidentalFish.HierarchicalToolbar.iOS.ItemViews
{
    internal delegate void ToolbarItemTouchDelegate(IToolbarView toolbarView, ToolbarItem toolbarItem);
    
    internal interface IToolbarView
    {
        event ToolbarItemTouchDelegate ToolbarItemTapped;
    }
}
