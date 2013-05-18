namespace AccidentalFish.HierarchicalToolbar
{
    public class ToolbarAnimateEventArgs
    {
        public IToolbar Sender { get; set; }
        public ToolbarItemBase From { get; set; }
        public ToolbarItemBase To { get; set; }
    }
}
