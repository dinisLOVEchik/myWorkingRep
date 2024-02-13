using PersonalFinance.Services;
using System.Collections.Generic;

public class FxRatesProviderResolver
{
    private readonly Dictionary<string, IRateProvider> providers = new Dictionary<string, IRateProvider>();

    public void Add(string source, IRateProvider provider)
    {
        providers.Add(source, provider);
    }

    public IRateProvider Resolve(string source)
    {
        return providers.ContainsKey(source)
            ? providers[source]
            : throw new KeyNotFoundException("FxRatesProvider not found");    
    }
}
