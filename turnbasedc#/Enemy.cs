namespace ConsoleAppTurnBased
{
    class Enemy
    {
        // Added properties here, instead of global variables, since they are more idiomatic to C#
        public int ThrustUses { get; set; } = 0;
        private int ParalyzedTurns { get; set; }
        private int AttackPower { get; set; }
        public int MaxThrustUses { get; private set; }

        Random random = new Random();

        public int Defense { get; private set; } = 0;
        public int StabUses { get; private set; }
        private int BleedTurns { get; set; }
        private int MaxStabUses { get; set; }
        public string User { get; private set; }
        public int HP { get; private set; }


        // Since only a few values are being initialized from Program.cs
        // those values can be set on the constructor and the rest can be initialized
        // to default values within the class
        public Enemy(int attackPower, int hp, int maxStabUses, int maxthrustUses, string user)
        {
            AttackPower = attackPower;
            HP = hp;
            MaxThrustUses = maxthrustUses;
            MaxStabUses = maxStabUses;
            User = user;
        }

        public void Attack(Player attacker)                                     // No need to pass Enemy here as an argument
        {
            int rng = (int)(random.NextDouble() * 0.45 + 0.75);

            // Calculate the random damage
            int randomDamage = rng * AttackPower;
            int randomParalyzedChance = random.Next(1, 4);
            int randomAttackNumber = random.Next(1, 8);
            BleedCheck(AttackPower / 2);
            Console.ReadKey();
            if (randomParalyzedChance == 4 && ParalyzedTurns > 0)               // & is for binary AND, && is for boolean comparison
            {
                ParalyzeCheck();
            }
            else
            {
                DisplayAttackMessage(attacker, randomDamage, randomAttackNumber); // Moved the switch to a function
            }
        }

        public void Defend()
        {
            int randomDefenseValue = random.Next(1, 8);
            int randomParalyzedChance = random.Next(1, 4);
            BleedCheck(AttackPower / 2);
            Console.ReadKey();
            if (randomParalyzedChance == 4 & ParalyzedTurns > 0)
            {
                ParalyzeCheck();
            }
            else
            {
                if (Defense <= 0)
                {
                    Defense++;
                    DisplayDefenseMessage(randomDefenseValue);
                }
            }
        }

        public void TakeDamage(int randomDamage)
        {
            HP -= randomDamage - Defense;
            if (Defense > 0)
            {
                Defense = 0;                                                    // t_Defense was being used as Defense so I removed it
            }
        }

        public void Stab(Player playerthrust)
        {

            int randomStabValue = random.Next(1, 4);
            int rng = (int)(random.NextDouble() * 0.45 + 0.75);
            int StabDamage = (int)(1.15 * AttackPower);
            int randStabDamage = rng * StabDamage;

            BleedCheck(AttackPower / 2);

            BleedTurns = 0;
            int paraChance = random.Next(1, 4);
            if (paraChance == 4 & ParalyzedTurns > 0)
            {
                playerthrust.DisplayParalyzedMessage();
            }
            else
            {
                DisplayStabMessage(randomStabValue, randStabDamage);
                Takestab(randStabDamage);
                ApplyBleed();
                StabUses++;
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

        public void ApplyBleed()
        {
            if (BleedTurns <= 0)
            {
                BleedTurns += 2;
            }
        }

        public void BleedCheck(int bleedDamage)
        {
            if (BleedTurns > 0)
            {
                HP -= bleedDamage;
                BleedTurns--;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{User} took {bleedDamage} damage from bleeding.");
                Console.ResetColor();
            }
            else if (BleedTurns == 0)
            {
                BleedTurns = 0;
            }
        }

        public void ParalyzingThrust(Player attacker)
        {
            int randThrustValue = random.Next(1, 4);
            int rng = (int)(random.NextDouble() * 0.45 + 0.75);
            int ThrustDamage = (int)(1.20 * AttackPower);
            int randThrustDamage = rng * ThrustDamage;
            BleedCheck(AttackPower / 2);

            if (ThrustUses < MaxThrustUses)
            {
                ThrustUses++;
                DisplayThrustMessage(attacker, randThrustValue, randThrustDamage);
                attacker.TakeThrust(randThrustDamage);
                attacker.ApplyParalyze();
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

        public void ParalyzeCheck()
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
                HP = 0;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{User} has been defeated! Nice job you deafeated your first foe.");
                Console.ResetColor();
            }
        }

        // Moved private functions to the bottom and kept public functions at the top
        private void DisplayAttackMessage(Player attacker, int randomDamage, int randomAttackNumber)
        {
            var attackMessage = randomAttackNumber switch
            {
                1 => $"{User} attacked and dealt {randomDamage - attacker.Defense} damage!",
                2 => $"{User} launched a focused strike and dealt {randomDamage - attacker.Defense} damage!",
                3 => $"{User} charged in confidently and dealt {randomDamage - attacker.Defense} damage!",
                4 => $"In a bold move, {User} inflicted {randomDamage - attacker.Defense} damage!",
                5 => $"With a calculated swing, {User} secured {randomDamage - attacker.Defense} damage! ",
                6 => $"{User} focused their energy into a swift strike, inflicting {randomDamage - attacker.Defense} damage!",
                7 => $"Executing a swift maneuver, {User} managed to inflict {randomDamage - attacker.Defense} damage.",
                8 => $"{User} lunged forward confidently, resulting in a solid hit of {randomDamage - attacker.Defense} damage!",
                _ => $"{User} lunged forward confidently, resulting in a solid hit of {randomDamage - attacker.Defense} damage!"
            };

            attacker.TakeDamage(randomDamage);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(attackMessage);
            Console.ResetColor();
        }

        private void DisplayDefenseMessage(int randomDefenseValue)
        {

            var defenseMessage = randomDefenseValue switch
            {
                1 => $"{User} positioned their weapon to block, eyes locked on the enemy. ",
                2 => $"{User} stood resolute, weapon raised high in preparation.",
                3 => $"{User} positioned their weapon at the ready, eyes scanning for danger.",
                4 => $"{User} held their weapon firmly, creating an imposing stance.",
                5 => $"{User} brought their weapon up to guard their body from strikes. ",
                6 => $"{User} steadied their weapon, ready to deflect any attack.",
                7 => $"{User} gripped their weapon, poised defensivly.",
                8 => $"{User} steadied their weapon, maintaining balance and readiness.",
                _ => $"{User} steadied their weapon, maintaining balance and readiness."
            };

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(defenseMessage);
            Console.ResetColor();
        }

        private void DisplayStabMessage(int randomStabValue, int randStabDamage)
        {
            if (StabUses < MaxStabUses)
            {
                var stabMessage = randomStabValue switch
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
            }
        }

        private void DisplayThrustMessage(Player attacker, int randThrustValue, int randThrustDamage)
        {
            var thrustMessage = randThrustValue switch
            {
                1 => $"With the speed of lightning, {User} executed a paralyzing thrust, striking in an instant and possibly immobilizing their enemy, inflicting {randThrustDamage - attacker.Defense} damage.",
                2 => $"With a swift motion, {User} executed a paralyzing thrust, piercing through and possibly rendering their enemy immobilized, inflicting {randThrustDamage - attacker.Defense} damage.",
                3 => $"In a flash, {User} delivered a paralyzing thrust, puncturing flesh and possibly immobilizing the enemy, inflicting {randThrustDamage - attacker.Defense} damage.",
                4 => $"In a blur of action, {User} performed a paralyzing thrust, making contact and possibly immobilizing the adversary, inflicting {randThrustDamage - attacker.Defense} damage.",
                _ => $"Like lightning, {User} dashed to their opponent and thrust their in there weakpoint doing {randThrustDamage - attacker.Defense}"
            };

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(thrustMessage);
            Console.ResetColor();
        }
    }

}
