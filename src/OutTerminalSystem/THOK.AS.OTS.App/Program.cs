using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace THOK.AS.OTS.App
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //新
            Application.Run(new FormMain());
            //旧
            //Application.Run(new MainForm());
        }
    }
}