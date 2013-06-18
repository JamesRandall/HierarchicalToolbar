using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using AccidentalFish.HierarchicalToolbar.Items;
using AccidentalFish.HierarchicalToolbar.iOS.ItemViews;
using AccidentalFish.UIKit;
using MonoTouch.UIKit;

namespace AccidentalFish.HierarchicalToolbar.iOS
{
    public class Toolbar : UIView, IToolbar
    {
        public const float DisabledAlpha = 0.5f;
        public const float TouchedAlpha = 0.5f;

        private static readonly Dictionary<Type, Type> ItemMappings = new Dictionary<Type, Type>
                                                                           {
                                                                               {
                                                                                   typeof (ImageToggleButtonItem),
                                                                                   typeof (ImageToggleButtonView)
                                                                               },
                                                                               {
                                                                                   typeof(SimpleButtonItem),
                                                                                   typeof(ImageButtonView)
                                                                               }
                                                                           }; 

        private readonly Definition _definition;
        private readonly UIView _parentView;
        private readonly Stack<ToolbarItemBase> _currentToolbar = new Stack<ToolbarItemBase>();
        private readonly List<UIView> _currentPrimaryViews = new List<UIView>();
        private readonly List<UIView> _currentSecondaryViews = new List<UIView>();
        private readonly Dictionary<string, ToolbarItemBase> _idsToItems = new Dictionary<string, ToolbarItemBase>();

        public Toolbar(UIView parentView, Definition definition)
        {
            _definition = definition;
            _parentView = parentView;

            Frame = _definition.IsVisible ? GetVisibleFrame() : GetHiddenFrame();
            Hidden = !_definition.IsVisible;
            SetAutoResizingMask();
            BackgroundColor = _definition.BackgroundColor.UIColor();
            RecursivelyFindIds(definition);
            PushToolbar(definition, false, null);

            _definition.PropertyChanged += OnDefinitionProperyChanged;
            _parentView.AddSubview(this);
        }

        private void OnDefinitionProperyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "IsVisible")
            {
                if (_definition.IsVisible)
                {
                    DoShow();
                }
                else
                {
                    DoHide();
                }             
            }
        }

        #region IToolbar

        public void Show()
        {
            _definition.IsVisible = true;
        }

        public void Hide()
        {
            _definition.IsVisible = false;
        }

        public void Pop(bool animated)
        {
            if (_currentToolbar.Peek() != _definition)
            {
                ToolbarItemBase from = _currentToolbar.Pop();
                if (!animated)
                {
                    StaticPresentToolbar(_currentToolbar.Peek(), null);
                }
                else
                {
                    AnimatePresentToolbar(_currentToolbar.Peek(), from, null);
                }
            }
        }

        public event ToolbarAnimationDelegate ToolbarWillAnimate;
        public event ToolbarAnimationDelegate ToolbarDidAnimate;
        public event ToolbarAnimationDelegate ToolbarDidRemoveOldToolbar;
        public event ToolbarVisibilityDelegate ToolbarWillShow;
        public event ToolbarVisibilityDelegate ToolbarDidShow;
        public event ToolbarVisibilityDelegate ToolbarWillHide;
        public event ToolbarVisibilityDelegate ToolbarDidHide;

        public Definition Definition { get { return _definition; } }

        public ToolbarItemBase this[string id]
        {
            get
            {
                ToolbarItemBase item;
                _idsToItems.TryGetValue(id, out item);
                return item;
            }
        }

        public void SetButtonEnablement(bool enabled, params string[] ids)
        {
            if (ids.Length > 0)
            {
                foreach (string id in ids)
                {
                    this[id].Enabled = enabled;
                }
            }
            else
            {
                foreach (ToolbarItem item in _currentToolbar.Peek().PrimaryItems)
                {
                    item.Enabled = enabled;
                }
                foreach (ToolbarItem item in _currentToolbar.Peek().SecondaryItems)
                {
                    item.Enabled = enabled;
                }
            }
        }

        #endregion

        private void RecursivelyFindIds(ToolbarItemBase node)
        {
            if (!String.IsNullOrWhiteSpace(node.Id))
            {
                _idsToItems.Add(node.Id, node);
            }

            foreach (ToolbarItem childNode in node.PrimaryItems)
            {
                RecursivelyFindIds(childNode);
            }

            foreach (ToolbarItem childNode in node.SecondaryItems)
            {
                RecursivelyFindIds(childNode);
            }
        }

        private RectangleF GetVisibleFrame()
        {
            RectangleF parentBounds = _parentView.Bounds;
            switch (_definition.Alignment)
            {
                case Definition.ToolbarAlignmentEnum.Top:
                    return new RectangleF(parentBounds.X, parentBounds.Y, parentBounds.Width, _definition.Breadth);

                case Definition.ToolbarAlignmentEnum.Middle:
                    return new RectangleF(parentBounds.X, parentBounds.Y + parentBounds.Height / 2 - _definition.Breadth / 2, parentBounds.Width, _definition.Breadth);

                case Definition.ToolbarAlignmentEnum.Left:
                    return new RectangleF(parentBounds.X, parentBounds.Y, _definition.Breadth, parentBounds.Height);

                case Definition.ToolbarAlignmentEnum.Right:
                    return new RectangleF(parentBounds.Right - _definition.Breadth, parentBounds.Y, _definition.Breadth, parentBounds.Height);

                case Definition.ToolbarAlignmentEnum.Bottom:
                    return new RectangleF(parentBounds.X, parentBounds.Bottom - _definition.Breadth, parentBounds.Width, _definition.Breadth);
            }
            throw new InvalidOperationException("Alignment not supported");
        }

        private RectangleF GetHiddenFrame()
        {
            RectangleF parentBounds = _parentView.Bounds;
            switch (_definition.Alignment)
            {
                case Definition.ToolbarAlignmentEnum.Top:
                    return new RectangleF(parentBounds.X, parentBounds.Y - _definition.Breadth, parentBounds.Width, _definition.Breadth);

                case Definition.ToolbarAlignmentEnum.Middle:
                    return new RectangleF(parentBounds.Width, parentBounds.Y + parentBounds.Height / 2 - _definition.Breadth / 2, parentBounds.Width, _definition.Breadth);

                case Definition.ToolbarAlignmentEnum.Left:
                    return new RectangleF(parentBounds.X - _definition.Breadth, parentBounds.Y, _definition.Breadth, parentBounds.Height);

                case Definition.ToolbarAlignmentEnum.Right:
                    return new RectangleF(parentBounds.Right, parentBounds.Y, _definition.Breadth, parentBounds.Height);

                case Definition.ToolbarAlignmentEnum.Bottom:
                    return new RectangleF(parentBounds.X, parentBounds.Bottom, parentBounds.Width, _definition.Breadth);
            }
            throw new InvalidOperationException("Alignment not supported");
        }

        private void SetAutoResizingMask()
        {
            switch (_definition.Alignment)
            {
                case Definition.ToolbarAlignmentEnum.Top:
                    AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleBottomMargin;
                    break;

                case Definition.ToolbarAlignmentEnum.Bottom:
                    AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin;
                    break;

                case Definition.ToolbarAlignmentEnum.Left:
                    AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleRightMargin;
                    break;

                case Definition.ToolbarAlignmentEnum.Right:
                    AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleLeftMargin;
                    break;
            }
        }

        internal void PushToolbar(ToolbarItemBase toolbar, bool animate, Action<IToolbar> afterToolbarUpdateAction)
        {
            if (!animate)
            {
                StaticPresentToolbar(toolbar, afterToolbarUpdateAction);
            }
            else
            {
                AnimatePresentToolbar(toolbar, _currentToolbar.Peek(), afterToolbarUpdateAction);
            }

            _currentToolbar.Push(toolbar);
        }

        private void StaticPresentToolbar(ToolbarItemBase toolbar, Action<IToolbar> afterToolbarUpdateAction)
        {
            _currentPrimaryViews.Clear();
            _currentSecondaryViews.Clear();
            
            foreach (UIView view in Subviews) view.RemoveFromSuperview();

            CreatePrimaryViews(toolbar);
            CreateSecondaryViews(toolbar);

            if (afterToolbarUpdateAction != null)
            {
                afterToolbarUpdateAction(this);
            }
        }

        private void AnimatePresentToolbar(ToolbarItemBase to, ToolbarItemBase from, Action<IToolbar> afterToolbarUpdateAction)
        {
            if (ToolbarWillAnimate != null)
            {
                ToolbarWillAnimate(new ToolbarAnimateEventArgs { From = from, To = to });
            }

            List<UIView> oldLeftViews = new List<UIView>(_currentPrimaryViews);
            List<UIView> oldRightViews = new List<UIView>(_currentSecondaryViews);

            List<RectangleF> leftFrames = new List<RectangleF>();
            List<RectangleF> rightFrames = new List<RectangleF>();

            _currentPrimaryViews.Clear();
            _currentSecondaryViews.Clear();
            CreatePrimaryViews(to);
            CreateSecondaryViews(to);

            foreach (UIView view in _currentPrimaryViews)
            {
                RectangleF frame = view.Frame;
                leftFrames.Add(frame);
                if (IsHorizontal)
                {
                    frame.X = -frame.Width;
                }
                else
                {
                    frame.Y = -frame.Height;
                }
                
                view.Frame = frame;
            }
            foreach (UIView view in _currentSecondaryViews)
            {
                RectangleF frame = view.Frame;
                rightFrames.Add(frame);
                if (IsHorizontal)
                {
                    frame.X = Bounds.Width;
                }
                else
                {
                    frame.Y = Bounds.Height;
                }
                view.Frame = frame;
            }

            FluentAnimate.EaseIn(_definition.AnimationDuration,
                () =>
                {
                    foreach (UIView view in oldLeftViews)
                    {
                        RectangleF frame = view.Frame;
                        if (IsHorizontal)
                        {
                            frame.X = -frame.Width;
                        }
                        else
                        {
                            frame.Y = -frame.Height;
                        }
                        
                        view.Frame = frame;
                    }
                    foreach (UIView view in oldRightViews)
                    {
                        RectangleF frame = view.Frame;
                        if (IsHorizontal)
                        {
                            frame.X = Bounds.Width;
                        }
                        else
                        {
                            frame.Y = Bounds.Height;
                        }
                        
                        view.Frame = frame;
                    }
                }).Then.Do(
                () =>
                {
                    if (ToolbarDidRemoveOldToolbar != null)
                    {
                        ToolbarDidRemoveOldToolbar(new ToolbarAnimateEventArgs { From = from, To = to });
                    }
                }).Then.EaseIn(
                () =>
                {
                    int index = 0;
                    foreach (UIView view in _currentPrimaryViews)
                    {
                        view.Frame = leftFrames[index];
                        index++;
                    }
                    index = 0;
                    foreach (UIView view in _currentSecondaryViews)
                    {
                        view.Frame = rightFrames[index];
                        index++;
                    }
                }).WhenComplete(
               () =>
               {
                   foreach (UIView view in oldLeftViews)
                   {
                       view.RemoveFromSuperview();
                   }
                   foreach (UIView view in oldRightViews)
                   {
                       view.RemoveFromSuperview();
                   }

                   if (ToolbarDidAnimate != null)
                   {
                       ToolbarDidAnimate(new ToolbarAnimateEventArgs { From = from, To = to });
                   }

                   if (afterToolbarUpdateAction != null)
                   {
                       afterToolbarUpdateAction(this);
                   }
               }).Start();
        }

        private void CreateSecondaryViews(ToolbarItemBase toolbar)
        {
            float offset = (IsHorizontal ? Bounds.Size.Width : Bounds.Size.Height) - _definition.ItemSpacing;
            foreach (ToolbarItem item in toolbar.SecondaryItems.Reverse())
            {
                UIView toolbarView = CreateToolbarView(item);
                offset -= IsHorizontal ? toolbarView.Frame.Width : toolbarView.Frame.Height;
                toolbarView.Frame = ToolbarItemRectangle(offset, toolbarView);
                toolbarView.AutoresizingMask = IsHorizontal ? UIViewAutoresizing.FlexibleLeftMargin : UIViewAutoresizing.FlexibleTopMargin;
                AddSubview(toolbarView);
                _currentSecondaryViews.Add(toolbarView);

                offset -= _definition.ItemSpacing;
            }
        }

        private void CreatePrimaryViews(ToolbarItemBase toolbar)
        {
            float offset = _definition.ItemSpacing;
            foreach (ToolbarItem item in toolbar.PrimaryItems)
            {
                UIView toolbarUiView = CreateToolbarView(item);
                toolbarUiView.Frame = ToolbarItemRectangle(offset, toolbarUiView);
                AddSubview(toolbarUiView);
                _currentPrimaryViews.Add(toolbarUiView);

                offset += (IsHorizontal ? toolbarUiView.Frame.Width : toolbarUiView.Frame.Height) + _definition.ItemSpacing;
            }
            
        }

        private void ToolbarItemTapped(IToolbarView toolbarView, ToolbarItem toolbarItem)
        {
            if (toolbarItem.HasChildren)
            {
                PushToolbar(toolbarItem, toolbarItem.AnimatesNavigation, null);
            }
            else if (toolbarItem.IsBackButton)
            {
                Pop(toolbarItem.AnimatesNavigation);
            }
            if (toolbarItem.Tapped != null) toolbarItem.Tapped(this, toolbarItem);
        }

        private RectangleF ToolbarItemRectangle(float offset, UIView view)
        {
            if (IsHorizontal)
            {
                return new RectangleF(offset, (Frame.Size.Height - view.Frame.Height) / 2, view.Frame.Width, view.Frame.Height);
            }
            return new RectangleF((Frame.Size.Width - view.Frame.Width) / 2, offset, view.Frame.Width, view.Frame.Height);
        }

        private UIView CreateToolbarView(ToolbarItem item)
        {
            Type itemViewType;
            if (!ItemMappings.TryGetValue(item.GetType(), out itemViewType))
            {
                throw new InvalidOperationException(String.Format("Toolbar item type {0} has no matching view registration", item.GetType().Name));
            }
            UIView view = (UIView)Activator.CreateInstance(itemViewType, item);
            IToolbarView toolbarView = view as IToolbarView;
            if (toolbarView != null)
            {
                toolbarView.ToolbarItemTapped += ToolbarItemTapped;
            }
            return view;
        }

        private void DoShow()
        {
            if (Hidden)
            {
                if (ToolbarWillShow != null) ToolbarWillShow(new ToolbarEventArgs { Sender = this });
                Hidden = false;
                RectangleF newFrame = GetVisibleFrame();
                FluentAnimate
                    .EaseInOut(_definition.AnimationDuration, () => Frame = newFrame)
                    .WhenComplete(() =>
                    {
                        if (ToolbarDidShow != null) ToolbarDidShow(new ToolbarEventArgs { Sender = this });
                    })
                    .Start();
            }
        }

        private void DoHide()
        {
            if (!Hidden)
            {
                RectangleF newFrame = GetHiddenFrame();
                FluentAnimate
                    .EaseInOut(_definition.AnimationDuration, () => Frame = newFrame)
                    .WhenComplete(() =>
                    {
                        Hidden = true;

                    })
                    .Start();
            }
        }

        private bool IsHorizontal
        {
            get
            {
                return _definition.Alignment == Definition.ToolbarAlignmentEnum.Top ||
                       _definition.Alignment == Definition.ToolbarAlignmentEnum.Bottom;
            }
        }
    }
}
