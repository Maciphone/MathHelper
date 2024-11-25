using System.Linq;

namespace MathHelperr.Service.Math.Factory;


public class AlgebraExampleGeneratorFactory : GenericExampleGenerator<IAlgebraExampleGenerator>
{
    public AlgebraExampleGeneratorFactory(Dictionary<int, IAlgebraExampleGenerator> generators) : base(generators)
    {
    }
}