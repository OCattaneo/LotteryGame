using Core.Services;
using System.Text.RegularExpressions;


var ticketProcessor = new TicketProcessor();
var balanceHandler = new BalanceHandler();
var prizeCalculator = new PrizeCalculator();
var balanceArray = new decimal[15];
Array.Fill(balanceArray, 10.00m);
var validInput = false;
var houseBalance = 0.00m;

Console.WriteLine("Welcome to the Lottery Game!\r\n");
Console.Write("Please enter your name: \r\n");
var userName = Console.ReadLine();

while(true)
{
    int userTickets = 0;

    Console.WriteLine($"\r\n{userName}, your current balance is ${balanceArray[0]}\r\n");
    Console.WriteLine("Tickets cost $1.00 each\r\n");

    while (!validInput)
    {
        Console.WriteLine("How many tickets would you like to purchase?");
         var input = Console.ReadLine();
        if (int.TryParse(input, out userTickets) && userTickets > 0)
        {
            validInput = true;
            continue;
        }
        Console.WriteLine($"{input} is not a valid input, please enter an integer value.");
    }

    var ticketCounts = ticketProcessor.Process(userTickets, balanceArray);
    (balanceArray, houseBalance) = balanceHandler.SubtractTickets(ticketCounts, balanceArray, houseBalance);

    var playerCount = ticketCounts.Where(x => x > 0).Count();

    Console.WriteLine($"\r\n{playerCount} players have purchased tickets, and the results are in!\r\n");

    var winners = prizeCalculator.CalucatePrize(ticketCounts);
    var grandPrizePlayer = winners[0].Select(x => x.Key).FirstOrDefault();
    var grandPrizeAmount = winners[0].Select(x => x.Value).FirstOrDefault();

    Console.WriteLine("****************************************************************");
    Console.WriteLine($"{IsUser(grandPrizePlayer)} is the winner of the Grand Prize, winning ${grandPrizeAmount}!\r\n");
    Console.WriteLine(GetWinners(winners[1], true));
    Console.WriteLine(GetWinners(winners[2], false));
    Console.WriteLine("A big congratulations to all of our winners!\r\n");
    Console.WriteLine("****************************************************************");
    
    (balanceArray, houseBalance) = balanceHandler.AddPrizeWinnings(winners, balanceArray, houseBalance);
    Console.WriteLine($"Current House Revenue: ${houseBalance}\r\n");

    if (balanceArray[0] < 1)
    {
        Console.WriteLine($"Sorry, your balance of {balanceArray[0]} is below the minimum limit.\nPlease press any key to exit.");
        Console.ReadKey();
        return;
    }
    if (!balanceHandler.ValidGame(balanceArray))
    {
        Array.Fill(balanceArray, 10.00m, 1, balanceArray.Length - 1);
    }
    Console.WriteLine("Press any key to continue.");
    validInput = false;
    Console.ReadKey();

}

string GetWinners(List<KeyValuePair<int, decimal>> winners, bool isSecondary)
{
    var playerString = "Player";
    var hasString = "has";
    if (winners.Count > 1)
    {
        playerString = "Players";
        hasString = "have";
    }

    var players = string.Join(", ", winners.Select(x => x.Key).OrderBy(x => x)); //Can distint if only want to mention each winner once, but this makes more sense given winnings display.
    players = Regex.Replace(players, @"\b0\b", $"{userName}");

    var tier = "Second";
    if (!isSecondary)
    {
        tier = "Third";
    }

    var response = $"{playerString} {players} {hasString} won the {tier} Tier Prize, winning ${winners.Select(x => x.Value).FirstOrDefault()} each!\r\n";
    return response;
}

string IsUser(int winner)
{
    if (winner == 0)
    {
        return userName;
    }
    return $"Player {winner}";
}