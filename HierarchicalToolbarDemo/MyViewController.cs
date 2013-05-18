using System;
using System.Collections.Generic;
using AccidentalFish.HierarchicalToolbar;
using AccidentalFish.HierarchicalToolbar.Items;
using AccidentalFish.HierarchicalToolbar.iOS;
using MonoTouch.UIKit;
using System.Drawing;

namespace HierarchicalToolbarDemo
{
    public class MyViewController : UIViewController
    {
        UIButton _button;
        private const float ButtonWidth = 200;
        private const float ButtonHeight = 50;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.Frame = UIScreen.MainScreen.Bounds;
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            Toolbar toolbar = new Toolbar(View, new Definition
                                                    {
                                                        Alignment = Definition.ToolbarAlignmentEnum.Top,
                                                        PrimaryItems = new List<ToolbarItem>
                                                                        {
                                                                            new SimpleButtonItem
                                                                                {
                                                                                    Id = "item1",
                                                                                    Image = "circle.png",
                                                                                    PrimaryItems = new List<ToolbarItem>
                                                                                                       {
                                                                                                           new ImageToggleButtonItem
                                                                                                               {
                                                                                                                   SelectedImage="bluecircle.png",
								 																				   UnselectedImage="circle.png",
																											       Selected=true,
																												   Tapped = (tb, tbi) => _button.SetTitle (((ImageToggleButtonItem)tbi).Selected ? "Selected" : "Not selected", UIControlState.Normal)
                                                                                                               },
                                                                                                            new SimpleButtonItem
                                                                                                                {
                                                                                                                    Image = "circle.png",
                                                                                                                    IsBackButton = true
                                                                                                                }
                                                                                                       }
                                                                                },
                                                                            new SimpleButtonItem
                                                                                {
                                                                                    Id = "item2",
                                                                                    Image = "polygon.png"
                                                                                }
                                                                        },
                                                        SecondaryItems = new List<ToolbarItem>
                                                                         {
                                                                             new SimpleButtonItem
                                                                                 {
                                                                                     Id= "item3",
                                                                                     Image = "star.png",
                                                                                     Tapped = (tb, tbi) => tb.SetButtonEnablement(!tb["item1"].Enabled, "item1", "item2")
                                                                                 }
                                                                         }
                                                    });

            _button = UIButton.FromType(UIButtonType.RoundedRect);

            _button.Frame = new RectangleF(
                View.Frame.Width / 2 - ButtonWidth / 2,
                View.Frame.Height / 2 - ButtonHeight / 2,
                ButtonWidth,
                ButtonHeight);

            _button.SetTitle("Click me", UIControlState.Normal);

            _button.TouchUpInside += (object sender, EventArgs e) =>
                                        {
                                            toolbar.Definition.IsVisible = !toolbar.Definition.IsVisible;
                                        };

            _button.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin |
                UIViewAutoresizing.FlexibleBottomMargin;

            View.AddSubview(_button);
        }

    }
}

