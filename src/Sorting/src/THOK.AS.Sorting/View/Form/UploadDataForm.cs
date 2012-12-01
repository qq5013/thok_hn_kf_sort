using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.AS.Sorting.Dal;
using THOK.MCP;

namespace THOK.AS.Sorting.View
{
    public partial class UploadDataForm : THOK.AF.View.ToolbarForm
    {
        UploadDataDal udal = new UploadDataDal();

        public UploadDataForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = udal.FindSortUploadInfo();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable sort = udal.FindEfficiency();
                if (sort.Rows[0]["SUMQUANTITY"].ToString() == sort.Rows[0]["SORTQUANTITY"].ToString())
                {
                    DataTable sortTable = udal.GetSortUploadData("0");
                    if (sortTable.Rows.Count > 0)
                    {
                        udal.GetSortingUploadData();
                        sortTable = udal.GetSortUploadData("0");
                        udal.UploadSortingData(sortTable);
                        MessageBox.Show("�ϱ��ּ������������ɣ�", "��Ϣ");
                    }
                    else
                        MessageBox.Show("û�зּ��������Ҫ�ϱ���", "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("�ּ�����û�зּ��꣬�����ϱ���", "��Ϣ");
            }
            catch (Exception exp)
            {
                MessageBox.Show("�ϱ��ּ���������ݳ���ԭ��"+exp.Message,"��Ϣ");
                Logger.Error(exp.Message);
            }
        }
    }
}

