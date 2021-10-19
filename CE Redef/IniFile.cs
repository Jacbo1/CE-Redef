// Source: https://stackoverflow.com/a/14906422

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

class IniFile   // revision 11
{
    string Path;

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    public IniFile(string IniPath)
    {
        Path = new FileInfo(IniPath).FullName;
    }

    public string Read(string Section, string Key)
    {
        StringBuilder RetVal = new StringBuilder(512);
        GetPrivateProfileString(Section, Key, "", RetVal, 512, Path);
        return RetVal.ToString();
    }

    public int ReadInt(string Section, string Key)
    {
        StringBuilder RetVal = new StringBuilder(512);
        GetPrivateProfileString(Section, Key, "", RetVal, 512, Path);
        int.TryParse(RetVal.ToString(), out int val);
        return val;
    }

    public double ReadDouble(string Section, string Key)
    {
        StringBuilder RetVal = new StringBuilder(512);
        GetPrivateProfileString(Section, Key, "", RetVal, 512, Path);
        double.TryParse(RetVal.ToString(), out double val);
        return val;
    }

    public string ReadOrDefault(string Section, string Key, string Value)
    {
        string val = Read(Section, Key);
        if (val.Length == 0)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
            return Value;
        }
        return val;
    }

    public double ReadOrDefault(string Section, string Key, double Value)
    {
        string val = Read(Section, Key);
        if (val.Length == 0)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), Path);
            return Value;
        }
        double.TryParse(val, out double doubleVal);
        return doubleVal;
    }

    public void Write(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, Path);
    }

    public void Write(string Section, string Key, double Value)
    {
        WritePrivateProfileString(Section, Key, Value.ToString(), Path);
    }

    public void DeleteKey(string Section, string Key)
    {
        Write(Section, Key, null);
    }

    public void DeleteSection(string Section)
    {
        Write(Section, null, null);
    }

    public bool KeyExists(string Section, string Key)
    {
        return Read(Section, Key).Length != 0;
    }
}