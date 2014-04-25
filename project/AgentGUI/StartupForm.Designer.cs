namespace AgentGUI
{
  partial class StartupForm
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
      this.port = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.lastName = new System.Windows.Forms.TextBox();
      this.firstName = new System.Windows.Forms.TextBox();
      this.agentType = new System.Windows.Forms.ComboBox();
      this.aNumber = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.refresh = new System.Windows.Forms.Button();
      this.availableGames = new System.Windows.Forms.ListBox();
      this.quit = new System.Windows.Forms.Button();
      this.startAgent = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.port)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.port);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.lastName);
      this.groupBox1.Controls.Add(this.firstName);
      this.groupBox1.Controls.Add(this.agentType);
      this.groupBox1.Controls.Add(this.aNumber);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(295, 192);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Agent Info";
      // 
      // port
      // 
      this.port.Location = new System.Drawing.Point(92, 53);
      this.port.Maximum = new decimal(new int[] {
            51999,
            0,
            0,
            0});
      this.port.Minimum = new decimal(new int[] {
            51100,
            0,
            0,
            0});
      this.port.Name = "port";
      this.port.Size = new System.Drawing.Size(191, 20);
      this.port.TabIndex = 10;
      this.port.Value = new decimal(new int[] {
            51100,
            0,
            0,
            0});
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(57, 55);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(29, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Port:";
      // 
      // lastName
      // 
      this.lastName.Location = new System.Drawing.Point(92, 154);
      this.lastName.Name = "lastName";
      this.lastName.Size = new System.Drawing.Size(191, 20);
      this.lastName.TabIndex = 7;
      this.lastName.Text = "Christensen";
      // 
      // firstName
      // 
      this.firstName.Location = new System.Drawing.Point(92, 128);
      this.firstName.Name = "firstName";
      this.firstName.Size = new System.Drawing.Size(191, 20);
      this.firstName.TabIndex = 6;
      this.firstName.Text = "Alan";
      // 
      // agentType
      // 
      this.agentType.FormattingEnabled = true;
      this.agentType.Items.AddRange(new object[] {
            "Brilliant Student",
            "Excuse Generator",
            "Whining Spinner"});
      this.agentType.Location = new System.Drawing.Point(92, 26);
      this.agentType.Name = "agentType";
      this.agentType.Size = new System.Drawing.Size(191, 21);
      this.agentType.TabIndex = 5;
      this.agentType.Text = "Brilliant Student";
      // 
      // aNumber
      // 
      this.aNumber.Location = new System.Drawing.Point(92, 102);
      this.aNumber.Name = "aNumber";
      this.aNumber.Size = new System.Drawing.Size(191, 20);
      this.aNumber.TabIndex = 4;
      this.aNumber.Text = "A01072246";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(26, 131);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(60, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "First Name:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(25, 157);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(61, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Last Name:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(29, 105);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "A Number:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(21, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Agent Type:";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.refresh);
      this.groupBox2.Controls.Add(this.availableGames);
      this.groupBox2.Location = new System.Drawing.Point(12, 210);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(295, 229);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Available Games";
      // 
      // refresh
      // 
      this.refresh.Location = new System.Drawing.Point(15, 188);
      this.refresh.Name = "refresh";
      this.refresh.Size = new System.Drawing.Size(265, 25);
      this.refresh.TabIndex = 1;
      this.refresh.Text = "Refresh";
      this.refresh.UseVisualStyleBackColor = true;
      this.refresh.Click += new System.EventHandler(this.refresh_Click);
      // 
      // availableGames
      // 
      this.availableGames.FormattingEnabled = true;
      this.availableGames.Location = new System.Drawing.Point(15, 22);
      this.availableGames.Name = "availableGames";
      this.availableGames.Size = new System.Drawing.Size(265, 160);
      this.availableGames.TabIndex = 0;
      // 
      // quit
      // 
      this.quit.Location = new System.Drawing.Point(207, 445);
      this.quit.Name = "quit";
      this.quit.Size = new System.Drawing.Size(100, 29);
      this.quit.TabIndex = 2;
      this.quit.Text = "Quit";
      this.quit.UseVisualStyleBackColor = true;
      this.quit.Click += new System.EventHandler(this.quit_Click);
      // 
      // startAgent
      // 
      this.startAgent.Location = new System.Drawing.Point(101, 445);
      this.startAgent.Name = "startAgent";
      this.startAgent.Size = new System.Drawing.Size(100, 29);
      this.startAgent.TabIndex = 3;
      this.startAgent.Text = "Start Agent";
      this.startAgent.UseVisualStyleBackColor = true;
      this.startAgent.Click += new System.EventHandler(this.startAgent_Click);
      // 
      // StartupForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(318, 486);
      this.Controls.Add(this.startAgent);
      this.Controls.Add(this.quit);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "StartupForm";
      this.Text = "BS vs ZP";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.port)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox lastName;
    private System.Windows.Forms.TextBox firstName;
    private System.Windows.Forms.ComboBox agentType;
    private System.Windows.Forms.TextBox aNumber;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button refresh;
    private System.Windows.Forms.ListBox availableGames;
    private System.Windows.Forms.Button quit;
    private System.Windows.Forms.Button startAgent;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown port;
  }
}

