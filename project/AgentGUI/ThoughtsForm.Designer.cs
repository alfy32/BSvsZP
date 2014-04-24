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
      this.getField = new System.Windows.Forms.Button();
      this.getWhiners = new System.Windows.Forms.Button();
      this.getZombies = new System.Windows.Forms.Button();
      this.getExcuses = new System.Windows.Forms.Button();
      this.getStudents = new System.Windows.Forms.Button();
      this.getConfiguration = new System.Windows.Forms.Button();
      this.getExcuse = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.genteratorId = new System.Windows.Forms.TextBox();
      this.spinnerId = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.getWhine = new System.Windows.Forms.Button();
      this.moveUp = new System.Windows.Forms.Button();
      this.moveDown = new System.Windows.Forms.Button();
      this.moveLeft = new System.Windows.Forms.Button();
      this.moveRight = new System.Windows.Forms.Button();
      this.moveSpeed = new System.Windows.Forms.TextBox();
      this.moveUpLeft = new System.Windows.Forms.Button();
      this.moveDownLeft = new System.Windows.Forms.Button();
      this.moveDownRight = new System.Windows.Forms.Button();
      this.moveUpRight = new System.Windows.Forms.Button();
      this.exitGame = new System.Windows.Forms.Button();
      this.endUpdates = new System.Windows.Forms.Button();
      this.startUpdates = new System.Windows.Forms.Button();
      this.SuspendLayout();
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
      // getExcuse
      // 
      this.getExcuse.Location = new System.Drawing.Point(117, 157);
      this.getExcuse.Name = "getExcuse";
      this.getExcuse.Size = new System.Drawing.Size(99, 23);
      this.getExcuse.TabIndex = 24;
      this.getExcuse.Text = "Get Excuse";
      this.getExcuse.UseVisualStyleBackColor = true;
      this.getExcuse.Click += new System.EventHandler(this.getExcuse_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 162);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(71, 13);
      this.label1.TabIndex = 25;
      this.label1.Text = "Generator ID:";
      // 
      // genteratorId
      // 
      this.genteratorId.Location = new System.Drawing.Point(81, 159);
      this.genteratorId.Name = "genteratorId";
      this.genteratorId.Size = new System.Drawing.Size(31, 20);
      this.genteratorId.TabIndex = 26;
      this.genteratorId.Text = "0";
      // 
      // spinnerId
      // 
      this.spinnerId.Location = new System.Drawing.Point(82, 188);
      this.spinnerId.Name = "spinnerId";
      this.spinnerId.Size = new System.Drawing.Size(31, 20);
      this.spinnerId.TabIndex = 29;
      this.spinnerId.Text = "0";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 191);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 13);
      this.label2.TabIndex = 28;
      this.label2.Text = "Spinner ID:";
      // 
      // getWhine
      // 
      this.getWhine.Location = new System.Drawing.Point(118, 186);
      this.getWhine.Name = "getWhine";
      this.getWhine.Size = new System.Drawing.Size(99, 23);
      this.getWhine.TabIndex = 27;
      this.getWhine.Text = "Get Whine";
      this.getWhine.UseVisualStyleBackColor = true;
      this.getWhine.Click += new System.EventHandler(this.getWhine_Click);
      // 
      // moveUp
      // 
      this.moveUp.Location = new System.Drawing.Point(89, 215);
      this.moveUp.Name = "moveUp";
      this.moveUp.Size = new System.Drawing.Size(55, 23);
      this.moveUp.TabIndex = 30;
      this.moveUp.Text = "^";
      this.moveUp.UseVisualStyleBackColor = true;
      this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
      // 
      // moveDown
      // 
      this.moveDown.Location = new System.Drawing.Point(89, 271);
      this.moveDown.Name = "moveDown";
      this.moveDown.Size = new System.Drawing.Size(55, 23);
      this.moveDown.TabIndex = 31;
      this.moveDown.Text = "v";
      this.moveDown.UseVisualStyleBackColor = true;
      this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
      // 
      // moveLeft
      // 
      this.moveLeft.Location = new System.Drawing.Point(43, 242);
      this.moveLeft.Name = "moveLeft";
      this.moveLeft.Size = new System.Drawing.Size(55, 23);
      this.moveLeft.TabIndex = 32;
      this.moveLeft.Text = "<";
      this.moveLeft.UseVisualStyleBackColor = true;
      this.moveLeft.Click += new System.EventHandler(this.moveLeft_Click);
      // 
      // moveRight
      // 
      this.moveRight.Location = new System.Drawing.Point(141, 242);
      this.moveRight.Name = "moveRight";
      this.moveRight.Size = new System.Drawing.Size(55, 23);
      this.moveRight.TabIndex = 33;
      this.moveRight.Text = ">";
      this.moveRight.UseVisualStyleBackColor = true;
      this.moveRight.Click += new System.EventHandler(this.moveRight_Click);
      // 
      // moveSpeed
      // 
      this.moveSpeed.Location = new System.Drawing.Point(104, 244);
      this.moveSpeed.Name = "moveSpeed";
      this.moveSpeed.Size = new System.Drawing.Size(31, 20);
      this.moveSpeed.TabIndex = 34;
      this.moveSpeed.Text = "2";
      // 
      // moveUpLeft
      // 
      this.moveUpLeft.Location = new System.Drawing.Point(43, 215);
      this.moveUpLeft.Name = "moveUpLeft";
      this.moveUpLeft.Size = new System.Drawing.Size(40, 23);
      this.moveUpLeft.TabIndex = 35;
      this.moveUpLeft.UseVisualStyleBackColor = true;
      this.moveUpLeft.Click += new System.EventHandler(this.moveUpLeft_Click);
      // 
      // moveDownLeft
      // 
      this.moveDownLeft.Location = new System.Drawing.Point(43, 271);
      this.moveDownLeft.Name = "moveDownLeft";
      this.moveDownLeft.Size = new System.Drawing.Size(40, 23);
      this.moveDownLeft.TabIndex = 36;
      this.moveDownLeft.UseVisualStyleBackColor = true;
      this.moveDownLeft.Click += new System.EventHandler(this.moveDownLeft_Click);
      // 
      // moveDownRight
      // 
      this.moveDownRight.Location = new System.Drawing.Point(147, 271);
      this.moveDownRight.Name = "moveDownRight";
      this.moveDownRight.Size = new System.Drawing.Size(49, 23);
      this.moveDownRight.TabIndex = 37;
      this.moveDownRight.UseVisualStyleBackColor = true;
      this.moveDownRight.Click += new System.EventHandler(this.moveDownRight_Click);
      // 
      // moveUpRight
      // 
      this.moveUpRight.Location = new System.Drawing.Point(150, 215);
      this.moveUpRight.Name = "moveUpRight";
      this.moveUpRight.Size = new System.Drawing.Size(46, 23);
      this.moveUpRight.TabIndex = 38;
      this.moveUpRight.UseVisualStyleBackColor = true;
      this.moveUpRight.Click += new System.EventHandler(this.moveUpRight_Click);
      // 
      // exitGame
      // 
      this.exitGame.Location = new System.Drawing.Point(117, 128);
      this.exitGame.Name = "exitGame";
      this.exitGame.Size = new System.Drawing.Size(99, 23);
      this.exitGame.TabIndex = 39;
      this.exitGame.Text = "Exit Game";
      this.exitGame.UseVisualStyleBackColor = true;
      this.exitGame.Click += new System.EventHandler(this.exitGame_Click);
      // 
      // endUpdates
      // 
      this.endUpdates.Location = new System.Drawing.Point(117, 99);
      this.endUpdates.Name = "endUpdates";
      this.endUpdates.Size = new System.Drawing.Size(99, 23);
      this.endUpdates.TabIndex = 40;
      this.endUpdates.Text = "End Updates";
      this.endUpdates.UseVisualStyleBackColor = true;
      this.endUpdates.Click += new System.EventHandler(this.endUpdates_Click);
      // 
      // startUpdates
      // 
      this.startUpdates.Location = new System.Drawing.Point(12, 99);
      this.startUpdates.Name = "startUpdates";
      this.startUpdates.Size = new System.Drawing.Size(99, 23);
      this.startUpdates.TabIndex = 41;
      this.startUpdates.Text = "Start Updates";
      this.startUpdates.UseVisualStyleBackColor = true;
      this.startUpdates.Click += new System.EventHandler(this.startUpdates_Click);
      // 
      // ThoughtsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(227, 342);
      this.Controls.Add(this.startUpdates);
      this.Controls.Add(this.endUpdates);
      this.Controls.Add(this.exitGame);
      this.Controls.Add(this.moveUpRight);
      this.Controls.Add(this.moveDownRight);
      this.Controls.Add(this.moveDownLeft);
      this.Controls.Add(this.moveUpLeft);
      this.Controls.Add(this.moveSpeed);
      this.Controls.Add(this.moveRight);
      this.Controls.Add(this.moveLeft);
      this.Controls.Add(this.moveDown);
      this.Controls.Add(this.moveUp);
      this.Controls.Add(this.spinnerId);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.getWhine);
      this.Controls.Add(this.genteratorId);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.getExcuse);
      this.Controls.Add(this.getConfiguration);
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

    private System.Windows.Forms.Button getField;
    private System.Windows.Forms.Button getWhiners;
    private System.Windows.Forms.Button getZombies;
    private System.Windows.Forms.Button getExcuses;
    private System.Windows.Forms.Button getStudents;
    private System.Windows.Forms.Button getConfiguration;
    private System.Windows.Forms.Button getExcuse;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox genteratorId;
    private System.Windows.Forms.TextBox spinnerId;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button getWhine;
    private System.Windows.Forms.Button moveUp;
    private System.Windows.Forms.Button moveDown;
    private System.Windows.Forms.Button moveLeft;
    private System.Windows.Forms.Button moveRight;
    private System.Windows.Forms.TextBox moveSpeed;
    private System.Windows.Forms.Button moveUpLeft;
    private System.Windows.Forms.Button moveDownLeft;
    private System.Windows.Forms.Button moveDownRight;
    private System.Windows.Forms.Button moveUpRight;
    private System.Windows.Forms.Button exitGame;
    private System.Windows.Forms.Button endUpdates;
    private System.Windows.Forms.Button startUpdates;

  }
}