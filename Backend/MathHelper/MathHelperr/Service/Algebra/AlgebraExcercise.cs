using MathHelperr.Model;

namespace MathHelperr.Service;

public class AlgebraExcercise:  IAlgebraExcercise
{
    private string _question;
    private AlgebraResult _generatedNumber;
    
    public AlgebraExcercise(IAlgebraExampleGenerator algebraExampleGenerator, IAlgebraTextGenerator mathTextGenerator)
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