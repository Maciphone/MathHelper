namespace MathHelperr.Service.Math.Factory.GeneratorFactory.AbstractText;

public class AlgebraTextGeneratorFactory : GenericTextGenerator<IAlgebraTextGenerator>
{
    public AlgebraTextGeneratorFactory(Dictionary<int, IAlgebraTextGenerator> generators) : base(generators)
    {
    }
}