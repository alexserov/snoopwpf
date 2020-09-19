namespace Snoop.DataAccess.WinUI3 {
    using System.Reflection;
    using Windows.Devices.Input;
    using Windows.UI.Core;
    using ABI.Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Media;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_Mouse : DataAccessBase, IDAS_Mouse{
        public ISnoopObject DirectlyOver {
            get { return SnoopObjectBase.Create(null); }
        }
    }
}