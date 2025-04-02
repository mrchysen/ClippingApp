using Core.Models.Points;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WindowApp.SubWindows.PointsDraw.PointsDrawCommands;

namespace WindowApp.SubWindows.PointsDraw;

public partial class PointsWindow : Window
{
    private PointsWindowViewModel _context;

    public const int DeltaX = 20;
    public const int DeltaY = 20;

    public const int ElipseSize = 10;

    public PointsWindow()
    {
        InitializeComponent();

        Background = Resources["BackgroundColor"] as SolidColorBrush;

        _context = new(WindowCanvas);
        DataContext = _context;

        ClearPointsButton.Click += (o, e) => new RemoveAllPointsCommand().Handle(_context);
        RevertLastAddButton.Click += (o, e) => new RevertLastAddCommand().Handle(_context);
        GeneratePointsButton.Click += (o, e) => new GenerateRandomPointsCommand().Handle(_context);
    }

    public PointsWindowViewModel Context => _context;

    private void WindowCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            CreatePoint(sender, e);
        }
    }

    private void CreatePoint(object sender, MouseButtonEventArgs e)
    {
        Point mousePosition = e.GetPosition(WindowCanvas);

        Ellipse ellipse = CreateEllipse();

        var ellipseX = mousePosition.X - ellipse.Width / 2;
        var ellipseY = mousePosition.Y - ellipse.Height / 2;

        _context.Points.Add(new(mousePosition.X / DeltaX, - mousePosition.Y / DeltaY));
        _context.Ellipses.Add(ellipse);

        Canvas.SetLeft(ellipse, ellipseX);
        Canvas.SetTop(ellipse, ellipseY);

        WindowCanvas.Children.Add(ellipse);
    }

    public static Ellipse CreateEllipse() => new Ellipse
    {
        Width = ElipseSize,
        Height = ElipseSize,
        Fill = new SolidColorBrush(Colors.Aqua),
        Stroke = Brushes.Black,
        StrokeThickness = 2
    };
}
