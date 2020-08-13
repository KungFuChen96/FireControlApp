namespace FireControlApp
{
    partial class ModStatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labCellID = new System.Windows.Forms.Label();
            this.txtCellID = new System.Windows.Forms.TextBox();
            this.txtRcl = new System.Windows.Forms.TextBox();
            this.labRCL = new System.Windows.Forms.Label();
            this.txtCurrSt = new System.Windows.Forms.TextBox();
            this.labCurrStatus = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboxStatus = new System.Windows.Forms.ComboBox();
            this.btnCancel = new BatMes.Client.Controls.IconButton();
            this.btnSave = new BatMes.Client.Controls.IconButton();
            this.SuspendLayout();
            // 
            // labCellID
            // 
            this.labCellID.AutoSize = true;
            this.labCellID.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCellID.Location = new System.Drawing.Point(165, 59);
            this.labCellID.Name = "labCellID";
            this.labCellID.Size = new System.Drawing.Size(82, 24);
            this.labCellID.TabIndex = 0;
            this.labCellID.Text = "库位ID";
            // 
            // txtCellID
            // 
            this.txtCellID.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCellID.Location = new System.Drawing.Point(260, 59);
            this.txtCellID.Name = "txtCellID";
            this.txtCellID.ReadOnly = true;
            this.txtCellID.Size = new System.Drawing.Size(255, 34);
            this.txtCellID.TabIndex = 1;
            // 
            // txtRcl
            // 
            this.txtRcl.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRcl.Location = new System.Drawing.Point(260, 120);
            this.txtRcl.Name = "txtRcl";
            this.txtRcl.ReadOnly = true;
            this.txtRcl.Size = new System.Drawing.Size(255, 34);
            this.txtRcl.TabIndex = 3;
            // 
            // labRCL
            // 
            this.labRCL.AutoSize = true;
            this.labRCL.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRCL.Location = new System.Drawing.Point(165, 120);
            this.labRCL.Name = "labRCL";
            this.labRCL.Size = new System.Drawing.Size(82, 24);
            this.labRCL.TabIndex = 2;
            this.labRCL.Text = "行列层";
            // 
            // txtCurrSt
            // 
            this.txtCurrSt.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrSt.Location = new System.Drawing.Point(260, 184);
            this.txtCurrSt.Name = "txtCurrSt";
            this.txtCurrSt.ReadOnly = true;
            this.txtCurrSt.Size = new System.Drawing.Size(255, 34);
            this.txtCurrSt.TabIndex = 5;
            // 
            // labCurrStatus
            // 
            this.labCurrStatus.AutoSize = true;
            this.labCurrStatus.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCurrStatus.Location = new System.Drawing.Point(141, 187);
            this.labCurrStatus.Name = "labCurrStatus";
            this.labCurrStatus.Size = new System.Drawing.Size(106, 24);
            this.labCurrStatus.TabIndex = 4;
            this.labCurrStatus.Text = "当前状态";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.Location = new System.Drawing.Point(141, 251);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(106, 24);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "修改状态";
            // 
            // cboxStatus
            // 
            this.cboxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxStatus.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboxStatus.FormattingEnabled = true;
            this.cboxStatus.Location = new System.Drawing.Point(260, 251);
            this.cboxStatus.Name = "cboxStatus";
            this.cboxStatus.Size = new System.Drawing.Size(255, 31);
            this.cboxStatus.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.IconType = BatMes.Client.Controls.IconButtonType.None;
            this.btnCancel.IconWidth = 50;
            this.btnCancel.IsActive = true;
            this.btnCancel.Location = new System.Drawing.Point(416, 349);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(145, 50);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.IconType = BatMes.Client.Controls.IconButtonType.Save;
            this.btnSave.IconWidth = 50;
            this.btnSave.IsActive = true;
            this.btnSave.Location = new System.Drawing.Point(145, 349);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(157, 50);
            this.btnSave.TabIndex = 20;
            this.btnSave.TextValue = "修改";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ModStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(667, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cboxStatus);
            this.Controls.Add(this.txtCurrSt);
            this.Controls.Add(this.labCurrStatus);
            this.Controls.Add(this.txtRcl);
            this.Controls.Add(this.labRCL);
            this.Controls.Add(this.txtCellID);
            this.Controls.Add(this.labCellID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ModStatusForm";
            this.Text = "修改库位状态";
            this.Load += new System.EventHandler(this.ModStatusForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labCellID;
        private System.Windows.Forms.TextBox txtCellID;
        private System.Windows.Forms.TextBox txtRcl;
        private System.Windows.Forms.Label labRCL;
        private System.Windows.Forms.TextBox txtCurrSt;
        private System.Windows.Forms.Label labCurrStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboxStatus;
        private BatMes.Client.Controls.IconButton btnCancel;
        private BatMes.Client.Controls.IconButton btnSave;
    }
}