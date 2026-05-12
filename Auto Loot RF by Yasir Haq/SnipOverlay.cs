using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Auto_Loot_RF_by_Yasir_Haq
{
    internal sealed class SnipOverlay : Form
    {
        private readonly Bitmap _screen;
        private Point     _start;
        private Rectangle _sel;
        private bool      _dragging;

        public Bitmap Result { get; private set; }

        public SnipOverlay()
        {
            var b = Screen.PrimaryScreen.Bounds;
            _screen = new Bitmap(b.Width, b.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(_screen))
                g.CopyFromScreen(b.Location, Point.Empty, b.Size);

            FormBorderStyle = FormBorderStyle.None;
            Bounds          = b;
            TopMost         = true;
            Cursor          = Cursors.Cross;
            DoubleBuffered  = true;
            ShowInTaskbar   = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _start    = e.Location;
            _sel      = Rectangle.Empty;
            _dragging = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_dragging) return;
            int x = Math.Min(_start.X, e.X);
            int y = Math.Min(_start.Y, e.Y);
            _sel = new Rectangle(x, y, Math.Abs(e.X - _start.X), Math.Abs(e.Y - _start.Y));
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!_dragging) return;
            _dragging = false;
            if (_sel.Width > 4 && _sel.Height > 4)
            {
                Result = new Bitmap(_sel.Width, _sel.Height);
                using (var g = Graphics.FromImage(Result))
                    g.DrawImage(_screen, new Rectangle(0, 0, _sel.Width, _sel.Height), _sel, GraphicsUnit.Pixel);
                DialogResult = DialogResult.OK;
            }
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(_screen, 0, 0);
            using (var dim = new SolidBrush(Color.FromArgb(130, 0, 0, 0)))
                e.Graphics.FillRectangle(dim, ClientRectangle);
            if (_sel.Width > 0 && _sel.Height > 0)
            {
                e.Graphics.DrawImage(_screen, _sel, _sel, GraphicsUnit.Pixel);
                using (var pen = new Pen(Color.FromArgb(210, 155, 0), 2))
                    e.Graphics.DrawRectangle(pen, _sel);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { DialogResult = DialogResult.Cancel; Close(); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { if (_screen != null) _screen.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
