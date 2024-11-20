using System.Linq;

namespace MathHelperr.Service.Math.Factory;


public class AlgebraExampleGeneratorFactory
{
    private readonly Dictionary<int, IAlgebraExampleGenerator> _generators;

    public AlgebraExampleGeneratorFactory(IEnumerable<IAlgebraExampleGenerator> generators)
    {
       
        _generators = generators.ToDictionary(g => g.Level);
    }

    public IAlgebraExampleGenerator GetGenerator(int level)
    {
        if (_generators.TryGetValue(level, out var generator))
        {
            return generator;
        }
        throw new InvalidOperationException($"No generator found for level {level}");
    }
}