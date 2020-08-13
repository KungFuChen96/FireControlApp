using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace Tools
{
    public class Utility
    {
        /// <summary>
        /// Ping指定主机
        /// </summary>
        /// <param name="hostAddress">主机名称或IP地址</param>
        /// <returns>true:主机可连通, false:主机不连通</returns>
        public static bool PingHost(string hostAddress)
        {
            try
            {
                Ping ping = new Ping();
                PingOptions pinOptions = new PingOptions();
                pinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 120;
                PingReply pinReply = ping.Send(hostAddress, intTimeout, buffer, pinOptions);
                IPStatus ipStatus = pinReply.Status;
                return ipStatus == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        public static void ViewTextInNotepad(string text)
        {
            Process proc;
            try
            {
                // 启动记事本
                proc = new Process();
                proc.StartInfo.FileName = "notepad.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;

                proc.Start();
            }
            catch
            {
                proc = null;
                return;
            }

            // 调用 API, 传递数据
            while (proc.MainWindowHandle == IntPtr.Zero)
            {
                proc.Refresh();
            }

            IntPtr vHandle = Win32.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, "Edit", null);

            // 传递数据给记事本
            if (vHandle == IntPtr.Zero) return;

            Win32.SendMessage(vHandle, Win32.WM_SETTEXT, 0, text);
        }

        /// <summary>
        /// 添加一个数据到队列，并检查数据队列中的数据是否全部相等，判断条件：datalist添加fvalue后的总数等于count后有效。
        /// </summary>
        /// <param name="fvalue"></param>
        /// <param name="count"></param>
        /// <param name="valuelist"></param>
        /// <returns></returns>
        public static bool IsDoubleValuesAllEqual(float fvalue, int count, Queue<float> valuelist)
        {
            while (valuelist.Count() >= count)
                valuelist.Dequeue();

            valuelist.Enqueue(fvalue);

            if (valuelist.Count == count)
            {
                float[] values = valuelist.ToArray();
                for (int i = 0; i < values.Count() - 1; i++)
                {
                    if (values[i] != values[i + 1])
                        return false;
                }
                return true;
            }
            return false;
        }
    }

    public static class Win32
    {
        public const uint WM_SETTEXT = 0x000C;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);

        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
    }
}