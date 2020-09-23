
namespace Snoop.DataAccess.WinUI3 {
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using Windows.Devices.Input;
    using Windows.UI.Core;
    using Windows.UI.Input.Preview.Injection;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Input;
    using Snoop.DataAccess.Interfaces;
    using Snoop.DataAccess.Internal.Interfaces;
    using Snoop.DataAccess.Sessions;

    public class DAS_InputManager : DataAccess, IDAS_InputManager {
        Action preProcessInput = new Action(() => { });
        // InputInjector injector;
        public event Action PreProcessInput {
            add { this.preProcessInput += value; }
            remove { this.preProcessInput -= value; }
        }

        public DAS_InputManager() {
            var wnd = WindowLocator.GetWindow();
            // this.injector = InputInjector.TryCreate();
        }

        void OnMouseMoved(MouseDevice sender, MouseEventArgs args) {
            this.preProcessInput();
        }
        
    }

    class WindowLocator {
        public static Window GetWindow() {
            var ta = Application.Current.GetType();
            foreach (var field in ta.GetFields(BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic)) {
                if (field.FieldType == typeof(Window)) {
                    var wnd = field.GetValue(field.IsStatic ? null : Application.Current);
                    if (wnd != null)
                        return wnd as Window;
                }
            }

            return null;
        }
#pragma warning disable 649
        // REQUIRED CONSTS

        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;

        // REQUIRED METHODS

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess
            (int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory
            (int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(
            IntPtr hProcess,
            IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);


        // REQUIRED STRUCTS

        public struct MEMORY_BASIC_INFORMATION {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int lType;
        }

        public struct SYSTEM_INFO {
            public ushort processorArchitecture;
#pragma warning disable 169
            ushort reserved;
#pragma warning restore 169
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

             public static void Execute()
        {
            // getting minimum & maximum address

            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            // saving the values as long ints so I won't have to do a lot of casts later
            long proc_min_address_l = (long)proc_min_address;
            long proc_max_address_l = (long)proc_max_address;


            // notepad better be runnin'
            Process process = Process.GetCurrentProcess();

            // opening the process with desired access level
            IntPtr processHandle =
                OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);
            // 40 7a a6 78 04 7a a6 78 f4 79 a6 78
            var searchingFor = new byte[] { 0x40, 0x7a, 0xa6, 0x78, 0x04, 0x7a, 0xa6, 0x78, 0xf4, 0x79, 0xa6, 0x78 };
            var searchingForString = Encoding.ASCII.GetString(searchingFor);

            // this will store any information we get from VirtualQueryEx()
            MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();

            int bytesRead = 0; // number of bytes read with ReadProcessMemory
            int i = 0;
            var acptr = Application.Current.ThisPtr;
            while (proc_min_address_l < proc_max_address_l)
            {
                // 28 = sizeof(MEMORY_BASIC_INFORMATION)
                VirtualQueryEx(processHandle, proc_min_address, out mem_basic_info, 28);

                // if this memory chunk is accessible
                if (mem_basic_info.Protect ==
                    PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT)
                {
                    byte[] buffer = new byte[mem_basic_info.RegionSize];

                    // read everything in the buffer above
                    ReadProcessMemory((int)processHandle,
                                      mem_basic_info.BaseAddress, buffer, mem_basic_info.RegionSize, ref bytesRead);
                    var bufferString = Encoding.ASCII.GetString(buffer);
                    if (bufferString.IndexOf(searchingForString) >= 0)
                    {
                        var ptr = new IntPtr(proc_min_address_l + bufferString.IndexOf(searchingForString));
                        try
                        {
                            if (ptr.ToInt64()>=acptr.ToInt64())
                            {
                                i++;
                                if (i < 5)
                                    continue;
                                var nw = new Window().ThisPtr;                                
                                var iu1 = WinRT.ObjectReference<WinRT.Interop.IUnknownVftbl>.Attach(ref nw);
                                var nw2 = new ABI.Microsoft.UI.Xaml.IWindow(iu1);
                                var sl = Marshal.GetEndComSlot(typeof(ABI.Microsoft.UI.Xaml.IWindow));
                                //var iu1 = WinRT.ObjectReference<WinRT.Interop.IUnknownVftbl>.Attach(ref ptr);
                                var iu2 = WinRT.ObjectReference<WinRT.Interop.IUnknownVftbl>.FromAbi(ptr);
                                var wnd = new ABI.Microsoft.UI.Xaml.IWindow(iu2);
                            }                            
                        }
                        catch
                        {

                        }
                    }
              
                }

                // move to the next memory chunk
                proc_min_address_l += mem_basic_info.RegionSize;
                proc_min_address = new IntPtr(proc_min_address_l);
            }


            Console.ReadLine();
        }
    }
}