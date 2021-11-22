﻿namespace Nodexr.Api.Functions.IntegrationTests.NodeTrees.Queries;

using FluentAssertions;
using Nodexr.Api.Contracts.NodeTrees;
using Nodexr.Api.Functions.Models;
using NUnit.Framework;
using System.Threading.Tasks;
using static Testing;

public class GetNodeTreesTests : TestBase
{
    [Test]
    public async Task ShouldReturnMatchingNodeTrees(string _)
    {
        var matchingTrees = new[]
        {
            new NodeTree("Animal", "a"),
            new NodeTree("Animals", "a"),
            new NodeTree("animals", "a"),
            new NodeTree("two animals", "a"),
        };

        var nonMatchingTrees = new[]
        {
            new NodeTree("Thing", "a"),
            new NodeTree("Anima", "a"),
        };

        await AddRangeAsync(matchingTrees);
        await AddRangeAsync(nonMatchingTrees);

        var query = new GetNodeTreesQuery()
        {
            SearchString = "Animal",
        };

        var result = await SendAsync(query);

        result.Contents.Should().BeEquivalentTo(
            matchingTrees,
            x => x.IncludingAllDeclaredProperties()
        );
    }
}