﻿using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace TypealizR.Abstractions;

public interface IStringTypealizR<T>
{    /// <summary>
    /// Gets the string resource with the given name.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <returns>The string resource as a <see cref="LocalizedString"/>.</returns>
    LocalizedString this[string name] { get; }

    /// <summary>
    /// Gets the string resource with the given name and formatted with the supplied arguments.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <param name="arguments">The values to format the string with.</param>
    /// <returns>The formatted string resource as a <see cref="LocalizedString"/>.</returns>
    LocalizedString this[string name, params object[] arguments] { get; }

    /// <summary>
    /// Gets all string resources.
    /// </summary>
    /// <param name="includeParentCultures">
    /// A <see cref="System.Boolean"/> indicating whether to include strings from parent cultures.
    /// </param>
    /// <returns>The strings.</returns>
    IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures);
}
