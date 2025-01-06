namespace MathHelperr.Service.Math.Factory.GeneratorFactory.AbstractText;

public class DivisionTextGeneratorFactory : GenericTextGenerator<IDivisionTextGenerator>
{
    public DivisionTextGeneratorFactory(Dictionary<int, IDivisionTextGenerator> generators) : base(generators)
    {
    }
}