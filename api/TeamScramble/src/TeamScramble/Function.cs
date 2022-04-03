using Amazon.Lambda.Core;
//using Newtonsoft.Json;
using System.Text.Json;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TeamScramble;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(int teamSize, ILambdaContext context)
    {
        var players = Create();
        players = players.Shuffle();
        players = players.Shuffle();
        var listCount = teamSize == 5 ? 5 : 6;
        var team1 = players.GetRange(0, listCount);
        var team2 = players.GetRange(listCount, listCount);
        context.Logger.LogInformation("Attacker: Lysekil");
        foreach (var player in team1)
        {
            
            context.Logger.LogInformation(player);
        }

        context.Logger.LogInformation("--------------");

        context.Logger.LogInformation("Defender: Strömstad");
        foreach (var player in team2)
        {
            context.Logger.LogInformation(player);
        }

        context.Logger.LogInformation("--------------");
        context.Logger.LogInformation("Avbytare:");
        var timeout = players.GetRange(team1.Count + team2.Count, players.Count - (team1.Count + team2.Count));
        foreach (var item in timeout)
        {
                context.Logger.LogInformation(item);
        }

        context.Logger.LogInformation(JsonSerializer.Serialize(team1));
        context.Logger.LogInformation(JsonSerializer.Serialize(team2));
        return JsonSerializer.Serialize(players);
    }

    private static List<string> Create() => new()
    {
        "frittzinator",
        "gravling138",
        "machinshin",
        "darkling",
        "bathamel",
        "pornflakes",
        "roboduck",
        "trayal",
        "geranos",
        "mepzon"
    };
}

public static class StringListExtensions
{
    public static List<string> Shuffle(this List<string> list)
    {
        var rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}