namespace Snoop.DataAccess.Wpf
{
    // public class ApplicationTreeItem : ResourceContainerTreeItem
    // {
    //     private readonly Application application;
    //
    //     public ApplicationTreeItem(Application application, TreeItem parent, TreeService treeService)
    //         : base(application, parent, treeService)
    //     {
    //         this.application = application;
    //         this.IsExpanded = true;
    //     }
    //
    //     public override Visual MainVisual => this.application.MainWindow;
    //
    //     protected override ResourceDictionary ResourceDictionary => this.application.Resources;
    //
    //     protected override void ReloadCore()
    //     {
    //         // having the call to base.ReloadCore here ... puts the application resources at the very top of the tree view
    //         base.ReloadCore();
    //
    //         foreach (Window window in this.application.Windows)
    //         {
    //             if (window.IsInitialized == false
    //                 || window.CheckAccess() == false
    //                 || window.IsPartOfSnoopVisualTree())
    //             {
    //                 continue;
    //             }
    //
    //             // windows which have an owner are added as child items in VisualItem, so we have to skip them here
    //             if (window.Owner != null)
    //             {
    //                 continue;
    //             }
    //
    //             this.Children.Add(this.TreeService.Construct(window, this));
    //         }
    //     }
    // }
    // public class AutomationPeerTreeItem : TreeItem
    // {
    //     private AdornerContainer adornerContainer;
    //
    //     public AutomationPeerTreeItem(AutomationPeer target, TreeItem parent, TreeService treeService)
    //         : base(target, parent, treeService)
    //     {
    //         if (this.Target is UIElementAutomationPeer uiElementAutomationPeer
    //             && uiElementAutomationPeer.Owner is null == false)
    //         {
    //             this.Visual = uiElementAutomationPeer.Owner;
    //         }
    //     }
    //
    //     [CanBeNull]
    //     public Visual Visual { get; }
    //
    //     protected override void ReloadCore()
    //     {
    //         if (this.Target is UIElementAutomationPeer uiElementAutomationPeer
    //             && uiElementAutomationPeer.Owner is null == false)
    //         {
    //             this.Children.Add(RawTreeServiceWithoutChildren.DefaultInstance.Construct(uiElementAutomationPeer.Owner, this));
    //         }
    //
    //         foreach (var child in this.TreeService.GetChildren(this))
    //         {
    //             if (child is null)
    //             {
    //                 continue;
    //             }
    //
    //             this.Children.Add(this.TreeService.Construct(child, this));
    //         }
    //     }
    //
    //     protected override void OnIsSelectedChanged()
    //     {
    //         if (this.Visual is null)
    //         {
    //             return;
    //         }
    //
    //         // Add adorners for the visual this is representing.
    //         var adornerLayer = AdornerLayer.GetAdornerLayer(this.Visual);
    //
    //         if (adornerLayer != null
    //             && this.Visual is UIElement visualElement)
    //         {
    //             if (this.IsSelected
    //                 && this.adornerContainer == null)
    //             {
    //                 var border = new Border
    //                 {
    //                     BorderThickness = new Thickness(4),
    //                     IsHitTestVisible = false
    //                 };
    //
    //                 var borderColor = new Color
    //                 {
    //                     ScA = .3f,
    //                     ScR = 1
    //                 };
    //                 border.BorderBrush = new SolidColorBrush(borderColor);
    //
    //                 this.adornerContainer = new AdornerContainer(visualElement)
    //                 {
    //                     Child = border
    //                 };
    //                 adornerLayer.Add(this.adornerContainer);
    //             }
    //             else if (this.adornerContainer != null)
    //             {
    //                 adornerLayer.Remove(this.adornerContainer);
    //                 this.adornerContainer.Child = null;
    //                 this.adornerContainer = null;
    //             }
    //         }
    //     }
    // }
    // public class ChildWindowsTreeItem : TreeItem
    // {
    //     private readonly Window targetWindow;
    //
    //     public ChildWindowsTreeItem(Window target, TreeItem parent, TreeService treeService)
    //         : base(target, parent, treeService)
    //     {
    //         this.targetWindow = target;
    //     }
    //
    //     protected override void ReloadCore()
    //     {
    //         base.ReloadCore();
    //
    //         foreach (Window ownedWindow in this.targetWindow.OwnedWindows)
    //         {
    //             if (ownedWindow.IsInitialized == false
    //                 || ownedWindow.CheckAccess() == false
    //                 || ownedWindow.IsPartOfSnoopVisualTree())
    //             {
    //                 continue;
    //             }
    //
    //             this.Children.Add(this.TreeService.Construct(ownedWindow, this));
    //         }
    //     }
    //
    //     public override string ToString()
    //     {
    //         return $"{this.Children.Count} child windows";
    //     }
    // }
    //  public class DependencyObjectTreeItem : ResourceContainerTreeItem
    // {
    //     private static readonly Attribute[] propertyFilterAttributes = { new PropertyFilterAttribute(PropertyFilterOptions.All) };
    //
    //     private AdornerContainer adornerContainer;
    //
    //     public DependencyObjectTreeItem(DependencyObject target, TreeItem parent, TreeService treeService)
    //         : base(target, parent, treeService)
    //     {
    //         this.DependencyObject = target;
    //         this.Visual = target as Visual;
    //     }
    //
    //     [NotNull]
    //     public DependencyObject DependencyObject { get; }
    //
    //     [CanBeNull]
    //     public Visual Visual { get; }
    //
    //     public override bool HasBindingError
    //     {
    //         get
    //         {
    //             var propertyDescriptors = TypeDescriptor.GetProperties(this.DependencyObject, propertyFilterAttributes);
    //
    //             foreach (PropertyDescriptor property in propertyDescriptors)
    //             {
    //                 var dpd = DependencyPropertyDescriptor.FromProperty(property);
    //                 if (dpd == null)
    //                 {
    //                     continue;
    //                 }
    //
    //                 var expression = BindingOperations.GetBindingExpressionBase(this.DependencyObject, dpd.DependencyProperty);
    //                 if (expression != null
    //                     && (expression.HasError || expression.Status != BindingStatus.Active))
    //                 {
    //                     return true;
    //                 }
    //             }
    //
    //             return false;
    //         }
    //     }
    //
    //     public override Visual MainVisual => this.Visual;
    //
    //     public override Brush TreeBackgroundBrush => Brushes.Transparent;
    //
    //     public override Brush VisualBrush
    //     {
    //         get
    //         {
    //             var brush = VisualCaptureUtil.CreateVisualBrushSafe(this.Visual);
    //             if (brush != null)
    //             {
    //                 brush.Stretch = Stretch.Uniform;
    //             }
    //
    //             return brush;
    //         }
    //     }
    //
    //     protected override ResourceDictionary ResourceDictionary
    //     {
    //         get
    //         {
    //             if (this.Target is FrameworkElement frameworkElement)
    //             {
    //                 return frameworkElement.Resources;
    //             }
    //
    //             if (this.Target is FrameworkContentElement frameworkContentElement)
    //             {
    //                 return frameworkContentElement.Resources;
    //             }
    //
    //             return null;
    //         }
    //     }
    //
    //     protected override void OnIsSelectedChanged()
    //     {
    //         if (this.Visual is null)
    //         {
    //             return;
    //         }
    //
    //         // Add adorners for the visual this is representing.
    //         var adornerLayer = AdornerLayer.GetAdornerLayer(this.Visual);
    //
    //         if (adornerLayer != null
    //             && this.Visual is UIElement visualElement)
    //         {
    //             if (this.IsSelected
    //                 && this.adornerContainer == null)
    //             {
    //                 var border = new Border
    //                 {
    //                     BorderThickness = new Thickness(4),
    //                     IsHitTestVisible = false
    //                 };
    //
    //                 var borderColor = new Color
    //                 {
    //                     ScA = .3f,
    //                     ScR = 1
    //                 };
    //                 border.BorderBrush = new SolidColorBrush(borderColor);
    //
    //                 this.adornerContainer = new AdornerContainer(visualElement)
    //                 {
    //                     Child = border
    //                 };
    //                 adornerLayer.Add(this.adornerContainer);
    //             }
    //             else if (this.adornerContainer != null)
    //             {
    //                 adornerLayer.Remove(this.adornerContainer);
    //                 this.adornerContainer.Child = null;
    //                 this.adornerContainer = null;
    //             }
    //         }
    //     }
    //     
    //     protected override void ReloadCore()
    //     {
    //         // having the call to base.ReloadCore here ... puts the application resources at the very top of the tree view.
    //         // this used to be at the bottom. putting it here makes it consistent (and easier to use) with ApplicationTreeItem
    //         base.ReloadCore();
    //
    //         foreach (var child in this.TreeService.GetChildren(this))
    //         {
    //             if (child is null)
    //             {
    //                 continue;
    //             }
    //
    //             this.Children.Add(this.TreeService.Construct(child, this));
    //         }
    //
    //         if (this.Target is Grid grid
    //             // The logical tree already contains these elements
    //             && this.TreeService.TreeType != TreeType.Logical)
    //         {
    //             foreach (var row in grid.RowDefinitions)
    //             {
    //                 this.Children.Add(this.TreeService.Construct(row, this));
    //             }
    //
    //             foreach (var column in grid.ColumnDefinitions)
    //             {
    //                 this.Children.Add(this.TreeService.Construct(column, this));
    //             }
    //         }
    //     }
    // }
    // public class PopupTreeItem : DependencyObjectTreeItem
    // {
    //     public PopupTreeItem(Popup target, TreeItem parent, TreeService treeService)
    //         : base(target, parent, treeService)
    //     {
    //         this.PopupTarget = target;
    //     }
    //
    //     public Popup PopupTarget { get; }
    //
    //     protected override void ReloadCore()
    //     {
    //         base.ReloadCore();
    //
    //         if (this.TreeService.TreeType == TreeType.Visual)
    //         {
    //             foreach (var child in LogicalTreeHelper.GetChildren(this.PopupTarget))
    //             {
    //                 this.Children.Add(this.TreeService.Construct(child, this));
    //             }
    //         }
    //     }
    // }
    // public abstract class ResourceContainerTreeItem : TreeItem
    // {
    //     protected ResourceContainerTreeItem(object target, TreeItem parent, TreeService treeService)
    //         : base(target, parent, treeService)
    //     {
    //     }
    //
    //     protected abstract ResourceDictionary ResourceDictionary { get; }
    //
    //     protected override void ReloadCore()
    //     {
    //         var resourceDictionary = this.ResourceDictionary;
    //
    //         if (resourceDictionary != null
    //             && (resourceDictionary.Count != 0 || resourceDictionary.MergedDictionaries.Count > 0))
    //         {
    //             this.Children.Add(this.TreeService.Construct(resourceDictionary, this));
    //         }
    //
    //         base.ReloadCore();
    //     }
    // }
    //  public class ResourceDictionaryTreeItem : TreeItem
    // {
    //     private static readonly SortDescription dictionarySortDescription = new SortDescription(nameof(SortOrder), ListSortDirection.Ascending);
    //     private static readonly SortDescription displayNameSortDescription = new SortDescription(nameof(DisplayName), ListSortDirection.Ascending);
    //
    //     private readonly ResourceDictionary dictionary;
    //
    //     public ResourceDictionaryTreeItem(ResourceDictionary dictionary, TreeItem parent, TreeService treeService)
    //         : base(dictionary, parent, treeService)
    //     {
    //         this.dictionary = dictionary;
    //
    //         var childrenView = CollectionViewSource.GetDefaultView(this.Children);
    //         childrenView.SortDescriptions.Add(dictionarySortDescription);
    //         childrenView.SortDescriptions.Add(displayNameSortDescription);
    //     }
    //
    //     public override TreeItem FindNode(object target)
    //     {
    //         return null;
    //     }
    //
    //     protected override string GetName()
    //     {
    //         var source = this.dictionary.Source?.ToString();
    //
    //         if (string.IsNullOrEmpty(source))
    //         {
    //             return base.GetName();
    //         }
    //
    //         return source;
    //     }
    //
    //     protected override void ReloadCore()
    //     {
    //         var order = 0;
    //         foreach (var mergedDictionary in this.dictionary.MergedDictionaries)
    //         {
    //             var resourceDictionaryItem = new ResourceDictionaryTreeItem(mergedDictionary, this, this.TreeService)
    //             {
    //                 SortOrder = order
    //             };
    //             resourceDictionaryItem.Reload();
    //
    //             this.Children.Add(resourceDictionaryItem);
    //
    //             ++order;
    //         }
    //
    //         foreach (var key in this.dictionary.Keys)
    //         {
    //             object target;
    //             try
    //             {
    //                 target = this.dictionary[key];
    //             }
    //             catch (XamlParseException)
    //             {
    //                 // sometimes you can get a XamlParseException ... because the xaml you are Snoop(ing) is bad.
    //                 // e.g. I got this once when I was Snoop(ing) some xaml that was referring to an image resource that was no longer there.
    //                 // in this case, just continue to the next resource in the dictionary.
    //                 continue;
    //             }
    //
    //             if (target == null)
    //             {
    //                 // you only get a XamlParseException once. the next time through target just comes back null.
    //                 // in this case, just continue to the next resource in the dictionary (as before).
    //                 continue;
    //             }
    //
    //             this.Children.Add(new ResourceItem(target, key, this, this.TreeService));
    //         }
    //     }
    //
    //     public override string ToString()
    //     {
    //         var source = this.dictionary.Source?.ToString();
    //
    //         if (string.IsNullOrEmpty(source))
    //         {
    //             return $"{this.Children.Count} resources";
    //         }
    //
    //         return $"{this.Children.Count} resources ({source})";
    //     }
    // }
    //
    // public class ResourceItem : TreeItem
    // {
    //     private readonly object key;
    //
    //     public ResourceItem(object target, object key, TreeItem parent, TreeService treeService)
    //         : base(target, parent, treeService)
    //     {
    //         this.key = key;
    //         this.SortOrder = int.MaxValue;
    //     }
    //
    //     public override string DisplayName => this.key.ToString();
    //
    //     public override string ToString()
    //     {
    //         return $"{this.key} ({this.Target.GetType().Name})";
    //     }
    // }
    // public class WindowTreeItem : DependencyObjectTreeItem
    // {
    //     public WindowTreeItem(Window target, TreeItem parent, TreeService treeService)
    //         : base(target, parent, treeService)
    //     {
    //         this.WindowTarget = target;
    //     }
    //
    //     public Window WindowTarget { get; }
    //
    //     protected override void ReloadCore()
    //     {
    //         foreach (Window ownedWindow in this.WindowTarget.OwnedWindows)
    //         {
    //             if (ownedWindow.IsInitialized == false
    //                 || ownedWindow.CheckAccess() == false
    //                 || ownedWindow.IsPartOfSnoopVisualTree())
    //             {
    //                 continue;
    //             }
    //
    //             var childWindowsTreeItem = new ChildWindowsTreeItem(this.WindowTarget, this, this.TreeService);
    //             childWindowsTreeItem.Reload();
    //             this.Children.Add(childWindowsTreeItem);
    //             break;
    //         }
    //
    //         base.ReloadCore();
    //     }
    // }
}