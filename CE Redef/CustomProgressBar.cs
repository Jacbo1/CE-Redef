// Source: https://stackoverflow.com/a/778866

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

[ToolboxItem(true)]
public class CustomProgressBar : ProgressBar
{
    private Color barColor = Color.FromArgb(0, 120, 215);
    private Brush barBrush;

    public CustomProgressBar()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
        DoubleBuffered = true;
        barBrush = new SolidBrush(barColor);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        // Draw bar
        Rectangle rec = e.ClipRectangle;

        rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
        if (ProgressBarRenderer.IsSupported)
            ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
        rec.Height -= 4;
        e.Graphics.FillRectangle(barBrush, 2, 2, rec.Width, rec.Height);

        // Draw text
        using (Font f = new Font(FontFamily.GenericSansSerif, 10))
        {
            double percent = (double)this.Value / this.Maximum;
            double barWidth = Width * percent;
            string text = (int)(percent * 100) + "%";
            SizeF len = e.Graphics.MeasureString(text, f);
            float x = (Width - len.Width) * 0.5f,
                y = (Height - len.Height) * 0.5f;
            foreach (char c in text)
            {
                string s = c.ToString();
                e.Graphics.DrawString(s, f, x < barWidth ? Brushes.White : barBrush, new PointF(x, y));
                x += 7;
            }
        }
    }
}