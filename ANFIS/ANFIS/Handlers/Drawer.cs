using System;
using System.Drawing;
using System.Windows.Forms;

namespace ANFIS.Handlers
{
	public class Drawer
	{
		private readonly PictureBox _screen;

		public Drawer(PictureBox drawingBoard)
		{
			if (drawingBoard.Image is null)
				throw new ArgumentException("Picture box must have image.");
			_screen = drawingBoard;
		}

		private void Draw(int x, int y)
		{
			if (x >= _screen.Width || x < 0)
				return;
			if (y >= _screen.Height || y < 0)
				return;
			((Bitmap)_screen.Image).SetPixel(x, y, Color.Black);
			_screen.Refresh();
		}

		private void ClearBoard()
		{
			_screen.Image = Properties.Resources.Board;
			_screen.Refresh();
		}
	}
}