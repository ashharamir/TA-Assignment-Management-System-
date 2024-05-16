namespace DB_PROJECT_DEMO
{
    partial class TA_LandingPage
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.btnScheduleDemo = new System.Windows.Forms.Button();
            this.btnResignTA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(389, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(245, 40);
            this.lblHeading.TabIndex = 4;
            this.lblHeading.Text = "TA Dashboard";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(336, 67);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 23);
            this.lblWelcome.TabIndex = 3;
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Location = new System.Drawing.Point(33, 118);
            this.dataGridViewStudents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersWidth = 51;
            this.dataGridViewStudents.Size = new System.Drawing.Size(942, 370);
            this.dataGridViewStudents.TabIndex = 2;
            this.dataGridViewStudents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStudents_CellContentClick);
            // 
            // btnScheduleDemo
            // 
            this.btnScheduleDemo.BackColor = System.Drawing.SystemColors.Control;
            this.btnScheduleDemo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScheduleDemo.ForeColor = System.Drawing.Color.Black;
            this.btnScheduleDemo.Location = new System.Drawing.Point(750, 507);
            this.btnScheduleDemo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnScheduleDemo.Name = "btnScheduleDemo";
            this.btnScheduleDemo.Size = new System.Drawing.Size(225, 32);
            this.btnScheduleDemo.TabIndex = 1;
            this.btnScheduleDemo.Text = "Schedule a Demo";
            this.btnScheduleDemo.UseVisualStyleBackColor = false;
            this.btnScheduleDemo.Click += new System.EventHandler(this.btnScheduleDemo_Click);
            // 
            // btnResignTA
            // 
            this.btnResignTA.BackColor = System.Drawing.Color.Red;
            this.btnResignTA.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResignTA.ForeColor = System.Drawing.Color.White;
            this.btnResignTA.Location = new System.Drawing.Point(33, 507);
            this.btnResignTA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnResignTA.Name = "btnResignTA";
            this.btnResignTA.Size = new System.Drawing.Size(178, 32);
            this.btnResignTA.TabIndex = 0;
            this.btnResignTA.Text = "Resign as TA";
            this.btnResignTA.UseVisualStyleBackColor = false;
            this.btnResignTA.Click += new System.EventHandler(this.btnResignTA_Click);
            // 
            // TA_LandingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 550);
            this.Controls.Add(this.btnResignTA);
            this.Controls.Add(this.btnScheduleDemo);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblHeading);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TA_LandingPage";
            this.Text = "TA Landing Page";
            this.Load += new System.EventHandler(this.TA_LandingPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.Button btnScheduleDemo;
        private System.Windows.Forms.Button btnResignTA;
    }
}
