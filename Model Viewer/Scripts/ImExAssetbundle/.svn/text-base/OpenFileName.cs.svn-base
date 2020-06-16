
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Auto )]  

public class OpenFileName 
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero; 
    public IntPtr   instance = IntPtr.Zero;

    public String   filter = null;
    public String   customFilter = null;
    public int      maxCustFilter = 0;
    public int      filterIndex = 0;

    public String   file = null;
    public int      maxFile = 0;

    public String   fileTitle = null;
    public int      maxFileTitle = 0;

    public String   initialDir = null;

    public String   title = null;   

    public int      flags = 0; 
    public short    fileOffset = 0;
    public short    fileExtension = 0;

    public String   defExt = null; 

    public IntPtr   custData = IntPtr.Zero;  
    public IntPtr   hook = IntPtr.Zero;  

    public String   templateName = null; 

    public IntPtr   reservedPtr = IntPtr.Zero; 
    public int      reservedInt = 0;
    public int      flagsEx = 0;
}

public class WindowDll
{
[DllImport("Comdlg32.dll",SetLastError=true,ThrowOnUnmappableChar=true, CharSet = CharSet.Auto)]          
     public static extern bool GetOpenFileName([ In, Out ] OpenFileName ofn );   
     public static  bool GetOpenFileName1([ In, Out ] OpenFileName ofn )
    {
        return GetOpenFileName(ofn);
    }

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]          
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
}

[Flags]
public enum OpenSaveFileDialgueFlags : int
{
    OFN_READONLY = 0x1,
    OFN_OVERWRITEPROMPT = 0x2,
    OFN_HIDEREADONLY = 0x4,
    OFN_NOCHANGEDIR = 0x8,
    OFN_SHOWHELP = 0x10,
    OFN_ENABLEHOOK = 0x20,
    OFN_ENABLETEMPLATE = 0x40,
    OFN_ENABLETEMPLATEHANDLE = 0x80,
    OFN_NOVALIDATE = 0x100,
    OFN_ALLOWMULTISELECT = 0x200,
    OFN_EXTENSIONDIFFERENT = 0x400,
    OFN_PATHMUSTEXIST = 0x800,
    OFN_FILEMUSTEXIST = 0x1000,
    OFN_CREATEPROMPT = 0x2000,
    OFN_SHAREAWARE = 0x4000,
    OFN_NOREADONLYRETURN = 0x8000,
    OFN_NOTESTFILECREATE = 0x10000,
    OFN_NONETWORKBUTTON = 0x20000,
    /// <summary>
    /// Force no long names for 4.x modules
    /// </summary>
    OFN_NOLONGNAMES = 0x40000,
    /// <summary>
    /// New look commdlg
    /// </summary>
    OFN_EXPLORER = 0x80000,
    OFN_NODEREFERENCELINKS = 0x100000,
    /// <summary>
    /// Force long names for 3.x modules
    /// </summary>
    OFN_LONGNAMES = 0x200000,
}