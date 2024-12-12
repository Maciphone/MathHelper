namespace MathHelperr.Service.Math.Factory;

public interface IMathGeneratorFactory

{
    T GetGenerator<T>( int level) where T : class;
    
}