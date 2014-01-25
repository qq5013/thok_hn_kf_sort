using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.Util;
using THOK.AF.View;

namespace THOK.HSS.View
{
    public partial class ParameterForm:ToolbarForm
    {
        private Parameter parameter = new Parameter();
        private DBConfigUtil config = new DBConfigUtil("DefaultConnection", "SQLSERVER");

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();

        }

        private void ReadParameter()
        {
            parameter.ServerName = config.Parameters["server"].ToString();
            parameter.DbName = config.Parameters["database"].ToString();
            parameter.DbUser = config.Parameters["uid"].ToString();
            parameter.Password = config.Parameters["password"].ToString();

            this.ppgSystem.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            config.Parameters["server"] = parameter.ServerName;
            config.Parameters["database"]=parameter.DbName;
            config.Parameters["uid"]=parameter.DbUser;
            config.Parameters["password"] = config.Parameters["password"].ToString() == parameter.Password ? config.Parameters["password"]:THOK.Util.Coding.Encoding(parameter.Password);
            config.Save();
            MessageBox.Show("保存成功，请重新启动系统!");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}