namespace THOK.AS.OTS.Process
{
    partial class MainPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.pnlFirst = new System.Windows.Forms.Panel();
            this.pnlFirstBottom = new System.Windows.Forms.Panel();
            this.dgvFirst = new System.Windows.Forms.DataGridView();
            this.卷烟名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.条数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.包号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFirstTop = new System.Windows.Forms.Panel();
            this.lblPackageCountFirst = new System.Windows.Forms.Label();
            this.lblTotalQuantityFirst = new System.Windows.Forms.Label();
            this.lblCustomerFirst = new System.Windows.Forms.Label();
            this.packageFirst = new System.Windows.Forms.Label();
            this.quantityFirst = new System.Windows.Forms.Label();
            this.customerNameFirst = new System.Windows.Forms.Label();
            this.orderIDFirst = new System.Windows.Forms.Label();
            this.lblOrderIDFirst = new System.Windows.Forms.Label();
            this.pnlNext = new System.Windows.Forms.Panel();
            this.pnlNextBottom = new System.Windows.Forms.Panel();
            this.dgvNext = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlNextTop = new System.Windows.Forms.Panel();
            this.lblPackageCountNext = new System.Windows.Forms.Label();
            this.lblQuantityNext = new System.Windows.Forms.Label();
            this.lblCustomerNext = new System.Windows.Forms.Label();
            this.packageNext = new System.Windows.Forms.Label();
            this.customerNameNext = new System.Windows.Forms.Label();
            this.quantityNext = new System.Windows.Forms.Label();
            this.lblOrderIDNext = new System.Windows.Forms.Label();
            this.orderIDNext = new System.Windows.Forms.Label();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.pnlFirst.SuspendLayout();
            this.pnlFirstBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFirst)).BeginInit();
            this.pnlFirstTop.SuspendLayout();
            this.pnlNext.SuspendLayout();
            this.pnlNextBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNext)).BeginInit();
            this.pnlNextTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.pnlFirst);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.pnlNext);
            this.scMain.Size = new System.Drawing.Size(1029, 560);
            this.scMain.SplitterDistance = 508;
            this.scMain.SplitterWidth = 1;
            this.scMain.TabIndex = 0;
            // 
            // pnlFirst
            // 
            this.pnlFirst.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlFirst.Controls.Add(this.pnlFirstBottom);
            this.pnlFirst.Controls.Add(this.pnlFirstTop);
            this.pnlFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFirst.Location = new System.Drawing.Point(0, 0);
            this.pnlFirst.Name = "pnlFirst";
            this.pnlFirst.Size = new System.Drawing.Size(508, 560);
            this.pnlFirst.TabIndex = 2;
            // 
            // pnlFirstBottom
            // 
            this.pnlFirstBottom.Controls.Add(this.dgvFirst);
            this.pnlFirstBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFirstBottom.Location = new System.Drawing.Point(0, 142);
            this.pnlFirstBottom.Name = "pnlFirstBottom";
            this.pnlFirstBottom.Size = new System.Drawing.Size(508, 418);
            this.pnlFirstBottom.TabIndex = 2;
            // 
            // dgvFirst
            // 
            this.dgvFirst.AllowUserToAddRows = false;
            this.dgvFirst.AllowUserToDeleteRows = false;
            this.dgvFirst.AllowUserToResizeColumns = false;
            this.dgvFirst.AllowUserToResizeRows = false;
            this.dgvFirst.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFirst.BackgroundColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFirst.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFirst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFirst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.卷烟名称,
            this.条数,
            this.包号});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 16F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFirst.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFirst.Location = new System.Drawing.Point(0, 0);
            this.dgvFirst.Name = "dgvFirst";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 20F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFirst.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFirst.RowHeadersVisible = false;
            this.dgvFirst.RowHeadersWidth = 20;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dgvFirst.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFirst.RowTemplate.Height = 23;
            this.dgvFirst.RowTemplate.ReadOnly = true;
            this.dgvFirst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFirst.ShowCellErrors = false;
            this.dgvFirst.ShowCellToolTips = false;
            this.dgvFirst.ShowEditingIcon = false;
            this.dgvFirst.ShowRowErrors = false;
            this.dgvFirst.Size = new System.Drawing.Size(508, 418);
            this.dgvFirst.TabIndex = 0;
            // 
            // 卷烟名称
            // 
            this.卷烟名称.DataPropertyName = "CIGARETTENAME";
            this.卷烟名称.HeaderText = "卷烟名称";
            this.卷烟名称.Name = "卷烟名称";
            // 
            // 条数
            // 
            this.条数.DataPropertyName = "Quantity";
            this.条数.HeaderText = "条数";
            this.条数.Name = "条数";
            // 
            // 包号
            // 
            this.包号.HeaderText = "包号";
            this.包号.Name = "包号";
            // 
            // pnlFirstTop
            // 
            this.pnlFirstTop.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlFirstTop.Controls.Add(this.lblPackageCountFirst);
            this.pnlFirstTop.Controls.Add(this.lblTotalQuantityFirst);
            this.pnlFirstTop.Controls.Add(this.lblCustomerFirst);
            this.pnlFirstTop.Controls.Add(this.packageFirst);
            this.pnlFirstTop.Controls.Add(this.quantityFirst);
            this.pnlFirstTop.Controls.Add(this.customerNameFirst);
            this.pnlFirstTop.Controls.Add(this.orderIDFirst);
            this.pnlFirstTop.Controls.Add(this.lblOrderIDFirst);
            this.pnlFirstTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFirstTop.Location = new System.Drawing.Point(0, 0);
            this.pnlFirstTop.Name = "pnlFirstTop";
            this.pnlFirstTop.Size = new System.Drawing.Size(508, 142);
            this.pnlFirstTop.TabIndex = 1;
            // 
            // lblPackageCountFirst
            // 
            this.lblPackageCountFirst.AutoSize = true;
            this.lblPackageCountFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.lblPackageCountFirst.ForeColor = System.Drawing.Color.Lime;
            this.lblPackageCountFirst.Location = new System.Drawing.Point(252, 96);
            this.lblPackageCountFirst.Name = "lblPackageCountFirst";
            this.lblPackageCountFirst.Size = new System.Drawing.Size(117, 34);
            this.lblPackageCountFirst.TabIndex = 0;
            this.lblPackageCountFirst.Text = "包数：";
            // 
            // lblTotalQuantityFirst
            // 
            this.lblTotalQuantityFirst.AutoSize = true;
            this.lblTotalQuantityFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.lblTotalQuantityFirst.ForeColor = System.Drawing.Color.Lime;
            this.lblTotalQuantityFirst.Location = new System.Drawing.Point(3, 96);
            this.lblTotalQuantityFirst.Name = "lblTotalQuantityFirst";
            this.lblTotalQuantityFirst.Size = new System.Drawing.Size(117, 34);
            this.lblTotalQuantityFirst.TabIndex = 0;
            this.lblTotalQuantityFirst.Text = "条数：";
            // 
            // lblCustomerFirst
            // 
            this.lblCustomerFirst.AutoSize = true;
            this.lblCustomerFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.lblCustomerFirst.ForeColor = System.Drawing.Color.Lime;
            this.lblCustomerFirst.Location = new System.Drawing.Point(3, 52);
            this.lblCustomerFirst.Name = "lblCustomerFirst";
            this.lblCustomerFirst.Size = new System.Drawing.Size(185, 34);
            this.lblCustomerFirst.TabIndex = 0;
            this.lblCustomerFirst.Text = "客户名称：";
            // 
            // packageFirst
            // 
            this.packageFirst.AutoSize = true;
            this.packageFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.packageFirst.ForeColor = System.Drawing.Color.Lime;
            this.packageFirst.Location = new System.Drawing.Point(362, 96);
            this.packageFirst.Name = "packageFirst";
            this.packageFirst.Size = new System.Drawing.Size(117, 34);
            this.packageFirst.TabIndex = 0;
            this.packageFirst.Text = "0     ";
            // 
            // quantityFirst
            // 
            this.quantityFirst.AutoSize = true;
            this.quantityFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.quantityFirst.ForeColor = System.Drawing.Color.Lime;
            this.quantityFirst.Location = new System.Drawing.Point(121, 96);
            this.quantityFirst.Name = "quantityFirst";
            this.quantityFirst.Size = new System.Drawing.Size(117, 34);
            this.quantityFirst.TabIndex = 0;
            this.quantityFirst.Text = "0     ";
            // 
            // customerNameFirst
            // 
            this.customerNameFirst.AutoSize = true;
            this.customerNameFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.customerNameFirst.ForeColor = System.Drawing.Color.Lime;
            this.customerNameFirst.Location = new System.Drawing.Point(182, 52);
            this.customerNameFirst.Name = "customerNameFirst";
            this.customerNameFirst.Size = new System.Drawing.Size(355, 34);
            this.customerNameFirst.TabIndex = 0;
            this.customerNameFirst.Text = "                    ";
            // 
            // orderIDFirst
            // 
            this.orderIDFirst.AutoSize = true;
            this.orderIDFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.orderIDFirst.ForeColor = System.Drawing.Color.Lime;
            this.orderIDFirst.Location = new System.Drawing.Point(123, 9);
            this.orderIDFirst.Name = "orderIDFirst";
            this.orderIDFirst.Size = new System.Drawing.Size(253, 34);
            this.orderIDFirst.TabIndex = 0;
            this.orderIDFirst.Text = "              ";
            // 
            // lblOrderIDFirst
            // 
            this.lblOrderIDFirst.AutoSize = true;
            this.lblOrderIDFirst.Font = new System.Drawing.Font("宋体", 25F);
            this.lblOrderIDFirst.ForeColor = System.Drawing.Color.Lime;
            this.lblOrderIDFirst.Location = new System.Drawing.Point(2, 9);
            this.lblOrderIDFirst.Name = "lblOrderIDFirst";
            this.lblOrderIDFirst.Size = new System.Drawing.Size(151, 34);
            this.lblOrderIDFirst.TabIndex = 0;
            this.lblOrderIDFirst.Text = "订单号：";
            // 
            // pnlNext
            // 
            this.pnlNext.Controls.Add(this.pnlNextBottom);
            this.pnlNext.Controls.Add(this.pnlNextTop);
            this.pnlNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNext.Location = new System.Drawing.Point(0, 0);
            this.pnlNext.Name = "pnlNext";
            this.pnlNext.Size = new System.Drawing.Size(520, 560);
            this.pnlNext.TabIndex = 3;
            // 
            // pnlNextBottom
            // 
            this.pnlNextBottom.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlNextBottom.Controls.Add(this.dgvNext);
            this.pnlNextBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNextBottom.Location = new System.Drawing.Point(0, 142);
            this.pnlNextBottom.Name = "pnlNextBottom";
            this.pnlNextBottom.Size = new System.Drawing.Size(520, 418);
            this.pnlNextBottom.TabIndex = 2;
            // 
            // dgvNext
            // 
            this.dgvNext.AllowUserToAddRows = false;
            this.dgvNext.AllowUserToDeleteRows = false;
            this.dgvNext.AllowUserToResizeColumns = false;
            this.dgvNext.AllowUserToResizeRows = false;
            this.dgvNext.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNext.BackgroundColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNext.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvNext.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNext.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.ColumnPack});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 16F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNext.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNext.Location = new System.Drawing.Point(0, 0);
            this.dgvNext.Name = "dgvNext";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 20F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNext.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvNext.RowHeadersVisible = false;
            this.dgvNext.RowHeadersWidth = 20;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dgvNext.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvNext.RowTemplate.Height = 23;
            this.dgvNext.RowTemplate.ReadOnly = true;
            this.dgvNext.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNext.ShowCellErrors = false;
            this.dgvNext.ShowCellToolTips = false;
            this.dgvNext.ShowEditingIcon = false;
            this.dgvNext.ShowRowErrors = false;
            this.dgvNext.Size = new System.Drawing.Size(520, 418);
            this.dgvNext.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CIGARETTENAME";
            this.dataGridViewTextBoxColumn1.HeaderText = "卷烟名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Quantity";
            this.dataGridViewTextBoxColumn2.HeaderText = "条数";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // ColumnPack
            // 
            this.ColumnPack.HeaderText = "包号";
            this.ColumnPack.Name = "ColumnPack";
            // 
            // pnlNextTop
            // 
            this.pnlNextTop.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlNextTop.Controls.Add(this.lblPackageCountNext);
            this.pnlNextTop.Controls.Add(this.lblQuantityNext);
            this.pnlNextTop.Controls.Add(this.lblCustomerNext);
            this.pnlNextTop.Controls.Add(this.packageNext);
            this.pnlNextTop.Controls.Add(this.customerNameNext);
            this.pnlNextTop.Controls.Add(this.quantityNext);
            this.pnlNextTop.Controls.Add(this.lblOrderIDNext);
            this.pnlNextTop.Controls.Add(this.orderIDNext);
            this.pnlNextTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNextTop.Location = new System.Drawing.Point(0, 0);
            this.pnlNextTop.Name = "pnlNextTop";
            this.pnlNextTop.Size = new System.Drawing.Size(520, 142);
            this.pnlNextTop.TabIndex = 1;
            // 
            // lblPackageCountNext
            // 
            this.lblPackageCountNext.AutoSize = true;
            this.lblPackageCountNext.Font = new System.Drawing.Font("宋体", 25F);
            this.lblPackageCountNext.ForeColor = System.Drawing.Color.Lime;
            this.lblPackageCountNext.Location = new System.Drawing.Point(256, 96);
            this.lblPackageCountNext.Name = "lblPackageCountNext";
            this.lblPackageCountNext.Size = new System.Drawing.Size(117, 34);
            this.lblPackageCountNext.TabIndex = 3;
            this.lblPackageCountNext.Text = "包数：";
            // 
            // lblQuantityNext
            // 
            this.lblQuantityNext.AutoSize = true;
            this.lblQuantityNext.Font = new System.Drawing.Font("宋体", 25F);
            this.lblQuantityNext.ForeColor = System.Drawing.Color.Lime;
            this.lblQuantityNext.Location = new System.Drawing.Point(7, 96);
            this.lblQuantityNext.Name = "lblQuantityNext";
            this.lblQuantityNext.Size = new System.Drawing.Size(117, 34);
            this.lblQuantityNext.TabIndex = 4;
            this.lblQuantityNext.Text = "条数：";
            // 
            // lblCustomerNext
            // 
            this.lblCustomerNext.AutoSize = true;
            this.lblCustomerNext.Font = new System.Drawing.Font("宋体", 25F);
            this.lblCustomerNext.ForeColor = System.Drawing.Color.Lime;
            this.lblCustomerNext.Location = new System.Drawing.Point(7, 52);
            this.lblCustomerNext.Name = "lblCustomerNext";
            this.lblCustomerNext.Size = new System.Drawing.Size(185, 34);
            this.lblCustomerNext.TabIndex = 1;
            this.lblCustomerNext.Text = "客户名称：";
            // 
            // packageNext
            // 
            this.packageNext.AutoSize = true;
            this.packageNext.Font = new System.Drawing.Font("宋体", 25F);
            this.packageNext.ForeColor = System.Drawing.Color.Lime;
            this.packageNext.Location = new System.Drawing.Point(368, 96);
            this.packageNext.Name = "packageNext";
            this.packageNext.Size = new System.Drawing.Size(117, 34);
            this.packageNext.TabIndex = 0;
            this.packageNext.Text = "0     ";
            // 
            // customerNameNext
            // 
            this.customerNameNext.AutoSize = true;
            this.customerNameNext.Font = new System.Drawing.Font("宋体", 25F);
            this.customerNameNext.ForeColor = System.Drawing.Color.Lime;
            this.customerNameNext.Location = new System.Drawing.Point(192, 52);
            this.customerNameNext.Name = "customerNameNext";
            this.customerNameNext.Size = new System.Drawing.Size(389, 34);
            this.customerNameNext.TabIndex = 0;
            this.customerNameNext.Text = "                      ";
            // 
            // quantityNext
            // 
            this.quantityNext.AutoSize = true;
            this.quantityNext.Font = new System.Drawing.Font("宋体", 25F);
            this.quantityNext.ForeColor = System.Drawing.Color.Lime;
            this.quantityNext.Location = new System.Drawing.Point(130, 96);
            this.quantityNext.Name = "quantityNext";
            this.quantityNext.Size = new System.Drawing.Size(117, 34);
            this.quantityNext.TabIndex = 0;
            this.quantityNext.Text = "0     ";
            // 
            // lblOrderIDNext
            // 
            this.lblOrderIDNext.AutoSize = true;
            this.lblOrderIDNext.Font = new System.Drawing.Font("宋体", 25F);
            this.lblOrderIDNext.ForeColor = System.Drawing.Color.Lime;
            this.lblOrderIDNext.Location = new System.Drawing.Point(6, 9);
            this.lblOrderIDNext.Name = "lblOrderIDNext";
            this.lblOrderIDNext.Size = new System.Drawing.Size(151, 34);
            this.lblOrderIDNext.TabIndex = 2;
            this.lblOrderIDNext.Text = "订单号：";
            // 
            // orderIDNext
            // 
            this.orderIDNext.AutoSize = true;
            this.orderIDNext.Font = new System.Drawing.Font("宋体", 25F);
            this.orderIDNext.ForeColor = System.Drawing.Color.Lime;
            this.orderIDNext.Location = new System.Drawing.Point(147, 9);
            this.orderIDNext.Name = "orderIDNext";
            this.orderIDNext.Size = new System.Drawing.Size(253, 34);
            this.orderIDNext.TabIndex = 0;
            this.orderIDNext.Text = "              ";
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.scMain);
            this.Name = "MainPanel";
            this.Size = new System.Drawing.Size(1029, 560);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.pnlFirst.ResumeLayout(false);
            this.pnlFirstBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFirst)).EndInit();
            this.pnlFirstTop.ResumeLayout(false);
            this.pnlFirstTop.PerformLayout();
            this.pnlNext.ResumeLayout(false);
            this.pnlNextBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNext)).EndInit();
            this.pnlNextTop.ResumeLayout(false);
            this.pnlNextTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Panel pnlFirst;
        private System.Windows.Forms.Panel pnlFirstBottom;
        private System.Windows.Forms.DataGridView dgvFirst;
        private System.Windows.Forms.Panel pnlFirstTop;
        private System.Windows.Forms.Label lblPackageCountFirst;
        private System.Windows.Forms.Label lblTotalQuantityFirst;
        private System.Windows.Forms.Label lblCustomerFirst;
        private System.Windows.Forms.Label packageFirst;
        private System.Windows.Forms.Label quantityFirst;
        private System.Windows.Forms.Label customerNameFirst;
        private System.Windows.Forms.Label orderIDFirst;
        private System.Windows.Forms.Label lblOrderIDFirst;
        private System.Windows.Forms.Panel pnlNext;
        private System.Windows.Forms.Panel pnlNextBottom;
        private System.Windows.Forms.DataGridView dgvNext;
        private System.Windows.Forms.Panel pnlNextTop;
        private System.Windows.Forms.Label lblPackageCountNext;
        private System.Windows.Forms.Label lblQuantityNext;
        private System.Windows.Forms.Label lblCustomerNext;
        private System.Windows.Forms.Label packageNext;
        private System.Windows.Forms.Label customerNameNext;
        private System.Windows.Forms.Label quantityNext;
        private System.Windows.Forms.Label lblOrderIDNext;
        private System.Windows.Forms.Label orderIDNext;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卷烟名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 条数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 包号;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPack;

    }
}
