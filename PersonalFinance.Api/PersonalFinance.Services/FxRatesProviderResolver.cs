using PersonalFinance.Services;
using System.Collections.Generic;

public class FxRatesProviderResolver
{
    private Dictionary<string, IRateProvider> providers = new Dictionary<string, IRateProvider>();

    public void Add(string source, IRateProvider provider)
    {
        providers.Add(source, provider);
    }

    public IRateProvider Resolve(string source)
    {
        if (providers.ContainsKey(source))
        {
            return providers[source];
        }
        throw new KeyNotFoundException("FxRatesProvider not found");
    }
}
