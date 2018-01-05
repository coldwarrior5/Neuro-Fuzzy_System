using System;
using System.Drawing;
using System.Windows.Forms;
using ANFIS.Handlers.Error;
using ANFIS.Handlers.GUI;

namespace ANFIS
{
	public partial class Start : Form
	{
		private readonly Mover _screenMover;

		public Start()
		{
			InitializeComponent();
			_screenMover = new Mover();
		}

		private void ButtonStart_Click(object sender, EventArgs e)
		{
			Main mainForm = new Main();
			Hide();
			mainForm.Show(this);
		}

		private void ButtonStart_MouseEnter(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.ButtonHighlighted;
		}

		private void ButtonStart_MouseLeave(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.Button;
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			ErrorHandler.TerminateExecution(ErrorCode.UserTermination);
		}

		private void ButtonExit_MouseEnter(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.ExitHighlighted;
		}

		private void ButtonExit_MouseLeave(object sender, EventArgs e)
		{
			if (sender is Button btn) btn.BackgroundImage = Properties.Resources.Exit;
		}

		private void Titlebar_MouseDown(object sender, MouseEventArgs e)
		{
			_screenMover.MouseDown(e.Location);
		}

		private void Titlebar_MouseMove(object sender, MouseEventArgs e)
		{
			var moved = _screenMover.MouseMove(e.Location, Location, out Point newLocation);
			if (moved)
				Location = newLocation;
			Update();
		}

		private void Titlebar_MouseUp(object sender, MouseEventArgs e)
		{
			_screenMover.MouseUp();
		}
	}
}
