﻿//HintName: IStringLocalizerExtensions_Some.Root.Namespace.TR0004_NoCode.g.cs
// <auto-generated/>
using System.CodeDom.Compiler;

using System.Diagnostics;

using System.Diagnostics.CodeAnalysis;

using Some.Root.Namespace;
namespace Microsoft.Extensions.Localization
{

    [GeneratedCode("TypealizR.StringLocalizerExtensionsSourceGenerator", "1.0.0.0")]
    internal static partial class IStringLocalizerExtensions_Some_Root_Namespace_TR0004_NoCode
    {

        /// <summary>
		/// Looks up a localized string similar to 'Greetings {name:wtf}'
		/// </summary>
		/// <returns>
		/// A localized version of the current default value of 'Greetings {0}'
		/// </returns>
		public static LocalizedString Greetings__name(this IStringLocalizer<Some.Root.Namespace.TR0004_NoCode> that, object name)
			=> that["Greetings {name:wtf}"].Format(name);

    }
}