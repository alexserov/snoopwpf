namespace Snoop.DataAccess.WinUI3.FW4Executor {
    using Snoop.DataAccess.Sessions;

    public class FW4Executor {
        public static int Start(string param) {
            ExtensionLocator.StartSnoop(param);
            return 0;
        }
    }
}