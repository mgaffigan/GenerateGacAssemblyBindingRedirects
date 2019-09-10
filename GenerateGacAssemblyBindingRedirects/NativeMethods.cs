using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GenerateGacAssemblyBindingRedirects.NativeMethods;

namespace GenerateGacAssemblyBindingRedirects
{
    static class NativeMethods
    {
        public const int
            ASM_CACHE_GAC = 2,
            ASM_DISPLAYF_VERSION_CULTURE_PUBLICKEYTOKEN = 0x07,
            CANOF_PARSE_DISPLAY_NAME = 1,
            S_OK = 0,
            S_FALSE = 1;

        [DllImport("fusion.dll", PreserveSig = false)]
        public static extern void CreateAssemblyEnum(
            out IAssemblyEnum ppEnum,
            IntPtr pUnkReserved,
            IAssemblyName pName,
            int flags,
            IntPtr pvReserved);

        [DllImport("fusion.dll", PreserveSig = false)]
        public static extern void CreateAssemblyNameObject(
            out IAssemblyName ppAssemblyNameObj,
            [MarshalAs(UnmanagedType.LPWStr)]
            string szAssemblyName,
            int flags,
            IntPtr pvReserved);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("21b8916c-f28e-11d2-a473-00c04f8ef448")]
    internal interface IAssemblyEnum
    {
        [PreserveSig]
        int GetNextAssembly(IntPtr pvReserved, out IAssemblyName ppName, int flags);
        void Reset();
        void Clone(out IAssemblyEnum ppEnum);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("CD193BC0-B4BC-11d2-9833-00C04FC31D2E")]
    internal interface IAssemblyName
    {
        void SetProperty(int PropertyId, IntPtr pvProperty, int cbProperty);
        void GetProperty(int PropertyId, IntPtr pvProperty, ref int pcbProperty);
        void Finalize();
        void GetDisplayName(StringBuilder pDisplayName, ref int pccDisplayName, int displayFlags);
        IntPtr Reserved(ref Guid guid, object obj1, object obj2, string string1, long llFlags, IntPtr pvReserved, int cbReserved);
        void GetName(ref int pccBuffer, StringBuilder pwzName);
        void GetVersion(out int versionHi, out int versionLow);
        void IsEqual(IAssemblyName pAsmName, int cmpFlags);

        IAssemblyName Clone();
    }
}
