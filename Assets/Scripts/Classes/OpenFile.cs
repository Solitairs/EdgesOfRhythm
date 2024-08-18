using System;
using System.Runtime.InteropServices;
/// <summary>
/// 工具：windows系统文件夹/文件选择窗口
/// </summary>
public class OpenFile
{
    /// <summary>
    /// 选择文件夹
    /// </summary>
    public static string ChooseWinFolder()
    {
        //使用如下：
        OpenDialogDir ofn = new OpenDialogDir();
        ofn.pszDisplayName = new string(new char[2000]); ;     // 存放目录路径缓冲区  
        ofn.title = "选择文件夹";// 标题  
        //ofn.ulFlags = BIF_NEWDIALOGSTYLE | BIF_EDITBOX; // 新的样式,带编辑框  
        IntPtr pidlPtr = WindowDll.SHBrowseForFolder(ofn);

        char[] charArray = new char[2000];
        for (int i = 0; i < 2000; i++)
            charArray[i] = '\0';

        WindowDll.SHGetPathFromIDList(pidlPtr, charArray);
        string fullDirPath = new String(charArray);
        return fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));

        //fullDirPath = fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));
        //Debug.Log(fullDirPath);//这个就是选择的目录路径
    }
    /// <summary>
    /// 选择应用程序文件
    /// </summary>
    public static string ChooseWinFile()
    {
        OpenFileName OpenFileName = new OpenFileName();
        OpenFileName.structSize = Marshal.SizeOf(OpenFileName);
        OpenFileName.filter = "应用程序(*.exe)\0*.exe";
        OpenFileName.file = new string(new char[1024]);
        OpenFileName.maxFile = OpenFileName.file.Length;
        OpenFileName.fileTitle = new string(new char[64]);
        OpenFileName.maxFileTitle = OpenFileName.fileTitle.Length;
        //OpenFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        OpenFileName.title = "选择exe文件";
        OpenFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        if (WindowDll.GetOpenFileName(OpenFileName))
            return OpenFileName.file;
        else
            return null;
    }
    /// <summary>
    /// 选择图片文件
    /// </summary>
    public static string ChooseImageFile()
    {
        OpenFileName OpenFileName = new OpenFileName();
        OpenFileName.structSize = Marshal.SizeOf(OpenFileName);
        OpenFileName.filter = "图片文件 (*.png;*.jpg;*.jpeg)\0*.png;*.jpg;*.jpeg";
        OpenFileName.file = new string(new char[1024]);
        OpenFileName.maxFile = OpenFileName.file.Length;
        OpenFileName.fileTitle = new string(new char[64]);
        OpenFileName.maxFileTitle = OpenFileName.fileTitle.Length;
        //OpenFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        OpenFileName.title = "选择图片文件";
        OpenFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        if (WindowDll.GetOpenFileName(OpenFileName))
            return OpenFileName.file;
        else
            return null;
    }
}

/// <summary>
/// windows系统文件选择窗口
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct OpenFileName
{
    public int structSize;
    public IntPtr dlgOwner;
    public IntPtr instance;
    public String filter;
    public String customFilter;
    public int maxCustFilter;
    public int filterIndex;
    public String file;
    public int maxFile;
    public String fileTitle;
    public int maxFileTitle;
    public String initialDir;
    public String title;
    public int flags;
    public short fileOffset;
    public short fileExtension;
    public String defExt;
    public IntPtr custData;
    public IntPtr hook;
    public String templateName;
    public IntPtr reservedPtr;
    public int reservedInt;
    public int flagsEx;
}

/// <summary>
/// windows系统文件夹选择窗口
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct OpenDialogDir
{
    public IntPtr hwndOwner;
    public IntPtr pidlRoot;
    public String pszDisplayName;
    public String title;
    public UInt32 ulFlags;
    public IntPtr lpfno;
    public IntPtr lParam;
    public int iImage;
}
public class WindowDll
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);
    }

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern IntPtr SHBrowseForFolder([In, Out] OpenDialogDir ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);
}