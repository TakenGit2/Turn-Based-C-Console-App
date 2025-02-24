using System.ComponentModel.Design;
using System.Security.Authentication;

using ConsoleAppTurnBased;

namespace ConsoleAppTurnBased
{ 

    internal class Program
    {
        static void Main(string[] args)
        {

            Player player = new Player(5, 40, 40, 0, 0, 3, 0, 0, "Water");
            Enemy enemy = new Enemy(7, 40, 40, 0, 0, 3, 0, 0, "Noob");

            Random random = new Random();
            //{player.r} Defense - {player.d}
            //{enemy.r} Health - {enemy.hp}");
            //Console.WriteLine($"{enemy.r} Defense - {enemy.d}");
            //Console.WriteLine($" {enemy.r} Health - {enemy.hp}");
            //
            //

            Console.WriteLine("Welcome to the colesumm");
            Console.ReadKey();
            Console.WriteLine("Well it time for your first fight againts a collesum noob");
            Console.ReadKey();
            while (true)

            {
                Console.WriteLine("========================");
                Console.WriteLine($"|{player.r} Health - {player.hp}|");
                Console.WriteLine($"|{player.r} Defense - {player.d}|");
                Console.WriteLine($"|{enemy.r} Health -  {enemy.hp}|");
                Console.WriteLine($"|{enemy.r} Defense - {enemy.d}|");
                Console.WriteLine("===========================");
                Console.WriteLine($"| (A)ttack   (S)tab({player.s}/{player.ms})|");
                Console.WriteLine($"| (D)efend              |");

                Console.WriteLine("===========================");
                string choice = Console.ReadLine();


                if (choice == "a")
                {
                    player.Attack(enemy);
                    Console.ReadKey();
                }
                else if (choice == "d")
                {
                    player.Defense_M(enemy);
                    Console.ReadKey();
                }
                else if (choice == "s")
                {
                    if (player.s < player.ms)
                    {
                        player.Stab(enemy);

                        Console.ReadKey();
                    }
                    else
                    {
                        player.youcantStab();
                        continue;
                    }

                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                Console.WriteLine("-- Enemy turn --");

                int EnemyChoice = random.Next(2, 5);

                if (EnemyChoice == 2 | EnemyChoice == 3)
                {
                    enemy.Attack(player, player);
                    Console.ReadKey();
                   

                }
                else if (EnemyChoice == 4 | EnemyChoice == 5)
                {
                    enemy.Defense_M(player);
                    Console.ReadKey();
                    
                }
                else
                {
                    Console.WriteLine("broke");
                }


            }




        }
    }
}

