namespace MathHelperr.Service.Math.Factory;

public class MathGeneratorFactory : IMathGeneratorFactory
{
   
        private readonly IServiceProvider _serviceProvider;

        public MathGeneratorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public  T GetGenerator<T>(int level) where T: class
        { 
            var generators = _serviceProvider.GetServices<T>();
            
            foreach (var generator in generators)
            {
                
                var levelProperty = generator.GetType().GetProperty("Level");
                if (levelProperty != null && (int)levelProperty.GetValue(generator) == level)
                {
                    return generator;
                }
            }
            var defaultGenerator =  generators.FirstOrDefault(g 
                => (int)g.GetType().GetProperty("Level").GetValue(g) == 1);

            if (defaultGenerator == null)
            {
                throw new NotImplementedException();
            }
            return defaultGenerator;

        }
        
    }


    
 
