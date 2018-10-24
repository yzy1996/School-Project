namespace wuziqi
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Game_button1_undo = new System.Windows.Forms.Button();
            this.Game_button2_admitDefeat = new System.Windows.Forms.Button();
            this.Game_button3_speakPeace = new System.Windows.Forms.Button();
            this.Game_panel1_toolbar = new System.Windows.Forms.Panel();
            this.Game_label_timeleft_own = new System.Windows.Forms.Label();
            this.Game_label_timeleft_opponent = new System.Windows.Forms.Label();
            this.Game_lable_timeShow_Own = new System.Windows.Forms.TextBox();
            this.Game_lable_timeShow_Opponent = new System.Windows.Forms.TextBox();
            this.Game_lable_currentPlayer = new System.Windows.Forms.Label();
            this.Main_panel2_Cover = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Main_lable_nameOfGame = new System.Windows.Forms.Label();
            this.Main_Button1_Publisher = new System.Windows.Forms.Button();
            this.Main_Button2_StartGame = new System.Windows.Forms.Button();
            this.Game_timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Game_panel1_toolbar.SuspendLayout();
            this.Main_panel2_Cover.SuspendLayout();
            this.SuspendLayout();
            // 
            // Game_button1_undo
            // 
            this.Game_button1_undo.BackColor = System.Drawing.Color.DarkOrange;
            this.Game_button1_undo.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.Game_button1_undo.FlatAppearance.BorderSize = 2;
            this.Game_button1_undo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Game_button1_undo.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Game_button1_undo.Location = new System.Drawing.Point(3, 0);
            this.Game_button1_undo.Name = "Game_button1_undo";
            this.Game_button1_undo.Size = new System.Drawing.Size(121, 40);
            this.Game_button1_undo.TabIndex = 1;
            this.Game_button1_undo.Text = "悔棋";
            this.Game_button1_undo.UseVisualStyleBackColor = false;
            this.Game_button1_undo.Click += new System.EventHandler(this.button1_Click);
            // 
            // Game_button2_admitDefeat
            // 
            this.Game_button2_admitDefeat.BackColor = System.Drawing.Color.DarkOrange;
            this.Game_button2_admitDefeat.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.Game_button2_admitDefeat.FlatAppearance.BorderSize = 2;
            this.Game_button2_admitDefeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Game_button2_admitDefeat.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Game_button2_admitDefeat.Location = new System.Drawing.Point(3, 79);
            this.Game_button2_admitDefeat.Name = "Game_button2_admitDefeat";
            this.Game_button2_admitDefeat.Size = new System.Drawing.Size(121, 40);
            this.Game_button2_admitDefeat.TabIndex = 2;
            this.Game_button2_admitDefeat.Text = "认输";
            this.Game_button2_admitDefeat.UseVisualStyleBackColor = false;
            this.Game_button2_admitDefeat.Click += new System.EventHandler(this.button2_Click);
            // 
            // Game_button3_speakPeace
            // 
            this.Game_button3_speakPeace.BackColor = System.Drawing.Color.DarkOrange;
            this.Game_button3_speakPeace.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.Game_button3_speakPeace.FlatAppearance.BorderSize = 2;
            this.Game_button3_speakPeace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Game_button3_speakPeace.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Game_button3_speakPeace.Location = new System.Drawing.Point(3, 155);
            this.Game_button3_speakPeace.Name = "Game_button3_speakPeace";
            this.Game_button3_speakPeace.Size = new System.Drawing.Size(121, 40);
            this.Game_button3_speakPeace.TabIndex = 3;
            this.Game_button3_speakPeace.Text = "讲和";
            this.Game_button3_speakPeace.UseVisualStyleBackColor = false;
            this.Game_button3_speakPeace.Click += new System.EventHandler(this.button3_Click);
            // 
            // Game_panel1_toolbar
            // 
            this.Game_panel1_toolbar.Controls.Add(this.Game_label_timeleft_own);
            this.Game_panel1_toolbar.Controls.Add(this.Game_label_timeleft_opponent);
            this.Game_panel1_toolbar.Controls.Add(this.Game_lable_timeShow_Own);
            this.Game_panel1_toolbar.Controls.Add(this.Game_lable_timeShow_Opponent);
            this.Game_panel1_toolbar.Controls.Add(this.Game_lable_currentPlayer);
            this.Game_panel1_toolbar.Controls.Add(this.Game_button1_undo);
            this.Game_panel1_toolbar.Controls.Add(this.Game_button3_speakPeace);
            this.Game_panel1_toolbar.Controls.Add(this.Game_button2_admitDefeat);
            this.Game_panel1_toolbar.Location = new System.Drawing.Point(649, 97);
            this.Game_panel1_toolbar.Name = "Game_panel1_toolbar";
            this.Game_panel1_toolbar.Size = new System.Drawing.Size(136, 551);
            this.Game_panel1_toolbar.TabIndex = 4;
            // 
            // Game_label_timeleft_own
            // 
            this.Game_label_timeleft_own.AutoSize = true;
            this.Game_label_timeleft_own.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Game_label_timeleft_own.ForeColor = System.Drawing.SystemColors.Info;
            this.Game_label_timeleft_own.Location = new System.Drawing.Point(0, 382);
            this.Game_label_timeleft_own.Name = "Game_label_timeleft_own";
            this.Game_label_timeleft_own.Size = new System.Drawing.Size(110, 16);
            this.Game_label_timeleft_own.TabIndex = 9;
            this.Game_label_timeleft_own.Text = "你的剩余时间";
            // 
            // Game_label_timeleft_opponent
            // 
            this.Game_label_timeleft_opponent.AutoSize = true;
            this.Game_label_timeleft_opponent.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Game_label_timeleft_opponent.ForeColor = System.Drawing.SystemColors.Info;
            this.Game_label_timeleft_opponent.Location = new System.Drawing.Point(-1, 442);
            this.Game_label_timeleft_opponent.Name = "Game_label_timeleft_opponent";
            this.Game_label_timeleft_opponent.Size = new System.Drawing.Size(127, 16);
            this.Game_label_timeleft_opponent.TabIndex = 8;
            this.Game_label_timeleft_opponent.Text = "对方的剩余时间";
            // 
            // Game_lable_timeShow_Own
            // 
            this.Game_lable_timeShow_Own.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Game_lable_timeShow_Own.Font = new System.Drawing.Font("Engravers MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Game_lable_timeShow_Own.ForeColor = System.Drawing.SystemColors.Info;
            this.Game_lable_timeShow_Own.Location = new System.Drawing.Point(2, 461);
            this.Game_lable_timeShow_Own.Margin = new System.Windows.Forms.Padding(2);
            this.Game_lable_timeShow_Own.Name = "Game_lable_timeShow_Own";
            this.Game_lable_timeShow_Own.ReadOnly = true;
            this.Game_lable_timeShow_Own.Size = new System.Drawing.Size(122, 26);
            this.Game_lable_timeShow_Own.TabIndex = 6;
            this.Game_lable_timeShow_Own.Text = "20:00";
            this.Game_lable_timeShow_Own.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Game_lable_timeShow_Opponent
            // 
            this.Game_lable_timeShow_Opponent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Game_lable_timeShow_Opponent.Font = new System.Drawing.Font("Engravers MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Game_lable_timeShow_Opponent.ForeColor = System.Drawing.SystemColors.Info;
            this.Game_lable_timeShow_Opponent.Location = new System.Drawing.Point(3, 401);
            this.Game_lable_timeShow_Opponent.Margin = new System.Windows.Forms.Padding(2);
            this.Game_lable_timeShow_Opponent.Name = "Game_lable_timeShow_Opponent";
            this.Game_lable_timeShow_Opponent.ReadOnly = true;
            this.Game_lable_timeShow_Opponent.Size = new System.Drawing.Size(121, 26);
            this.Game_lable_timeShow_Opponent.TabIndex = 7;
            this.Game_lable_timeShow_Opponent.Text = "20:00";
            this.Game_lable_timeShow_Opponent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Game_lable_currentPlayer
            // 
            this.Game_lable_currentPlayer.AutoSize = true;
            this.Game_lable_currentPlayer.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Game_lable_currentPlayer.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Game_lable_currentPlayer.Location = new System.Drawing.Point(3, 334);
            this.Game_lable_currentPlayer.Name = "Game_lable_currentPlayer";
            this.Game_lable_currentPlayer.Size = new System.Drawing.Size(0, 21);
            this.Game_lable_currentPlayer.TabIndex = 4;
            this.Game_lable_currentPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Main_panel2_Cover
            // 
            this.Main_panel2_Cover.Controls.Add(this.label1);
            this.Main_panel2_Cover.Controls.Add(this.Main_lable_nameOfGame);
            this.Main_panel2_Cover.Controls.Add(this.Main_Button1_Publisher);
            this.Main_panel2_Cover.Controls.Add(this.Main_Button2_StartGame);
            this.Main_panel2_Cover.Location = new System.Drawing.Point(0, -2);
            this.Main_panel2_Cover.Name = "Main_panel2_Cover";
            this.Main_panel2_Cover.Size = new System.Drawing.Size(643, 650);
            this.Main_panel2_Cover.TabIndex = 5;
            this.Main_panel2_Cover.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Algerian", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(277, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 39);
            this.label1.TabIndex = 9;
            this.label1.Text = "FIVE IN A ROW";
            // 
            // Main_lable_nameOfGame
            // 
            this.Main_lable_nameOfGame.AutoSize = true;
            this.Main_lable_nameOfGame.Font = new System.Drawing.Font("方正舒体", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Main_lable_nameOfGame.ForeColor = System.Drawing.Color.DarkOrange;
            this.Main_lable_nameOfGame.Location = new System.Drawing.Point(231, 127);
            this.Main_lable_nameOfGame.Name = "Main_lable_nameOfGame";
            this.Main_lable_nameOfGame.Size = new System.Drawing.Size(334, 101);
            this.Main_lable_nameOfGame.TabIndex = 6;
            this.Main_lable_nameOfGame.Text = "五子棋";
            this.Main_lable_nameOfGame.Click += new System.EventHandler(this.标题_Click);
            // 
            // Main_Button1_Publisher
            // 
            this.Main_Button1_Publisher.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Main_Button1_Publisher.BackColor = System.Drawing.Color.DarkOrange;
            this.Main_Button1_Publisher.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.Main_Button1_Publisher.FlatAppearance.BorderSize = 2;
            this.Main_Button1_Publisher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Main_Button1_Publisher.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Main_Button1_Publisher.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Main_Button1_Publisher.Location = new System.Drawing.Point(317, 484);
            this.Main_Button1_Publisher.Name = "Main_Button1_Publisher";
            this.Main_Button1_Publisher.Size = new System.Drawing.Size(153, 53);
            this.Main_Button1_Publisher.TabIndex = 5;
            this.Main_Button1_Publisher.Text = " PUBLISHER";
            this.Main_Button1_Publisher.UseVisualStyleBackColor = false;
            this.Main_Button1_Publisher.Click += new System.EventHandler(this.Main_Publish_Click);
            // 
            // Main_Button2_StartGame
            // 
            this.Main_Button2_StartGame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Main_Button2_StartGame.BackColor = System.Drawing.Color.DarkOrange;
            this.Main_Button2_StartGame.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Main_Button2_StartGame.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.Main_Button2_StartGame.FlatAppearance.BorderSize = 2;
            this.Main_Button2_StartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Main_Button2_StartGame.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Main_Button2_StartGame.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Main_Button2_StartGame.Location = new System.Drawing.Point(317, 380);
            this.Main_Button2_StartGame.Name = "Main_Button2_StartGame";
            this.Main_Button2_StartGame.Size = new System.Drawing.Size(153, 52);
            this.Main_Button2_StartGame.TabIndex = 4;
            this.Main_Button2_StartGame.Text = "START";
            this.Main_Button2_StartGame.UseVisualStyleBackColor = false;
            this.Main_Button2_StartGame.Click += new System.EventHandler(this.Main_Begin_Click);
            // 
            // Game_timer1
            // 
            this.Game_timer1.Interval = 1000;
            this.Game_timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(789, 661);
            this.Controls.Add(this.Main_panel2_Cover);
            this.Controls.Add(this.Game_panel1_toolbar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "五子棋联机版";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Game_panel1_toolbar.ResumeLayout(false);
            this.Game_panel1_toolbar.PerformLayout();
            this.Main_panel2_Cover.ResumeLayout(false);
            this.Main_panel2_Cover.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button Game_button1_undo;
        private System.Windows.Forms.Button Game_button2_admitDefeat;
        private System.Windows.Forms.Button Game_button3_speakPeace;
        private System.Windows.Forms.Panel Game_panel1_toolbar;
        private System.Windows.Forms.Panel Main_panel2_Cover;
        private System.Windows.Forms.Button Main_Button2_StartGame;
        private System.Windows.Forms.Button Main_Button1_Publisher;
        private System.Windows.Forms.Label Main_lable_nameOfGame;
        private System.Windows.Forms.Label Game_lable_currentPlayer;
        private System.Windows.Forms.TextBox Game_lable_timeShow_Own;
        private System.Windows.Forms.TextBox Game_lable_timeShow_Opponent;
        private System.Windows.Forms.Timer Game_timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Game_label_timeleft_own;
        private System.Windows.Forms.Label Game_label_timeleft_opponent;
    }
}

