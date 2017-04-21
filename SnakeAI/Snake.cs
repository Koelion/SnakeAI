using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI
{
    public enum MoveDirecion
    {
        Left,
        Right,
        Up,
        Down
    }
    public class Body
    {
        public int X;
        public int Y;
        public Body(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class Snake
    {
        public List<Body> body { get; private set; } = new List<Body>(); //first is head
        public Snake(int x, int y)
        {
            body.Add(new Body(x, y));
        }

        public delegate void OnMoveHandler();
        event OnMoveHandler OnMove;
        public void Move(MoveDirecion Direction)
        {
            switch(Direction)
            {
               case MoveDirecion.Left:
                    Move(-1, 0);
                    break;
                case MoveDirecion.Right:
                    Move(1, 0);
                    break;
                case MoveDirecion.Up:
                    Move(0, -1);
                    break;
                case MoveDirecion.Down:
                    Move(0, 1);
                    break;
            }

            OnMove?.Invoke();
        }

        private void Move(int x, int y)
        {
            for(int i=body.Count; i>0; i--)
            {
                body[i].X = body[i - 1].X;
                body[i].Y = body[i - 1].Y;
            }
            body[0].X += x;
            body[0].Y += y;
        }


    }
}
