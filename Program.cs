using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BricKartOyunu
{
    static class Program
    {
        [STAThread]
        [SupportedOSPlatform("windows")]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Varsayılan yazı tipini Segoe UI olarak ayarla
            Application.SetDefaultFont(new Font("Segoe UI", 9F));

            Application.Run(new Form1());
        }
    }
}
