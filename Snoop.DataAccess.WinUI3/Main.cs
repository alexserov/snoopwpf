namespace Snoop.DataAccess {
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Loader;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.DataAccess.WinUI3;
    using Snoop.Infrastructure;

    public class Extension : ExtensionBase<Extension> {
        public override void RegisterInterfaces() {
            this.Set<IDAS_TypeDescriptor>(new DAS_TypeDescriptor());
            this.Set<IDAS_CurrentApplication>(new DAS_CurrentApplication());
            this.Set<IDAS_InputManager>(new DAS_InputManager());
            this.Set<IDAS_Mouse>(new DAS_Mouse());
            this.Set<IDAS_RootProvider>(new DAS_RootProvider());
            this.Set<IDAS_TreeHelper>(new DAS_TreeHelper());
            this.Set<IDAS_WindowHelper>(new DAS_WindowHelper());
        }

        public override void StartSnoop() { SnoopManager.CreateSnoopWindow(this, this.data, this.data.StartTarget); }
        public Extension() : base("WinUI3") { }
    }

    public class ExtensionExecutor {
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public static int Start(string param) {
            PrepareWpfAsmLoader();
            AssemblyLoadContext.Default.Resolving += DefaultOnResolving;
            return (int)typeof(ExtensionExecutor).Assembly.GetType("Snoop.DataAccess.Extension").GetMethod("StartCore", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).Invoke(null, new[] { param });
        }


        static string GetAttachedAppPath() {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        static void PrepareWpfAsmLoader() {
            var elAsm = typeof(ExtensionExecutor).Assembly;
            var rootDir = GetAssemblyLoaderSolutionPath();
            if (!Directory.Exists(rootDir)) {
                Directory.CreateDirectory(rootDir);
                foreach (var element in new[] {
                    "App.xaml",
                    "App.xaml.cs",
                    "Snoop.DataAccess.WinUI3.WpfAsmLoader.csproj",
                }) {
                    var prefix = "Snoop.DataAccess.WinUI3.WpfAsmLoader";
                    var resourceName = string.Join('.', prefix, element);
                    var fileFullName = Path.Combine(rootDir, element);
                    if (!Directory.Exists(rootDir))
                        Directory.CreateDirectory(rootDir);
                    using var resourceStream = elAsm.GetManifestResourceStream(resourceName);
                    using var fileStream = new FileStream(fileFullName, FileMode.CreateNew, FileAccess.Write);
                    resourceStream.CopyTo(fileStream);
                }
            }

            if (!Directory.Exists(GetAssemblyLoaderBinariesPath())) {
                var arc = IntPtr.Size == 8 ? "x64" : "x86";
                // ReSharper disable once PossibleNullReferenceException
                var dotnetPSI = new ProcessStartInfo("dotnet") {
                    Arguments = $@"publish -f ""net5.0"" -c ""Debug"" -r ""win-{arc}"" -o ""{GetAssemblyLoaderBinariesPath()}"" ""{rootDir}\Snoop.DataAccess.WinUI3.WpfAsmLoader.csproj""",
                    Verb = "runas",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };
                var proc = new Process();
                proc.StartInfo = dotnetPSI;
                proc.OutputDataReceived += (sender, args) => { Debug.WriteLine(args.Data); };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.WaitForExit();
            }
        }

        static string GetAssemblyLoaderSolutionPath() { return Path.Combine(GetAttachedAppPath(), "Snoop.WpfAsmLoader"); }
        static string GetAssemblyLoaderBinariesPath() { return Path.Combine(GetAssemblyLoaderSolutionPath(), $"pub{(IntPtr.Size == 8 ? "x64" : "x86")}"); }
        static string GetGetSnoopDataAccessBinariesPath() { return Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location);}

        static string GetAssemblyFileName(AssemblyName name) {
            return $"{name.Name}.dll";
        }
        

        static Assembly DefaultOnResolving(AssemblyLoadContext arg1, AssemblyName arg2) {
            var asmFileName = GetAssemblyFileName(arg2);
            var targetFileName = Path.Combine(GetAttachedAppPath(), asmFileName);
            var sourceFileName = Path.Combine(GetAssemblyLoaderBinariesPath(), asmFileName);
            var alternativeSourceFileName = Path.Combine(GetGetSnoopDataAccessBinariesPath(), asmFileName);             
            try {
                if (File.Exists(sourceFileName))
                    File.Copy(sourceFileName, targetFileName, true);
                else if (File.Exists(alternativeSourceFileName))
                    File.Copy(alternativeSourceFileName, targetFileName, true);
            } catch {
                //bdyeetsch!!!
            }
            if (File.Exists(targetFileName))
                return Assembly.LoadFrom(targetFileName);
            return null;
        }
    }
}