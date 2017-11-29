using System;
using System.Windows.Forms;

namespace UndertaleSaveManager
{
	public partial class RenameForm : Form
	{
		internal string SaveName
		{
			get => saveNameBox.Text;
			set
			{
				label2.Visible = false;
				saveNameBox.Text = value;
			}
		}

		public RenameForm()
		{
			InitializeComponent();
		}

		private void acceptButton_Click(object sender, EventArgs e)
		{
			if (((MainForm)Owner).CheckExist(SaveName))
			{
				label2.Visible = true;
				return;
			}
			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}
