namespace THOK.HSS.View
{
    partial class SortChannelPrintForm
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
            this.crvChannelReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // crvChannelReport
            // 
            this.crvChannelReport.ActiveViewIndex = -1;
            this.crvChannelReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvChannelReport.DisplayGroupTree = false;
            this.crvChannelReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvChannelReport.Location = new System.Drawing.Point(0, 0);
            this.crvChannelReport.Name = "crvChannelReport";
            this.crvChannelReport.SelectionFormula = "";
            this.crvChannelReport.Size = new System.Drawing.Size(499, 262);
            this.crvChannelReport.TabIndex = 0;
            this.crvChannelReport.ViewTimeSelectionFormula = "";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(232, 118);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 1;
            // 
            // SortChannelPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 262);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.crvChannelReport);
            this.Name = "SortChannelPrintForm";
            this.Text = "分拣烟道信息打印";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvChannelReport;
        private System.Windows.Forms.Label lblInfo;
    }
}