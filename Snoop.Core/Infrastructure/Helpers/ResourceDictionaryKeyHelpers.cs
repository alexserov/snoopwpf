// (c) Copyright Cory Plotts.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace Snoop.Infrastructure.Helpers
{
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public static class ResourceDictionaryKeyHelpers
    {
        public static string GetKeyOfResourceItem(ISO_DependencyObject dependencyObject, object resourceItem)
        {
            if (dependencyObject is null
                || resourceItem is null)
            {
                return string.Empty;
            }

            var ext = Extension.From(dependencyObject);
            // Walk up the visual tree, looking for the resourceItem in each frameworkElement's resource dictionary.
            while (dependencyObject != null)
            {
                var frameworkElement = dependencyObject as ISO_FrameworkElement;
                if (frameworkElement != null)
                {
                    var resourceKey = GetKeyInResourceDictionary(frameworkElement.GetResources(), resourceItem);
                    if (resourceKey != null)
                    {
                        return resourceKey;
                    }
                }
                else
                {
                    break;
                }

                dependencyObject = VisualTreeHelper2.GetParent(dependencyObject);
            }

            // check the application resources
            if (Application.Current != null)
            {
                var resourceKey = GetKeyInResourceDictionary(ext.Get<IDAS_CurrentApplication>().Resources, resourceItem);
                if (resourceKey != null)
                {
                    return resourceKey;
                }
            }

            return string.Empty;
        }

        public static string GetKeyInResourceDictionary(ISO_ResourceDictionary dictionary, object resourceItem)
        {
            foreach (var key in dictionary.GetKeys())
            {
                if (dictionary.GetValue(key) == resourceItem)
                {
                    return key.ToString();
                }
            }

            if (dictionary.GetMergedDictionaries() != null)
            {
                foreach (var dic in dictionary.GetMergedDictionaries())
                {
                    var name = GetKeyInResourceDictionary(dic, resourceItem);
                    if (!string.IsNullOrEmpty(name))
                    {
                        return name;
                    }
                }
            }

            return null;
        }
    }
}