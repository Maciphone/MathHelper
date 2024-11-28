using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Factory;
using MathHelperr.Utility;

namespace MathHelperr.Service.Math.Factory;

public class MathFactoryMarkingWithFactory : IMathFactory
{
    private readonly IMathGeneratorFactory _factory;
    private readonly IAlgebraTextGenerator _algebraTextGenerator;
    private readonly IDivisionTextGenerator _divisionTextGenerator;
    private readonly IMultiplicationTextGenerator _multiplicationTextGenerator;
    private readonly IRemainDivisionTextGenerator _remainDivisionTextGenerator;

    public MathFactoryMarkingWithFactory(IMathGeneratorFactory factory, IAlgebraTextGenerator algebraTextGenerator, IDivisionTextGenerator divisionTextGenerator, IMultiplicationTextGenerator multiplicationTextGenerator, IRemainDivisionTextGenerator remainDivisionTextGenerator)
    {
        _factory = factory;
        _algebraTextGenerator = algebraTextGenerator;
        _divisionTextGenerator = divisionTextGenerator;
        _multiplicationTextGenerator = multiplicationTextGenerator;
        _remainDivisionTextGenerator = remainDivisionTextGenerator;
    }


    
    public IMathExcercise GetMathExercise(MathTypeName operationType, int level)
    {
        switch (operationType)
        {
            case MathTypeName.Algebra:
                return new AlgebraExerciseFromAbstract(
                    _factory.GetGenerator<IAlgebraExampleGenerator>(typeof(IAlgebraExampleGenerator), level),
                    _algebraTextGenerator);
            case MathTypeName.Division:
                return new DivisionExerciseFromAbstract(
                    _factory.GetGenerator<IDivisionExampleGenerator>(typeof(IDivisionExampleGenerator), level),
                    _divisionTextGenerator);
                break;
            case MathTypeName.Multiplication:
                return new MultiplicationExerciseFromAbstract(
                    _factory.GetGenerator<IMultiplicationExampleGenerator>(typeof(IMultiplicationExampleGenerator),
                        level),
                    _multiplicationTextGenerator);
            case MathTypeName.RemainDivision:
                return new RemainDivisionExerciseFromAbstract(
                    _factory.GetGenerator<IRemainDivisonExampleGenerator>(typeof(IRemainDivisonExampleGenerator),
                        level), _remainDivisionTextGenerator);
                  //  _factory.GetTextGenerator<IRemainDivisionTextGenerator>(typeof(IRemainDivisionTextGenerator)));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(operationType), operationType, null);
        }
    }
}

 
