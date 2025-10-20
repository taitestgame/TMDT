using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMDT
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Ensure modern TLS for HTTPS image URLs
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
                | System.Net.SecurityProtocolType.Tls11
                | System.Net.SecurityProtocolType.Tls;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
