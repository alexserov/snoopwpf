namespace Snoop.DataAccess.Wpf {
    using System.Windows;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_CurrentApplication :DataAccess,  IDAS_CurrentApplication{
        public ISO_ResourceDictionary Resources {
            get { return (ISO_ResourceDictionary)SnoopObjectBase.Create(Application.Current.OnUI(x=>x.Resources)); }
        }
    }
}