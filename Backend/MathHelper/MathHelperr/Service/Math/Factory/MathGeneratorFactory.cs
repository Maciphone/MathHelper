namespace MathHelperr.Service.Math.Factory;

public class MathGeneratorFactory
{
   
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<IMathTextGenerator> _textGenerators;

        public MathGeneratorFactory(IServiceProvider serviceProvider, IEnumerable<IMathTextGenerator> textGenerators)
        {
            _serviceProvider = serviceProvider;
            _textGenerators = textGenerators;
        }

        public object GetGenerator(Type generatorType, int level)
        { 
            var generators = _serviceProvider.GetServices(generatorType);
            
            foreach (var generator in generators)
            {
                var levelProperty = generator.GetType().GetProperty("Level");
                if (levelProperty != null && (int)levelProperty.GetValue(generator) == level)
                {
                    return generator;
                }
            }

            throw new InvalidOperationException($"No generator found for type {generatorType.Name} and level {level}");
        }

        public object GetTextGenerator(Type generatorType)
        {
            var generator = _serviceProvider.GetServices(generatorType).ToList()[0];
           //var generator =  _textGenerators.FirstOrDefault(g => g.GetType() == generatorType);
            if (generator == null)
            {
                throw new NotImplementedException();
            }
            return generator;
        }
        
        
    }


    

    
    /*
    public IMathExampleGenerator GetExampleGenerator(int level)
    {
        var generator = _exampleGenerators.FirstOrDefault(g => g.Level == level);
        if (generator == null)
        {
            throw new NotImplementedException();
            _exampleGenerators.First(g => g.Level == 1);
        }
        return generator;
    }

    public IMathTextGenerator GetTextGenerator(int level)
    {
        var generator = _textGenerators.FirstOrDefault(g => g.Level == level);
        if (generator == null)
        {
            throw new NotImplementedException();
            _textGenerators.Where(g => g.Level == 1);
        }
        return generator;
    }
    */
    
 
