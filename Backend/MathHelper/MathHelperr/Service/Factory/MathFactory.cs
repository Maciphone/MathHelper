using MathHelperr.Service.AbstractImplementation;

namespace MathHelperr.Service.Factory;

public class MathFactory : IMathFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAlgebraExampleGenerator _algebraExampleGenerator;
    private readonly IAlgebraTextGenerator _algebraTextGenerator;
    private readonly IMultiplicationExampleGenerator _multiplicationExampleGenerator;
    private readonly IMultiplicationTextGenerator _multiplicationTextGenerator;

    public MathFactory(IAlgebraExampleGenerator algebraExampleGenerator, IAlgebraTextGenerator algebraTextGenerator, IMultiplicationExampleGenerator multiplicationExampleGenerator, IMultiplicationTextGenerator multiplicationTextGenerator, IServiceProvider serviceProvider)
    {
        _algebraExampleGenerator = algebraExampleGenerator;
        _algebraTextGenerator = algebraTextGenerator;
        _multiplicationExampleGenerator = multiplicationExampleGenerator;
        _multiplicationTextGenerator = multiplicationTextGenerator;
        _serviceProvider = serviceProvider;
    }
    
    public IMathExcercise getMathExcercise(string operationType)
    {
        switch (operationType)
        {
                //Same outcome, works both way! shorter code with serviceProvider!!!
            case "algebra":
                var algebraExampleGenerator = _serviceProvider.GetRequiredService<IAlgebraExampleGenerator>();
                var algebraTextGenerator = _serviceProvider.GetRequiredService<IAlgebraTextGenerator>();
                return new AlgebraExerciseFromAbstract(algebraExampleGenerator, algebraTextGenerator);
           
            case "multiplication":
                return new MultiplicationExerciseFromAbstract(_multiplicationExampleGenerator,
                    _multiplicationTextGenerator);
            case "division":
                return new DivisionExerciseFromAbstract(
                    _serviceProvider.GetRequiredService<IDivisionExampleGenerator>(),
                    _serviceProvider.GetRequiredService<IDivisionTextGenerator>());
            default:
                throw new NotImplementedException("operation not implemented");
        }
    }
}