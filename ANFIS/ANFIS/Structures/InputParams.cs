using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ANFIS.Handlers.IO;
using ANFIS.Handlers.Mathematics;

namespace ANFIS.Structures
{
	public static class InputParams
	{
		public static void FillTrainSetChoices(ComboBox loadTrainSetBox, ComboBox functionsBox)
		{
			FileHandler fileHandler = new FileHandler();
			List<string> files = fileHandler.FileList();
			FillComboBox(loadTrainSetBox, files);
			FillComboBox(functionsBox, Functions.GetFunctions());
		}

		public static void SetClasses(TableLayoutPanel panel, int numSymbols, int numSamples)
		{
			int columnCount = numSymbols > 4 ? 5 : numSymbols;
			int rowCount = (numSymbols - 1) / 5 + 1;

			panel.ColumnCount = columnCount * 2;
			panel.RowCount = rowCount;

			panel.ColumnStyles.Clear();
			panel.RowStyles.Clear();
			panel.Controls.Clear();

			for (var i = 0; i < columnCount; i++)
			{
				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount * 0.7f));
				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount * 0.3f));
			}
			for (var i = 0; i < rowCount; i++)
			{
				panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rowCount));
			}

			for (var i = 0; i < numSymbols; i++)
			{
				var button = new Button
				{
					Text = (i + 1).ToString(),
					Name = $"b_{i + 1}",
					Dock = DockStyle.Fill
				};

				var label = new Label
				{
					Text = numSamples.ToString(),
					Name = $"l_{i + 1}",
					AutoSize = false,
					TextAlign = ContentAlignment.MiddleCenter,
					Dock = DockStyle.Fill
				};

				panel.Controls.Add(button);
				panel.Controls.Add(label);
			}
		}

		private static void FillComboBox<T>(ComboBox comboBox, List<T> items)
		{
			comboBox.Items.Clear();
			foreach (var item in items)
			{
				comboBox.Items.Add(item.ToString());
			}
			if (comboBox.Items.Count > 0)
				comboBox.SelectedItem = comboBox.Items[0];
		}
	}
}