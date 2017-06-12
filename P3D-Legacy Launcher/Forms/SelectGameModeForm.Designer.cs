namespace P3D.Legacy.Launcher.Forms
{
    partial class SelectGameModeForm
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
            this.DataGridView_GameModes = new System.Windows.Forms.DataGridView();
            this.Button_Select = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_GameModes)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView_GameModes
            // 
            this.DataGridView_GameModes.AllowUserToAddRows = false;
            this.DataGridView_GameModes.AllowUserToDeleteRows = false;
            this.DataGridView_GameModes.AllowUserToResizeRows = false;
            this.DataGridView_GameModes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_GameModes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_GameModes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_GameModes.Location = new System.Drawing.Point(0, 0);
            this.DataGridView_GameModes.MultiSelect = false;
            this.DataGridView_GameModes.Name = "DataGridView_GameModes";
            this.DataGridView_GameModes.RowHeadersVisible = false;
            this.DataGridView_GameModes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_GameModes.Size = new System.Drawing.Size(684, 325);
            this.DataGridView_GameModes.TabIndex = 0;
            // 
            // Button_Select
            // 
            this.Button_Select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Select.Location = new System.Drawing.Point(522, 331);
            this.Button_Select.Name = "Button_Select";
            this.Button_Select.Size = new System.Drawing.Size(150, 23);
            this.Button_Select.TabIndex = 1;
            this.Button_Select.Text = "sgm_button_select";
            this.Button_Select.UseVisualStyleBackColor = true;
            // 
            // SelectGameModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 366);
            this.Controls.Add(this.Button_Select);
            this.Controls.Add(this.DataGridView_GameModes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectGameModeForm";
            this.StringID_Title = "sgm_title";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_GameModes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_GameModes;
        private Controls.LocalizableButton Button_Select;
    }
}