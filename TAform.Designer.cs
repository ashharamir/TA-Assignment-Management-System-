using System;
using System.Windows.Forms;

namespace DB_PROJECT_DEMO
{
    public partial class TAform : Form
    {
        private ComboBox deptComboBox;
        private ComboBox courseComboBox;
        private ComboBox sectionComboBox;
        private Button confirmButton;
        private Label deptLabel; // Declaration of deptLabel
        private Label courseLabel; // Declaration of courseLabel
        private Label sectionLabel; // Declaration of sectionLabel


        private void InitializeComponent()
        {
            this.deptComboBox = new System.Windows.Forms.ComboBox();
            this.courseComboBox = new System.Windows.Forms.ComboBox();
            this.sectionComboBox = new System.Windows.Forms.ComboBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.deptLabel = new System.Windows.Forms.Label();
            this.courseLabel = new System.Windows.Forms.Label();
            this.sectionLabel = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // deptComboBox
            // 
            this.deptComboBox.FormattingEnabled = true;
            this.deptComboBox.Location = new System.Drawing.Point(422, 154);
            this.deptComboBox.Name = "deptComboBox";
            this.deptComboBox.Size = new System.Drawing.Size(200, 24);
            this.deptComboBox.TabIndex = 0;
            this.deptComboBox.SelectedIndexChanged += new System.EventHandler(this.deptComboBox_SelectedIndexChanged);
            // 
            // courseComboBox
            // 
            this.courseComboBox.FormattingEnabled = true;
            this.courseComboBox.Location = new System.Drawing.Point(422, 204);
            this.courseComboBox.Name = "courseComboBox";
            this.courseComboBox.Size = new System.Drawing.Size(200, 24);
            this.courseComboBox.TabIndex = 1;
            this.courseComboBox.SelectedIndexChanged += new System.EventHandler(this.courseComboBox_SelectedIndexChanged);
            // 
            // sectionComboBox
            // 
            this.sectionComboBox.FormattingEnabled = true;
            this.sectionComboBox.Location = new System.Drawing.Point(422, 254);
            this.sectionComboBox.Name = "sectionComboBox";
            this.sectionComboBox.Size = new System.Drawing.Size(200, 24);
            this.sectionComboBox.TabIndex = 2;
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.confirmButton.Location = new System.Drawing.Point(472, 304);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(100, 30);
            this.confirmButton.TabIndex = 3;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // deptLabel
            // 
            this.deptLabel.AutoSize = true;
            this.deptLabel.Location = new System.Drawing.Point(377, 156);
            this.deptLabel.Name = "deptLabel";
            this.deptLabel.Size = new System.Drawing.Size(42, 16);
            this.deptLabel.TabIndex = 4;
            this.deptLabel.Text = "Dept: ";
            // 
            // courseLabel
            // 
            this.courseLabel.AutoSize = true;
            this.courseLabel.Location = new System.Drawing.Point(367, 204);
            this.courseLabel.Name = "courseLabel";
            this.courseLabel.Size = new System.Drawing.Size(56, 16);
            this.courseLabel.TabIndex = 5;
            this.courseLabel.Text = "Course: ";
            // 
            // sectionLabel
            // 
            this.sectionLabel.AutoSize = true;
            this.sectionLabel.Location = new System.Drawing.Point(367, 254);
            this.sectionLabel.Name = "sectionLabel";
            this.sectionLabel.Size = new System.Drawing.Size(58, 16);
            this.sectionLabel.TabIndex = 6;
            this.sectionLabel.Text = "Section: ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(446, 355);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(155, 16);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "View vacancies instead?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(416, 82);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(300, 30);
            this.lblWelcome.TabIndex = 18;
            this.lblWelcome.Text = "APPLY FOR TA";
            // 
            // TAform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.sectionComboBox);
            this.Controls.Add(this.courseComboBox);
            this.Controls.Add(this.deptComboBox);
            this.Controls.Add(this.deptLabel);
            this.Controls.Add(this.courseLabel);
            this.Controls.Add(this.sectionLabel);
            this.Name = "TAform";
            this.Text = "TA Application";
            this.Load += new System.EventHandler(this.TAform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private LinkLabel linkLabel1;
        private Label lblWelcome;
    }
}
