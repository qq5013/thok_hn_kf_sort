namespace THOK.HSS.View
{
    partial class HandSupplyForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvHandSupply = new System.Windows.Forms.DataGridView();
            this.SetSupplyStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BATCHNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPPLYNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SORTNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.补货批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandSupply)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(800, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvHandSupply);
            this.pnlContent.Size = new System.Drawing.Size(800, 229);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(800, 282);
            // 
            // dgvHandSupply
            // 
            this.dgvHandSupply.AllowUserToAddRows = false;
            this.dgvHandSupply.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvHandSupply.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHandSupply.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvHandSupply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHandSupply.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SetSupplyStatus,
            this.BATCHNO,
            this.SUPPLYNO,
            this.SORTNO,
            this.补货批次,
            this.CHANNELNAME,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY,
            this.LINECODE,
            this.ORDERDATE,
            this.STATUS});
            this.dgvHandSupply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHandSupply.Location = new System.Drawing.Point(0, 0);
            this.dgvHandSupply.Name = "dgvHandSupply";
            this.dgvHandSupply.RowTemplate.Height = 23;
            this.dgvHandSupply.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHandSupply.Size = new System.Drawing.Size(800, 229);
            this.dgvHandSupply.TabIndex = 2;
            this.dgvHandSupply.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvHandSupply_ColumnDisplayIndexChanged);
            this.dgvHandSupply.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHandSupply_CellContentClick);
            // 
            // SetSupplyStatus
            // 
            this.SetSupplyStatus.HeaderText = "补货";
            this.SetSupplyStatus.Name = "SetSupplyStatus";
            this.SetSupplyStatus.Width = 40;
            // 
            // BATCHNO
            // 
            this.BATCHNO.DataPropertyName = "BATCHNO";
            this.BATCHNO.HeaderText = "批次号";
            this.BATCHNO.Name = "BATCHNO";
            this.BATCHNO.ReadOnly = true;
            this.BATCHNO.Visible = false;
            this.BATCHNO.Width = 60;
            // 
            // SUPPLYNO
            // 
            this.SUPPLYNO.DataPropertyName = "SUPPLYNO";
            this.SUPPLYNO.HeaderText = "补货编号";
            this.SUPPLYNO.Name = "SUPPLYNO";
            this.SUPPLYNO.ReadOnly = true;
            this.SUPPLYNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SUPPLYNO.Width = 60;
            // 
            // SORTNO
            // 
            this.SORTNO.DataPropertyName = "SORTNO";
            this.SORTNO.HeaderText = "流水号";
            this.SORTNO.Name = "SORTNO";
            this.SORTNO.ReadOnly = true;
            this.SORTNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SORTNO.Width = 80;
            // 
            // 补货批次
            // 
            this.补货批次.DataPropertyName = "SUPPLYBATCH";
            this.补货批次.HeaderText = "补货批次";
            this.补货批次.Name = "补货批次";
            this.补货批次.ReadOnly = true;
            this.补货批次.Width = 80;
            // 
            // CHANNELNAME
            // 
            this.CHANNELNAME.DataPropertyName = "CHANNELNAME";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.CHANNELNAME.DefaultCellStyle = dataGridViewCellStyle2;
            this.CHANNELNAME.HeaderText = "烟道名称";
            this.CHANNELNAME.Name = "CHANNELNAME";
            this.CHANNELNAME.ReadOnly = true;
            this.CHANNELNAME.Width = 80;
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
            dataGridViewCellStyle3.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.CIGARETTENAME.DefaultCellStyle = dataGridViewCellStyle3;
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Green;
            this.QUANTITY.DefaultCellStyle = dataGridViewCellStyle4;
            this.QUANTITY.HeaderText = "卷烟数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QUANTITY.Width = 80;
            // 
            // LINECODE
            // 
            this.LINECODE.DataPropertyName = "LINECODE";
            this.LINECODE.HeaderText = "生产线";
            this.LINECODE.Name = "LINECODE";
            this.LINECODE.ReadOnly = true;
            this.LINECODE.Visible = false;
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.HeaderText = "日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            this.ORDERDATE.Visible = false;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STATUS.DefaultCellStyle = dataGridViewCellStyle5;
            this.STATUS.HeaderText = "补货状态";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.STATUS.Width = 80;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Image = global::THOK.HSS.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(53, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefresh.Image = global::THOK.HSS.Properties.Resources.Bar_Chart;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(3, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 47);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // HandSupplyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 282);
            this.Name = "HandSupplyForm";
            this.Text = "手工补货信息";
            this.Load += new System.EventHandler(this.HandSupplyForm_Load);
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandSupply)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHandSupply;
        protected System.Windows.Forms.Button btnRefresh;
        protected System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SetSupplyStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn BATCHNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLYNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SORTNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn 补货批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
    }
}