using Core.Clustering.Metrics;
using Core.Models.Points;
using Core.Models.Points.Generator;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace Core.Clustering;

public class KMeansAlgorithm : IClusteringAlgorithm
{
    private readonly IList<PointD> _points;
    private readonly IMetric<double, PointD> _metric;
    private readonly int _clusterCount;
    
    private IPointGenerator _pointGenerator;
    private Cluster[] _clusters;

    private int _iterations = 0;
    private const int _iterationsLimit = 100_000;
    private bool _iterationLimitEnable = false;

    public KMeansAlgorithm(
        IMetric<double, PointD> metric,
        IList<PointD> points,
        int clusterCount, 
        bool iterationLimitEnable = false)
    {
        _clusterCount = clusterCount;
        _metric = metric;
        _points = points;
        _clusters = new Cluster[clusterCount];
        _pointGenerator = CreatePointGenerator();
        _iterationLimitEnable = iterationLimitEnable;
    }

    public List<Cluster> CreateClusters()
    {
        InitializeCentroids();

        // Делаем до тех пор пока центроиды не перестанут менять местоположение
        while (AreAllCentroidNotChanged())
        {
            if (_iterationLimitEnable && _iterations > _iterationsLimit)
                break;

            foreach (var cluster in _clusters)
            {
                cluster.Points.Clear();
            }

            // Смотрим какие точки принадлежат центроидам
            foreach (var point in _points)
            {
                Cluster nearCluster = _clusters[0];
                double distance = _metric.Compute(nearCluster.Centroid, point);

                foreach (var cluster in _clusters)
                {
                    if(distance > _metric.Compute(cluster.Centroid, point))
                    {
                        distance = _metric.Compute(cluster.Centroid, point);
                        nearCluster = cluster;
                    }
                }

                nearCluster.Points.Add(point);
            }

            // Пересчитываем центроиды
            foreach (var cluster in _clusters)
            {
                cluster.ComputeNewCentroid();
            }

            _iterations++;
        }

        return _clusters.ToList();
    }

    // Все центроиды сильно не сместились
    private bool AreAllCentroidNotChanged()
        => _clusters.All(c => !c.IsCentroidChanged());
    
    private void InitializeCentroids()
    {
        for (int i = 0; i < _clusters.Length; i++)
        {
            _clusters[i] = new Cluster();
            _clusters[i].Centroid = _pointGenerator.GeneratePoint();
        }
    }

    private PointGenerator CreatePointGenerator()
    {
        var leftX = _points.Min(x => x.X);
        var rightX = _points.Max(x => x.X);

        var leftY = _points.Min(x => x.Y);
        var rightY = _points.Max(x => x.Y);

        return new PointGenerator(new Rectangle()
        {
            Width = (int)(rightX - leftX),
            Height = (int)(rightY - leftY),
            X = (int)leftX,
            Y = (int)leftY
        });
    }
}
