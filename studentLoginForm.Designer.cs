namespace DB_PROJECT_DEMO
{
    partial class studentLoginForm
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.grpStudent = new System.Windows.Forms.GroupBox();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.txtStudentPassword = new System.Windows.Forms.TextBox();
            this.btnStudentLogin = new System.Windows.Forms.Button();
            this.grpStudent.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(109, 47);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(243, 30);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "STUDENT LOGIN";
            // 
            // grpStudent
            // 
            this.grpStudent.BackColor = System.Drawing.Color.Linen;
            this.grpStudent.Controls.Add(this.txtStudentID);
            this.grpStudent.Controls.Add(this.txtStudentPassword);
            this.grpStudent.Controls.Add(this.btnStudentLogin);
            this.grpStudent.Font = new System.Drawing.Font("Arial", 12F);
            this.grpStudent.Location = new System.Drawing.Point(75, 80);
            this.grpStudent.Name = "grpStudent";
            this.grpStudent.Size = new System.Drawing.Size(300, 350);
            this.grpStudent.TabIndex = 1;
            this.grpStudent.TabStop = false;
            // 
            // txtStudentID
            // 
            this.txtStudentID.Location = new System.Drawing.Point(50, 120);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(200, 30);
            this.txtStudentID.TabIndex = 0;
            this.txtStudentID.Text = "Student ID";
            // 
            // txtStudentPassword
            // 
            this.txtStudentPassword.Location = new System.Drawing.Point(50, 180);
            this.txtStudentPassword.Name = "txtStudentPassword";
            this.txtStudentPassword.PasswordChar = '*';
            this.txtStudentPassword.Size = new System.Drawing.Size(200, 30);
            this.txtStudentPassword.TabIndex = 1;
            this.txtStudentPassword.Text = "Password";
            // 
            // btnStudentLogin
            // 
            this.btnStudentLogin.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnStudentLogin.Location = new System.Drawing.Point(50, 244);
            this.btnStudentLogin.Name = "btnStudentLogin";
            this.btnStudentLogin.Size = new System.Drawing.Size(200, 50);
            this.btnStudentLogin.TabIndex = 2;
            this.btnStudentLogin.Text = "Login";
            this.btnStudentLogin.UseVisualStyleBackColor = false;
            this.btnStudentLogin.Click += new System.EventHandler(this.btnStudentLogin_Click);
            // 
            // studentLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 495);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.grpStudent);
            this.Name = "studentLoginForm";
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.studentLoginForm_Load);
            this.grpStudent.ResumeLayout(false);
            this.grpStudent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.GroupBox grpStudent;

        private System.Windows.Forms.Button btnStudentLogin;

        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.TextBox txtStudentPassword;
    }
}
