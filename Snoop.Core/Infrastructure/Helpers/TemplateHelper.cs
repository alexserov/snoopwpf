namespace Snoop.Infrastructure.Helpers
{
    using System.Windows;
    using System.Windows.Controls;
    using Snoop.DataAccess.Interfaces;

    public class TemplateHelper
    {
        public static ISnoopObject GetChildFromTemplateIfNeeded(ISO_DependencyObject element, string templatePartName)
        {
            if (string.IsNullOrEmpty(templatePartName))
            {
                return element;
            }
#if TODO
            if (element is ISO_DependencyObject control
                && control.Template != null)
            {
                return control.Template.FindName(templatePartName, control);
            }

            if (element is ISO_DependencyObject fe)
            {
                return fe.FindName(templatePartName);
            }
#endif
            return null;
        }
    }
}