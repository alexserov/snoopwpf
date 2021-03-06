﻿// (c) Copyright Cory Plotts.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

namespace Snoop
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using CommandLine;
    using Snoop.Data;
    using Snoop.InjectorLauncher;

    /// <summary>
    /// Class responsible for launching a new injector process.
    /// </summary>
    public static class InjectorLauncherManager
    {
        // This method has to work without exceptions.
        // If we get the architecture wrong here the InjectorLauncher will fix this by starting a secondary instance.
        // IsProcess64BitWithoutException will return false in case of a failure, this way we ensure that the x86 InjectorLauncher is run, which should be able to run on every system.
        private static string GetArchitectureSuffix(bool x64) { return x64 ? "x64" : "x86"; }

        public static void Launch(bool x64, int targetpid, bool debug, string assembly, string className, string methodName, string launcherParam, string forcedFramework = null)
        {

            try
            {
                var location = typeof(InjectorLauncherManager).Assembly.Location;
                var directory = Path.GetDirectoryName(location) ?? string.Empty;
                var injectorLauncherExe = Path.Combine(directory, $"Snoop.InjectorLauncher.{GetArchitectureSuffix(x64)}.exe");
                if (!File.Exists(injectorLauncherExe))
                {
                    injectorLauncherExe = Path.GetFullPath(Path.Combine(directory, $@"..\Snoop.InjectorLauncher.{GetArchitectureSuffix(x64)}.exe"));
                }
                if (File.Exists(injectorLauncherExe) == false)
                {
                    var message = @$"Could not find the injector launcher ""{injectorLauncherExe}"".
Snoop requires this component, which is part of the Snoop project, to do it's job.
- If you compiled snoop yourself, you should compile all required components.
- If you downloaded snoop you should not omit any files contained in the archive you downloaded and make sure that no anti virus deleted the file.";
                    throw new FileNotFoundException(message, injectorLauncherExe);
                }

                var injectorLauncherCommandLineOptions = new InjectorLauncherCommandLineOptions
                {
                    TargetPID = targetpid,
                    Assembly = assembly,
                    ClassName = className,
                    MethodName = methodName,
                    LauncherParam = launcherParam,
                    ForcedFramework = forcedFramework,
                    Debug = debug
                };

                var commandLine = Parser.Default.FormatCommandLine(injectorLauncherCommandLineOptions);
                var startInfo = new ProcessStartInfo(injectorLauncherExe, commandLine)
                {
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "runas"
                };

                using var process = Process.Start(startInfo);
                process?.WaitForExit();
            }
            finally
            {
                File.Delete(launcherParam);
            }
        }
    }
}