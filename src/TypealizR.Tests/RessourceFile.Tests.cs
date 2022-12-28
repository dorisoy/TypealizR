﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FluentAssertions;
using Microsoft.CodeAnalysis.Text;
using TypealizR.Core;

namespace TypealizR.Tests;
public class RessourceFile_Tests
{

	private record LineInfo(int LineNumber = 42, int LinePosition = 1337, bool HasLineInfo = true) : IXmlLineInfo
	{
        bool IXmlLineInfo.HasLineInfo() => HasLineInfo;
	}


	[Theory]
    [InlineData(3, 
        "Ressource1.resx",
        "Ressource2.resx",
        "Ressource3.resx"
    )]
    [InlineData(3,
        "A/Ressource1.resx",
        "A/Ressource2.resx",
        "B/Ressource1.resx"
    )]
    public void Parsing_Paths_Does_Not_Group_Files_With_Different_Names_And_Paths(int expected, params string[] paths)
    {
        var actual = RessourceFile.From(paths);
        actual.Should().HaveCount(expected);
    }

    [Theory]
    [InlineData(2,
        "Ressource1.resx",          //<--
        "Ressource1.de.resx",
        "Ressource1.de-DE.resx",
        "Ressource1.en-US.resx",
        "Ressource1.en-EN.resx",
        "Ressource2.resx",          //<--
        "Ressource2.de.resx",
        "Ressource2.de-DE.resx",
        "Ressource2.en-US.resx",
        "Ressource2.en-EN.resx",
        "Ressource3.en-EN.resx",    //<--ignored
        "Ressource4.en.resx"        //<--ignored
    )]
    [InlineData(2,
        "A/Ressource1.resx",          //<--
        "A/Ressource1.de.resx",
        "A/Ressource1.de-DE.resx",
        "A/Ressource1.en-US.resx",
        "A/Ressource1.en-EN.resx",
        "B/Ressource1.resx",          //<--
        "B/Ressource1.de.resx",
        "B/Ressource1.de-DE.resx",
        "B/Ressource1.en-US.resx",
        "B/Ressource1.en-EN.resx",
        "B/Ressource3.en-EN.resx",    //<--ignored
        "B/Ressource4.en.resx",       //<--ignored
        "C/Ressource1.en-EN.resx",    //<--ignored
        "C/Ressource2.en.resx"        //<--ignored
    )]
    public void Parsing_Paths_Groups_Localizations_By_Path(int expected, params string[] paths)
    {
        var actual = RessourceFile.From(paths);
        actual.Should().HaveCount(expected);
    }

    [Theory]
    [InlineData("SomeFile", @"SomeFile")]
    [InlineData("SomeFile", @"SomeFile.resx")]
    [InlineData("SomeFile", @"SomeFile.de.resx")]
    [InlineData("SomeFile", @"SomeFile.de-DE.resx")]
    [InlineData("SomeFile", @"SomeRelativePath/SomeFile")]
    [InlineData("SomeFile", @"SomeRelativePath/SomeFile.resx")]
    [InlineData("SomeFile", @"SomeRelativePath/SomeFile.de.resx")]
    [InlineData("SomeFile", @"SomeRelativePath/SomeFile.de-DE.resx")]
    [InlineData("SomeFile", @"c:/SomeRelativePath/SomeFile")]
    [InlineData("SomeFile", @"c:/SomeRelativePath/SomeFile.resx")]
    [InlineData("SomeFile", @"c:/SomeRelativePath/SomeFile.de.resx")]
    [InlineData("SomeFile", @"c:/SomeRelativePath/SomeFile.de-DE.resx")]
    public void SimpleFileNameOf_Reduces_All_Additional_Extensions(string expected, string input)
    {
        var actual = RessourceFile.GetSimpleFileNameOf(input);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello")]
	[InlineData("Hello [world]")]
	[InlineData("Hello [world]:")]
	[InlineData("[Hello] world")]
	[InlineData("[Hello]world")]
	[InlineData("[]: Hello")]
	public void Entry_Has_No_Group(string input)
	{
        var sut = new RessourceFile.Entry(input, input, new LineInfo());
        sut.GroupKey.Should().BeNull();
	}

	[Theory]
	[InlineData("Hello", null, "Hello")]
	[InlineData("Hello [world]", null, "Hello [world]")]
	[InlineData("Hello [world]:", null, "Hello [world]:")]
	[InlineData("[world]:", null, "[world]:")]
	[InlineData("[world]: ", "world", "[world]: ")]
	[InlineData("[world]:  ", "world", "[world]:  ")]
	[InlineData("[Hello] world", null, "[Hello] world")]
	[InlineData("[Hello]world", null, "[Hello]world")]
	[InlineData("[]: Hello", null, "[]: Hello")]
	[InlineData("[logs]: Hello [world]", "logs", "Hello [world]")]
	[InlineData("[message.info]: Hello [world]:", "message.info", "Hello [world]:")]
	[InlineData("[Hello]: world","Hello", "world")]
	[InlineData("[Hello]:world", "Hello", "world")]
	[InlineData(" [Hello]:world", "Hello", "world")]
	[InlineData("[ Hello]:world", "Hello", "world")]
	[InlineData("[Hello ]:world", "Hello", "world")]
	[InlineData("[ Hello ]:world", "Hello", "world")]
	[InlineData("[ Hello.]:world", "Hello", "world")]
	[InlineData("[ Hello. ]:world", "Hello", "world")]
	[InlineData("[ Hello .]:world", "Hello", "world")]
	[InlineData("[ Hello . ]:world", "Hello", "world")]
	[InlineData("[Hello .World]:world", "Hello.World", "world")]
	[InlineData("[Hello. World]:world", "Hello.World", "world")]
	[InlineData("[ Hello. World]:world", "Hello.World", "world")]
	[InlineData("[ Hello . World]:world", "Hello.World", "world")]
	[InlineData("[ Hello .World ]:world", "Hello.World", "world")]
	[InlineData("[ Hello. World ]:world", "Hello.World", "world")]
	[InlineData("[ Hello . World ]:world", "Hello.World", "world")]
	[InlineData("[ Hello .World.]:world", "Hello.World", "world")]
	[InlineData("[ Hello .World. ]:world", "Hello.World", "world")]
	public void Entry_Extracts_GroupKey(string input, string expectedGroupKey, string expectedKey)
	{
		var sut = new RessourceFile.Entry(input, input, new LineInfo());
        sut.GroupKey.Should().Be(expectedGroupKey);
		sut.Key.Should().Be(expectedKey);
	}
}
