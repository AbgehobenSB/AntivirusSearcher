using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Program
    {
        
        public static bool santivirus()
        {
            return Directory.Exists("C:\\Program Files (x86)\\Digital Communications\\SAntivirus");
        }
       
        public class ScreenCapture
        {
            [DllImport("user32.dll")]
            private static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetDesktopWindow();

            [StructLayout(LayoutKind.Sequential)]
            private struct Rect
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }

            [DllImport("user32.dll")]
            private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

            public static Image CaptureDesktop()
            {
                return CaptureWindow(GetDesktopWindow());
            }

            public static Bitmap CaptureActiveWindow()
            {
                return CaptureWindow(GetForegroundWindow());
            }

            public static Bitmap CaptureWindow(IntPtr handle)
            {
                var rect = new Rect();
                GetWindowRect(handle, ref rect);
                var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                var result = new Bitmap(bounds.Width, bounds.Height);

                using (var graphics = Graphics.FromImage(result))
                {
                    graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                return result;
            }
        }
        public static void Main(string[] args)
        {
            
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("       ::::    ::: ::::::::::: ::::::::  :::    ::: ::::::::::: \n      :+:+:   :+:     :+:    :+:    :+: :+:    :+:     :+:      \n     :+:+:+  +:+     +:+    +:+        +:+    +:+     +:+       \n    +#+ +:+ +#+     +#+    :#:        +#++:++#++     +#+        \n   +#+  +#+#+#     +#+    +#+   +#+# +#+    +#+     +#+         \n  #+#   #+#+#     #+#    #+#    #+# #+#    #+#     #+#          \n ###    #### ########### ########  ###    ###     ###           ");
            Console.WriteLine("=====================================================================");
            Console.Title = "Antivirus Search Tool made by Night#5000";
            ManagementObjectSearcher Abgehoben = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
            ManagementObjectCollection Selfbot = Abgehoben.Get();

            foreach (ManagementObject KomischeSachenmachen in Selfbot)
            {
                var IchhasseAntivirenProgramme = KomischeSachenmachen["displayName"];
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("                       " + IchhasseAntivirenProgramme +" = True");
                
                
            }
            Console.WriteLine("                       Santivirus = {0}", Program.santivirus());
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("=====================================================================");



            
            Console.Read();
            var image = ScreenCapture.CaptureActiveWindow();
            image.Save(@"Sendthistotheticket.jpg", ImageFormat.Jpeg);
            

        }

    }
}