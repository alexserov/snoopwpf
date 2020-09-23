using System;

namespace Snoop.DataAccess.Wpf {
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using Snoop.DataAccess.Generic;

    public class WpfLocator : Locator {
        protected override Regex WindowClassNameRegex {
            get { return new Regex(@"^HwndWrapper\[.*;.*;.*\]$", RegexOptions.Compiled); }
        }
        protected override IEnumerable<string> ValidModuleNames {
            get {
                yield return "PresentationFramework";
                yield return "PresentationCore";
                yield return "wpfgfx";
            }
        }

        public override string ExtensionPath {
            get { return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(typeof(WpfLocator).Assembly.Location), @"..\net472\Snoop.DataAccess.Wpf.dll")); }
        }
    }
}