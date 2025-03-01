namespace ConsoleAppTurnBased
{
    class Player
    {
        //tommrow make list of text for all text in atack and defend

        // Added properties here, instead of global variables, since they are more idiomatic to C#
        private int ThrustUses { get; set; } = 0;
        private int ParalyzedTurns { get; set; } = 0;
        private int AttackPower { get; set; }
        private int MaxThrustUses { get; set; }

        private Random random = new();

        public int Defense { get; private set; } = 0;                           // This is like having a public getter and a private setter
        public int StabUses { get; private set; } = 0;
        private int BleedTurns { get; set; } = 0;
        public int MaxStabUses { get; private set; }
        public string User { get; private set; }
        public int HP {get; private set; }

        // Since only a few values are being initialized from Program.cs
        // those values can be set on the constructor and the rest can be initialized
        // to default values within the class
        public Player(int attackPower, int currentHealth, int maxstabUses, int maxthrustUses, string user)
        {
            AttackPower = attackPower;
            HP = currentHealth;
            MaxStabUses = maxstabUses;
            MaxThrustUses = maxthrustUses;
            User = user;
        }

        public void Attack(Enemy enemy)                                         // You can reference this Player instance without having to pass it as an argument to this class
        {
            int rng = (int)(random.NextDouble() * 0.45 + 0.75);                 // Cast here to avoid casting below

            // Calculate the random damage
            int randomDamage = rng * AttackPower;
            int randomParalyzedChance = random.Next(4);                         // added random to the name of random generated variables
            int randomAttackNumber = random.Next(1, 8);
            BleedCheck(AttackPower / 2);                                        // Functions should be named with Capital CamelCasing

            if (randomParalyzedChance == 1 && ParalyzedTurns > 0)
            {
                DisplayParalyzedMessage();
            }
            else
            {
                if (ParalyzedTurns > 0)
                {
                    ParalyzedTurns--;
                }

                DisplayAtttackMessage(enemy, randomDamage, randomAttackNumber);      // Functions in C# use capital CamelCasing
                enemy.TakeDamage(randomDamage);                                 // variables use lower case camelCasing
            }
        }


        public void Defend()
        {
            int defenseValue = random.Next(1, 8);
            int randomParalyzedChance = random.Next(1, 4);

            BleedCheck(AttackPower / 2);                                        // BleedCheck is part of Player, it can be called directly

            if (randomParalyzedChance == 1 && ParalyzedTurns > 0)
            {
                DisplayParalyzedMessage();
            }
            else
            {
                if (ParalyzedTurns > 0)
                {
                    ParalyzedTurns--;
                }

                if (Defense <= 0)
                {
                    Defense++;
                    DisplayDefenseMessage(defenseValue);
                }
            }
        }

        public void TakeDamage(int RandomDamage)
        {
            HP -= RandomDamage - Defense;

            if (Defense > 0)
            {
                Defense = 0;
            }
        }

        public void Stab(Enemy enemy)
        {
            int randomStabValue = random.Next(1, 4);
            int rng = (int)(random.NextDouble() * 0.45 + 0.75);                 // Cast here to avoid casting below
            int StabDamage = (int)(1.15 * AttackPower);
            int randStabDamage = rng * StabDamage;
            int paraChance = random.Next(1, 4);

            BleedTurns = 0;
            if (paraChance == 4 && ParalyzedTurns > 0)                               // & is for binary AND, && is for comparisons AND
            {
                DisplayParalyzedMessage();                                      // Named all functions that output to Console as DisplayXXXMessage for consistency
            }
            else
            {
                if (ParalyzedTurns > 0)
                {
                    ParalyzedTurns--;
                }

                DisplayStabMessage(enemy, randomStabValue, randStabDamage);

            }
        }

        public void Takestab(int randStabDamage)
        {
            HP -= randStabDamage - Defense;
            if (Defense > 0)
            {
                Defense = 0;
            }
        }

        public void YoucantStab()
        {
            if (StabUses >= MaxStabUses)
            {
                Console.WriteLine("You cant use stab anymore.");
            }
        }

        public void ApplyBleed()
        {
            if (BleedTurns <= 0)
            {
                BleedTurns += 3;
            }
        }

        public void BleedCheck(int bleedDamage)
        {
            if (BleedTurns > 0)
            {
                HP -= bleedDamage;
                BleedTurns--;
                Console.WriteLine($"{User} took {bleedDamage} damage from bleeding.");
            }
        }

        public void ParalyzingThrust()                                          // This method is not being called
        {
            int randThrustValue = random.Next(1, 4);
            int rng = (int)(random.NextDouble() * 0.45 + 0.75);
            int ThrustDamage = (int)(1.20 * AttackPower);
            int randThrustDamage = rng * ThrustDamage;

            if (ThrustUses < MaxThrustUses)
            {
                DisplayThrustMessage(randThrustValue, randThrustDamage);
            }
        }

        public void ApplyParalyze()
        {
            if (ParalyzedTurns < 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{User} is parylyzed! They may be unable to move");
                Console.ResetColor();
                ParalyzedTurns += 7;
            }
        }

        public void DisplayParalyzedMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{User} is parylyzed!");
            Console.WriteLine("They cant move!");
            Console.ResetColor();
        }

        public void TakeThrust(int randThrustDamage)
        {
            HP -= randThrustDamage - Defense;
            if (Defense > 0)
            {
                Defense = 0;
            }
        }

        public void DeathCheck()
        {
            if (HP <= 0)
            {
                Console.WriteLine($"{User} has been defeated!");
                Console.WriteLine("You died!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        // Moved private functions to the bottom and public to the top
        private void DisplayAtttackMessage(Enemy enemy, int randomDamage, int attackNumbers)
        {
            // This is called a switch expression
            // it's similar to a switch case but the difference it that
            // it returns a value for each case
            var attackMessage = attackNumbers switch
            {
                1 => $"{User} attacked and dealt {randomDamage - enemy.Defense} damage!",
                2 => $"{User} launched a focused strike and dealt {randomDamage - enemy.Defense} damage!",
                3 => $"{User} charged in confidently and dealt {randomDamage - enemy.Defense} damage!",
                4 => $"In a bold move, {User} inflicted {randomDamage - enemy.Defense} damage!",
                5 => $"With a calculated swing, {User} secured {randomDamage - enemy.Defense} damage! ",
                6 => $"{User} focused their energy into a swift strike, inflicting {randomDamage - enemy.Defense} damage!",
                7 => $"Executing a swift maneuver, {User} managed to inflict {randomDamage - enemy.Defense} damage.",
                8 => $"{User} lunged forward confidently, resulting in a solid hit of {randomDamage - enemy.Defense} damage!",
                _ => $"{User} lunged forward confidently, resulting in a solid hit of {randomDamage - enemy.Defense} damage!"
            };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(attackMessage);
            Console.ResetColor();

        }

        private void DisplayDefenseMessage(int defenseValue)
        {
            var defenseMessage = defenseValue switch
            {
                1 => $"{User} held their weapon in a tactical grip, aimed for maximum control.",
                2 => $"{User} twirled their weapon in a defensive dance.",
                3 => $"{User} stood at the ready, weapon anchored in a defensive position, poised for observation and protection.",
                4 => $"The light glinted off {User}’s weapon, held protectively as they maintained a vigilant watch over their surroundings.",
                _ => $"{User} steadied their weapon, maintaining balance and readiness."
            };

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(defenseMessage);
            Console.ResetColor();
        }

        private void DisplayStabMessage(Enemy enemy, int stabValue, int randStabDamage)
        {
            var stabMessage = stabValue switch
            {
                1 => $"{User} rushed their enemy and stabbed doing {randStabDamage - Defense}!",
                2 => $"{User} darted at their foe and thrust their weapon, dealing {randStabDamage - Defense} damage!",
                3 => $"{User} lunged at the opponent and stabbed, inflicting {randStabDamage - Defense} damage!",
                4 => $"{User} leaped into action and executed a stab, causing {randStabDamage - Defense} damage!",
                _ => $"{User} rushed their enemy and stabbed doing {randStabDamage - Defense}!"
            };

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(stabMessage);
            Console.ResetColor();

            enemy.Takestab(randStabDamage);
            enemy.ApplyBleed();

            StabUses++;
        }

        private void DisplayThrustMessage(int randThrustValue, int randThrustDamage)
        {
            var thrustMessage = randThrustValue switch
            {
                1 => $"Like lightning, {User} dashed to their opponent and thrust their in there weakpoint doing {randThrustDamage - Defense}. ",
                _ => $"Like lightning, {User} dashed to their opponent and thrust their in there weakpoint doing {randThrustDamage - Defense} "
            };

            Console.WriteLine(thrustMessage);
            TakeThrust(randThrustDamage);
            ApplyParalyze();
            ThrustUses++;
        }
    }
}
