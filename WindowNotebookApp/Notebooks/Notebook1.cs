using NotebookCompilator;
using ScottPlot;
using WindowShared.Components.Notebooks;

GeneratorMarkup.StartNotebook();

var virtualNotebook = new VirtualNotebook();

virtualNotebook.AddTextBlock("Пример ноутбука");

var plot = new Plot();

plot.Add.ScatterLine((int[])[1, 2, 3], [10, 5, 9]);

virtualNotebook.AddTextBlock("График", 24);
virtualNotebook.AddPlot(plot);

GeneratorMarkup.EndNotebook();