using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (s, e) =>
                MessageBox.Show("Unhandled error:\n\n" + e.Exception.Message + "\n\n" + e.Exception.StackTrace,
                                "RF Auto Loot - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Run(new Form1());
        }
    }
}
