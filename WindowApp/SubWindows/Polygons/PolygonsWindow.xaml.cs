using Core.Models.Points;
using Core.Models.Polygons;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WindowApp.SubWindows.Polygons;

public partial class PolygonsWindow : Window
{
    private List<Polygon> _polygons;
    private ObservableCollection<PolygonListViewModel> _listBoxPolygons { get; set; } = [];

    public PolygonsWindow(List<Polygon> polygons)
    {
        InitializeComponent();

        _polygons = polygons;

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

        if (polygons.Count > 0)
        {
            CurrentPolygonDataGrid.ItemsSource = CreatePolygonDataList(_polygons[0].Points);
            
            var polygonListViewModels = _polygons.Select(el => PolygonListViewModel.Create(el, "Полигон ")).ToList();
            
            for (int i = 0; i < polygonListViewModels.Count; i++)
            {
                polygonListViewModels[i].Name += (i + 1).ToString();
            }

            CurrentPolygonName.Text = "Полигон 1";
            PolygonListBox.ItemsSource = polygonListViewModels;
        }
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
