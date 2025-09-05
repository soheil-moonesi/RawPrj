using CompositionRoot.DemoFeature;
using Microsoft.Extensions.DependencyInjection;
using static CompositionRoot.DemoFeature.MyFeature;

public static class DemoFeatureExtentions
{
//use "this" for create extension method
    public static IServiceCollection AddDemoFeature(this IServiceCollection services)
    {
     //By convention, return the IServiceCollection to allow method chaining.
        return services.
        AddSingleton<MyFeature>().
        AddSingleton<IMyFeatureDependency, MyFeatureDependency>();
     } 

    
}