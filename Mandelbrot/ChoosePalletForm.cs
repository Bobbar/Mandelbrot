using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Mandelbrot
{
	public partial class ChoosePalletForm : Form
	{
		public List<Color> PalletSource;

		private readonly List<Color> _defaultPallet;
		private ListViewItem _selectedColor = null;

		public ChoosePalletForm()
		{
			InitializeComponent();
		}

		public ChoosePalletForm(List<Color> colors, List<Color> defaultColors)
		{
			PalletSource = colors;
			InitializeComponent();

			InitButtons();
			_defaultPallet = defaultColors;
		}

		private void InitButtons()
		{
			colorView.Items.Clear();

			for (int i = 0; i < PalletSource.Count; i++)
			{
				var color = PalletSource[i];
				var item = new ListViewItem($"Color {i + 1}") { BackColor = color, ForeColor = Color.LightGray, Tag = i };
				colorView.Items.Add(item);
			}
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void defaultButton_Click(object sender, EventArgs e)
		{
			PalletSource.Clear();
			PalletSource = new List<Color>(_defaultPallet);
			InitButtons();
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			using (var dlg = new ColorDialog())
			{
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					PalletSource.Add(dlg.Color);
					InitButtons();
				}
			}
		}

		private void colorView_ItemActivate(object sender, EventArgs e)
		{
			using (var dlg = new ColorDialog())
			{
				dlg.Color = _selectedColor.BackColor;
				var idx = _selectedColor.Index;

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					PalletSource[idx] = dlg.Color;
					_selectedColor.BackColor = dlg.Color;
				}
			}
		}

		private void colorView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			_selectedColor = e.Item;
		}

		private void moveUpButton_Click(object sender, EventArgs e)
		{
			if (_selectedColor != null)
			{
				var curIdx = _selectedColor.Index;
				var newIdx = curIdx - 1;

				if (newIdx < 0)
					return;

				var tmp = PalletSource[curIdx];
				PalletSource[curIdx] = PalletSource[newIdx];
				PalletSource[newIdx] = tmp;

				_selectedColor = null;
			}

			InitButtons();
		}

		private void moveDownButton_Click(object sender, EventArgs e)
		{
			if (_selectedColor != null)
			{
				var curIdx = _selectedColor.Index;
				var newIdx = curIdx + 1;

				if (newIdx > PalletSource.Count)
					return;

				var tmp = PalletSource[curIdx];
				PalletSource[curIdx] = PalletSource[newIdx];
				PalletSource[newIdx] = tmp;

				_selectedColor = null;
			}

			InitButtons();
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			if (_selectedColor == null) 
				return;

			PalletSource.RemoveAt(_selectedColor.Index);

			InitButtons();
		}
	}
}
