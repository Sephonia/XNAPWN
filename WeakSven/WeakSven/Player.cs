﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace WeakSven
{
	// sealed
	class Player : InteractiveCharacter
	{
		#region Singleton Stuff
		private static Player instance = null;
		public static Player Instance
		{
			get
			{
				if (instance == null)
					instance = new Player();

				return instance;
			}
		}


		public Player() : base() { }
		#endregion

		//public AudioSFX bing = new AudioSFX();

		public void SetName(string name) { Name = name; }

        protected int health = 100;
        public int GetHealth
        {
            get { return Health; }
            set
            {
                Health = value;
                if (Health < 0)
                    Health = 0;
            }
        }

		public override void Load(ContentManager Content, string imageFile)
		{
			base.Load(Content, imageFile);

			//bing.Sound = Content.Load<SoundEffect>("Audio/SFX/bing");
		}

		public override void Update(GameTime gameTime)
		{
			// if (((int)gameTime.TotalGameTime.TotalSeconds) % 3 == 0)
			//	bing.Play(gameTime);

			// TODO:  Change player controls to fit your game

			if (Keyboard.GetState().IsKeyDown(Keys.W) ||
				Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				Velocity.Y = -Speed;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.A) ||
				Keyboard.GetState().IsKeyDown(Keys.Left))
			{
				Velocity.X = -Speed;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.S) ||
				Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				Velocity.Y = Speed;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.D) ||
				Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				Velocity.X = Speed;
			}
			else
				Velocity = Vector2.Zero;
            base.Update(gameTime);
		}

       
    }

}