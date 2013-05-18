namespace AccidentalFish.HierarchicalToolbar
{
    public delegate void ToolbarAnimationDelegate(ToolbarAnimateEventArgs args);
    public delegate void ToolbarVisibilityDelegate(ToolbarEventArgs args);

    public interface IToolbar
    {
        void Show();
        void Hide();
        void Pop(bool animated);
        event ToolbarAnimationDelegate ToolbarWillAnimate;
        event ToolbarAnimationDelegate ToolbarDidAnimate;
        event ToolbarAnimationDelegate ToolbarDidRemoveOldToolbar;
        event ToolbarVisibilityDelegate ToolbarWillShow;
        event ToolbarVisibilityDelegate ToolbarDidShow;
        event ToolbarVisibilityDelegate ToolbarWillHide;
        event ToolbarVisibilityDelegate ToolbarDidHide;
        Definition Definition { get; }
        ToolbarItemBase this[string id] { get; }
		void SetButtonEnablement(bool enabled, params string[] ids);
    }
}