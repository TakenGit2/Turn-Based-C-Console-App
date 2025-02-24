using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ConsoleAppTurnBased;

namespace ConsoleAppTurnBased
{
    class Player
    {
        //tommrow make list of text for all text in atack and defend
        private int attackPower;
        private int CurrentHealth;
        private int MaxHealth;
        private int Defense;
        private int t_Defense;
        private int bleedturns;

        private int stabUses;
        private int maxstabUses;

        private string User;


        Random random = new Random();

        public int hp { get { return CurrentHealth; } }
        public string r { get { return User; } }

        public int d { get { return Defense; } }

        public int s { get { return stabUses; } }
        public int ms { get { return maxstabUses; } }

        public int a { get { return attackPower; } }

        // ininiliaze fields
        public Player(int attackPower, int CurrentHealth, int MaxHealth, int Defense, int t_Defense, int maxstabUses, int stabUses, int bleedturns, string User)
        {
            this.attackPower = attackPower;
            this.CurrentHealth = CurrentHealth;
            this.MaxHealth = MaxHealth;
            this.Defense = Defense;
            this.t_Defense = t_Defense;

            this.bleedturns = bleedturns;

            this.User = User;
            this.stabUses = stabUses;
            this.maxstabUses = maxstabUses;


        }

        public void Attack(Enemy unitthatsgetAttacking)
        {


            unitthatsgetAttacking.bleedCheck(unitthatsgetAttacking, attackPower / 2);
            double rng = random.NextDouble() * 0.45 + 0.75;

           
            int RandomDamage = (int)(rng * attackPower);


            
            int AttackNumbers = random.Next(1, 8);
            //diff text
            switch (AttackNumbers)
            {
                case 1:
                    Console.WriteLine($"With a sharp focus, {User} stepped in and dealt {RandomDamage - Defense} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 2:
                    Console.WriteLine($"{User} executed a well-timed strike, resulting in {RandomDamage - Defense} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 3:
                    Console.WriteLine($"With a burst of speed, {User} darted in and inflicted {RandomDamage - Defense} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 4:
                    Console.WriteLine($"{User} seized the moment and dealt {RandomDamage - Defense} damage with clarity!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 5:
                    Console.WriteLine($"With a calculated swing, {User} secured {RandomDamage - Defense} damage! ");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 6:
                    Console.WriteLine($"{User} focused their energy into a swift strike, inflicting {RandomDamage - Defense} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 7:
                    Console.WriteLine($"Executing a swift maneuver, {User} managed to inflict {RandomDamage - Defense} damage.");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 8:
                    Console.WriteLine($"{User} lunged forward confidently, resulting in a solid hit of {RandomDamage - Defense} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;






            }
        }


        public void Defense_M(Enemy bleedout)
        {

            bleedout.bleedCheck(bleedout, attackPower / 2);

            int TextsNumbers = random.Next(1, 4);
            if (t_Defense <= 0 | Defense <= 0)
            {
                Defense += 2;
                t_Defense++;
                if (TextsNumbers == 1)
                {
                    Console.WriteLine($"{User} held their weapon in a tactical grip, aimed for maximum control. ");

                }
                else if (TextsNumbers == 2)
                {
                    Console.WriteLine($"{User} twirled their weapon in a defensive dance. ");
                }
                else if (TextsNumbers == 3)
                {
                    Console.WriteLine($"{User} stood at the ready, weapon anchored in a defensive position, poised for observation and protection.");
                }
                else if (TextsNumbers == 4)
                {
                    Console.WriteLine($"The light glinted off {User}’s weapon, held protectively as they maintained a vigilant watch over their surroundings.");
                }
            }

        }








        public void TakeDamage(int RandomDamage)
        {

            CurrentHealth -= RandomDamage - Defense;

            if (t_Defense > 0)
            {
                t_Defense = 0;
                Defense = 0;
            }



        }
        public void Stab(Enemy stabnbleedenemy)
        {

            int stabtext = random.Next(1, 4);


            double rng = random.NextDouble() * 0.45 + 0.75;
            int StabDamage = (int)(1.15 * (int)attackPower);
            int randStabDamage = (int)(rng * StabDamage);

            bleedturns = 0;


            if (stabUses < maxstabUses)
            {
// i dont feel like adding commets to everything

                switch (stabtext)
                {
                    case 1:
                        Console.WriteLine($"{User} rushed their enemy and stabbed doing {randStabDamage - Defense}!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower / 2);

                        stabUses++;
                        break;

                    case 2:
                        Console.WriteLine($"{User} darted at their foe and thrust their weapon, dealing {randStabDamage - Defense} damage!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower / 2);
                        stabUses++;
                        break;

                    case 3:
                        Console.WriteLine($"{User} lunged at the opponent and stabbed, inflicting {randStabDamage - Defense} damage!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower / 2);
                        stabUses++;
                        break;

                    case 4:
                        Console.WriteLine($"{User} leaped into action and executed a stab, causing {randStabDamage - Defense} damage!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower / 2);
                        stabUses++;
                        break;




                    default:
                        Console.WriteLine($"{User} rushed their enemy and stabbed doing {randStabDamage - Defense}!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower / 2);
                        stabUses++;
                        break;
                }
            }

        }




        public void youcantStab()
        {
            if (stabUses >= maxstabUses)
            {
                Console.WriteLine("You cant use stab anymore.");
            }
        }
        public void takestab(int randStabDamage)
        {


            CurrentHealth -= randStabDamage - Defense;



        }




        public void ApplyBleed(int bleeddamage)
        {
            if (bleedturns <= 0)
            {
                bleedturns += 2;

            }








        }

        public void bleedCheck(Enemy bleedout, int bleeddamage)
        {

            if (bleedturns > 0)
            {
                CurrentHealth -= bleeddamage;
                bleedturns--;
                Console.WriteLine($"{User} took {bleeddamage} damage from bleeding.");

            }
            else if (bleedturns == 0)
            {
                bleedturns = 0;
              


            }

        }
    }
}
