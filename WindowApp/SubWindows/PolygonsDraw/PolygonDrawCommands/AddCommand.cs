﻿using Core.Colors;
using Core.Models.Points;
using System.Windows.Controls;
using WindowApp.Extensions;

namespace WindowApp.SubWindows.PolygonsDraw.PolygonDrawCommands;

public class AddCommand : IDrawPolygonCommand
{
    public void Handle(PointsWindowContext context)
    {
        if (context.Points.Count > 2)
        {
            context.Polygons.Add(new Core.Models.Polygons.Polygon(context.Points
                .Select(p => new PointD(p.X / PolygonDraw.DeltaX, (-p.Y) / PolygonDraw.DeltaY))
                .ToList()));

            context.Points.Clear();
            context.Arrows.Clear();
            context.LineBetweenFirstAndEndPoint = new();
            context.PolygonBetweenFirstAndEndPoint = new();

            var name = "Полигон " + context.Polygons.Count.ToString();

            context.PolygonNames.Add(name);
            context.Brush = RandomColor.Get().GetBrushColor();
            context.StackPanel.Children.Add(CreateTextBlock(name));
        }
    }

    private TextBlock CreateTextBlock(string content)
    {
        return new()
        {
            Text = content,
            Margin = new(2),
            Padding = new(10, 4, 10, 4),
        };
    }
}
