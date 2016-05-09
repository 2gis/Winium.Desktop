using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace DotNetRemoteWebDriver
{
    /// <summary>
    /// A utility class to determine a process parent.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ParentProcessUtilities
    {
        // These members must match PROCESS_BASIC_INFORMATION
        internal IntPtr Reserved1;
        internal IntPtr PebBaseAddress;
        internal IntPtr Reserved2_0;
        internal IntPtr Reserved2_1;
        internal IntPtr UniqueProcessId;
        internal IntPtr InheritedFromUniqueProcessId;

        [DllImport("ntdll.dll")]
        private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);

        /// <summary>Gets the parent process of the current process.</summary>
        /// <returns>An instance of the Process class.</returns>
        public static int GetParentProcessId()
        {
            return GetParentProcessId(Process.GetCurrentProcess().Handle);
        }

        /// <summary>
        /// Gets the parent process of specified process.
        /// </summary>
        /// <param name="id">The process id.</param>
        /// <returns>An instance of the Process class.</returns>
        public static int GetParentProcessId(int id)
        {
            var process = Process.GetProcesses().FirstOrDefault(p => p.Id == id);
            return process == null ? 0 : GetParentProcessId(process.Handle);
        }

        /// <summary>
        /// Gets the parent process of a specified process.
        /// </summary>
        /// <param name="handle">The process handle.</param>
        /// <returns>An instance of the Process class or null if an error occurred.</returns>
        public static int GetParentProcessId(IntPtr handle)
        {
            ParentProcessUtilities pbi = new ParentProcessUtilities();
            int returnLength;
            int status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out returnLength);
            return status != 0 ? 0 : pbi.InheritedFromUniqueProcessId.ToInt32();
        }

        public static IEnumerable<int> GetAncestorIds(IntPtr handle)
        {
            int ancestorId = GetParentProcessId(handle);
            while(ancestorId > 0)
            {
                yield return ancestorId;
                ancestorId = GetParentProcessId(ancestorId);
            }
        }

        public static IEnumerable<IntPtr> GetDescendents(int id)
        {
            return Process.GetProcesses().Where(p => GetAncestorIds(p.Handle).Contains(id)).Select(p => p.Handle);
        }
    }
}
