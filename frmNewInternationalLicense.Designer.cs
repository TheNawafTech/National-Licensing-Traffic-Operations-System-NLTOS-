namespace Driving___Vehicle_License_Department__DVLD__Project
{
    partial class frmNewInternationalLicense
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
            this.label1 = new System.Windows.Forms.Label();
            this.usInternationalApp1 = new DVLD_Controls.UsInternationalApp();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            this.lbShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.lbShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.usFindDriverLicenseInfo1 = new LocalDrivingLicenseList.UsFindDriverLicenseInfo();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(317, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "International License Application";
            // 
            // usInternationalApp1
            // 
            this.usInternationalApp1.Location = new System.Drawing.Point(21, 499);
            this.usInternationalApp1.Name = "usInternationalApp1";
            this.usInternationalApp1.Size = new System.Drawing.Size(921, 154);
            this.usInternationalApp1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::Driving___Vehicle_License_Department__DVLD__Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(662, 671);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 48);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnIssue
            // 
            this.btnIssue.BackgroundImage = global::Driving___Vehicle_License_Department__DVLD__Project.Properties.Resources.International_32;
            this.btnIssue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(800, 671);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(114, 48);
            this.btnIssue.TabIndex = 3;
            this.btnIssue.Text = " Issue";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // lbShowLicenseHistory
            // 
            this.lbShowLicenseHistory.AutoSize = true;
            this.lbShowLicenseHistory.Enabled = false;
            this.lbShowLicenseHistory.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbShowLicenseHistory.Location = new System.Drawing.Point(30, 682);
            this.lbShowLicenseHistory.Name = "lbShowLicenseHistory";
            this.lbShowLicenseHistory.Size = new System.Drawing.Size(177, 22);
            this.lbShowLicenseHistory.TabIndex = 5;
            this.lbShowLicenseHistory.TabStop = true;
            this.lbShowLicenseHistory.Text = "Show License History";
            this.lbShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbShowLicenseHistory_LinkClicked);
            // 
            // lbShowLicenseInfo
            // 
            this.lbShowLicenseInfo.AutoSize = true;
            this.lbShowLicenseInfo.Enabled = false;
            this.lbShowLicenseInfo.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbShowLicenseInfo.Location = new System.Drawing.Point(240, 682);
            this.lbShowLicenseInfo.Name = "lbShowLicenseInfo";
            this.lbShowLicenseInfo.Size = new System.Drawing.Size(155, 22);
            this.lbShowLicenseInfo.TabIndex = 6;
            this.lbShowLicenseInfo.TabStop = true;
            this.lbShowLicenseInfo.Text = "Show License Info";
            this.lbShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbShowLicenseInfo_LinkClicked);
            // 
            // usFindDriverLicenseInfo1
            // 
            this.usFindDriverLicenseInfo1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.usFindDriverLicenseInfo1.Location = new System.Drawing.Point(1, 50);
            this.usFindDriverLicenseInfo1.Name = "usFindDriverLicenseInfo1";
            this.usFindDriverLicenseInfo1.Size = new System.Drawing.Size(941, 443);
            this.usFindDriverLicenseInfo1.TabIndex = 0;
            this.usFindDriverLicenseInfo1.Load += new System.EventHandler(this.usFindDriverLicenseInfo1_Load);
            // 
            // frmNewInternationalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(944, 731);
            this.Controls.Add(this.lbShowLicenseInfo);
            this.Controls.Add(this.lbShowLicenseHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.usInternationalApp1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usFindDriverLicenseInfo1);
            this.Name = "frmNewInternationalLicense";
            this.Text = "frmNewInternationalLicense";
            this.Load += new System.EventHandler(this.frmNewInternationalLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LocalDrivingLicenseList.UsFindDriverLicenseInfo usFindDriverLicenseInfo1;
        private System.Windows.Forms.Label label1;
        private DVLD_Controls.UsInternationalApp usInternationalApp1;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel lbShowLicenseHistory;
        private System.Windows.Forms.LinkLabel lbShowLicenseInfo;
    }
}