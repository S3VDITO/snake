using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class Point
    {
        private Vector2D _origin = Vector2D.Zero;
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
        public char PointSymbol { get; set; } = '#';

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
