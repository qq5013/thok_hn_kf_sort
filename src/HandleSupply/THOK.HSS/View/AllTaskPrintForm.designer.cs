namespace THOK.HSS.View
{
    partial class PrintForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.crvAllTaskReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // crvAllTaskReport
            // 
            this.crvAllTaskReport.ActiveViewIndex = -1;
            this.crvAllTaskReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvAllTaskReport.DisplayGroupTree = false;
            this.crvAllTaskReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvAllTaskReport.Location = new System.Drawing.Point(0, 0);
            this.crvAllTaskReport.Name = "crvAllTaskReport";
            this.crvAllTaskReport.SelectionFormula = "";
            this.crvAllTaskReport.Size = new System.Drawing.Size(574, 366);
            this.crvAllTaskReport.TabIndex = 0;
            this.crvAllTaskReport.ViewTimeSelectionFormula = "";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(264, 162);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 1;
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 366);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.crvAllTaskReport);
            this.Name = "PrintForm";
            this.Text = "手工补货作业打印预览";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvAllTaskReport;
        private System.Windows.Forms.Label lblInfo;
    }
}