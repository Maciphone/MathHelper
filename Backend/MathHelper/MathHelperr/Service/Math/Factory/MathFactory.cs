using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Utility;

namespace MathHelperr.Service.Factory;

public class MathFactory : IMathFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAlgebraExampleGenerator _algebraExampleGenerator;
    private readonly IAlgebraTextGenerator _algebraTextGenerator;
    private readonly IMultiplicationExampleGenerator _multiplicationExampleGenerator;
    private readonly IMultiplicationTextGenerator _multiplicationTextGenerator;
    private readonly IRemainDivisonExampleGenerator _remainDivisonExampleGenerator;
    private readonly IRemainDivisionTextGenerator _remainDivisionTextGenerator;

    public MathFactory(IAlgebraExampleGenerator algebraExampleGenerator, IAlgebraTextGenerator algebraTextGenerator, IMultiplicationExampleGenerator multiplicationExampleGenerator, IMultiplicationTextGenerator multiplicationTextGenerator, IServiceProvider serviceProvider, IRemainDivisionTextGenerator remainDivisionTextGenerator, IRemainDivisonExampleGenerator remainDivisonExampleGenerator)
    {
        _algebraExampleGenerator = algebraExampleGenerator;
        _algebraTextGenerator = algebraTextGenerator;
        _multiplicationExampleGenerator = multiplicationExampleGenerator;
        _multiplicationTextGenerator = multiplicationTextGenerator;
        _serviceProvider = serviceProvider;
        _remainDivisionTextGenerator = remainDivisionTextGenerator;
        _remainDivisonExampleGenerator = remainDivisonExampleGenerator;
    }
    
    public IMathExcercise getMathExcercise(MathTypeName operationType)
    {
        switch (operationType)
        {
                //Same outcome, works both way! shorter code with serviceProvider!!!
            case MathTypeName.Algebra:
                var algebraExampleGenerator = _serviceProvider.GetRequiredService<IAlgebraExampleGenerator>();
                var algebraTextGenerator = _serviceProvider.GetRequiredService<IAlgebraTextGenerator>();
                return new AlgebraExerciseFromAbstract(
                    algebraExampleGenerator, algebraTextGenerator);
           
            case MathTypeName.Multiplication:
                return new MultiplicationExerciseFromAbstract(_multiplicationExampleGenerator,
                    _multiplicationTextGenerator);
            case MathTypeName.Division:
                return new DivisionExerciseFromAbstract(
                    _serviceProvider.GetRequiredService<IDivisionExampleGenerator>(),
                    _serviceProvider.GetRequiredService<IDivisionTextGenerator>());
            case MathTypeName.RemainDivision:
                return new RemainDivisionExerciseFromAbstract(
                    _remainDivisonExampleGenerator,
                    _remainDivisionTextGenerator);
                
            default:
                throw new NotImplementedException("operation not implemented");
        }
    }
}