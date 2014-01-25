namespace THOK.HSS.View
{
    partial class AllTaskQueryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvHandSupply = new System.Windows.Forms.DataGridView();
            this.SUPPLYNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BATCHNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.补货批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print = new System.Windows.Forms.Button();
            this.NextPage = new System.Windows.Forms.Button();
            this.UpPage = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblSupplyBatch = new System.Windows.Forms.Label();
            this.txtSupplyBatch = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandSupply)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnSelect);
            this.pnlTool.Controls.Add(this.txtSupplyBatch);
            this.pnlTool.Controls.Add(this.lblSupplyBatch);
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.Print);
            this.pnlTool.Controls.Add(this.NextPage);
            this.pnlTool.Controls.Add(this.UpPage);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(839, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvHandSupply);
            this.pnlContent.Size = new System.Drawing.Size(839, 229);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(839, 282);
            // 
            // dgvHandSupply
            // 
            this.dgvHandSupply.AllowUserToAddRows = false;
            this.dgvHandSupply.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvHandSupply.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHandSupply.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvHandSupply.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SUPPLYNO,
            this.BATCHNO,
            this.SORTNO,
            this.补货批次,
            this.CHANNELNAME,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY,
            this.ORDERDATE,
            this.LINECODE,
            this.STATUS});
            this.dgvHandSupply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHandSupply.Location = new System.Drawing.Point(0, 0);
            this.dgvHandSupply.Name = "dgvHandSupply";
            this.dgvHandSupply.ReadOnly = true;
            this.dgvHandSupply.RowTemplate.Height = 23;
            this.dgvHandSupply.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHandSupply.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHandSupply.Size = new System.Drawing.Size(839, 229);
            this.dgvHandSupply.TabIndex = 3;
            // 
            // SUPPLYNO
            // 
            this.SUPPLYNO.DataPropertyName = "SUPPLYNO";
            this.SUPPLYNO.HeaderText = "补货编号";
            this.SUPPLYNO.Name = "SUPPLYNO";
            this.SUPPLYNO.ReadOnly = true;
            this.SUPPLYNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SUPPLYNO.Width = 80;
            // 
            // BATCHNO
            // 
            this.BATCHNO.DataPropertyName = "BATCHNO";
            this.BATCHNO.HeaderText = "批次号";
            this.BATCHNO.Name = "BATCHNO";
            this.BATCHNO.ReadOnly = true;
            this.BATCHNO.Visible = false;
            // 
            // SORTNO
            // 
            this.SORTNO.DataPropertyName = "SORTNO";
            this.SORTNO.HeaderText = "分拣流水号";
            this.SORTNO.Name = "SORTNO";
            this.SORTNO.ReadOnly = true;
            this.SORTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 补货批次
            // 
            this.补货批次.DataPropertyName = "SUPPLYBATCH";
            this.补货批次.HeaderText = "补货批次";
            this.补货批次.Name = "补货批次";
            this.补货批次.ReadOnly = true;
            this.补货批次.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CHANNELNAME
            // 
            this.CHANNELNAME.DataPropertyName = "CHANNELNAME";
            this.CHANNELNAME.HeaderText = "烟道名称";
            this.CHANNELNAME.Name = "CHANNELNAME";
            this.CHANNELNAME.ReadOnly = true;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "CIGARETTECODE";
            this.CIGARETTECODE.HeaderText = "卷烟代码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            this.CIGARETTECODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CIGARETTECODE.Width = 80;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle2;
            this.QUANTITY.HeaderText = "卷烟数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QUANTITY.Width = 80;
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.HeaderText = "日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            this.ORDERDATE.Visible = false;
            // 
            // LINECODE
            // 
            this.LINECODE.DataPropertyName = "LINECODE";
            this.LINECODE.HeaderText = "生产线";
            this.LINECODE.Name = "LINECODE";
            this.LINECODE.ReadOnly = true;
            this.LINECODE.Visible = false;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STATUS.DefaultCellStyle = dataGridViewCellStyle3;
            this.STATUS.HeaderText = "补货状态";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.STATUS.Width = 80;
            // 
            // Print
            // 
            this.Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Print.Image = global::THOK.HSS.Properties.Resources.Print;
            this.Print.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Print.Location = new System.Drawing.Point(153, 2);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(48, 47);
            this.Print.TabIndex = 6;
            this.Print.Text = "打印";
            this.Print.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Print.UseVisualStyleBackColor = true;
            this.Print.Click += new System.EventHandler(this.Print_Click);
            // 
            // NextPage
            // 
            this.NextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextPage.Image = global::THOK.HSS.Properties.Resources.Next;
            this.NextPage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.NextPage.Location = new System.Drawing.Point(103, 2);
            this.NextPage.Name = "NextPage";
            this.NextPage.Size = new System.Drawing.Size(48, 47);
            this.NextPage.TabIndex = 5;
            this.NextPage.Text = "下页";
            this.NextPage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.NextPage.UseVisualStyleBackColor = true;
            this.NextPage.Click += new System.EventHandler(this.DownPage_Click);
            // 
            // UpPage
            // 
            this.UpPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.UpPage.Image = global::THOK.HSS.Properties.Resources.Back;
            this.UpPage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.UpPage.Location = new System.Drawing.Point(53, 2);
            this.UpPage.Name = "UpPage";
            this.UpPage.Size = new System.Drawing.Size(48, 47);
            this.UpPage.TabIndex = 4;
            this.UpPage.Text = "上页";
            this.UpPage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.UpPage.UseVisualStyleBackColor = true;
            this.UpPage.Click += new System.EventHandler(this.UpPage_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefresh.Image = global::THOK.HSS.Properties.Resources.Bar_Chart;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 47);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Image = global::THOK.HSS.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(203, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblSupplyBatch
            // 
            this.lblSupplyBatch.AutoSize = true;
            this.lblSupplyBatch.Location = new System.Drawing.Point(286, 21);
            this.lblSupplyBatch.Name = "lblSupplyBatch";
            this.lblSupplyBatch.Size = new System.Drawing.Size(53, 12);
            this.lblSupplyBatch.TabIndex = 8;
            this.lblSupplyBatch.Text = "批次号：";
            // 
            // txtSupplyBatch
            // 
            this.txtSupplyBatch.Location = new System.Drawing.Point(345, 18);
            this.txtSupplyBatch.Name = "txtSupplyBatch";
            this.txtSupplyBatch.Size = new System.Drawing.Size(145, 21);
            this.txtSupplyBatch.TabIndex = 9;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(506, 18);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 10;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // AllTaskQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 282);
            this.Name = "AllTaskQueryForm";
            this.Text = "AllTaskQueryForm";
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandSupply)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHandSupply;
        protected System.Windows.Forms.Button btnRefresh;
        protected System.Windows.Forms.Button NextPage;
        protected System.Windows.Forms.Button UpPage;
        protected System.Windows.Forms.Button Print;
        protected System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblSupplyBatch;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtSupplyBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLYNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BATCHNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn 补货批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
    }
}