﻿using Core.Clustering.Metrics;
using Core.Models.Points;
using Core.Models.Points.Generator;
using System.Drawing;

namespace Core.Clustering;

public class KMeansAlgorithm : IClusteringAlgorithm
{
    private readonly IList<PointD> _points;
    private readonly IMetric<double, PointD> _metric;
    private readonly int _clusterCount;
    
    private IPointGenerator _pointGenerator;
    private Cluster[] _clusters;

    public KMeansAlgorithm(
        IMetric<double, PointD> metric,
        IList<PointD> points,
        int clusterCount)
    {
        _clusterCount = clusterCount;
        _metric = metric;
        _points = points;
        _clusters = new Cluster[clusterCount];
        _pointGenerator = CreatePointGenerator();  
    }


    public List<Cluster> CreateClusters()
    {
        InitializeCentroids();

        // Делаем до тех пор пока центроиды не перестанут менять местоположение
        while (AreAllCentroidNotChanged())
        {
            foreach(var point in _points)
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


        }

        return _clusters.ToList();
    }

    private bool AreAllCentroidNotChanged()
        => _clusters.All(c => !c.IsCentroidChanged());
    

    private void InitializeCentroids()
    {
        foreach (var cluster in _clusters)
        {
            cluster.Centroid = _pointGenerator.GeneratePoint();
        }
    }

    private PointGenerator CreatePointGenerator()
    {
        var highPoint = _points.Aggregate(PointDHelper.MaxPointD); 
        var lowPoint = _points.Aggregate(PointDHelper.MinPointD);

        return new PointGenerator(new Rectangle()
        {
            Width = (int)(highPoint.X - lowPoint.X),
            Height = (int)(highPoint.Y - highPoint.Y),
            X = (int)lowPoint.X,
            Y = (int)lowPoint.Y
        });
    }
}
