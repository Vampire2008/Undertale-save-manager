using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using UndertaleSaveManager.Properties;

namespace UndertaleSaveManager
{
	public partial class MainForm : Form
	{
		private readonly string _currentSavePath =
			Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UNDERTALE");

		private readonly string _savesPath;
		private readonly Dictionary<string, ListViewGroup> _groups;
		public MainForm()
		{
			InitializeComponent();
			_savesPath = Path.Combine(_currentSavePath, "Saves");
			if (!Directory.Exists(_savesPath))
				Directory.CreateDirectory(_savesPath);
			var files = Directory.GetFiles(_savesPath, "*.uts", SearchOption.AllDirectories);
			_groups = new Dictionary<string, ListViewGroup>();
			foreach (var file in files)
			{
				try
				{
					var save = JsonConvert.DeserializeObject<SaveModel>(File.ReadAllText(file));
					save.Path = Path.GetDirectoryName(file);
					if (!_groups.ContainsKey(save.CharacterName))
					{
						var group = new ListViewGroup(save.CharacterName);
						savesList.Groups.Add(group);
						_groups.Add(save.CharacterName, group);
					}
					savesList.Items.Add(new ListViewItem(_groups[save.CharacterName])
					{
						Text = save.Name,
						SubItems = { save.Date.ToString("dd.MM.yyyy HH:mm") },
						Tag = save
					});
				}
				catch (Exception)
				{
					//Skip if save broken
				}
			}
		}

		private void loadButton_Click(object sender, EventArgs e)
		{
			if (savesList.SelectedItems.Count == 0)
				return;
			if (MessageBox.Show(Resources.Load_question_text, Resources.Load_question_title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			var save = savesList.SelectedItems[0];
			foreach (var file in Directory.GetFiles(((SaveModel)save.Tag).Path))
			{
				if (Path.GetExtension(file) != ".uts")
				{
					File.Copy(file, Path.Combine(_currentSavePath, Path.GetFileName(file)), true);
				}
			}
		}

		private void savesList_DoubleClick(object sender, EventArgs e)
		{
			loadButton.PerformClick();
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			if (!Directory.GetFiles(_currentSavePath, "*", SearchOption.TopDirectoryOnly).Any())
			{
				MessageBox.Show(Resources.Nothing_to_save, Resources.Nothing, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (string.IsNullOrWhiteSpace(saveNameBox.Text))
			{
				MessageBox.Show(Resources.Name_of_save, Resources.No_save, MessageBoxButtons.OK, MessageBoxIcon.Error);
				saveNameBox.Focus();
				return;
			}
			var saveDir = saveNameBox.Text;
			if (savesList.Items.OfType<ListViewItem>().Any(l => l.Text == saveNameBox.Text))
			{
				var save = savesList.Items.OfType<ListViewItem>().Single(l => l.Text == saveNameBox.Text);
				var removeGroup = false;
				if (File.ReadAllText(Path.Combine(_currentSavePath, "undertale.ini"))
						.IndexOf($"Name=\"{((SaveModel)save.Tag).CharacterName}\"", StringComparison.Ordinal) >= 0)
				{
					if (MessageBox.Show(Resources.Overwrite_confirmation, Resources.Confirmation_title, MessageBoxButtons.YesNo,
						MessageBoxIcon.Warning) == DialogResult.No) return;
				}
				else
				{
					if (MessageBox.Show(Resources.Overwrite_another_character, Resources.Confirmation_title, MessageBoxButtons.YesNo,
							MessageBoxIcon.Warning) == DialogResult.No) return;
					removeGroup = true;
				}
				Directory.Delete(((SaveModel)save.Tag).Path, true);
				savesList.Items.Remove(save);
				if (removeGroup)
				{
					var group = _groups[((SaveModel)save.Tag).CharacterName];
					_groups.Remove(((SaveModel)save.Tag).CharacterName);
					savesList.Groups.Remove(group);
				}
			}
			var currentFiles = Directory.GetFiles(_currentSavePath, "*", SearchOption.TopDirectoryOnly);

			if (Directory.Exists(Path.Combine(_savesPath, saveDir)))
				Directory.Delete(Path.Combine(_savesPath, saveDir), true);
			Directory.CreateDirectory(Path.Combine(_savesPath, saveDir));
			foreach (var file in currentFiles)
			{
				File.Copy(file, Path.Combine(_savesPath, saveDir, Path.GetFileName(file)), true);
			}
			var charRegex = new Regex("Name=\"([A-Za-z]+)\"");

			var model = new SaveModel
			{
				CharacterName = charRegex.Match(File.ReadAllText(Path.Combine(_currentSavePath, "undertale.ini"))).Groups[1].Value,
				Path = Path.Combine(_savesPath, saveDir),
				Name = saveDir,
				Date = DateTime.Now
			};
			File.WriteAllText(Path.Combine(_savesPath, saveDir, $"{saveDir}.uts"), JsonConvert.SerializeObject(model));
			if (!_groups.ContainsKey(model.CharacterName))
			{
				var group = new ListViewGroup(model.CharacterName);
				savesList.Groups.Add(group);
				_groups.Add(model.CharacterName, group);
			}
			savesList.Items.Add(new ListViewItem(_groups[model.CharacterName])
			{
				Text = model.Name,
				SubItems = { model.Date.ToString("dd.MM.yyyy HH:mm") },
				Tag = model
			});
		}

		private void savesList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (savesList.SelectedItems.Count == 0)
			{
				saveNameBox.Text = string.Empty;
				return;
			}
			saveNameBox.Text = savesList.SelectedItems[0].Text;
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			if (savesList.SelectedItems.Count == 0)
				return;
			if (MessageBox.Show(Resources.Delete_dialog, Resources.Confirmation_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				return;
			var save = savesList.SelectedItems[0];
			Directory.Delete(Path.Combine(_savesPath, save.Text), true);
			var group = save.Group;
			savesList.Items.Remove(save);
			if (group.Items.Count == 0)
			{
				savesList.Groups.Remove(group);
				_groups.Remove(group.Header);
			}
		}

		private void newGameButton_Click(object sender, EventArgs e)
		{
			if (!Directory.GetFiles(_currentSavePath, "*", SearchOption.TopDirectoryOnly).Any())
			{
				return;
			}
			if (MessageBox.Show(Resources.New_game_dialog, Resources.Confirmation_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
			foreach (var file in Directory.GetFiles(_currentSavePath, "*", SearchOption.TopDirectoryOnly))
			{
				File.Delete(file);
			}
		}

		private void convertButton_Click(object sender, EventArgs e)
		{
			if (searchOldSavesDialog.ShowDialog() == DialogResult.Cancel) return;
			RenameForm renameForm = null;
			var charRegex = new Regex("Name=\"([A-Za-z]+)\"");
			foreach (var file in Directory.GetFiles(searchOldSavesDialog.SelectedPath, "*.undertale", SearchOption.AllDirectories))
			{
				var saveName = Path.GetFileNameWithoutExtension(file);
				if (CheckExist(saveName))
				{
					if (MessageBox.Show(Resources.Save_exist, Resources.Conflict,
						MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) continue;
					if (renameForm == null) renameForm = new RenameForm();
					var counter = 1;
					var suggestedSaveName = $"{saveName} ({counter})";
					while (File.Exists(Path.Combine(_savesPath, suggestedSaveName, $"{suggestedSaveName}.uts")))
					{
						counter++;
						suggestedSaveName = $"{saveName} ({counter})";
					}
					renameForm.SaveName = suggestedSaveName;
					if (renameForm.ShowDialog(this) == DialogResult.Cancel) continue;
					saveName = renameForm.SaveName;
				}
				var saveDir = Path.Combine(_savesPath, saveName);
				if (Directory.Exists(saveDir))
					Directory.Delete(saveDir, true);
				Directory.CreateDirectory(saveDir);
				foreach (var oldFile in Directory.EnumerateFiles(Path.GetDirectoryName(file)).Where(f => Path.GetExtension(f) != ".undertale"))
				{
					File.Copy(oldFile, Path.Combine(_savesPath, saveName, Path.GetFileName(oldFile)), true);
				}
				var model = new SaveModel
				{
					Path = saveDir,
					CharacterName = charRegex.Match(File.ReadAllText(Path.Combine(_savesPath, saveName, "undertale.ini"))).Groups[1]
						.Value,
					Name = saveName,
					Date = File.GetCreationTime(file)
				};
				File.WriteAllText(Path.Combine(_savesPath, saveName, $"{saveName}.uts"), JsonConvert.SerializeObject(model));
				if (!_groups.ContainsKey(model.CharacterName))
				{
					var group = new ListViewGroup(model.CharacterName);
					savesList.Groups.Add(group);
					_groups.Add(model.CharacterName, group);
				}
				savesList.Items.Add(new ListViewItem(_groups[model.CharacterName])
				{
					Text = model.Name,
					SubItems = { model.Date.ToString("dd.MM.yyyy HH:mm") },
					Tag = model
				});
			}
			renameForm?.Dispose();
		}

		internal bool CheckExist(string saveName)
		{
			return File.Exists(Path.Combine(_savesPath, saveName, $"{saveName}.uts"));
		}
	}
}
