using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using THOK.ParamUtil;

namespace THOK.HSS
{
    public class Parameter:BaseObject
    {
        private string serverName;

        [CategoryAttribute("数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")] 
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }
        private string dbName;
        [CategoryAttribute("数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string DbName
        {
            get { return dbName; }
            set { dbName = value; }
        }
        private string dbUser;
        [CategoryAttribute("数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]

        public string DbUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;
        [CategoryAttribute("数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
