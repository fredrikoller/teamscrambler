using Amazon.Lambda.Core;
using System.Text.Json;
using TeamScramble.Constants;
using TeamScramble.Extensions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TeamScramble;

public static class Function
{
    /// <summary>
    /// A simple function that scrambles a list of players in to two teams
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string FunctionHandler(int teamSize, ILambdaContext context)
    {
        var players = CreatePlayers();
        players = players.Shuffle();
        players = players.Shuffle();
        var listCount = teamSize == 5 ? 5 : 6;
        var team1 = players.GetRange(0, listCount);
        var team2 = players.GetRange(listCount, listCount);
        PrintTeam(team1, Teams.LYSEKIL, context);
        PrintTeam(team2, Teams.STROMSTAD, context);
        
        var timeout = players.GetRange(team1.Count + team2.Count, players.Count - (team1.Count + team2.Count));
        PrintTeam(timeout, Teams.BENCH_WARMERS, context);

        context.Logger.LogInformation(JsonSerializer.Serialize(team1));
        context.Logger.LogInformation(JsonSerializer.Serialize(team2));
        context.Logger.LogInformation(JsonSerializer.Serialize(timeout));
        return JsonSerializer.Serialize(players);
    }

    private static List<string> CreatePlayers() => new()
    {
        "frittzinator",
        "gravling138",
        "machinshin",
        "darkling",
        "bathamel",
        "pornflakes",
        "roboduck",
        "trayal",
        "mepzon",
        "nejon",
        "boobo",
        "deja",
        "nobody"
    };

    private static void PrintTeam(List<string> team, string channel, ILambdaContext context)
    {
        if (channel == Teams.BENCH_WARMERS)
        {
            context.Logger.LogInformation(channel);
        }
        else
        {
            context.Logger.LogInformation($"Channel: {channel}");
        }
        foreach (var player in team)
        {
            context.Logger.LogInformation(player);
        }

        context.Logger.LogInformation("--------------");
    }
}
