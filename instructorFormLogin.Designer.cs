namespace DB_PROJECT_DEMO
{
    partial class instructorFormLogin
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
            this.txtInstructorID = new System.Windows.Forms.TextBox();
            this.txtInstructorPassword = new System.Windows.Forms.TextBox();
            this.btnInstructorLogin = new System.Windows.Forms.Button();
            this.grpStudent.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(103, 47);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(300, 30);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "FACULTY LOGIN";
            // 
            // grpStudent
            // 
            this.grpStudent.BackColor = System.Drawing.Color.Linen;
            this.grpStudent.Controls.Add(this.txtInstructorID);
            this.grpStudent.Controls.Add(this.txtInstructorPassword);
            this.grpStudent.Controls.Add(this.btnInstructorLogin);
            this.grpStudent.Font = new System.Drawing.Font("Arial", 12F);
            this.grpStudent.Location = new System.Drawing.Point(75, 80);
            this.grpStudent.Name = "grpStudent";
            this.grpStudent.Size = new System.Drawing.Size(300, 350);
            this.grpStudent.TabIndex = 1;
            this.grpStudent.TabStop = false;
            // 
            // txtInstructorID
            // 
            this.txtInstructorID.Location = new System.Drawing.Point(50, 120);
            this.txtInstructorID.Name = "txtInstructorID";
            this.txtInstructorID.Size = new System.Drawing.Size(200, 30);
            this.txtInstructorID.TabIndex = 0;
            this.txtInstructorID.Text = "Instructor ID";
            // 
            // txtInstructorPassword
            // 
            this.txtInstructorPassword.Location = new System.Drawing.Point(50, 180);
            this.txtInstructorPassword.Name = "txtInstructorPassword";
            this.txtInstructorPassword.PasswordChar = '*';
            this.txtInstructorPassword.Size = new System.Drawing.Size(200, 30);
            this.txtInstructorPassword.TabIndex = 1;
            this.txtInstructorPassword.Text = "Password";
            // 
            // btnInstructorLogin
            // 
            this.btnInstructorLogin.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnInstructorLogin.Location = new System.Drawing.Point(50, 244);
            this.btnInstructorLogin.Name = "btnInstructorLogin";
            this.btnInstructorLogin.Size = new System.Drawing.Size(200, 50);
            this.btnInstructorLogin.TabIndex = 2;
            this.btnInstructorLogin.Text = "Login";
            this.btnInstructorLogin.UseVisualStyleBackColor = false;
            this.btnInstructorLogin.Click += new System.EventHandler(this.btnInstructorLogin_Click);
            // 
            // instructorFormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 495);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.grpStudent);
            this.Name = "instructorFormLogin";
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.instructorFormLogin_Load);
            this.grpStudent.ResumeLayout(false);
            this.grpStudent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.GroupBox grpStudent;

        private System.Windows.Forms.Button btnInstructorLogin;

        private System.Windows.Forms.TextBox txtInstructorID;
        private System.Windows.Forms.TextBox txtInstructorPassword;
    }
}
