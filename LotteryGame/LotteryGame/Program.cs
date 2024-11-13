var balanceArray = new decimal[15];
Array.Fill(balanceArray, 10.00m);

Console.WriteLine("Welcome to the Lottery Game!\r\n");
Console.Write("Please enter your name: \r\n");
var userName = Console.ReadLine();
while(true)
{
    Console.WriteLine($"{userName}, your current balance is ${balanceArray[0]}\r\n");
    Console.WriteLine("Tickets cost $1.00 each\r\n");
    Console.WriteLine("How many tickets would you like to purchase?");
    var ticketCount = Console.ReadLine();
    //Check ticketCount < Max, ensure player can afford, deduct price from balance
    Console.WriteLine($"\r\nx players have purchased tickets, and the results are in!\r\n");
    Console.WriteLine($"Player X is the winner of the Grand Prize, winning $Y!");
    //Conditional for player count.
    Console.WriteLine($"Player(s) X,Y,Z have won the Second Tier Prize, winning $X each!");
    //Conditional for player count.
    Console.WriteLine($"Player(s) X,Y,Z have won the Third Tier Prize, winning $X each!\r\n");
    Console.WriteLine("A big congratulations to all of our winners!\r\n");
    //Calculate user & CPU new balances.
    Console.WriteLine($"Current House Revenue: $X\r\n");
}