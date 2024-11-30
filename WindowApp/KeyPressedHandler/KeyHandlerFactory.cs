using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using WindowApp.KeyPressedHandler.Handlers;

namespace WindowApp.KeyPressedHandler;

public class KeyHandlerFactory
{
    public static KeyHandler? GetHandler(KeyEventArgs e, IServiceProvider serviceProvider)
    {
        return e.Key switch
        {
            Key.N => serviceProvider.GetService<KeyNHandler>(),
            Key.S => serviceProvider.GetService<KeySHandler>(),
            Key.F => serviceProvider.GetService<KeyFHandler>(),
            Key.I => serviceProvider.GetService<KeyIHandler>(),
            Key.B => serviceProvider.GetService<KeyBHandler>(),
            _ => null
        };
    }
}
