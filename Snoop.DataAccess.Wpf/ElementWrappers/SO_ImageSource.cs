﻿namespace Snoop.DataAccess.Wpf {
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Snoop.DataAccess.Interfaces;

    public class SO_ImageSource : SO_DependencyObject, ISO_ImageSource {
        public override Type DataAccessType {
            get { return typeof(ISO_ImageSource); }
        }
        public SO_ImageSource(ImageSource source) : base(source) { }
        public byte[] GetData() { return null; }

    }
}