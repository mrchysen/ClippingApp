using Application.PolygonPlotting;
using Application.Randoms;
using Core.Clippers;
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
using WindowApp.KeyPressedHandler.Handlers;
using WindowApp.Settings;

namespace WindowApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKeyHandlers(this IServiceCollection services)
    {
        services.AddScoped<KeyBHandler>();
        services.AddScoped<KeyNHandler>();
        services.AddScoped<KeySHandler>();
        services.AddScoped<KeyFHandler>();
        services.AddScoped<KeyIHandler>();
        services.AddScoped<KeyAHandler>();

        return services;
    }

    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IAccuracySettings, AccuracySettings>();
        
        services.AddScoped<ConvexPolygonClipper>();
        services.AddScoped<RourkeChienPolygonClipper>();
        services.AddScoped<WeilerAthertonPolygonClipper>();

        services.AddScoped((_) => new Random());
        services.AddScoped<RandomPolygon>();
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
