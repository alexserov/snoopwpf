namespace Snoop.DataAccess.Wpf {
    using System;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_InjectorLibraryPath : IDAS_InjectorLibraryPath {
        public DAS_InjectorLibraryPath() {
            this.Id = Guid.NewGuid().ToString();
        }
        public string GetPath() { return typeof(DAS_InjectorLibraryPath).Assembly.Location; }
        public string Id { get; }
    }
}