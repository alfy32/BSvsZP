namespace AgentGUI
{
  partial class ThoughtsForm
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
      this.label11 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.moveToY = new System.Windows.Forms.TextBox();
      this.moveToX = new System.Windows.Forms.TextBox();
      this.move = new System.Windows.Forms.Button();
      this.getField = new System.Windows.Forms.Button();
      this.getWhiners = new System.Windows.Forms.Button();
      this.getZombies = new System.Windows.Forms.Button();
      this.getExcuses = new System.Windows.Forms.Button();
      this.getStudents = new System.Windows.Forms.Button();
      this.getConfiguration = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(68, 102);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(15, 13);
      this.label11.TabIndex = 22;
      this.label11.Text = "y:";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(19, 102);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(15, 13);
      this.label9.TabIndex = 21;
      this.label9.Text = "x:";
      // 
      // moveToY
      // 
      this.moveToY.Location = new System.Drawing.Point(80, 99);
      this.moveToY.Name = "moveToY";
      this.moveToY.Size = new System.Drawing.Size(31, 20);
      this.moveToY.TabIndex = 20;
      // 
      // moveToX
      // 
      this.moveToX.Location = new System.Drawing.Point(31, 99);
      this.moveToX.Name = "moveToX";
      this.moveToX.Size = new System.Drawing.Size(31, 20);
      this.moveToX.TabIndex = 19;
      // 
      // move
      // 
      this.move.Location = new System.Drawing.Point(117, 97);
      this.move.Name = "move";
      this.move.Size = new System.Drawing.Size(99, 23);
      this.move.TabIndex = 18;
      this.move.Text = "Move";
      this.move.UseVisualStyleBackColor = true;
      this.move.Click += new System.EventHandler(this.move_Click);
      // 
      // getField
      // 
      this.getField.Location = new System.Drawing.Point(12, 70);
      this.getField.Name = "getField";
      this.getField.Size = new System.Drawing.Size(99, 23);
      this.getField.TabIndex = 17;
      this.getField.Text = "Get Field Layout";
      this.getField.UseVisualStyleBackColor = true;
      this.getField.Click += new System.EventHandler(this.getField_Click);
      // 
      // getWhiners
      // 
      this.getWhiners.Location = new System.Drawing.Point(117, 41);
      this.getWhiners.Name = "getWhiners";
      this.getWhiners.Size = new System.Drawing.Size(99, 23);
      this.getWhiners.TabIndex = 16;
      this.getWhiners.Text = "Get Whiners";
      this.getWhiners.UseVisualStyleBackColor = true;
      this.getWhiners.Click += new System.EventHandler(this.getWhiners_Click);
      // 
      // getZombies
      // 
      this.getZombies.Location = new System.Drawing.Point(12, 41);
      this.getZombies.Name = "getZombies";
      this.getZombies.Size = new System.Drawing.Size(99, 23);
      this.getZombies.TabIndex = 15;
      this.getZombies.Text = "Get Zombies";
      this.getZombies.UseVisualStyleBackColor = true;
      this.getZombies.Click += new System.EventHandler(this.getZombies_Click);
      // 
      // getExcuses
      // 
      this.getExcuses.Location = new System.Drawing.Point(117, 12);
      this.getExcuses.Name = "getExcuses";
      this.getExcuses.Size = new System.Drawing.Size(99, 23);
      this.getExcuses.TabIndex = 14;
      this.getExcuses.Text = "Get Excuses";
      this.getExcuses.UseVisualStyleBackColor = true;
      this.getExcuses.Click += new System.EventHandler(this.getExcuses_Click);
      // 
      // getStudents
      // 
      this.getStudents.Location = new System.Drawing.Point(12, 12);
      this.getStudents.Name = "getStudents";
      this.getStudents.Size = new System.Drawing.Size(99, 23);
      this.getStudents.TabIndex = 13;
      this.getStudents.Text = "Get Students";
      this.getStudents.UseVisualStyleBackColor = true;
      this.getStudents.Click += new System.EventHandler(this.getStudents_Click);
      // 
      // getConfiguration
      // 
      this.getConfiguration.Location = new System.Drawing.Point(116, 70);
      this.getConfiguration.Name = "getConfiguration";
      this.getConfiguration.Size = new System.Drawing.Size(99, 23);
      this.getConfiguration.TabIndex = 23;
      this.getConfiguration.Text = "Get Configuration";
      this.getConfiguration.UseVisualStyleBackColor = true;
      this.getConfiguration.Click += new System.EventHandler(this.getConfiguration_Click);
      // 
      // ThoughtsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(227, 131);
      this.Controls.Add(this.getConfiguration);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.moveToY);
      this.Controls.Add(this.moveToX);
      this.Controls.Add(this.move);
      this.Controls.Add(this.getField);
      this.Controls.Add(this.getWhiners);
      this.Controls.Add(this.getZombies);
      this.Controls.Add(this.getExcuses);
      this.Controls.Add(this.getStudents);
      this.Name = "ThoughtsForm";
      this.Text = "ThoughtsForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox moveToY;
    private System.Windows.Forms.TextBox moveToX;
    private System.Windows.Forms.Button move;
    private System.Windows.Forms.Button getField;
    private System.Windows.Forms.Button getWhiners;
    private System.Windows.Forms.Button getZombies;
    private System.Windows.Forms.Button getExcuses;
    private System.Windows.Forms.Button getStudents;
    private System.Windows.Forms.Button getConfiguration;

  }
}