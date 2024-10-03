using MathHelperr.Model;

namespace MathHelperr.Service;

public class MultiplicationExcercise:  IMultiplicationExcercise
{
    private string _question;
    private AlgebraResult _generatedNumber;
    
    public MultiplicationExcercise(IMathExampleGenerator algebraExampleGenerator, IMathTextGenerator mathTextGenerator)
    {
        _generatedNumber = algebraExampleGenerator.Example();
        _question = mathTextGenerator.Answer(_generatedNumber);
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