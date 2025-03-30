using Application.PolygonPlotting;
using Application.Randoms;
using Core.Clippers.ConvexPolygonClipper;
using Core.Clippers.RourkeChienPolygonClipper;
using Core.Clippers.WeilerAthertonPolygonClipper;
using Core.Intersection;
using Core.Models.Points;
using Core.Models.Polygons;
using Core.PointInclusionAlgorithms;
using Core.PointsOrderers;
using Core.Settings;
using Core.Utils.Equalizers;
using Microsoft.Extensions.DependencyInjection;
using WindowApp.Commands;
using WindowApp.Settings;

namespace WindowApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMainWindowCommands(this IServiceCollection services)
    {
        services.AddScoped<CreateRandomHullCommand>();
        services.AddScoped<CreateRandomNonconvexHullCommand>();
        services.AddScoped<ShowPointDrawWindowCommand>();
        services.AddScoped<ShowPolygonDrawWindowCommand>();
        services.AddScoped<FindIntersectionCommand>();
        services.AddScoped<OpenFolderCommand>();
        services.AddScoped<ClearPlotCommand>();
        services.AddScoped<ClusteringCommand>();

        return services;
    }

    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IAccuracySettings, AccuracySettings>();
        
        services.AddScoped<ConvexPolygonClipper>();
        services.AddScoped<RourkeChienPolygonClipper>();
        services.AddScoped<WeilerAthertonPolygonClipper>();

        services.AddScoped((_) => new Random());
        services.AddScoped<IPolygonGenerator, RandomPolygonGenerator>();
        services.AddScoped<IPolygonArtist, PolygonArtist>();
        services.AddScoped<PointDEqualizer>();
        services.AddScoped<PointPolygonInclusionFinder>();
        services.AddScoped<SegmentAndPolygonIntersector>();
        services.AddScoped<SegmentIntersector>();
        services.AddScoped<PointsOrdererByAngle>();
        services.AddScoped<IEqualizer<PointD>, PointDEqualizer>();
        services.AddScoped<IEqualizer<double>, DoubleEqualizer>();

        return services;
    }
}
