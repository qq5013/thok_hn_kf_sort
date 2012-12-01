namespace THOK.HSS.View
{
    partial class ChannelQueryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.LINECODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELNAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CHANNELTYPENAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CIGARETTECODE = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.CIGARETTENAME = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.QUANTITY = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.BOXQUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMQUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.pnlTool.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.btnExit);
            this.pnlTool.Controls.Add(this.btnPrint);
            this.pnlTool.Controls.Add(this.btnRefresh);
            this.pnlTool.Size = new System.Drawing.Size(793, 53);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvMain);
            this.pnlContent.Size = new System.Drawing.Size(793, 229);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(793, 282);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::THOK.HSS.Properties.Resources.Exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.Location = new System.Drawing.Point(111, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(48, 47);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::THOK.HSS.Properties.Resources.Print;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.Location = new System.Drawing.Point(57, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(48, 47);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "打印";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::THOK.HSS.Properties.Resources.Info;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(3, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(48, 47);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LINECODE,
            this.CHANNELNAME,
            this.CHANNELTYPENAME,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.QUANTITY,
            this.BOXQUANTITY,
            this.ITEMQUANTITY});
            this.dgvMain.DataSource = this.bsMain;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersWidth = 30;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1);
            this.dgvMain.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.RowTemplate.Height = 23;
            this.dgvMain.RowTemplate.ReadOnly = true;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(793, 229);
            this.dgvMain.TabIndex = 1;
            // 
            // LINECODE
            // 
            this.LINECODE.DataPropertyName = "LINECODE";
            this.LINECODE.FilteringEnabled = false;
            this.LINECODE.HeaderText = "生产线";
            this.LINECODE.Name = "LINECODE";
            this.LINECODE.ReadOnly = true;
            // 
            // CHANNELNAME
            // 
            this.CHANNELNAME.DataPropertyName = "CHANNELNAME";
            this.CHANNELNAME.FilteringEnabled = false;
            this.CHANNELNAME.HeaderText = "烟道名称";
            this.CHANNELNAME.Name = "CHANNELNAME";
            this.CHANNELNAME.ReadOnly = true;
            // 
            // CHANNELTYPENAME
            // 
            this.CHANNELTYPENAME.DataPropertyName = "CHANNELTYPENAME";
            this.CHANNELTYPENAME.FilteringEnabled = false;
            this.CHANNELTYPENAME.HeaderText = "烟道类别";
            this.CHANNELTYPENAME.Name = "CHANNELTYPENAME";
            this.CHANNELTYPENAME.ReadOnly = true;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "CIGARETTECODE";
            this.CIGARETTECODE.FilteringEnabled = false;
            this.CIGARETTECODE.HeaderText = "卷烟代码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.FilteringEnabled = false;
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            this.QUANTITY.FilteringEnabled = false;
            this.QUANTITY.HeaderText = "数量";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            // 
            // BOXQUANTITY
            // 
            this.BOXQUANTITY.DataPropertyName = "BOXQUANTITY";
            this.BOXQUANTITY.HeaderText = "整件件数";
            this.BOXQUANTITY.Name = "BOXQUANTITY";
            this.BOXQUANTITY.ReadOnly = true;
            // 
            // ITEMQUANTITY
            // 
            this.ITEMQUANTITY.DataPropertyName = "ITEMQUANTITY";
            this.ITEMQUANTITY.HeaderText = "零烟";
            this.ITEMQUANTITY.Name = "ITEMQUANTITY";
            this.ITEMQUANTITY.ReadOnly = true;
            // 
            // ChannelQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 282);
            this.Name = "ChannelQueryForm";
            this.Text = "分拣烟道信息";
            this.pnlTool.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.BindingSource bsMain;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn LINECODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELNAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CHANNELTYPENAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CIGARETTECODE;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn CIGARETTENAME;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOXQUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMQUANTITY;
    }
}