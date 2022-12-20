﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.CodeAnalysis;
using TypealizR.Extensions;

namespace TypealizR.StringLocalizer;


internal class ParameterModel
{
	public readonly string Token;
	public readonly string Type;
	public readonly string Name;
	public readonly string DisplayName;


	public ParameterModel(string token, string name, string type)
    {
		Token = token;
        Type = type;
        Name = name;
		DisplayName = SanitizeName(name);

	}

	private string SanitizeName(string rawParameterName)
    {
        var parameterName = new string(
            rawParameterName
                .Replace(" ", "_")
                .ToArray()
        );

        if (!parameterName.First().IsValidInIdentifier())
        {
            return $"_{rawParameterName}";
        }

        return parameterName;
    }
}