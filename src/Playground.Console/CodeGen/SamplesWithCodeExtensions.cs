using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Playground.Console.CodeGen;
internal static class SamplesWithCodeExtensions
{
    static LocalizedString GetLocalizable(string key, params object[] args)
    {
        var value = key;
        var raw = SamplesWithCode.ResourceManager.GetString(key, SamplesWithCode.Culture);
        if (raw is not null)
        {
            value = raw;
        }

        var formattedValue = string.Format(value, args);

        return new LocalizedString(key, formattedValue, raw is null);
    }

    public static LocalizedString Hello__world(this SamplesWithCode _, string world)
        => GetLocalizable("Hello {world:s}", world);

    public static LocalizedString Greeting(this SamplesWithCode _)
        => GetLocalizable("Greeting");
}
