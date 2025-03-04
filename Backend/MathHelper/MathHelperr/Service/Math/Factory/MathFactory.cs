using MathHelperr.Service.AbstractImplementation;
using MathHelperr.Service.Factory;
using MathHelperr.Utility;

namespace MathHelperr.Service.Math.Factory;

public class MathFactory //: IMathFactory
{
    // private readonly IServiceProvider _serviceProvider;
    // private readonly IAlgebraExampleGenerator _algebraExampleGenerator;
    // private readonly IAlgebraTextGenerator _algebraTextGenerator;
    // private readonly IMultiplicationExampleGenerator _multiplicationExampleGenerator;
    // private readonly IMultiplicationTextGenerator _multiplicationTextGenerator;
    // private readonly IRemainDivisonExampleGenerator _remainDivisonExampleGenerator;
    // private readonly IRemainDivisionTextGenerator _remainDivisionTextGenerator;
    // private readonly IDivisionExampleGenerator _divisionExampleGenerator;
    // private readonly IDivisionTextGenerator _divisionTextGenerator;
    //
    // //IGenerator Branch registration test
    // private readonly AlgebraExampleGeneratorFactory _algebraExampleGeneratorFactory;
    // private readonly AlgebraTextGeneratorFactory _algebraTextGeneratorFactory;
    //
    // public MathFactory(IAlgebraExampleGenerator algebraExampleGenerator, IAlgebraTextGenerator algebraTextGenerator, IMultiplicationExampleGenerator multiplicationExampleGenerator, IMultiplicationTextGenerator multiplicationTextGenerator, IServiceProvider serviceProvider, IRemainDivisionTextGenerator remainDivisionTextGenerator, IRemainDivisonExampleGenerator remainDivisonExampleGenerator, IDivisionTextGenerator divisionTextGenerator, IDivisionExampleGenerator divisionExampleGenerator, AlgebraExampleGeneratorFactory algebraExampleGeneratorFactory, AlgebraTextGeneratorFactory algebraTextGeneratorFactory)
    // {
    //     _algebraExampleGenerator = algebraExampleGenerator;
    //     _algebraTextGenerator = algebraTextGenerator;
    //     _multiplicationExampleGenerator = multiplicationExampleGenerator;
    //     _multiplicationTextGenerator = multiplicationTextGenerator;
    //     _serviceProvider = serviceProvider;
    //     _remainDivisionTextGenerator = remainDivisionTextGenerator;
    //     _remainDivisonExampleGenerator = remainDivisonExampleGenerator;
    //     _divisionTextGenerator = divisionTextGenerator;
    //     _divisionExampleGenerator = divisionExampleGenerator;
    //     _algebraExampleGeneratorFactory = algebraExampleGeneratorFactory;
    //     _algebraTextGeneratorFactory = algebraTextGeneratorFactory;
    // }
    //
    // public IMathExcercise GetMathExercise(MathTypeName operationType, int level)
    // {
    //     switch (operationType)
    //     {
    //             //Same outcome, works both way! shorter code with serviceProvider!!!
    //         case MathTypeName.Algebra:
    //             // var algebraExampleGenerator = _serviceProvider.GetRequiredService<IAlgebraExampleGenerator>();
    //             // var algebraTextGenerator = _serviceProvider.GetRequiredService<IAlgebraTextGenerator>();
    //             // return new AlgebraExerciseFromAbstract(
    //             //     algebraExampleGenerator, algebraTextGenerator);
    //             var algebraExampleGenerator = _algebraExampleGeneratorFactory.GetGenerator(level);
    //             var algebraTextGenerator = _algebraTextGeneratorFactory.GetTextGenerator(level);
    //             return new AlgebraExerciseFromAbstract(algebraExampleGenerator, algebraTextGenerator);
    //         
    //        
    //         case MathTypeName.Multiplication:
    //             return new MultiplicationExerciseFromAbstract(_multiplicationExampleGenerator,
    //                 _multiplicationTextGenerator);
    //         case MathTypeName.Division:
    //             return new DivisionExerciseFromAbstract(_divisionExampleGenerator, _divisionTextGenerator);
    //         case MathTypeName.RemainDivision:
    //             return new RemainDivisionExerciseFromAbstract(
    //                 _remainDivisonExampleGenerator,
    //                 _remainDivisionTextGenerator);
    //             
    //         default:
    //             throw new NotImplementedException("operation not implemented");
    //     }
    // }
    
    //additional line for workflow test
}