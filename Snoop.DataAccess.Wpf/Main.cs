using System.Threading;

namespace Snoop.DataAccess {
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.DataAccess.Wpf;
    using Snoop.Infrastructure;

    public class Extension : ExtensionBase<Extension> {
        public Extension() : base("Wpf") { }


        public override void StartSnoop() { SnoopManager.CreateSnoopWindow(this, this.data, this.data.StartTarget); }

        public override void RegisterInterfaces() {
            this.Set<IDAS_CurrentApplication>(new DAS_CurrentApplication());
            this.Set<IDAS_InputManager>(new DAS_InputManager());
            this.Set<IDAS_Mouse>(new DAS_Mouse());
            this.Set<IDAS_RootProvider>(new DAS_RootProvider());
            this.Set<IDAS_TreeHelper>(new DAS_TreeHelper());            
            this.Set<IDAS_WindowHelper>(new DAS_WindowHelper());
        }
        
    }
    public class ExtensionExecutor {
        public static int Start(string param) {
            return Extension.StartCore(param);
        }
    }
}