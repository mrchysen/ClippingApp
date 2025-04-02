using Core.Models.Points;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WindowApp.Extensions;
using WindowApp.SubWindows.PolygonsDraw.PolygonDrawCommands;

namespace WindowApp.SubWindows.PolygonsDraw;

public partial class PolygonDraw : Window
{
    private PointsWindowContext _context;

    public const int GridXLines = 99;
    public const int DeltaX = 20;
    public const int GridYLines = 59;
    public const int DeltaY = 20;
    public const double ArrowWidth = 3;
    public const double ArrowHeight = 11;
    public const double CircleRadius = 14;

    public PointsWindowContext PolygonDrawContext => _context;

    public PolygonDraw()
    {
        InitializeComponent();
        DrawCanvasGrid();
        _context = new(WindowCanvas, PolygonInfo);

        Background = Resources["BackgroundColor"] as SolidColorBrush;

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

    public static (Point, Point, Point) CreateArrow(Point p1, Point p2)
    {
        var line1 = new Line();
        var line2 = new Line();

        PointD normal = new PointD(-p2.Y + p1.Y, p2.X - p1.X);
        normal = normal.Normilized;

        var vec = new PointD(p1.X - p2.X, p1.Y - p2.Y);
        vec = vec.Normilized;

        var arrowPoint1 = p2.ToPointD() + (CircleRadius + ArrowHeight) * vec + normal * ArrowWidth;
        var arrowPoint2 = p2.ToPointD() + CircleRadius * vec;
        var arrowPoint3 = p2.ToPointD() + (CircleRadius + ArrowHeight) * vec - normal * ArrowWidth;

        return (arrowPoint1.ToWindowPoint(), arrowPoint2.ToWindowPoint(), arrowPoint3.ToWindowPoint());
    }

    public static Polygon CreatePolygon(params Point[] points)
    {
        var polygon = new Polygon();
        polygon.Stroke = Brushes.Black;
        polygon.Fill = Brushes.Black;
        polygon.StrokeThickness = 2;

        PointCollection myPointCollection = new PointCollection();
        foreach (var point in points)
        {
            myPointCollection.Add(point);
        }
        polygon.Points = myPointCollection;

        return polygon;
    }

    private void DrawCanvasGrid()
    {
        var brush = new SolidColorBrush(Color.FromArgb(255, 190, 194, 200));

        for (int i = 1; i < GridXLines + 1; i++)
        {
            WindowCanvas.Children.Add(CreateLine(new Point(i * DeltaX, 0), new Point(i * DeltaX, Height * 3), Resources["FontColor"] as SolidColorBrush));
        }

        for (int i = 1; i < GridYLines + 1; i++)
        {
            WindowCanvas.Children.Add(CreateLine(new Point(0, i * DeltaY), new Point(Width * 2, i * DeltaY), Resources["FontColor"] as SolidColorBrush));
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
            Width = CircleRadius,
            Height = CircleRadius,
            Fill = _context.Brush,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        var ellipseX = mousePosition.X - ellipse.Width / 2;
        var ellipseY = mousePosition.Y - ellipse.Height / 2;

        var point = new Point(mousePosition.X, mousePosition.Y);

        if (_context.Points.Count > 0)
        {
            var line = CreateLine(_context.Points.Last(), point);
            var arrowPoints = CreateArrow(_context.Points.Last(), point);

            WindowCanvas.Children.Add(line);
            var polygon = CreatePolygon(arrowPoints.Item1, arrowPoints.Item2, arrowPoints.Item3);
            WindowCanvas.Children.Add(polygon);
            _context.Arrows.Add(polygon);

            if (_context.Points.Count > 1)
            {
                WindowCanvas.Children.Remove(_context.LineBetweenFirstAndEndPoint);
                WindowCanvas.Children.Remove(_context.PolygonBetweenFirstAndEndPoint);

                _context.LineBetweenFirstAndEndPoint = CreateLine(point, _context.Points.First());
                var firstArrowPoints = CreateArrow(point, _context.Points.First());
                _context.PolygonBetweenFirstAndEndPoint = 
                    CreatePolygon(firstArrowPoints.Item1, firstArrowPoints.Item2, firstArrowPoints.Item3); ;

                WindowCanvas.Children.Add(_context.LineBetweenFirstAndEndPoint);
                WindowCanvas.Children.Add(_context.PolygonBetweenFirstAndEndPoint);
            }
        }

        _context.Points.Add(new Point(mousePosition.X, mousePosition.Y));

        Canvas.SetLeft(ellipse, ellipseX);
        Canvas.SetTop(ellipse, ellipseY);

        _context.Ellipses.Add(ellipse);
        WindowCanvas.Children.Add(ellipse);
    }
}