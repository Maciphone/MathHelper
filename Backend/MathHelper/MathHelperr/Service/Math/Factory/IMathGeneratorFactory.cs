namespace MathHelperr.Service.Math.Factory;

public interface IMathGeneratorFactory

{
    T GetGenerator<T>(Type generatorType, int level) where T : class;
    
}