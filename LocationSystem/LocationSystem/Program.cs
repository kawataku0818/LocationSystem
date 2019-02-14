using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationSystem
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //二重起動をチェックする
            if (System.Diagnostics.Process.GetProcessesByName(
                System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                //すでに起動していると判断して終了
                MessageBox.Show("多重起動はできません。");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashForm splash = new SplashForm();
            splash.Show();
            var mainform = new FormMain();
            mainform.Shown += (_, __) => splash.Close();
            Application.Run(mainform);

            //Application.Run(new FormMain());
        }
    }
}
