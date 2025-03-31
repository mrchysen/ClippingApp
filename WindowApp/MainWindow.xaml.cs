﻿using Core.Clippers.ConvexPolygonClipper;
using Core.Models.Polygons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WindowApp.Commands;
using WindowApp.Components.Plates;
using WindowApp.Infrastructure;
using WindowApp.Settings;

namespace WindowApp;

public partial class MainWindow : Window
{
    private PlotManager _plotManager;

    public MainWindow(IServiceProvider serviceProvider, IOptions<FilesPathSettings> filePathSettingsOptions)
    {
        InitializeComponent();

        _plotManager = new(Plot);
        
        InitializePlateDataContext();
        InitializeMenuItemClicks();
        EnsureDataFoldersExistence(filePathSettingsOptions.Value);
    }

    private void InitializeMenuItemClicks()
    {
        SaveFileMenuItem.Click += (o, e) => new SaveFileCommand(_plotManager).Handle();
        OpenFileMenuItem.Click += (o, e) => new OpenFileCommand(_plotManager).Handle();
        OpenFolderMenuItem.Click += (o, e) => new OpenFolderCommand().Handle();

        ClearPlotMenuItem.Click += (o, e) => new ClearPlotCommand(_plotManager).Handle();

        ShowObjectsInfoMenuItem.Click += (o, e) => new ShowObjectsInfoCommand(_plotManager).Handle();
    }

    private void InitializePlateDataContext()
    {
        PolygonsPlate.PlotManager = _plotManager;
        HullsPlate.PlotManager = _plotManager;
        ClusteringPlate.PlotManager = _plotManager;
    }

    private void EnsureDataFoldersExistence(FilesPathSettings filePathSettings) 
        => Directory.CreateDirectory(filePathSettings.GetPolygonDataFolderPath);
   private void Window_Closed(object sender, EventArgs e) => App.Current.Shutdown();
}