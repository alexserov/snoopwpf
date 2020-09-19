namespace Snoop.Data.Tree
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;
    using Snoop.DataAccess.Interfaces;
    using Snoop.Infrastructure.Helpers;

    public enum TreeType
    {
        Visual,

        Logical,

        Automation
    }

    public class TreeService
    {
        // public abstract TreeType TreeType { get; }

        // public IEnumerable GetChildren(TreeItem treeItem)
        // {
        //     return this.GetChildren(treeItem.Target);
        // }

        // public abstract IEnumerable GetChildren(object target);

        public virtual TreeItem Construct(ISnoopObject target, TreeItem parent)
        {
            TreeItem treeItem = new TreeItem(target, parent, this);

            treeItem.Reload();

            return treeItem;
        }

        // public static TreeService From(TreeType treeType)
        // {
        //     switch (treeType)
        //     {
        //         case TreeType.Visual:
        //             return new VisualTreeService();
        //
        //         case TreeType.Logical:
        //             throw new NotImplementedException();
        //             // return new LogicalTreeService();
        //
        //         case TreeType.Automation:
        //             throw new NotImplementedException();
        //             // return new AutomationPeerTreeService();
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(treeType), treeType, null);
        //     }
        // }
    }

    // public sealed class RawTreeServiceWithoutChildren : TreeService
    // {
    //     public static readonly RawTreeServiceWithoutChildren DefaultInstance = new RawTreeServiceWithoutChildren();
    //
    //     public override TreeType TreeType { get; } = TreeType.Visual;
    //
    //     public override IEnumerable GetChildren(object target)
    //     {
    //         yield break;
    //     }
    // }
    //
    // public sealed class VisualTreeService : TreeService
    // {
    //     public override TreeType TreeType { get; } = TreeType.Visual;
    //
    //     public override IEnumerable GetChildren(ISnoopObject target)
    //     {
    //         if (!(target is ISO_DependencyObject dependencyObject)
    //             || (target is ISO_Visual == false && target is Visual3D == false))
    //         {
    //             yield break;
    //         }
    //
    //         var childrenCount = VisualTreeHelper2.GetChildrenCount(dependencyObject);
    //
    //         for (var i = 0; i < childrenCount; i++)
    //         {
    //             var child = VisualTreeHelper2.GetChild(dependencyObject, i);
    //             yield return child;
    //         }
    //     }
    // }

    // public sealed class LogicalTreeService : TreeService
    // {
    //     public override TreeType TreeType { get; } = TreeType.Logical;
    //
    //     public override IEnumerable GetChildren(object target)
    //     {
    //         if (!(target is DependencyObject dependencyObject))
    //         {
    //             yield break;
    //         }
    //
    //         foreach (var child in LogicalTreeHelper.GetChildren(dependencyObject))
    //         {
    //             yield return child;
    //         }
    //     }
    // }

    // public sealed class AutomationPeerTreeService : TreeService
    // {
    //     public override TreeType TreeType { get; } = TreeType.Automation;
    //
    //     public override TreeItem Construct(object target, TreeItem parent)
    //     {
    //         if (!(target is AutomationPeer automationPeer)
    //             && target is UIElement element)
    //         {
    //             target = UIElementAutomationPeer.CreatePeerForElement(element);
    //         }
    //
    //         return base.Construct(target, parent);
    //     }
    //
    //     public override IEnumerable GetChildren(object target)
    //     {
    //         if (!(target is AutomationPeer automationPeer))
    //         {
    //             yield break;
    //         }
    //
    //         var children = automationPeer.GetChildren();
    //
    //         if (children is null)
    //         {
    //             yield break;
    //         }
    //
    //         foreach (var child in children)
    //         {
    //             yield return child;
    //         }
    //     }
    // }

    // public sealed class AutomationElementTreeService : TreeService
    // {
    //     private static readonly TreeWalker treeWalker = TreeWalker.ControlViewWalker;
    //
    //     public override TreeType TreeType { get; } = TreeType.Automation;
    //
    //     public override IEnumerable GetChildren(object target)
    //     {
    //         if (!(target is AutomationElement automationElement))
    //         {
    //             yield break;
    //         }
    //
    //         AutomationElement child;
    //         try
    //         {
    //             child = treeWalker.GetFirstChild(automationElement);
    //         }
    //         catch (Exception)
    //         {
    //             yield break;
    //         }
    //
    //         while (child != null)
    //         {
    //             yield return child;
    //
    //             try
    //             {
    //                 child = treeWalker.GetNextSibling(child);
    //             }
    //             catch (Exception)
    //             {
    //                 child = null;
    //             }
    //         }
    //     }
    // }
}