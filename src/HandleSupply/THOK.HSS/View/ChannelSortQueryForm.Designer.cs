namespace THOK.HSS.View
{
    partial class ChannelSortQueryForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvChannelSort = new System.Windows.Forms.DataGridView();
            this.bsChannelSort = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.STATUS = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ORDERDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BATCHNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHANNELCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPPLYBATCH = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.SUPPLYNO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.SORTNO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ORDERID = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ORDERNO = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ROUTENAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CUSTOMERNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CIGARETTENAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.SUPPLYSTATUS = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.SORTSTATUS = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChannelSort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChannelSort)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(1093, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvChannelSort);
            this.pnlContent.Size = new System.Drawing.Size(1093, 416);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1093, 469);
            // 
            // dgvChannelSort
            // 
            this.dgvChannelSort.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvChannelSort.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChannelSort.AutoGenerateColumns = false;
            this.dgvChannelSort.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChannelSort.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvChannelSort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChannelSort.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STATUS,
            this.ORDERDATE,
            this.BATCHNO,
            this.LINECODE,
            this.CIGARETTECODE,
            this.CHANNELCODE,
            this.SUPPLYBATCH,
            this.SUPPLYNO,
            this.SORTNO,
            this.ORDERID,
            this.ORDERNO,
            this.ROUTENAME,
            this.CUSTOMERNAME,
            this.CHANNELNAME,
            this.CIGARETTENAME,
            this.QUANTITY,
            this.SUPPLYSTATUS,
            this.SORTSTATUS});
            this.dgvChannelSort.DataSource = this.bsChannelSort;
            this.dgvChannelSort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChannelSort.Location = new System.Drawing.Point(0, 0);
            this.dgvChannelSort.Name = "dgvChannelSort";
            this.dgvChannelSort.RowTemplate.Height = 23;
            this.dgvChannelSort.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChannelSort.Size = new System.Drawing.Size(1093, 416);
            this.dgvChannelSort.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Image = global::THOK.HSS.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(53, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 14;
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
            this.btnRefresh.Location = new System.Drawing.Point(3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 47);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.FilteringEnabled = false;
            this.STATUS.HeaderText = "补货状态";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.STATUS.Visible = false;
            // 
            // ORDERDATE
            // 
            this.ORDERDATE.DataPropertyName = "ORDERDATE";
            this.ORDERDATE.HeaderText = "日期";
            this.ORDERDATE.Name = "ORDERDATE";
            this.ORDERDATE.ReadOnly = true;
            this.ORDERDATE.Visible = false;
            // 
            // BATCHNO
            // 
            this.BATCHNO.DataPropertyName = "BATCHNO";
            this.BATCHNO.HeaderText = "批次";
            this.BATCHNO.Name = "BATCHNO";
            this.BATCHNO.ReadOnly = true;
            this.BATCHNO.Visible = false;
            // 
            // LINECODE
            // 
            this.LINECODE.DataPropertyName = "LINECODE";
            this.LINECODE.HeaderText = "分拣线";
            this.LINECODE.Name = "LINECODE";
            this.LINECODE.ReadOnly = true;
            this.LINECODE.Visible = false;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "CIGARETTECODE";
            this.CIGARETTECODE.HeaderText = "卷烟代码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            this.CIGARETTECODE.Visible = false;
            // 
            // CHANNELCODE
            // 
            this.CHANNELCODE.DataPropertyName = "CHANNELCODE";
            this.CHANNELCODE.HeaderText = "烟道代码";
            this.CHANNELCODE.Name = "CHANNELCODE";
            this.CHANNELCODE.ReadOnly = true;
            this.CHANNELCODE.Visible = false;
            // 
            // SUPPLYBATCH
            // 
            this.SUPPLYBATCH.DataPropertyName = "SUPPLYBATCH";
            this.SUPPLYBATCH.FilteringEnabled = false;
            this.SUPPLYBATCH.HeaderText = "补货批次号";
            this.SUPPLYBATCH.Name = "SUPPLYBATCH";
            this.SUPPLYBATCH.ReadOnly = true;
            this.SUPPLYBATCH.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SUPPLYNO
            // 
            this.SUPPLYNO.DataPropertyName = "SUPPLYNO";
            this.SUPPLYNO.FilteringEnabled = false;
            this.SUPPLYNO.HeaderText = "补货流水号";
            this.SUPPLYNO.Name = "SUPPLYNO";
            this.SUPPLYNO.ReadOnly = true;
            this.SUPPLYNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SORTNO
            // 
            this.SORTNO.DataPropertyName = "SORTNO";
            this.SORTNO.FilteringEnabled = false;
            this.SORTNO.HeaderText = "分拣流水号";
            this.SORTNO.Name = "SORTNO";
            this.SORTNO.ReadOnly = true;
            this.SORTNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ORDERID
            // 
            this.ORDERID.DataPropertyName = "ORDERID";
            this.ORDERID.FilteringEnabled = false;
            this.ORDERID.HeaderText = "订单号";
            this.ORDERID.Name = "ORDERID";
            this.ORDERID.ReadOnly = true;
            this.ORDERID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ORDERNO
            // 
            this.ORDERNO.DataPropertyName = "ORDERNO";
            this.ORDERNO.FilteringEnabled = false;
            this.ORDERNO.HeaderText = "订单序号";
            this.ORDERNO.Name = "ORDERNO";
            this.ORDERNO.ReadOnly = true;
            // 
            // ROUTENAME
            // 
            this.ROUTENAME.DataPropertyName = "ROUTENAME";
            this.ROUTENAME.FilteringEnabled = false;
            this.ROUTENAME.HeaderText = "线路";
            this.ROUTENAME.Name = "ROUTENAME";
            this.ROUTENAME.ReadOnly = true;
            this.ROUTENAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CUSTOMERNAME
            // 
            this.CUSTOMERNAME.DataPropertyName = "CUSTOMERNAME";
            this.CUSTOMERNAME.FilteringEnabled = false;
            this.CUSTOMERNAME.HeaderText = "客户名";
            this.CUSTOMERNAME.Name = "CUSTOMERNAME";
            this.CUSTOMERNAME.ReadOnly = true;
            this.CUSTOMERNAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CHANNELNAME
            // 
            this.CHANNELNAME.DataPropertyName = "CHANNELNAME";
            this.CHANNELNAME.FilteringEnabled = false;
            this.CHANNELNAME.HeaderText = "烟道名称";
            this.CHANNELNAME.Name = "CHANNELNAME";
            this.CHANNELNAME.ReadOnly = true;
            this.CHANNELNAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.FilteringEnabled = false;
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            this.QUANTITY.FilteringEnabled = false;
            this.QUANTITY.HeaderText = "数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            this.QUANTITY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SUPPLYSTATUS
            // 
            this.SUPPLYSTATUS.DataPropertyName = "SUPPLYSTATUS";
            this.SUPPLYSTATUS.FilteringEnabled = false;
            this.SUPPLYSTATUS.HeaderText = "补货状态";
            this.SUPPLYSTATUS.Name = "SUPPLYSTATUS";
            this.SUPPLYSTATUS.ReadOnly = true;
            // 
            // SORTSTATUS
            // 
            this.SORTSTATUS.DataPropertyName = "SORTSTATUS";
            this.SORTSTATUS.FilteringEnabled = false;
            this.SORTSTATUS.HeaderText = "下单状态";
            this.SORTSTATUS.Name = "SORTSTATUS";
            this.SORTSTATUS.ReadOnly = true;
            // 
            // ChannelSortQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 469);
            this.Name = "ChannelSortQueryForm";
            this.Text = "ChannelSortQueryForm";
            this.Load += new System.EventHandler(this.ChannelSortQueryForm_Load);
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChannelSort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsChannelSort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btnRefresh;
        protected System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvChannelSort;
        private System.Windows.Forms.BindingSource bsChannelSort;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BATCHNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHANNELCODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SUPPLYBATCH;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SUPPLYNO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SORTNO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ORDERID;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ORDERNO;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ROUTENAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CUSTOMERNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CIGARETTENAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QUANTITY;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SUPPLYSTATUS;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SORTSTATUS;
    }
}