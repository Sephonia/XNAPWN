using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace XNAGUI
{
    public class Component
    {
        public Rectangle Rect { get; set; }

        private bool hovering = false;
        private bool released = false;
        private bool clicked = false;

        // to make a delegate, do the first line (making the delegate name and giving it it's type) then create events with variable names of the delegate type.
        public delegate void ComponentEvent(Component sender);

        public event ComponentEvent onMouseOver = null;
        public event ComponentEvent onMouseOut = null;
        public event ComponentEvent onMouseDown = null;
        public event ComponentEvent onMouseUp = null;
        public event ComponentEvent onClick = null;

        public Component(Rectangle whereItsAt)
        {
            Rect = whereItsAt;
            UIManager.Add(this);
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState(); // if any of the buttons are clicked, calling "(this)" it refers to the button that was clicked.
            if (Rect.Contains(mouse.X, mouse.Y)) 
            {
                if (!hovering)
                    if (onMouseOver != null) 
                        onMouseOver(this); 

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (!clicked)
                        if (onMouseDown != null)
                            onMouseDown(this); 

                    clicked = true;
                    released = false;
                }
                else if (mouse.LeftButton == ButtonState.Released)
                {
                    if (released)
                        if (onMouseUp != null)
                            onMouseUp(this);
                    if (clicked)
                        if (onClick != null)
                            onClick(this);
                    clicked = false;
                    released = true;
                }
                hovering = true;
            }
            else
            {
                if (hovering)
                    if (onMouseOut != null)
                        onMouseOut(this);
                hovering = false;
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
