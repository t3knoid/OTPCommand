using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLineSwitchParser;
using OtpNet;
using System.Windows;
using System.Runtime.InteropServices;

namespace OTPCommand
{
    internal class Program
    {
        static uint CF_UNICODETEXT = 13;

        [DllImport("user32.dll")]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        internal static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        internal static extern bool SetClipboardData(uint uFormat, IntPtr data);
        class Options
        {
            public string Key { get; set; }
        }

        [STAThread]
        static int Main(string[] args)
        {
            if (!CommandLineSwitch.TryParse<Options>(ref args, out var options, out var err))
            {
                Console.WriteLine($"ERROR: {err}");
                return -1;
            }
            if (string.IsNullOrEmpty(options.Key))
            {
                Console.WriteLine("ERROR: Secret Key is not specified.");
                ShowUsage();
                return -1;
            }

            var secretKey = default(byte[]);
            try { secretKey = Base32Encoding.ToBytes(options.Key); }
            catch (ArgumentException)
            {
                Console.WriteLine("ERROR: Invalid key format.");
                return -1;
            }

            var totp = new Totp(secretKey);
            var totpCode = totp.ComputeTotp();

            Console.WriteLine(totpCode);

            SetClipboard(totpCode);
            return 0;
        }

        private static void SetClipboard(string totpCode)
        {
            try
            {
                OpenClipboard(IntPtr.Zero);
                var ptr = Marshal.StringToHGlobalUni(totpCode);
                SetClipboardData(CF_UNICODETEXT, ptr);
                CloseClipboard();
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to copy text to clipboard. " + ex.Message);
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: -k <totp secret key>");
        }

    }
}
