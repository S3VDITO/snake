using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    internal class GamePoint : IDisposable
    {
        private Vector2D _origin = Vector2D.Zero;
        private char _pointSymbol = '#';

        public GamePoint(Vector2D origin, char pointSymbol)
        {
            _origin = origin;
            PointSymbol = pointSymbol;
        }

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

        public void Draw() => DrawPoint(PointSymbol);

        public void Clear() => DrawPoint(' ');

        public void Delete() => Dispose();

        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }

        private void DrawPoint(char symbol)
        {
            Console.SetCursorPosition(Origin.X, Origin.Y);
            Console.Write(symbol);
        }
    }
}
