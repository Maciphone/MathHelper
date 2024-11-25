using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Factory;
using MathHelperr.Utility;

namespace MathHelperr.Service.Math.Factory;

public class MathFactoryMarkingWithFactory : IMathFactory
{
    private readonly MathGeneratorFactory _factory;

    public MathFactoryMarkingWithFactory(MathGeneratorFactory factory)
    {
        _factory = factory;
    }

    public IMathExcercise GetMathExercise(MathTypeName operationType, int level)
    {
        switch (operationType)
        {
            case MathTypeName.Algebra:
                return new AlgebraExerciseFromAbstract(
                    (IAlgebraExampleGenerator)_factory.GetGenerator(typeof(IAlgebraExampleGenerator), level),
                    (IAlgebraTextGenerator)_factory.GetTextGenerator(typeof(IAlgebraTextGenerator)));

            case MathTypeName.Division:
                return new DivisionExerciseFromAbstract(
                    (IDivisionExampleGenerator)_factory.GetGenerator(typeof(IDivisionExampleGenerator), level),
                    (IDivisionTextGenerator)_factory.GetTextGenerator(typeof(IDivisionTextGenerator)));
                break;
            case MathTypeName.Multiplication:
                return new MultiplicationExerciseFromAbstract(
                    (IMultiplicationExampleGenerator)_factory.GetGenerator(typeof(IMultiplicationExampleGenerator),
                        level),
                    (IMultiplicationTextGenerator)_factory.GetTextGenerator(typeof(IMultiplicationTextGenerator)));
            case MathTypeName.RemainDivision:
                return new RemainDivisionExerciseFromAbstract(
                    (IRemainDivisonExampleGenerator)_factory.GetGenerator(typeof(IRemainDivisonExampleGenerator),
                        level),
                    (IRemainDivisionTextGenerator)_factory.GetTextGenerator(typeof(IRemainDivisionTextGenerator)));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(operationType), operationType, null);
        }
    }
}



// public IMathExcercise GetMathExercise(MathTypeName operationType, int level)
    // {
    //     switch (operationType)
    //     {
    //        case MathTypeName.Algebra:
    //            return new AlgebraExerciseFromAbstract(
    //                (IAlgebraExampleGenerator)  _generatorFactory.GetExampleGenerator(level, typeof(IAlgebraExampleGenerator)),
    //                (IAlgebraTextGenerator) _generatorFactory.GetTextGenerator(level, typeof(IAlgebraTextGenerator)));
    //        case MathTypeName.Division:
    //            return new DivisionExerciseFromAbstract(
    //                _generatorFactory.GetExampleGenerator(level) as IDivisionExampleGenerator,
    //                _generatorFactory.GetTextGenerator(level) as IDivisionTextGenerator);
    //        case MathTypeName.Multiplication:
    //            return new MultiplicationExerciseFromAbstract(
    //                _generatorFactory.GetExampleGenerator(level) as IMultiplicationExampleGenerator,
    //                _generatorFactory.GetTextGenerator(level) as IMultiplicationTextGenerator);
    //        case MathTypeName.RemainDivision:
    //            return new RemainDivisionExerciseFromAbstract(
    //                _generatorFactory.GetExampleGenerator(level) as IRemainDivisonExampleGenerator,
    //                _generatorFactory.GetTextGenerator(level) as IRemainDivisionTextGenerator);
    //        default:
    //            throw new NotImplementedException();
    //            
    //     }
    // }
    //
    // public IMathExcercise GetMathExerciseGPT(MathTypeName operationType, int level)
    // {
    //     switch (operationType)
    //     {
    //         case MathTypeName.Algebra:
    //             return CreateExercise<IAlgebraExampleGenerator, IAlgebraTextGenerator, AlgebraExerciseFromAbstract>(level);
    //         case MathTypeName.Division:
    //             return CreateExercise<IDivisionExampleGenerator, IDivisionTextGenerator, DivisionExerciseFromAbstract>(level);
    //         case MathTypeName.Multiplication:
    //             return CreateExercise<IMultiplicationExampleGenerator, IMultiplicationTextGenerator, MultiplicationExerciseFromAbstract>(level);
    //         case MathTypeName.RemainDivision:
    //             return CreateExercise<IRemainDivisonExampleGenerator, IRemainDivisionTextGenerator, RemainDivisionExerciseFromAbstract>(level);
    //         default:
    //             throw new NotImplementedException($"The operation type {operationType} is not implemented.");
    //     }
    // }
    //
    // private TExercise CreateExercise<TExample, TText, TExercise>(int level)
    //     where TExample : class, IMathExampleGenerator
    //     where TText : class, IMathTextGenerator
    //     where TExercise : IMathExcercise
    // {
    //     var exampleGenerator = _generatorFactory.GetExampleGenerator(level) as TExample;
    //     var textGenerator = _generatorFactory.GetTextGenerator(level) as TText;
    //
    //     if (exampleGenerator == null || textGenerator == null)
    //     {
    //         throw new InvalidOperationException($"Unable to resolve generators for level {level} and types {typeof(TExample).Name}, {typeof(TText).Name}");
    //     }
    //
    //     return (TExercise)Activator.CreateInstance(typeof(TExercise), exampleGenerator, textGenerator);
    // }

 
