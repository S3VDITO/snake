using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    internal enum ObjectType
    {
        Eat,
        Barrier
    }

    internal class Point
    {
        private Vector2D _origin = Vector2D.Zero;
        private char _pointSymbol = '#';

        public Vector2D Origin
        {
            get => _origin;

            set
            {
                Clear();
                _origin = value;
                Draw();
            }
        }

        public char PointSymbol 
        {
            get => _pointSymbol;
            set
            {
                _pointSymbol = value;
                Draw();
            }
        }

        public Point(Vector2D origin, char pointSymbol)
        {
            _origin = origin;
            PointSymbol = pointSymbol;
        }

        public void Draw() => DrawPoint(PointSymbol);

        public void Clear() => DrawPoint(' ');

        private void DrawPoint(char symbol)
        {
            Console.SetCursorPosition(Origin.X, Origin.Y);
            Console.Write(symbol);
        }
    }
}
