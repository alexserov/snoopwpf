// (c) Copyright Cory Plotts.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace Snoop.Infrastructure
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Snoop.DataAccess.Interfaces;
    using Snoop.Infrastructure.Helpers;

    public static class ZoomerUtilities
    {
        public static UIElement CreateIfPossible(ISnoopObject item)
        {
            if (item is ISO_Window && VisualTreeHelper2.GetChildrenCount((ISO_Visual)item) == 1)
            {
                item = VisualTreeHelper2.GetChild((ISO_Visual)item, 0);
            }

            if (item is ISO_FrameworkElement)
            {
                var uiElement = (ISO_FrameworkElement)item;
                return CreateRectangleForFrameworkElement(uiElement);
            }
            else if (item is ISO_Visual)
            {
                var visual = (ISO_Visual)item;
                return CreateRectangleForVisual(visual);
            }
            else if (item is ISO_ResourceDictionary)
            {
                var stackPanel = new StackPanel();

                foreach (var value in ((ISO_ResourceDictionary)item).GetValues())
                {
                    var element = CreateIfPossible(value as ISnoopObject);
                    if (element != null)
                    {
                        stackPanel.Children.Add(element);
                    }
                }

                return stackPanel;
            }
            else if (item is ISO_Brush)
            {
                var rect = new Rectangle();
                rect.Width = 10;
                rect.Height = 10;
                rect.Fill = FromISOBrush((ISO_Brush)item);
                return rect;
            }
            else if (item is ISO_ImageSource)
            {
                var image = new Image();
                image.Source = FromISOImageSource((ISO_ImageSource)item);
                return image;
            }

            return null;
        }

        static Brush FromISOBrush(ISO_Brush source) {
            throw new NotImplementedException();
        }

        static ImageSource FromISOImageSource(ISO_ImageSource source) {
            throw new NotImplementedException();
        }

        private static UIElement CreateRectangleForVisual(ISO_Visual uiElement)
        {
            var brush = FromISOVisual(uiElement);
            
            var rect = new Rectangle();
            rect.Fill = brush;
            rect.Width = 50;
            rect.Height = 50;

            return rect;
        }

        public static Brush FromISOVisual(ISO_Visual source) {
            throw new NotImplementedException();
            // brush.Stretch = Stretch.Uniform;
        }

        private static UIElement CreateRectangleForFrameworkElement(ISO_FrameworkElement uiElement)
        {
            var brush = VisualCaptureUtil.CreateVisualBrushSafe(uiElement);
            if (brush == null)
            {
                return null;
            }

            var rect = new Rectangle();
            rect.Fill = brush;
            if (uiElement.GetActualHeight() == 0 && uiElement.GetActualWidth() == 0) //sometimes the actual size might be 0 despite there being a rendered visual with a size greater than 0. This happens often on a custom panel (http://snoopwpf.codeplex.com/workitem/7217). Having a fixed size visual brush remedies the problem.
            {
                rect.Width = 50;
                rect.Height = 50;
            }
            else
            {
                rect.Width = uiElement.GetActualWidth();
                rect.Height = uiElement.GetActualHeight();
            }

            return rect;
        }
    }
}
