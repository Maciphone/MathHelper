namespace MathHelperr.Service.Math.Factory.GeneratorFactory.AbstractText;

public class RemainDivisonTextGeneratorFactory : GenericTextGenerator<IRemainDivisionTextGenerator>
{
    public RemainDivisonTextGeneratorFactory(Dictionary<int, IRemainDivisionTextGenerator> generators) : base(generators)
    {
    }
}