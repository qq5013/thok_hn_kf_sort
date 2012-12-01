using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP;
using THOK.AS.Sorting.Util;

namespace THOK.AS.OTS.App
{
    public partial class MainForm : Form
    {
        private Context context = null;
       
        public MainForm()
        {
            InitializeComponent();
            Logger.OnLog += new LogEventHandler(Logger_OnLog);

            context = new Context();

            try
            {
                ContextInitialize initialize = new ContextInitialize();
                initialize.InitializeContext(context);
                //context.RegisterProcessControl(mainPanel);
                //context.RegisterProcessControl(switchStatus);
                //context.RegisterProcessControl(packerStatus);
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
                path = path.TrimEnd(new char[] { '-'});
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Release();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LogFile.DeleteFile();
            Application.Exit();
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            THOK.AS.OTS.View.RouteForm routeForm = new THOK.AS.OTS.View.RouteForm();
            routeForm.ShowIcon = false;
            routeForm.WindowState = FormWindowState.Maximized;
            routeForm.ShowDialog();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            THOK.AS.OTS.View.OrderForm orderForm = new THOK.AS.OTS.View.OrderForm();
            orderForm.context = context;
            orderForm.WindowState = FormWindowState.Maximized;
            orderForm.ShowIcon = false;
            orderForm.ShowDialog();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            THOK.AS.OTS.View.ParameterForm parameterForm = new THOK.AS.OTS.View.ParameterForm();
            parameterForm.ShowIcon = false;
            parameterForm.WindowState = FormWindowState.Maximized;
            parameterForm.ShowDialog();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
        }
    }
}