namespace MathHelperr.Service.Math.Factory;

public class DivisionExampleGeneratorFactory : GenericExampleGenerator<IDivisionExampleGenerator>
{
    public DivisionExampleGeneratorFactory(Dictionary<int, IDivisionExampleGenerator> generators) : base(generators)
    {
    }
}