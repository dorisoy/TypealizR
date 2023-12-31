﻿//HintName: TypealizR.Tests.CodeFirst.ITranslatables.g.cs
// <auto-generated/>
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Localization;
namespace TypealizR.Tests.CodeFirst {
    [GeneratedCode("TypealizR.CodeFirstSourceGenerator", "1.0.0.0")]
    public partial class Translatables: ITranslatables {
        private readonly IStringLocalizer<ITranslatables> localizer;
        public Translatables (IStringLocalizer<ITranslatables> localizer) {
            this.localizer = localizer;
        }
        private const string Hello_Key = @"Hello";
        private const string Hello_FallbackKey = @"Hello {0}";
        public LocalizedString Hello_Raw => localizer[Hello_Key].Or(localizer[Hello_FallbackKey]);
        public LocalizedString Hello (string world) => localizer[Hello_Key, world].Or(localizer[Hello_FallbackKey, world]);
        private const string Hello_Key = @"Hello";
        private const string Hello_FallbackKey = @"Hello {0} {1} {2} {3}";
        public LocalizedString Hello_Raw => localizer[Hello_Key].Or(localizer[Hello_FallbackKey]);
        public LocalizedString Hello (string user, string world, int visitCount, bool dontPanic) => localizer[Hello_Key, user, world, visitCount, dontPanic].Or(localizer[Hello_FallbackKey, user, world, visitCount, dontPanic]);
        private const string Greeting_Key = @"Greeting";
        private const string Greeting_FallbackKey = @"Greeting";
        public LocalizedString Greeting => localizer[Greeting_Key].Or(localizer[Greeting_FallbackKey]);
    }
}