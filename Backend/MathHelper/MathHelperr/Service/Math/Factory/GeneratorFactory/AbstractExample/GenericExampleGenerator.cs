namespace MathHelperr.Service.Math.Factory;

[Obsolete("new factroy method implemented, but have worked on it, so I did not delete it")]
public class GenericExampleGenerator<T> where T : IMathExampleGenerator
{
    private readonly Dictionary<int, T> _generators;


    public GenericExampleGenerator(Dictionary<int, T> generators)
    {
        _generators = generators;
    }

    public T GetGenerator(int level)
    {
        if (_generators.TryGetValue(level, out var generator))
        {
            return generator;
        }
        throw new InvalidOperationException($"No generator found for level {level}");
    }
}