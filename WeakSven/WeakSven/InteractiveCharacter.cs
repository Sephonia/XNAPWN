using System.Collections.Generic;
namespace WeakSven
{
	class InteractiveCharacter : Character
	{
        protected int health = 100;
        public int Health { get; protected set; }
		public int Attack { get; protected set; }
		public int Defense { get; protected set; }
		public int Money { get; protected set; }      


        public InteractiveCharacter()
            : base()
        {


        }
       
        
	}
}