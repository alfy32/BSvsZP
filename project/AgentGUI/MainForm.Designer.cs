namespace AgentGUI
{
  partial class MainForm
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.agentType = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.publicIP = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.aNumber = new System.Windows.Forms.Label();
      this.name = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.location = new System.Windows.Forms.Label();
      this.points = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.messageBox = new System.Windows.Forms.ListBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.points);
      this.groupBox1.Controls.Add(this.location);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.name);
      this.groupBox1.Controls.Add(this.aNumber);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.publicIP);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.agentType);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(432, 115);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Agent Info";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(22, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Agent Type:";
      // 
      // agentType
      // 
      this.agentType.AutoSize = true;
      this.agentType.Location = new System.Drawing.Point(93, 28);
      this.agentType.Name = "agentType";
      this.agentType.Size = new System.Drawing.Size(113, 13);
      this.agentType.TabIndex = 1;
      this.agentType.Text = "AGENT TYPE VALUE";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(236, 28);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(52, 13);
      this.label4.TabIndex = 2;
      this.label4.Text = "Public IP:";
      // 
      // publicIP
      // 
      this.publicIP.AutoSize = true;
      this.publicIP.Location = new System.Drawing.Point(294, 28);
      this.publicIP.Name = "publicIP";
      this.publicIP.Size = new System.Drawing.Size(96, 13);
      this.publicIP.TabIndex = 3;
      this.publicIP.Text = "PUBLIC IP VALUE";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(30, 58);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "A Number:";
      // 
      // aNumber
      // 
      this.aNumber.AutoSize = true;
      this.aNumber.Location = new System.Drawing.Point(93, 58);
      this.aNumber.Name = "aNumber";
      this.aNumber.Size = new System.Drawing.Size(102, 13);
      this.aNumber.TabIndex = 5;
      this.aNumber.Text = "A NUMBER VALUE";
      // 
      // name
      // 
      this.name.AutoSize = true;
      this.name.Location = new System.Drawing.Point(294, 58);
      this.name.Name = "name";
      this.name.Size = new System.Drawing.Size(67, 13);
      this.name.TabIndex = 6;
      this.name.Text = "FULL NAME";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(250, 58);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(38, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Name:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(36, 86);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(51, 13);
      this.label5.TabIndex = 8;
      this.label5.Text = "Location:";
      // 
      // location
      // 
      this.location.AutoSize = true;
      this.location.Location = new System.Drawing.Point(93, 86);
      this.location.Name = "location";
      this.location.Size = new System.Drawing.Size(99, 13);
      this.location.TabIndex = 9;
      this.location.Text = "LOCATION VALUE";
      // 
      // points
      // 
      this.points.AutoSize = true;
      this.points.Location = new System.Drawing.Point(294, 86);
      this.points.Name = "points";
      this.points.Size = new System.Drawing.Size(78, 13);
      this.points.TabIndex = 10;
      this.points.Text = "POINT VALUE";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(249, 86);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(39, 13);
      this.label6.TabIndex = 11;
      this.label6.Text = "Points:";
      // 
      // messageBox
      // 
      this.messageBox.FormattingEnabled = true;
      this.messageBox.Location = new System.Drawing.Point(12, 133);
      this.messageBox.Name = "messageBox";
      this.messageBox.Size = new System.Drawing.Size(432, 251);
      this.messageBox.TabIndex = 1;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(453, 392);
      this.Controls.Add(this.messageBox);
      this.Controls.Add(this.groupBox1);
      this.Name = "MainForm";
      this.Text = "Brilliant Students VS Zombie Professors";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label publicIP;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label agentType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label points;
    private System.Windows.Forms.Label location;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label name;
    private System.Windows.Forms.Label aNumber;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox messageBox;
  }
}