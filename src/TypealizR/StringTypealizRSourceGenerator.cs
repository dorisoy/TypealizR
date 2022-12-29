﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using TypealizR.Builder;
using TypealizR.Core;
using TypealizR.Diagnostics;

namespace TypealizR;

[Generator(LanguageNames.CSharp)]
public sealed class StringTypealizRSourceGenerator : ResxFileSourceGeneratorBase
{
    protected override GeneratedSourceFile GenerateSourceFileFor(DirectoryInfo projectDirectory, string rootNamespace, Compilation compilation, RessourceFile file, IDictionary<string, DiagnosticSeverity> severityConfig)
    {
        (var targetNamespace, var visibility) = FindNameSpaceAndVisibilityOf(compilation, rootNamespace, file, projectDirectory.FullName);

        var markerType = new TypeModel (targetNamespace, file.SimpleName, visibility);

        var builder = new StringTypealizRClassBuilder(markerType, $"StringTypealizR_{markerType.FullNameForClassName}", rootNamespace, severityConfig);

        var diagnostics = new List<Diagnostic>();

        foreach (var entry in file.Entries)
        {
            var collector = new DiagnosticsCollector(file.FullPath, entry.RawKey, entry.Location.LineNumber, severityConfig);

            if (!entry.Groups.Any())
            {
                builder.WithMember(entry.Key, entry.RawKey, entry.Value, collector);
            }
            else
            {
                builder.WithGroups(entry.Key, entry.RawKey, entry.Value, entry.Groups, collector);
            }

            diagnostics.AddRange(collector.Diagnostics);
        }

        var extensionClass = builder.Build();

        return new(extensionClass.FileName, extensionClass.ToCSharp(GetType()), diagnostics);
    }

    private (string, Visibility) FindNameSpaceAndVisibilityOf(Compilation compilation, string rootNameSpace, RessourceFile resx, string projectFullPath)
    {
        var possibleMarkerTypeSymbols = compilation.GetSymbolsWithName(resx.SimpleName);
        var nameSpace = resx.FullPath.Replace(projectFullPath, "");
        nameSpace = nameSpace.Replace(Path.GetFileName(resx.FullPath), "");
        nameSpace = nameSpace.Trim('/', '\\').Replace('/', '.').Replace('\\', '.');
        if (nameSpace != rootNameSpace)
        {
            nameSpace = $"{rootNameSpace}.{nameSpace}".Trim('.');
        }

        if (!possibleMarkerTypeSymbols.Any())
        {
            return (nameSpace.Trim('.', ' '), Visibility.Internal);
        }

        var matchingMarkerType = possibleMarkerTypeSymbols.FirstOrDefault(x => x.ContainingNamespace.OriginalDefinition.ToDisplayString() == nameSpace);

        if (matchingMarkerType is null)
        {
            return (nameSpace.Trim('.', ' '), Visibility.Internal);
        }

        return (matchingMarkerType.ContainingNamespace.OriginalDefinition.ToDisplayString(), matchingMarkerType.DeclaredAccessibility.ToVisibilty());

    }

}