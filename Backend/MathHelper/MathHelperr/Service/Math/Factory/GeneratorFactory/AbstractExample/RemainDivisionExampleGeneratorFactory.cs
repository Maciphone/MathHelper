namespace MathHelperr.Service.Math.Factory;

public class RemainDivisionExampleGeneratorFactory : GenericExampleGenerator<IRemainDivisonExampleGenerator>
{
    public RemainDivisionExampleGeneratorFactory(Dictionary<int, IRemainDivisonExampleGenerator> generators) : base(generators)
    {
    }
}