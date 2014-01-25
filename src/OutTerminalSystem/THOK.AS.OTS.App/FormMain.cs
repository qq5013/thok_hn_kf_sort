using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using THOK.MCP;

namespace THOK.AS.OTS.App
{
    public partial class FormMain : Form
    {
        private Context context = null;

        public FormMain()
        {
            InitializeComponent();
            THOK.MCP.Logger.Info("没有刷新数据或者已全部打印完成！");
            Logger.OnLog += new LogEventHandler(Logger_OnLog);

            context = new Context(); 
            try
            {
                ContextInitialize initialize = new ContextInitialize();
                initialize.InitializeContext(context);
                context.RegisterProcessControl(switchStatus);
                context.RegisterProcessControl(packerStatus);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        private void CreateDirectory(string directoryName)
        {
            if (!System.IO.Directory.Exists(directoryName))
                System.IO.Directory.CreateDirectory(directoryName);
        }

        private void WriteLoggerFile(string text)
        {
            try
            {
                string path = "";
                CreateDirectory("日志");
                path = "日志";
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 4).Trim();
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 7).Trim();
                path = path.TrimEnd(new char[] { '-' });
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToShortDateString() + ".txt";
                System.IO.File.AppendAllText(path, string.Format("{0} {1}", DateTime.Now, text + "\r\n"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void Logger_OnLog(THOK.MCP.LogEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new LogEventHandler(Logger_OnLog), args);
            }
            else
            {
                lock (lbLog)
                {
                    string msg = string.Format("[{0}] {1} {2}", args.LogLevel, DateTime.Now, args.Message);
                    lbLog.Items.Insert(0, msg);
                    WriteLoggerFile(msg);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void btnRoute_Click(object sender, EventArgs e)
        //{
        //    THOK.AS.OTS.View.RouteForm routeForm = new THOK.AS.OTS.View.RouteForm();
        //    routeForm.ShowIcon = false;
        //    routeForm.WindowState = FormWindowState.Maximized;
        //    routeForm.ShowDialog();

        //    //Logger.Error("123");  
        //}

        private void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                THOK.AS.OTS.View.OrderForm orderForm = new THOK.AS.OTS.View.OrderForm();
                orderForm.WindowState = FormWindowState.Maximized;
                orderForm.ShowIcon = false;
                orderForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            THOK.AS.OTS.View.ParameterForm parameterForm = new THOK.AS.OTS.View.ParameterForm();
            parameterForm.ShowIcon = false;
            parameterForm.WindowState = FormWindowState.Maximized;
            parameterForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes==(MessageBox.Show("您确定要退出本系统？","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question)))
            {
                Application.Exit();
            }
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            try
            {
                THOK.AS.OTS.View.RouteForm routeForm = new THOK.AS.OTS.View.RouteForm();
                routeForm.ShowIcon = false;
                routeForm.WindowState = FormWindowState.Maximized;
                routeForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            THOK.AS.OTS.View.ReportForm reportForm = new THOK.AS.OTS.View.ReportForm();
            reportForm.ShowIcon = false;
            reportForm.WindowState = FormWindowState.Maximized;
            reportForm.ShowDialog();
        }
    }
}