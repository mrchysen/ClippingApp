using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WindowApp.Components.ButtonsPopupComponent;

public partial class ButtonsPopup : Popup
{
    private Dictionary<int, Action?> _actions = new();
    private int _count => _actions.Count;

    public static readonly DependencyProperty OnFocusBackgroundProperty = DependencyProperty.Register(
        "OnFocusBackground",
        typeof(Brush),
        typeof(MainWindow)
        );
    public Brush OnFocusBackground
    {
        get => (Brush)GetValue(OnFocusBackgroundProperty);
        set => SetValue(OnFocusBackgroundProperty, value);
    }
    public static readonly DependencyProperty LostFocusBackgroundProperty = DependencyProperty.Register(
        "LostFocusBackground",
        typeof(Brush),
        typeof(MainWindow)
        );
    public Brush LostFocusBackground
    {
        get => (Brush)GetValue(LostFocusBackgroundProperty);
        set => SetValue(LostFocusBackgroundProperty, value);
    }

    public Dictionary<int, Action?> Actions
    {
        get
        {
            return _actions;
        }
        set
        {
            _actions = value;
        }
    }

    public ButtonsPopup()
    {
        InitializeComponent();

        Opened += (o, e) => SetFirstFocus();

        LostFocusBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFDDDDDD")!;
        OnFocusBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#D6A99A")!;
    }

    public void Show()
    {
        IsOpen = true;
        SetFirstFocus();
    }

    public void AddButton(string buttonText, Action? action)
    {
        var button = new Button()
        {
            Width = 200,
            Height = 50,
            Content = buttonText,
            Tag = (_count + 1).ToString()
        };

        button.GotFocus += ButtonGotFocus;
        button.LostFocus += ButtonLostFocus;
        button.Click += ButtonClick;

        _actions.Add(_count + 1, action);

        Box.Children.Add(button);
    }

    private void SetFirstFocus()
    {
        if (_count <= 0)
            return;

        Box.Children[0].Focus();
    }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        IsOpen = false;

        if (sender is Button button)
        {
            var id = int.Parse((string)button.Tag);

            if (!_actions.ContainsKey(id))
                return;

            _actions[id]?.Invoke();
        }
    }

    private void ButtonGotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            button.Background = OnFocusBackground;
        }
    }

    private void ButtonLostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            button.Background = LostFocusBackground;
        }
    }
}
