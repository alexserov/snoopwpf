namespace Snoop.DataAccess {
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Loader;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Sessions;
    using Snoop.DataAccess.WinUI3;
    using Snoop.Infrastructure;
    using NativeMethods = Snoop.Infrastructure.NativeMethods;

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
            AssemblyLoadContext.Default.ResolvingUnmanagedDll += DefaultOnResolvingUnmanagedDll;
            return (int)typeof(ExtensionExecutor).Assembly.GetType("Snoop.DataAccess.Extension").GetMethod("StartCore", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).Invoke(null, new[] { param });
        }


        static string GetAttachedAppPath() { return AppDomain.CurrentDomain.BaseDirectory; }

        static void PrepareWpfAsmLoader() {
            var elAsm = typeof(ExtensionExecutor).Assembly;
            var rootDir = GetAssemblyLoaderSolutionPath();
            Debug.WriteLine(@"=============================================
███████╗███╗   ██╗ ██████╗  ██████╗ ██████╗ 
██╔════╝████╗  ██║██╔═══██╗██╔═══██╗██╔══██╗
███████╗██╔██╗ ██║██║   ██║██║   ██║██████╔╝
╚════██║██║╚██╗██║██║   ██║██║   ██║██╔═══╝ 
███████║██║ ╚████║╚██████╔╝╚██████╔╝██║     
╚══════╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝ ╚═╝     
=============================================      
");
            if (!Directory.Exists(rootDir)) {
                Directory.CreateDirectory(rootDir);
                Debug.WriteLine("Copying WPFAsmLoader:");
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
                    Debug.WriteLine($"+ {element}");
                }
            }

            Debug.WriteLine("=============================================");
            if (!Directory.Exists(GetAssemblyLoaderBinariesPath())) {
                Debug.WriteLine("Publishing WPFAsmLoader");
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
                Debug.WriteLine("=============================================");
                Debug.WriteLine("Copying binaries: ");
                foreach (var file in Directory.GetFiles(GetAssemblyLoaderBinariesPath(), "*.dll", SearchOption.TopDirectoryOnly)) {
                    var fileName = Path.GetFileName(file);
                    try {
                        File.Copy(file, Path.Combine(GetAttachedAppPath(), fileName), true);
                        Debug.WriteLine($"- {fileName} ... OK");
                    } catch {
                        //bdyeetsch!!!
                        Debug.WriteLine($"- {fileName} ... FAILURE");
                    }
                }
                Debug.WriteLine("Done!");
                Debug.WriteLine("=============================================");
            }
        }

        static string GetAssemblyLoaderSolutionPath() {
            var vsuffix = 1;//change vsuffix every time you change the loading logic
            return Path.Combine(GetAttachedAppPath(), $"Snoop.WpfAsmLoader_v{vsuffix}"); 
            
        }
        static string GetAssemblyLoaderBinariesPath() { return Path.Combine(GetAssemblyLoaderSolutionPath(), $"pub{(IntPtr.Size == 8 ? "x64" : "x86")}"); }
        static string GetGetSnoopDataAccessBinariesPath() { return Path.GetDirectoryName(typeof(ExtensionExecutor).Assembly.Location); }

        static string GetAssemblyFileName(AssemblyName name) { return $"{name.Name}.dll"; }
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);
        [System.Flags]
        enum LoadLibraryFlags : uint
        {    
            None = 0,
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }
        static IntPtr DefaultOnResolvingUnmanagedDll(Assembly arg1, string arg2) {
            var fileName = Path.Combine(GetAssemblyLoaderBinariesPath(), arg2);            
            if (File.Exists(fileName)) {
                var result = LoadLibraryEx(fileName, IntPtr.Zero, LoadLibraryFlags.None);
                if (result == IntPtr.Zero) {
                    var error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error);
                }
            }
            return IntPtr.Zero;
        }

        static string CopyAndGetFileName(string asmFileName) {
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
                return null;
            }

            if (File.Exists(targetFileName))
                return targetFileName;
            return null;
        }

        static Assembly DefaultOnResolving(AssemblyLoadContext arg1, AssemblyName arg2) {
            var fileName = CopyAndGetFileName(GetAssemblyFileName(arg2));
            if (!string.IsNullOrEmpty(fileName))
                return Assembly.LoadFrom(fileName);
            return null;
        }
    }
}