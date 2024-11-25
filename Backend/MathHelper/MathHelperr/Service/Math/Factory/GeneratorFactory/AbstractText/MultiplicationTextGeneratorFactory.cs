namespace MathHelperr.Service.Math.Factory.GeneratorFactory.AbstractText;

public class MultiplicationTextGeneratorFactory : GenericTextGenerator<IMultiplicationTextGenerator>
{
    public MultiplicationTextGeneratorFactory(Dictionary<int, IMultiplicationTextGenerator> generators) : base(generators)
    {
    }
}