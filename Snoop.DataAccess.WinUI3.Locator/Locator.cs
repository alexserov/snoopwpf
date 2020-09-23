using System;

namespace Snoop.DataAccess.WinUI3 {
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Snoop.DataAccess.Generic;

    public class WinUI3Locator : Locator {
        protected override Regex WindowClassNameRegex {
            get { return new Regex(@"Microsoft.UI.Input.DesktopWindowLiftedContentBridge", RegexOptions.Compiled); }
        }

        protected override IEnumerable<string> ValidModuleNames {
            get {
                yield break;
            }
        }

        public override string ExtensionPath {
            get { return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(typeof(WinUI3Locator).Assembly.Location), @"..\net5.0\Snoop.DataAccess.WinUI3.dll")); }
        }
    }
}