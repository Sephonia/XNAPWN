using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAGUI
{
    class Component
    {
        private Texture2D square = null;

        public Vector2 Position { get; set; }
        public Rectangle Rect { get; set; }

        private bool hovering = false;
        private bool clicked = false;

        public delegate void ComponentEvent(Component sender);

        public event ComponentEvent onMouseOver = null;
        public event ComponentEvent onMouseOut = null;
        public event ComponentEvent onMouseDown = null;
        public event ComponentEvent onMouseUp = null;
        public event ComponentEvent onClick = null;

        public Component(Rectangle whereItsAt) 
        {
            Rect = whereItsAt;
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            if(Rect.Contains(mouse.X, mouse.Y))
            {
                if(!hovering)
                {
                    if(onMouseOver != null)
                        onMouseOver(this);

                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if(!clicked)
                            if(onMouseDown != null)
                                onMouseDown(this);

                        clicked = false;
                    }
                    else if(mouse.LeftButton == 

                    }

                    hovering = true;
                }

                
            }
        }
    }
}
