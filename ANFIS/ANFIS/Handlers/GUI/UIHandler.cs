using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using ANFIS.ANN;
using ANFIS.Handlers.Mathematics;
using Point = System.Drawing.Point;

namespace ANFIS.Handlers.GUI
{
	public static class UiHandler
	{
		public static double TextBoxLeft(object sender, EventArgs e, double valueToBeChanged, double min, double max)
		{
			if (!(sender is TextBox t)) return valueToBeChanged;

			double.TryParse(t.Text, out double value);
			value = MathHandler.Clamp(value, min, max);
			t.Text = value.ToString(CultureInfo.InvariantCulture);
			return value;
		}

		public static double TextBoxChanged(object sender, KeyEventArgs e, double valueToBeChanged, double min, double max)
		{
			if (!(sender is TextBox t)) return valueToBeChanged;

			switch (e.KeyCode)
			{
				case Keys.Enter:
					double.TryParse(t.Text, out double value);
					value = MathHandler.Clamp(value, min, max);
					t.Text = value.ToString(CultureInfo.InvariantCulture);
					return value;
				case Keys.Escape:
					t.Text = valueToBeChanged.ToString(CultureInfo.InvariantCulture);
					break;
			}
			return valueToBeChanged;
		}

		public static int TextBoxLeft(object sender, EventArgs e, int valueToBeChanged, int min, int max)
		{
			if (!(sender is TextBox t)) return valueToBeChanged;

			int.TryParse(t.Text, out int value);
			value = MathHandler.Clamp(value, min, max);
			t.Text = value.ToString(CultureInfo.InvariantCulture);
			return value;
		}

		public static int TextBoxChanged(object sender, KeyEventArgs e, int valueToBeChanged, int min, int max)
		{
			if (!(sender is TextBox t)) return valueToBeChanged;

			switch (e.KeyCode)
			{
				case Keys.Enter:
					int.TryParse(t.Text, out int value);
					value = MathHandler.Clamp(value, min, max);
					t.Text = value.ToString(CultureInfo.InvariantCulture);
					return value;
				case Keys.Escape:
					t.Text = valueToBeChanged.ToString(CultureInfo.InvariantCulture);
					break;
			}
			return valueToBeChanged;
		}

		public static void PanelVisible(Panel visible, List<Panel> panels)
		{
			foreach (Panel panel in panels)
			{
				panel.Visible = panel.Equals(visible);
			}
		}

		public static void SetSlider(Panel panelSlider, int top, int height)
		{
			panelSlider.Top = top;
			panelSlider.Height = height;
		}

		public static void DecrementValue(TableLayoutPanel panel, int which)
		{
			int.TryParse(panel.Controls[which * 2 + 1].Text, out int amount);
			panel.Controls[which * 2 + 1].Text = (--amount).ToString();
			if (amount == 0)
				panel.Controls[which * 2].Enabled = false;
		}

		public static bool AllButtonsDisabled(TableLayoutPanel panel)
		{
			for (int i = 0; i < panel.Controls.Count; i++)
			{
				if (!(panel.Controls[i] is Button)) continue;
				if (panel.Controls[i].Enabled)
					return false;
			}
			return true;
		}
	}

	public class Mover
	{
		private bool _mouseDown;
		private Point _lastLocation;

		public void MouseDown(Point location)
		{
			_mouseDown = true;
			_lastLocation = location;
		}

		public bool MouseMove(Point mouseLocation, Point location, out Point newLocation)
		{
			newLocation = new Point();
			if (!_mouseDown) return false;
			newLocation = new Point(
				location.X - _lastLocation.X + mouseLocation.X, location.Y - _lastLocation.Y + mouseLocation.Y);
			return true;
		}

		public void MouseUp()
		{
			_mouseDown = false;
		}
	}

	public class Worker
	{
		private readonly NeuralNetwork _ann;
		private readonly BackgroundWorker _worker;
		private readonly Button _stop;
		private readonly Button _result;

		public Worker(NeuralNetwork ann, Button stop, Button result)
		{
			_ann = ann;
			_stop = stop;
			_result = result;
			_worker = new BackgroundWorker();
		}

		public void Start()
		{
			_worker.DoWork += _ann.Train;
			_worker.RunWorkerCompleted += worker_Finished;
			_worker.RunWorkerAsync();
		}

		void worker_Finished(object sender, RunWorkerCompletedEventArgs e)
		{
			_stop.Visible = false;
			_result.Visible = true;
		}
	}
}