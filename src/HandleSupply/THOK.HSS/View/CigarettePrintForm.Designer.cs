namespace THOK.HSS.View
{
    partial class CigarettePrintForm
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
            this.crvCigaretteReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // crvCigaretteReport
            // 
            this.crvCigaretteReport.ActiveViewIndex = -1;
            this.crvCigaretteReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvCigaretteReport.DisplayGroupTree = false;
            this.crvCigaretteReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvCigaretteReport.Location = new System.Drawing.Point(0, 0);
            this.crvCigaretteReport.Name = "crvCigaretteReport";
            this.crvCigaretteReport.SelectionFormula = "";
            this.crvCigaretteReport.Size = new System.Drawing.Size(613, 318);
            this.crvCigaretteReport.TabIndex = 0;
            this.crvCigaretteReport.ViewTimeSelectionFormula = "";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(267, 124);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 1;
            // 
            // CigarettePrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 318);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.crvCigaretteReport);
            this.Name = "CigarettePrintForm";
            this.Text = "手工补货卷烟信息打印预览";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvCigaretteReport;
        private System.Windows.Forms.Label lblInfo;
    }
}