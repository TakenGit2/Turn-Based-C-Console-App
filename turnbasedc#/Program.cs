namespace ConsoleAppTurnBased
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new(2, 20, 3, 3, "Water");
            Enemy enemy = new(2, 20, 50, 3, "Noob");

            Random random = new Random();
            //{player.r} Defense - {player.d}
            //{enemy.r} Health - {enemy.hp}");
            //Console.WriteLine($"{enemy.r} Defense - {enemy.d}");
            //Console.WriteLine($" {enemy.r} Health - {enemy.hp}");
            //
            //

            bool death = false;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=================================");
            Console.WriteLine("   Welcome to the Colosseum!    ");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Prepare for your first battle againts a colosseum noob!");
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey();

            while (death == false)

            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================");
                Console.WriteLine("         ** BATTLE STATUS **     ");
                Console.WriteLine("==================================");
                Console.ForegroundColor = ConsoleColor.Green;

                // Display Player Stats
                Console.WriteLine($"| Player {player.User} Health: {player.HP}         |");
                Console.WriteLine($"| Defense: {player.Defense}                      |");

                Console.WriteLine("|---------------------------------|");

                // Display Enemy Stats
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"| Enemy {enemy.User} Health: {enemy.HP}           |");
                Console.WriteLine($"| Defense: {enemy.Defense}  PThrust({enemy.ThrustUses}/{enemy.MaxThrustUses})        |");

                Console.WriteLine("|=================================|");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"| (A)ttack   (S)tab({player.StabUses}/{player.MaxStabUses})          |");
                Console.WriteLine($"| (D)efend                        |");
                Console.WriteLine("|=================================|");
                Console.ResetColor();

                Console.WriteLine("Choose an action: ");

                string? choice = Console.ReadLine();                            // Make choice a nullable type in case the user types an empty value

                if (string.Equals(choice, "a", StringComparison.OrdinalIgnoreCase)) //Compare regardless of upper case or lower case
                {
                    player.Attack(enemy);
                    Console.ReadKey();
                }
                else if (string.Equals(choice, "d", StringComparison.OrdinalIgnoreCase))
                {
                    if (player.Defense == 0)
                    {
                        player.Defend();
                        Console.ReadKey();
                    }
                    else if (player.Defense > 0)
                    {
                        Console.WriteLine("You can't defend again until it is broke by an attack!");
                        Console.ReadKey();
                        continue;
                    }
                }
                else if (string.Equals(choice, "s", StringComparison.OrdinalIgnoreCase))
                {
                    if (player.StabUses < player.MaxStabUses)
                    {
                        player.Stab(enemy);

                        Console.ReadKey();
                    }
                    else
                    {
                        player.YoucantStab();
                        continue;
                    }

                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("-- Enemy's turn --");

                enemy.DeathCheck();
                player.DeathCheck();



                int EnemyChoice = random.Next(2, 7);

                if (EnemyChoice == 2 || EnemyChoice == 3)
                {
                    enemy.Attack(player);
                    Console.ReadKey();


                }
                else if (EnemyChoice == 4 || EnemyChoice == 5)
                {
                    enemy.Defend();
                    Console.ReadKey();

                }
                else if (EnemyChoice == 6 || EnemyChoice == 7)

                    if (enemy.ThrustUses < enemy.MaxThrustUses)
                    {
                        enemy.ParalyzingThrust(player);
                        Console.ReadKey();
                    }
                    else
                    {
                        int enemyChoice = random.Next(2, 4);
                        if (enemyChoice == 2 | enemyChoice == 3)
                        {
                            enemy.Attack(player);
                            Console.ReadKey();
                        }
                        else if (enemyChoice == 4)
                        {
                            enemy.Defend();
                            Console.ReadKey();
                        }
                        enemy.ParalyzingThrust(player);
                        Console.ReadKey();
                    }

                Console.WriteLine($"{EnemyChoice} is choice");
                if (enemy.HP <= 0)
                {
                    enemy.DeathCheck();
                    Console.WriteLine("To be contiued...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                if (player.HP <= 0)
                {
                    player.DeathCheck();
                }

            }
        }
    }
}

