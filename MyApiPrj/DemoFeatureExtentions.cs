using CompositionRoot.DemoFeature;
using Microsoft.Extensions.DependencyInjection;
using static CompositionRoot.DemoFeature.MyFeature;

public static class DemoFeatureExtentions
{

    public static IServiceCollection AddDemoFeature(this IServiceCollection services)
    {

        return services.AddSingleton<MyFeature>().
        AddSingleton<IMyFeatureDependency, MyFeatureDependency>();

     } 

    
}