using System;
using System.Runtime.InteropServices;
/// <summary>
/// ���ߣ�windowsϵͳ�ļ���/�ļ�ѡ�񴰿�
/// </summary>
public class OpenFile
{
    /// <summary>
    /// ѡ���ļ���
    /// </summary>
    public static string ChooseWinFolder()
    {
        //ʹ�����£�
        OpenDialogDir ofn = new OpenDialogDir();
        ofn.pszDisplayName = new string(new char[2000]); ;     // ���Ŀ¼·��������  
        ofn.title = "ѡ���ļ���";// ����  
        //ofn.ulFlags = BIF_NEWDIALOGSTYLE | BIF_EDITBOX; // �µ���ʽ,���༭��  
        IntPtr pidlPtr = WindowDll.SHBrowseForFolder(ofn);

        char[] charArray = new char[2000];
        for (int i = 0; i < 2000; i++)
            charArray[i] = '\0';

        WindowDll.SHGetPathFromIDList(pidlPtr, charArray);
        string fullDirPath = new String(charArray);
        return fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));

        //fullDirPath = fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));
        //Debug.Log(fullDirPath);//�������ѡ���Ŀ¼·��
    }
    /// <summary>
    /// ѡ��Ӧ�ó����ļ�
    /// </summary>
    public static string ChooseWinFile()
    {
        OpenFileName OpenFileName = new OpenFileName();
        OpenFileName.structSize = Marshal.SizeOf(OpenFileName);
        OpenFileName.filter = "Ӧ�ó���(*.exe)\0*.exe";
        OpenFileName.file = new string(new char[1024]);
        OpenFileName.maxFile = OpenFileName.file.Length;
        OpenFileName.fileTitle = new string(new char[64]);
        OpenFileName.maxFileTitle = OpenFileName.fileTitle.Length;
        //OpenFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//Ĭ��·��
        OpenFileName.title = "ѡ��exe�ļ�";
        OpenFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        if (WindowDll.GetOpenFileName(OpenFileName))
            return OpenFileName.file;
        else
            return null;
    }
    /// <summary>
    /// ѡ��ͼƬ�ļ�
    /// </summary>
    public static string ChooseImageFile()
    {
        OpenFileName OpenFileName = new OpenFileName();
        OpenFileName.structSize = Marshal.SizeOf(OpenFileName);
        OpenFileName.filter = "ͼƬ�ļ� (*.png;*.jpg;*.jpeg)\0*.png;*.jpg;*.jpeg";
        OpenFileName.file = new string(new char[1024]);
        OpenFileName.maxFile = OpenFileName.file.Length;
        OpenFileName.fileTitle = new string(new char[64]);
        OpenFileName.maxFileTitle = OpenFileName.fileTitle.Length;
        //OpenFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//Ĭ��·��
        OpenFileName.title = "ѡ��ͼƬ�ļ�";
        OpenFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        if (WindowDll.GetOpenFileName(OpenFileName))
            return OpenFileName.file;
        else
            return null;
    }
}

/// <summary>
/// windowsϵͳ�ļ�ѡ�񴰿�
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
/// windowsϵͳ�ļ���ѡ�񴰿�
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
    //����ָ��ϵͳ����       ���ļ��Ի���
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }

    //����ָ��ϵͳ����        ���Ϊ�Ի���
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