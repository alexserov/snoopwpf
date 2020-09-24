// (c) Copyright Cory Plotts.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace Snoop.Infrastructure
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Threading;
    using JetBrains.Annotations;
    using Snoop.Converters;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.Infrastructure.Helpers;

    public class PropertyInformation : DependencyObject, IComparable, INotifyPropertyChanged
    {
        /// <summary>
        /// Normal constructor used when constructing PropertyInformation objects for properties.
        /// </summary>
        /// <param name="target">target object being shown in the property grid</param>
        /// <param name="property">the property around which we are constructing this PropertyInformation object</param>
        /// <param name="propertyName">the property name for the property that we use in the binding in the case of a non-dependency property</param>
        /// <param name="propertyDisplayName">the display name for the property that goes in the name column</param>
        public PropertyInformation(ISnoopObject target, ISO_PropertyDescriptor property, string propertyName, string propertyDisplayName)
        {
            this.Target = target;
            this.property = property;
            this.displayName = propertyDisplayName;
            try {
                this.Value = property.GetValue(target);                
            }catch{}

            this.Update();

            this.isRunning = true;
        }

        /// <summary>
        /// Normal constructor used when constructing PropertyInformation objects for properties.
        /// </summary>
        /// <param name="target">target object being shown in the property grid</param>
        /// <param name="property">the property around which we are constructing this PropertyInformation object</param>
        /// <param name="binding">the <see cref="BindingBase"/> from which the value should be retrieved</param>
        /// <param name="propertyDisplayName">the display name for the property that goes in the name column</param>
        public PropertyInformation(ISnoopObject target, ISO_PropertyDescriptor property, BindingBase binding, string propertyDisplayName)
        {
            this.Target = target;
            this.property = property;
            this.displayName = propertyDisplayName;

            try
            {
                BindingOperations.SetBinding(this, ValueProperty, binding);
            }
            catch (Exception)
            {
                // cplotts note:
                // warning: i saw a problem get swallowed by this empty catch (Exception) block.
                // in other words, this empty catch block could be hiding some potential future errors.
            }

            this.Update();

            this.isRunning = true;
        }

        /// <summary>
        /// Constructor used when constructing PropertyInformation objects for an item in a collection.
        /// In this case, we set the PropertyDescriptor for this object (in the property Property) to be null.
        /// This kind of makes since because an item in a collection really isn't a property on a class.
        /// That is, in this case, we're really hijacking the PropertyInformation class
        /// in order to expose the items in the Snoop property grid.
        /// </summary>
        /// <param name="target">the item in the collection</param>
        /// <param name="component">the collection</param>
        /// <param name="displayName">the display name that goes in the name column, i.e. this[x]</param>
        public PropertyInformation(ISnoopObject target, object component, string displayName, ISnoopObject value, bool isCopyable = false)
            : this(target, null, displayName, displayName)
        {
            this.component = component;
            this.isCopyable = isCopyable;
            this.isRunning = false;
            this.Value = value;
            this.isRunning = true;
        }

        public void Teardown()
        {
            this.isRunning = false;
            BindingOperations.ClearAllBindings(this);
        }

        public ISnoopObject Target { get; }

        public ISnoopObject Value
        {
            get { return (ISnoopObject)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(ISnoopObject),
                typeof(PropertyInformation),
                new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PropertyInformation)d).OnValueChanged(e);
        }

        protected virtual void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Update();

            if (this.isRunning)
            {
                if (this.breakOnChange)
                {
                    if (Debugger.IsAttached == false)
                    {
                        Debugger.Launch();
                    }

                    Debugger.Break();
                }

                this.HasChangedRecently = (e.OldValue?.Equals(e.NewValue) ?? e.OldValue == e.NewValue) == false;

                if (this.changeTimer == null)
                {
                    this.changeTimer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromSeconds(1.5)
                    };
                    this.changeTimer.Tick += this.HandleChangeExpiry;
                    this.changeTimer.Start();
                }
                else
                {
                    this.changeTimer.Stop();
                    this.changeTimer.Start();
                }
            }
        }

        private void HandleChangeExpiry(object sender, EventArgs e)
        {
            this.changeTimer.Stop();
            this.changeTimer = null;

            this.HasChangedRecently = false;
        }

        private DispatcherTimer changeTimer;

        public string StringValue
        {
            get
            {
                var value = this.Value;
                if (value != null)
                {
                    return value.ToString();
                }

                return string.Empty;
            }

            set
            {
#pragma warning disable WPF0036 // Avoid side effects in CLR accessors.
                if (this.property == null)
                {
                    // if this is a PropertyInformation object constructed for an item in a collection
                    // then just return, since setting the value via a string doesn't make sense.
                    return;
                }
#pragma warning restore WPF0036 // Avoid side effects in CLR accessors.

                var targetType = this.property.PropertyType;

                try
                {
                    this.property.SetValue(this.Target, value);
                }
                catch
                {
                }
            }
        }

        public string ResourceKey
        {
            get
            {
                var value = this.Value;
                if (value is null
                    || this.property is null)
                {
                    return null;
                }

                string resourceKey = null;

                if (this.Target is ISO_DependencyObject dependencyObject)
                {
                    // Cache the resource key for this item if not cached already. This could be done for more types, but would need to optimize perf.
                    if (this.TypeMightHaveResourceKey(this.property.PropertyType))
                    {
                        var resourceItem = value;
                        resourceKey = ResourceKeyCache.GetKey(resourceItem);

                        if (string.IsNullOrEmpty(resourceKey))
                        {
                            resourceKey = ResourceDictionaryKeyHelpers.GetKeyOfResourceItem(dependencyObject, resourceItem);
                            ResourceKeyCache.Cache(resourceItem, resourceKey);
                        }

                        Debug.Assert(resourceKey != null, "resourceKey != null");
                    }
                }

                return resourceKey;
            }
        }

        public string DescriptiveValue
        {
            get {
                return Convert.ToString(Value);
                //                 var value = this.Value;
                //                 if (value is null)
                //                 {
                //                     return string.Empty;
                //                 }
                //
                //                 var stringValue = value.ToString();
                //
                //                 if (stringValue.Equals(value.GetType().ToString()))
                //                 {
                //                     // Add brackets around types to distinguish them from values.
                //                     // Replace long type names with short type names for some specific types, for easier readability.
                //                     // FUTURE: This could be extended to other types.
                //                     if (value is BindingBase)
                //                     {
                // #pragma warning disable INPC013
                //                         stringValue = string.Format("[{0}]", "Binding");
                // #pragma warning restore INPC013
                //                     }
                //                     else if (value is DynamicResourceExtension)
                //                     {
                //                         stringValue = string.Format("[{0}]", "DynamicResource");
                //                     }
                //                     else if (this.property != null &&
                //                              (this.property.PropertyType == typeof(Brush) || this.property.PropertyType == typeof(Style)))
                //                     {
                //                         stringValue = string.Format("[{0}]", value.GetType().Name);
                //                     }
                //                     else
                //                     {
                //                         stringValue = string.Format("[{0}]", stringValue);
                //                     }
                //                 }
                //
                //                 // Display #00FFFFFF as Transparent for easier readability
                //                 if (this.property != null &&
                //                     this.property.PropertyType == typeof(Brush) &&
                //                     stringValue.Equals("#00FFFFFF"))
                //                 {
                //                     stringValue = "Transparent";
                //                 }
                //
                //                 if (this.Target is DependencyObject)
                //                 {
                //                     // Display both the value and the resource key, if there's a key for this property.
                //                     if (string.IsNullOrEmpty(this.ResourceKey) == false)
                //                     {
                //                         return string.Format("{0} {1}", this.ResourceKey, stringValue);
                //                     }
                //
                //                     // if the value comes from a Binding, show the path in [] brackets
                //                     if (this.IsExpression
                //                         && this.Binding is Binding)
                //                     {
                //                         stringValue = string.Format("{0} {1}", stringValue, this.BuildBindingDescriptiveString((Binding)this.Binding, true));
                //                     }
                //
                //                     // if the value comes from a MultiBinding, show the binding paths separated by , in [] brackets
                //                     else if (this.IsExpression
                //                         && this.Binding is MultiBinding)
                //                     {
                //                         stringValue += this.BuildMultiBindingDescriptiveString(((MultiBinding)this.Binding).Bindings.OfType<Binding>().ToArray());
                //                     }
                //
                //                     // if the value comes from a PriorityBinding, show the binding paths separated by , in [] brackets
                //                     else if (this.IsExpression && this.Binding is PriorityBinding)
                //                     {
                //                         stringValue += this.BuildMultiBindingDescriptiveString(((PriorityBinding)this.Binding).Bindings.OfType<Binding>().ToArray());
                //                     }
                //                 }
                //
                //                 return stringValue;
            }
        }

        private bool TypeMightHaveResourceKey(Type type)
        {
            return type == typeof(Style)
                   || type == typeof(ControlTemplate)
                   || type == typeof(Color)
                   || type == typeof(Brush);
        }

        /// <summary>
        /// Build up a string of Paths for a MultiBinding separated by ;
        /// </summary>
        private string BuildMultiBindingDescriptiveString(IEnumerable<Binding> bindings)
        {
            var ret = " {Paths=";
            foreach (var binding in bindings)
            {
                ret += this.BuildBindingDescriptiveString(binding, false);
                ret += ";";
            }

            ret = ret.Substring(0, ret.Length - 1); // remove trailing ,
            ret += "}";

            return ret;
        }

        /// <summary>
        /// Build up a string describing the Binding.  Path and ElementName (if present)
        /// </summary>
        private string BuildBindingDescriptiveString(Binding binding, bool isSinglePath)
        {
            var sb = new StringBuilder();
            var bindingPath = binding.Path.Path;
            var elementName = binding.ElementName;

            if (isSinglePath)
            {
                sb.Append("{Path=");
            }

            sb.Append(bindingPath);
            if (!string.IsNullOrEmpty(elementName))
            {
                sb.AppendFormat(", ElementName={0}", elementName);
            }

            if (isSinglePath)
            {
                sb.Append("}");
            }

            return sb.ToString();
        }

        public Type ComponentType
        {
            get
            {
                if (this.property == null)
                {
                    // if this is a PropertyInformation object constructed for an item in a collection
                    // then this.property will be null, but this.component will contain the collection.
                    // use this object to return the type of the collection for the ComponentType.
                    return this.component.GetType();
                }
                else
                {
                    return this.property.ComponentType;
                }
            }
        }

        private readonly object component;
        private readonly bool isCopyable;

        public Type PropertyType
        {
            get
            {
                if (this.property == null)
                {
                    // if this is a PropertyInformation object constructed for an item in a collection
                    // just return typeof(object) here, since an item in a collection ... really isn't a property.
                    return typeof(object);
                }
                else
                {
                    return this.property.PropertyType;
                }
            }
        }

        public Type ValueType
        {
            get
            {
                if (this.Value != null)
                {
                    return this.Value.GetType();
                }
                else
                {
                    return typeof(object);
                }
            }
        }

        public string BindingError
        {
            get { return this.bindingError; }
        }

        private string bindingError = string.Empty;

        public ISO_PropertyDescriptor Property
        {
            get { return this.property; }
        }

        private readonly ISO_PropertyDescriptor property;

        public string DisplayName
        {
            get { return this.displayName; }
        }

        private readonly string displayName;

        public bool IsInvalidBinding
        {
            get { return this.isInvalidBinding; }
        }

        private bool isInvalidBinding;

        public bool IsLocallySet
        {
            get { return this.isLocallySet; }
        }

        private bool isLocallySet;

        public bool IsValueChangedByUser { get; set; }

        public bool CanEdit
        {
            get
            {
                if (this.property == null)
                {
                    // if this is a PropertyInformation object constructed for an item in a collection
                    //return false;
                    return this.isCopyable;
                }
                else
                {
                    return !this.property.IsReadOnly;
                }
            }
        }

        public bool IsDatabound
        {
            get { return this.isDatabound; }
        }

        private bool isDatabound;

        public bool IsExpression
        {
            get { return this.valueSource.IsExpression; }
        }

        public bool IsAnimated
        {
            get { return this.valueSource.IsAnimated; }
        }

        public int Index
        {
            get { return this.index; }

            set
            {
                if (this.index != value)
                {
                    this.index = value;
                    this.OnPropertyChanged(nameof(this.Index));
                    this.OnPropertyChanged(nameof(this.IsOdd));
                }
            }
        }

        private int index;

        public bool IsOdd
        {
            get { return this.index % 2 == 1; }
        }

        public ISO_Binding Binding
        {
            get {
                return this.property.GetBinding(this.Target);
            }
        }

        public PropertyFilter Filter
        {
            get { return this.filter; }

            set
            {
                this.filter = value;

                this.OnPropertyChanged(nameof(this.IsVisible));
            }
        }

        private PropertyFilter filter;

        public bool BreakOnChange
        {
            get { return this.breakOnChange; }

            set
            {
                this.breakOnChange = value;
                this.OnPropertyChanged(nameof(this.BreakOnChange));
            }
        }

        private bool breakOnChange;

        public bool HasChangedRecently
        {
            get { return this.hasChangedRecently; }

            set
            {
                this.hasChangedRecently = value;
                this.OnPropertyChanged(nameof(this.HasChangedRecently));
            }
        }

        private bool hasChangedRecently;

        public ISO_ValueSource ValueSource
        {
            get { return this.valueSource; }
        }

        private ISO_ValueSource valueSource;

        public bool IsVisible
        {
            get { return this.filter.Show(this); }
        }

        public void Clear() {
            this.Property.Clear();
        }

        /// <summary>
        /// Returns the DependencyProperty identifier for the property that this PropertyInformation wraps.
        /// If the wrapped property is not a DependencyProperty, null is returned.
        /// </summary>

        private void Update()
        {

            this.isLocallySet = false;
            this.isInvalidBinding = false;
            this.isDatabound = false;
            
            this.bindingError = this.property.BindingError;
            this.valueSource = this.property.GetValueSource(Target);

            this.OnPropertyChanged(nameof(this.IsLocallySet));
            this.OnPropertyChanged(nameof(this.IsInvalidBinding));
            this.OnPropertyChanged(nameof(this.StringValue));
            this.OnPropertyChanged(nameof(this.ResourceKey));
            this.OnPropertyChanged(nameof(this.DescriptiveValue));
            this.OnPropertyChanged(nameof(this.IsDatabound));
            this.OnPropertyChanged(nameof(this.IsExpression));
            this.OnPropertyChanged(nameof(this.IsAnimated));
            this.OnPropertyChanged(nameof(this.ValueSource));
        }

        public static List<PropertyInformation> GetProperties(ISnoopObject obj)
        {
            return GetProperties(obj, PertinentPropertyFilter.Filter);
        }

        public static List<PropertyInformation> GetProperties(ISnoopObject obj, Func<ISnoopObject, ISO_PropertyDescriptor, bool> filter)
        {
            var properties = new List<PropertyInformation>();

            if (obj is null)
            {
                return properties;
            }

            // get the properties
            var propertyDescriptors = GetAllProperties(obj, getAllPropertiesAttributeFilter);

            // filter the properties
            foreach (var property in propertyDescriptors)
            {
                if (filter(obj, property))
                {
                    var prop = new PropertyInformation(obj, property, property.Name, property.DisplayName);
                    properties.Add(prop);
                }
            }

            // sort the properties before adding potential collection items
            properties.Sort();

            //delve path. also, issue 4919
            var extendedProps = GetExtendedProperties(obj);
            if (extendedProps != null)
            {
                properties.InsertRange(0, extendedProps);
            }

            // if the object is a collection, add the items in the collection as properties
            // if (obj is ICollection collection)
            // {
            //     var index = 0;
            //     foreach (var item in collection)
            //     {
            //         var info = new PropertyInformation(item, collection, "this[" + index + "]", item);
            //
            //         index++;
            //         properties.Add(info);
            //     }
            // }

            return properties;
        }

        /// <summary>
        /// 4919 + Delve
        /// </summary>
        /// <returns></returns>
        private static IList<PropertyInformation> GetExtendedProperties(ISnoopObject obj)
        {
            if (obj is null)
            {
                return null;
            }

            // if (ResourceKeyCache.Contains(obj))
            // {
            //     var key = ResourceKeyCache.GetKey(obj);
            //     var prop = new PropertyInformation(key, null, "x:Key", key, isCopyable: true);
            //     return new List<PropertyInformation>
            //     {
            //         prop
            //     };
            // }

            if (obj.Source is string
                || obj.Source.GetType().IsValueType)
            {
                return new List<PropertyInformation> { new PropertyInformation(obj, null, "ToString", obj, isCopyable: true) };
            }

            return null;
        }

        private static List<ISO_PropertyDescriptor> GetAllProperties(ISnoopObject obj, Attribute[] attributes)
        {
            var propertiesToReturn = new List<ISO_PropertyDescriptor>();

            // keep looping until you don't have an AmbiguousMatchException exception
            // and you normally won't have an exception, so the loop will typically execute only once.
            var noException = false;
            while (!noException && obj != null)
            {
                try
                {
                    
                    // try to get the properties using the GetProperties method that takes an instance
                    var properties = ExtensionLocator.From(obj as ISnoopObject).Get<IDAS_TypeDescriptor>().GetProperties(obj.Source.GetType(), attributes);
                    noException = true;

                    MergeProperties(properties, propertiesToReturn);
                }
                catch (System.Reflection.AmbiguousMatchException)
                {
                    // if we get an AmbiguousMatchException, the user has probably declared a property that hides a property in an ancestor
                    // see issue 6258 (http://snoopwpf.codeplex.com/workitem/6258)
                    //
                    // public class MyButton : Button
                    // {
                    //     public new double? Width
                    //     {
                    //         get { return base.Width; }
                    //         set { base.Width = value.Value; }
                    //     }
                    // }

                    var t = obj.GetType();
                    var properties = ExtensionLocator.From(obj as ISnoopObject).Get<IDAS_TypeDescriptor>().GetProperties(t, attributes);

                    MergeProperties(properties, propertiesToReturn);

                    var nextBaseTypeWithDefaultConstructor = GetNextTypeWithDefaultConstructor(t);
                    obj = null;
                }
            }

            return propertiesToReturn;
        }

        public static bool HasDefaultConstructor(Type type)
        {
            var constructors = type.GetConstructors();

            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static Type GetNextTypeWithDefaultConstructor(Type type)
        {
            var t = type.BaseType;

            while (!HasDefaultConstructor(t))
            {
                t = t.BaseType;
            }

            return t;
        }

        private static void MergeProperties(IEnumerable newProperties, ICollection<ISO_PropertyDescriptor> allProperties)
        {
            foreach (var newProperty in newProperties)
            {
                var newPropertyDescriptor = newProperty as ISO_PropertyDescriptor;
                if (newPropertyDescriptor == null)
                {
                    continue;
                }

                if (!allProperties.Contains(newPropertyDescriptor))
                {
                    allProperties.Add(newPropertyDescriptor);
                }
            }
        }

        private bool isRunning;
        private static readonly Attribute[] getAllPropertiesAttributeFilter = { new PropertyFilterAttribute(PropertyFilterOptions.All) };

        public bool IsCollection()
        {
            var pattern = "^this\\[\\d+\\]$";
            return Regex.IsMatch(this.DisplayName, pattern);
        }

        public int CollectionIndex()
        {
            if (this.IsCollection())
            {
                return int.Parse(this.DisplayName.Substring(5, this.DisplayName.Length - 6));
            }

            return -1;
        }

        #region IComparable Members
        public int CompareTo(object obj)
        {
            var thisIndex = this.CollectionIndex();
            var objIndex = ((PropertyInformation)obj).CollectionIndex();
            if (thisIndex >= 0 && objIndex >= 0)
            {
                return thisIndex.CompareTo(objIndex);
            }

            return this.DisplayName.CompareTo(((PropertyInformation)obj).DisplayName);
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
