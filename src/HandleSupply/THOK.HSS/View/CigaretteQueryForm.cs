using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.AF.View;
using THOK.HSS.Dal;

namespace THOK.HSS.View
{
    public partial class CigaretteQueryForm : ToolbarForm
    {
        private HandSupplyDal handSupplyDal = new HandSupplyDal();

        public CigaretteQueryForm()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable table = handSupplyDal.GetAllCigarette();
            if (table.Rows.Count == 0)
                MessageBox.Show("无数据显示");
            else
                this.dgvHandSupply.DataSource = table;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void CigaretteQueryForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Print_Click(object sender, EventArgs e)
        {
            CigarettePrintForm cigarettePrintForm = new CigarettePrintForm();
            cigarettePrintForm.ShowDialog();
        }
    }
}