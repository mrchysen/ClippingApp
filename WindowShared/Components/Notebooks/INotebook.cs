using ScottPlot;
using System.Windows;

namespace WindowShared.Components.Notebooks;

public interface INotebook
{
    void AddTextBlock(string text, int fontSize = 32);

    void AddButton(string buttonText, Action<object, RoutedEventArgs> buttonAction);

    void AddPlot(Plot plot);
}