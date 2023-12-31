﻿using Microsoft.Extensions.Localization;
using TypealizR.CodeFirst.Abstractions;

namespace TypealizR.Tests.CodeFirst;

internal interface ISomeInterface
{
    bool IsEnabledProperty { get; }
    bool IsEnabledMethod();

    LocalizedString Greeting { get; }

    LocalizedString Hello(string world);

    LocalizedString Hello(string user, string world, int visitCount, bool dontPanic);
}
