using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WeakSven
{
    class Combat
    {

        protected bool playerDamaged = false;
        protected double damageTimer = 3.00;
        protected double timeDamaged = 0.00;
        

        private static Combat instance = null; //create a private, static version of instance and make it null.

        public static Combat Instance  //create a public version and get a new instance if it doesn't exist (which it doesn't at this point).
        {
            get
            {
                if (instance == null)
                    instance = new Combat();

                return instance;
            }
        }

        List<Character> combatants = new List<Character>();

        public void AddCombatant(Character newCombatant)
        {
            if (newCombatant == Player.Instance)
                throw new Exception("Player instances don't belong within this list.");

            if (combatants.Contains(newCombatant))
                return;

            combatants.Add(newCombatant);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Character other in combatants)
            {
                if (Player.Instance.rect.Intersects(other.rect) && playerDamaged == false)
                {
                    Player.Instance.Health -= 5;
                    timeDamaged = gameTime.TotalGameTime.Seconds; ;
                    playerDamaged = true;
                }

                if (gameTime.TotalGameTime.Seconds - timeDamaged == damageTimer)
                {
                    playerDamaged = false;
                    timeDamaged = 0;
                }
            }
        }
    }
}
