using MathHelperr.Model;
using MathHelperr.Model.Db;

namespace MathHelperr.Service.AbstractImplementation;

public abstract class AbstractMathExercise<TExample, TText> : IMathExcercise
where TExample : IMathExampleGenerator
where TText : IMathTextGenerator

{
    protected string _question;
    protected AlgebraResult _generatedNumber;
    protected readonly TExample ExampleGenerator;
    protected readonly TText TextGenerator;

    protected AbstractMathExercise(TExample exampleGenerator, TText textGenerator)
    {
        ExampleGenerator = exampleGenerator;
        TextGenerator = textGenerator;
        GenerateExercise();
    }
    public virtual string Question()
    {
        return _question;
    }
    public virtual AlgebraResult Answer()
    {

        return _generatedNumber;
    }
    
    private void GenerateExercise()
    {
        _generatedNumber = ExampleGenerator.Example();
        _question = TextGenerator.Answer(_generatedNumber);
    }


    
}