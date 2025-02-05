using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WindowApp.SubWindows.PolygonsDraw.PolygonDrawCommands;

namespace WindowApp.SubWindows.PolygonsDraw;

public partial class PolygonDraw : Window
{
    private PolygonDrawContext _context;
    public const int GridXLines = 99;
    public const int DeltaX = 20;
    public const int GridYLines = 59;
    public const int DeltaY = 20;

    public PolygonDrawContext PolygonDrawContext => _context;

    public PolygonDraw()
    {
        InitializeComponent();
        DrawCanvasGrid();
        _context = new(WindowCanvas, PolygonInfo);

        AddButton.Click += (o, e) => new AddCommand().Handle(_context);
        RevertLastAddButton.Click += (o, e) => new RevertLastAddCommand().Handle(_context);
        ClearPolygonButton.Click += (o, e) => new ClearPolygonCommand().Handle(_context);
    }

    public static Line CreateLine(Point p1, Point p2, Brush? brush = null)
    {
        var line = new Line();
        line.X1 = p1.X;
        line.Y1 = p1.Y;

        line.X2 = p2.X;
        line.Y2 = p2.Y;

        line.Stroke = brush ?? Brushes.Black;
        line.StrokeThickness = 1;
        line.SnapsToDevicePixels = true;
        line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

        return line;
    }

    private void DrawCanvasGrid()
    {
        var brush = new SolidColorBrush(Color.FromArgb(255, 190, 194, 200));

        for (int i = 1; i < GridXLines + 1; i++)
        {
            WindowCanvas.Children.Add(CreateLine(new Point(i * DeltaX, 0), new Point(i * DeltaX, Height * 3), brush));
        }

        for (int i = 1; i < GridYLines + 1; i++)
        {
            WindowCanvas.Children.Add(CreateLine(new Point(0, i * DeltaY), new Point(Width * 2, i * DeltaY), brush));
        }
    }

    private void WindowCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            CreatePolygonPoint(sender, e);
        }
    }

    private void CreatePolygonPoint(object sender, MouseButtonEventArgs e)
    {
        Point mousePosition = e.GetPosition(WindowCanvas);

        Ellipse ellipse = new Ellipse
        {
            Width = 10,
            Height = 10,
            Fill = _context.Brush,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        var ellipseX = mousePosition.X - ellipse.Width / 2;
        var ellipseY = mousePosition.Y - ellipse.Height / 2;

        var point = new Point(mousePosition.X, mousePosition.Y);

        if (_context.Points.Count > 0)
        {
            var line = CreateLine(point, _context.Points.Last());

            WindowCanvas.Children.Add(line);

            if (_context.Points.Count > 1)
            {
                WindowCanvas.Children.Remove(_context.LineBetwenFirstAndEndPoint);

                _context.LineBetwenFirstAndEndPoint = CreateLine(point, _context.Points.First());

                WindowCanvas.Children.Add(_context.LineBetwenFirstAndEndPoint);
            }
        }

        _context.Points.Add(new Point(mousePosition.X, mousePosition.Y));

        Canvas.SetLeft(ellipse, ellipseX);
        Canvas.SetTop(ellipse, ellipseY);

        _context.Ellipses.Add(ellipse);
        WindowCanvas.Children.Add(ellipse);
    }
}