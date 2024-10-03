using MathHelperr.Model;

namespace MathHelperr.Service.AbstractImplementation;

public abstract class AbstractMathExercise<TExample, TText>
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

    private void GenerateExercise()
    {
        _generatedNumber = ExampleGenerator.Example();
        _question = TextGenerator.Answer(_generatedNumber);
    }


    public string Question()
    {
        return _question;
    }

    public AlgebraResult Answer()
    {

        return _generatedNumber;
    }
}