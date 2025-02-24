using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ConsoleAppTurnBased;

namespace ConsoleAppTurnBased
{
    class Enemy
    {


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

        
        public Enemy(int attackPower, int CurrentHealth, int MaxHealth, int Defense, int t_Defense, int maxstabUses, int stabUses, int bleedturns, string User)
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

        public void Attack(Player unitthatsgetAttacking, Player player)
        {
            double rng = random.NextDouble() * 0.45 + 0.75;

            // Calculate the random damage
            int RandomDamage = (int)(rng * attackPower);

            int AttackNumbers = random.Next(1, 8);



            switch (AttackNumbers)
            {
                case 1:
                    Console.WriteLine($"{User} attacked and dealt {RandomDamage - player.d} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 2:
                    Console.WriteLine($"{User} launched a focused strike and dealt {RandomDamage - player.d} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 3:
                    Console.WriteLine($"{User} charged in confidently and dealt {RandomDamage - player.d} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 4:
                    Console.WriteLine($"In a bold move, {User} inflicted {RandomDamage - player.d} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 5:
                    Console.WriteLine($"With a calculated swing, {User} secured {RandomDamage - player.d} damage! ");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 6:
                    Console.WriteLine($"{User} focused their energy into a swift strike, inflicting {RandomDamage - player.d} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                case 7:
                    Console.WriteLine($"Executing a swift maneuver, {User} managed to inflict {RandomDamage - player.d} damage.");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;


                case 8:
                    Console.WriteLine($"{User} lunged forward confidently, resulting in a solid hit of {RandomDamage - player.d} damage!");
                    unitthatsgetAttacking.TakeDamage(RandomDamage);
                    break;

                default:
                    Console.WriteLine($"{User} lunged forward confidently, resulting in a solid hit of {RandomDamage - player.d} damage!");
                    break;







            }

        }


        public void Defense_M(Player bleedout)
        {
            int TextsNumbers = random.Next(1, 8);
            if (Defense <= 0)
            {
                Defense++;


                switch (TextsNumbers)
                {
                    case 1:
                        Console.WriteLine($"{User} positioned their weapon to block, eyes locked on the enemy. ");
                        break;

                    case 2:
                        Console.WriteLine($"{User} stood resolute, weapon raised high in preparation.");
                        break;

                    case 3:
                        Console.WriteLine($"{User} positioned their weapon at the ready, eyes scanning for danger.");
                        break;

                    case 4:
                        Console.WriteLine($"{User} held their weapon firmly, creating an imposing stance.");
                        break;

                    case 5:
                        Console.WriteLine($"{User} brought their weapon up to guard their body from strikes. ");
                        break;

                    case 6:
                        Console.WriteLine($"{User} steadied their weapon, ready to deflect any attack.");
                        break;

                    case 7:
                        Console.WriteLine($"{User} gripped their weapon, poised defensivly.");
                        break;

                    case 8:
                        Console.WriteLine($"{User} steadied their weapon, maintaining balance and readiness.");
                        break;

                    default:
                        Console.WriteLine($"{User} steadied their weapon, maintaining balance and readiness.");
                        break;
                }


            }

        }








        public void TakeDamage(int RandomDamage)
        {
            CurrentHealth -= RandomDamage - Defense;
            if (t_Defense > 0)
            {
                Defense = 0;
                t_Defense = 0;

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


                switch (stabtext)
                {
                    case 1:
                        Console.WriteLine($"{User} rushed their enemy and stabbed doing {randStabDamage - Defense}!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower/2);
                        stabUses++;
                        break;

                    case 2:
                        Console.WriteLine($"{User} darted at their foe and thrust their weapon, dealing {randStabDamage - Defense} damage!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower/2);
                        stabUses++;
                        break;

                    case 3:
                        Console.WriteLine($"{User} lunged at the opponent and stabbed, inflicting {randStabDamage - Defense} damage!");
                        stabnbleedenemy.takestab(randStabDamage);
                        stabnbleedenemy.ApplyBleed(attackPower/2);
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
                        stabnbleedenemy.ApplyBleed(attackPower/2);
                        stabUses++;
                        break;
                }
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

