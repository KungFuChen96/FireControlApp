using System.IO;
using System.Text;

namespace Tools
{
    public class IniConfig
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string sPath = Path.Combine("C:\\", "Config.ini");

        //public static string sPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini");

        public IniConfig(string path)
        {
            sPath = path;
        }

        public static void Writue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, sPath);
        }

        public static string ReadValue(string section, string key)
        {
            // 每次从ini中读取多少字节
            var temp = new StringBuilder(10240);
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 10240, sPath);
            return temp.ToString();
        }

        public static string ReadValue(string key)
        {
            // 每次从ini中读取多少字节
            var temp = new StringBuilder(10240);
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString("Setting", key, "", temp, 10240, sPath);
            return temp.ToString();
        }
    }
}