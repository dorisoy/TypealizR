﻿using System;using System.Collections.Generic;using System.Collections.Specialized;using System.IO;using System.Linq;using System.Reflection;using System.Runtime.InteropServices.ComTypes;using System.Xml;using Microsoft.CodeAnalysis;using TypealizR.Extensions;namespace TypealizR.Builder;internal class ExtensionClassModel{    public IEnumerable<string> Usings => usings;    private readonly TypeModel markertType;    private readonly IEnumerable<ExtensionMethodModel> methods;    public ExtensionClassModel(TypeModel markertType, string rootNamespace, IEnumerable<ExtensionMethodModel> methods)    {        this.markertType = markertType;        this.methods = methods;        usings.Add(rootNamespace);        usings.Add(markertType.Namespace);        usings.Add($"{markertType.Namespace}.TypealizR");    }    private readonly HashSet<string> usings = new()    {        "System.CodeDom.Compiler",        "System.Diagnostics",        "System.Diagnostics.CodeAnalysis"    };    public string FileName => $"IStringLocalizerExtensions_{markertType.FullName}.g.cs";    public string ToCSharp(Type generatorType) => $$"""        // <auto-generated/>        {{Usings.Select(x => $"using {x};").ToMultiline(appendNewLineAfterEach: false)}}        namespace Microsoft.Extensions.Localization        {            {{generatorType.GeneratedCodeAttribute()}}            {{markertType.Accessibility.ToVisibilty().ToString().ToLower()}} static partial class IStringLocalizerExtensions_{{markertType.FullNameForClassName}}            {                {{methods.Select(x => x.ToCSharp()).ToMultiline()}}                /// <summary>                /// wraps the specified <see cref="IStringLocalizer&lt{{markertType.FullName}}&gt"/> into a generated type providing properties to access [Some.Nested.Group]: via properties                /// IStringLocalizer<{{markertType.FullName}}> localize = ...                /// localize.Some.Nested.Group...                /// </summary>                [DebuggerStepThrough]                public static StringTypealizR_{{markertType.FullNameForClassName}} WithGroups(this IStringLocalizer<{{markertType.FullName}}> that)                    => new StringTypealizR_{{markertType.FullNameForClassName}}(that);            }        }        """;}