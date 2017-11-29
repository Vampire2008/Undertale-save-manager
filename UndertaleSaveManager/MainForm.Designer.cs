namespace UndertaleSaveManager
{
	partial class MainForm
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.savesList = new System.Windows.Forms.ListView();
			this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.loadButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.newGameButton = new System.Windows.Forms.Button();
			this.saveNameBox = new System.Windows.Forms.TextBox();
			this.searchOldSavesDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.convertButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// savesList
			// 
			this.savesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.DateColumn});
			this.savesList.FullRowSelect = true;
			resources.ApplyResources(this.savesList, "savesList");
			this.savesList.MultiSelect = false;
			this.savesList.Name = "savesList";
			this.savesList.UseCompatibleStateImageBehavior = false;
			this.savesList.View = System.Windows.Forms.View.Details;
			this.savesList.SelectedIndexChanged += new System.EventHandler(this.savesList_SelectedIndexChanged);
			this.savesList.DoubleClick += new System.EventHandler(this.savesList_DoubleClick);
			// 
			// NameColumn
			// 
			resources.ApplyResources(this.NameColumn, "NameColumn");
			// 
			// DateColumn
			// 
			resources.ApplyResources(this.DateColumn, "DateColumn");
			// 
			// loadButton
			// 
			resources.ApplyResources(this.loadButton, "loadButton");
			this.loadButton.Name = "loadButton";
			this.loadButton.UseVisualStyleBackColor = true;
			this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
			// 
			// saveButton
			// 
			resources.ApplyResources(this.saveButton, "saveButton");
			this.saveButton.Name = "saveButton";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// deleteButton
			// 
			resources.ApplyResources(this.deleteButton, "deleteButton");
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// newGameButton
			// 
			resources.ApplyResources(this.newGameButton, "newGameButton");
			this.newGameButton.Name = "newGameButton";
			this.newGameButton.UseVisualStyleBackColor = true;
			this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
			// 
			// saveNameBox
			// 
			resources.ApplyResources(this.saveNameBox, "saveNameBox");
			this.saveNameBox.Name = "saveNameBox";
			// 
			// searchOldSavesDialog
			// 
			this.searchOldSavesDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.searchOldSavesDialog.ShowNewFolderButton = false;
			// 
			// convertButton
			// 
			resources.ApplyResources(this.convertButton, "convertButton");
			this.convertButton.Name = "convertButton";
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.convertButton);
			this.Controls.Add(this.saveNameBox);
			this.Controls.Add(this.newGameButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.loadButton);
			this.Controls.Add(this.savesList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView savesList;
		private System.Windows.Forms.ColumnHeader NameColumn;
		private System.Windows.Forms.ColumnHeader DateColumn;
		private System.Windows.Forms.Button loadButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button newGameButton;
		private System.Windows.Forms.TextBox saveNameBox;
		private System.Windows.Forms.FolderBrowserDialog searchOldSavesDialog;
		private System.Windows.Forms.Button convertButton;
	}
}

