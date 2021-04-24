using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FireFighter
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
            //Form1 f1 = new Form1();
            Application.Run(new FirstForm());
            //Application.Run(f1);
        }
    }
}
