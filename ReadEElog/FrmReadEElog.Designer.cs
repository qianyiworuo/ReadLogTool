namespace ReadEElog
{
    partial class FrmReadEElog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReadEElog));
            btnSelectFile = new Button();
            txtFilePath = new TextBox();
            txtSearchString = new TextBox();
            btnSearchMission = new Button();
            label1 = new Label();
            label2 = new Label();
            lvMissions = new ListView();
            label3 = new Label();
            lblResult = new Label();
            label4 = new Label();
            dtpStart = new DateTimePicker();
            label5 = new Label();
            dtpTarget = new DateTimePicker();
            btnMissionTime = new Button();
            lvMissionTime = new ListView();
            lblRefreshTime = new Label();
            SuspendLayout();
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(278, 6);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(75, 23);
            btnSelectFile.TabIndex = 0;
            btnSelectFile.Text = "选择文件";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(74, 6);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(182, 23);
            txtFilePath.TabIndex = 1;
            // 
            // txtSearchString
            // 
            txtSearchString.Location = new Point(74, 45);
            txtSearchString.Name = "txtSearchString";
            txtSearchString.Size = new Size(182, 23);
            txtSearchString.TabIndex = 2;
            // 
            // btnSearchMission
            // 
            btnSearchMission.Location = new Point(278, 45);
            btnSearchMission.Name = "btnSearchMission";
            btnSearchMission.Size = new Size(75, 23);
            btnSearchMission.TabIndex = 3;
            btnSearchMission.Text = "筛选任务";
            btnSearchMission.UseVisualStyleBackColor = true;
            btnSearchMission.Click += btnSearchMission_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 4;
            label1.Text = "文件路径";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 50);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 5;
            label2.Text = "筛选文字";
            // 
            // lvMissions
            // 
            lvMissions.Location = new Point(12, 113);
            lvMissions.Name = "lvMissions";
            lvMissions.Size = new Size(341, 129);
            lvMissions.TabIndex = 6;
            lvMissions.UseCompatibleStateImageBehavior = false;
            lvMissions.View = View.Details;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 86);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 7;
            label3.Text = "筛选结果";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(74, 86);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(0, 15);
            lblResult.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 256);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 9;
            label4.Text = "起始时间";
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = "";
            dtpStart.Location = new Point(73, 250);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(183, 23);
            dtpStart.TabIndex = 10;
            dtpStart.Value = new DateTime(2024, 12, 26, 8, 57, 52, 0);
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 287);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 11;
            label5.Text = "目标日期";
            // 
            // dtpTarget
            // 
            dtpTarget.CustomFormat = "";
            dtpTarget.Location = new Point(73, 281);
            dtpTarget.Name = "dtpTarget";
            dtpTarget.Size = new Size(183, 23);
            dtpTarget.TabIndex = 12;
            dtpTarget.Value = new DateTime(2024, 12, 26, 8, 57, 52, 0);
            // 
            // btnMissionTime
            // 
            btnMissionTime.Location = new Point(278, 281);
            btnMissionTime.Name = "btnMissionTime";
            btnMissionTime.Size = new Size(75, 23);
            btnMissionTime.TabIndex = 13;
            btnMissionTime.Text = "任务时间";
            btnMissionTime.UseVisualStyleBackColor = true;
            btnMissionTime.Click += btnMissionTime_Click;
            // 
            // lvMissionTime
            // 
            lvMissionTime.Location = new Point(9, 310);
            lvMissionTime.Name = "lvMissionTime";
            lvMissionTime.Size = new Size(341, 99);
            lvMissionTime.TabIndex = 14;
            lvMissionTime.UseCompatibleStateImageBehavior = false;
            lvMissionTime.View = View.Details;
            // 
            // lblRefreshTime
            // 
            lblRefreshTime.AutoSize = true;
            lblRefreshTime.Location = new Point(262, 256);
            lblRefreshTime.Name = "lblRefreshTime";
            lblRefreshTime.Size = new Size(0, 15);
            lblRefreshTime.TabIndex = 15;
            // 
            // FrmReadEElog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(362, 416);
            Controls.Add(lblRefreshTime);
            Controls.Add(lvMissionTime);
            Controls.Add(btnMissionTime);
            Controls.Add(dtpTarget);
            Controls.Add(label5);
            Controls.Add(dtpStart);
            Controls.Add(label4);
            Controls.Add(lblResult);
            Controls.Add(label3);
            Controls.Add(lvMissions);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSearchMission);
            Controls.Add(txtSearchString);
            Controls.Add(txtFilePath);
            Controls.Add(btnSelectFile);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmReadEElog";
            Text = "夜灵平原任务查看器";
            Load += FrmReadEElog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectFile;
        private TextBox txtFilePath;
        private TextBox txtSearchString;
        private Button btnSearchMission;
        private Label label1;
        private Label label2;
        private ListView lvMissions;
        private Label label3;
        private Label lblResult;
        private Label label4;
        private DateTimePicker dtpStart;
        private Label label5;
        private DateTimePicker dtpTarget;
        private Button btnMissionTime;
        private ListView lvMissionTime;
        private Label lblRefreshTime;
    }
}
