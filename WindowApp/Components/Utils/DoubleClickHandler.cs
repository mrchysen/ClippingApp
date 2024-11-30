using System.Windows.Input;

namespace WindowApp.Components.Utils;

public class DoubleClickHandler<T>
{
    private Action<T>? _action;
    private T? _value;
    private DoubleClick _doubleClick = DoubleClick.None;
    private HashSet<Key> _keys;
    private DateTime _lastClickTime;
    private TimeSpan _timeOffset;

    public DoubleClickHandler(
        Action<T>? action,
        T? value,
        HashSet<Key> keys,
        TimeSpan timeOffset)
    {
        _action = action;
        _value = value;
        _keys = keys;
        _timeOffset = timeOffset;
    }

    public void Click(KeyEventArgs e)
    {
        if (_doubleClick == DoubleClick.None && _keys.Contains(e.Key))
        {
            _lastClickTime = DateTime.Now;
            _doubleClick = DoubleClick.FirstClick;
        }
        else if (_doubleClick == DoubleClick.FirstClick && _keys.Contains(e.Key))
        {
            if (DateTime.Now - _lastClickTime < _timeOffset)
            {
                _action?.Invoke(_value!);
            }

            _doubleClick = DoubleClick.None;
        }
    }
}

public enum DoubleClick
{
    None,       // Не было нажатия
    FirstClick, // Было одно нажатие
}