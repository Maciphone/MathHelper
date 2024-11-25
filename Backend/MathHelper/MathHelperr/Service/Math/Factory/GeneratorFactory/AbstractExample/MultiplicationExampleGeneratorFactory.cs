namespace MathHelperr.Service.Math.Factory;

public class MultiplicationExampleGeneratorFactory : GenericExampleGenerator<IMultiplicationExampleGenerator>
{
    public MultiplicationExampleGeneratorFactory(Dictionary<int, IMultiplicationExampleGenerator> generators) : base(generators)
    {
    }
}