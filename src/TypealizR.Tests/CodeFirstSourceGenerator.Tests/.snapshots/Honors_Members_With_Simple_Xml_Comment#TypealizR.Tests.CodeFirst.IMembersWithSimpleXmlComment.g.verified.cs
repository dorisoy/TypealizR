﻿//HintName: TypealizR.Tests.CodeFirst.IMembersWithSimpleXmlComment.g.cs
// <auto-generated/>
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Localization;
namespace TypealizR.Tests.CodeFirst {
    [GeneratedCode("TypealizR.CodeFirstSourceGenerator", "1.0.0.0")]
    public partial class MembersWithSimpleXmlComment: IMembersWithSimpleXmlComment {
        private readonly IStringLocalizer<IMembersWithSimpleXmlComment> localizer;
        public MembersWithSimpleXmlComment (IStringLocalizer<IMembersWithSimpleXmlComment> localizer) {
            this.localizer = localizer;
        }
        private const string Hello_Key = @"Hello {0}!";
        public LocalizedString Hello_Value => localizer[Hello_Key];
        public LocalizedString Hello (string world) => localizer[Hello_Key, world];
        public LocalizedString HelloProperty => localizer[@"Hello world!"];
        public LocalizedString Greeting => localizer[@"Greetings, fellow developer!"];
        public LocalizedString GreetingWithMultilineComment => localizer[@"Greetings, fellow developer!
 This line here will be in the generated default resource-key, also.
 And also this one, even with newlines #wowh@x0r!"];
    }
}