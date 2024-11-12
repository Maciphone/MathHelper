using System.Reflection;

namespace MathHelperr.Service.LevelChecker;

public class LevelChecker
{
    // counts given interfaces implementation: 
    // visszaadja egy adott interface implementációjának a számát -
    // meghatározható hány level van belőle
    
    public static int GetNumberOfImplementations<TInterface>()
    {
        var interfaceType = typeof(TInterface);
        
        // A jelenlegi assembly-ben keressük meg az implementációkat
        var assembly = Assembly.GetExecutingAssembly();
        
        // Keressük meg az összes típust, amely az adott interfészt implementálja
        var implementations = assembly.GetTypes()
            .Where(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .ToList();
        
      
        return implementations.Count;
    }

  
    
}