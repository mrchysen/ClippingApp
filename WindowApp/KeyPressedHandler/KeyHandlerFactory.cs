using System.Windows.Input;
using WindowApp.KeyPressedHandler.Handlers;

namespace WindowApp.KeyPressedHandler;

public class KeyHandlerFactory
{
    public static KeyHandler? GetHendler(Key key)
    {
        return key switch
        {
            Key.N => new KeyNHandler(),
            Key.S => new KeySHandler(),
            Key.F => new KeyFHandler(),
            Key.I => new KeyIHandler(),
            _ => null
        };
    }
}
