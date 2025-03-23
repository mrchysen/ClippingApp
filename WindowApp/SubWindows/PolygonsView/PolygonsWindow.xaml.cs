using Core.Clustering;
using Core.Colors;
using Core.Models.Points;
using Core.Models.Polygons;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WindowApp.SubWindows.Polygons;

public partial class PolygonsWindow : Window
{
    private ObservableCollection<PolygonListViewModel> _listBoxPolygons { get; set; } = [];

    public PolygonsWindow(List<Polygon> polygons, List<PointD> points, List<Cluster> clusters)
    {
        InitializeComponent();

        PolygonListBox.SelectionChanged += (o, e) =>
        {
            if (o is null || o is not ListBox)
                return;

            ListBox listBox = (ListBox)o;

            if (listBox.SelectedItem != null)
            {
                var selectedItem = (PolygonListViewModel)listBox.SelectedItem;

                CurrentPolygonName.Text = selectedItem.Name;
                CurrentPolygonDataGrid.ItemsSource = CreatePolygonDataList(selectedItem.Points);
            }
        };

        List<PolygonListViewModel> polygonListViewModels = new();

        if (polygons.Count > 0)
        {
            var currentPolygonListViewModels = polygons.Select(el =>
                PolygonListViewModel.Create(el.Points, el.Color, "Полигон "))
                .ToList();

            for (int i = 0; i < currentPolygonListViewModels.Count; i++)
            {
                currentPolygonListViewModels[i].Name += (i + 1).ToString();
            }

            polygonListViewModels.AddRange(currentPolygonListViewModels);
        }

        if (points.Count > 0)
        {
            var pointsCloud = PolygonListViewModel
                .Create(points, RandomColor.Get(), "Облако точек");

            polygonListViewModels.Add(pointsCloud);
        }

        if (clusters.Count > 0)
        {
            foreach (var cluster in clusters)
            {
                var color = RandomColor.Get();

                polygonListViewModels.Add(PolygonListViewModel.Create(cluster.Points, color, "Кластер "));
                polygonListViewModels.Add(PolygonListViewModel.Create([cluster.Centroid], color, "Центроида"));
            }
        }

        PolygonListBox.ItemsSource = polygonListViewModels;
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        Visibility = Visibility.Hidden;
    }

    private List<PolygonPointModel> CreatePolygonDataList(List<PointD> list)
    {
        var polygonDataList = list.Select(p => new PolygonPointModel()
        {
            X = p.X,
            Y = p.Y
        }).ToList();

        for (int i = 0; i < polygonDataList.Count; i++)
        {
            polygonDataList[i].Number = i + 1;
        }

        return polygonDataList;
    }
}
