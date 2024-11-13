using Core.Services;


var ticketProcessor = new TicketProcessor();
var balanceHandler = new BalanceHandler();
var balanceArray = new decimal[15];
Array.Fill(balanceArray, 10.00m);
var validInput = false;

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
    balanceArray = balanceHandler.SubtractTickets(ticketCounts, balanceArray);
    var playerCount = ticketCounts.Where(x => x > 0).Count();

    Console.WriteLine($"\r\n{playerCount} players have purchased tickets, and the results are in!\r\n");


    Console.WriteLine($"Player X is the winner of the Grand Prize, winning $Y!");
    //Conditional for player count.
    Console.WriteLine($"Player(s) X,Y,Z have won the Second Tier Prize, winning $X each!");
    //Conditional for player count.
    Console.WriteLine($"Player(s) X,Y,Z have won the Third Tier Prize, winning $X each!\r\n");
    Console.WriteLine("A big congratulations to all of our winners!\r\n");
    //Calculate user & CPU new balances.
    Console.WriteLine($"Current House Revenue: $X\r\n");

    if (balanceArray[0] < 1) // exit if balance < $1
    {
        Console.WriteLine("Sorry, your balance is below the minimum limit.\nPlease press any key to exit.");
        Console.ReadKey();
        return;
    }
    Console.WriteLine("Press any key to continue."); // or exit key
    Console.ReadKey();

}