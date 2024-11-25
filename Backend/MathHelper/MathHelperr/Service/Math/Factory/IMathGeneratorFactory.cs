namespace MathHelperr.Service.Math.Factory;

public interface IMathGeneratorFactory

{
    IMathExampleGenerator GetExampleGenerator(int level);
    IMathTextGenerator GetTextGenerator(int level);
}