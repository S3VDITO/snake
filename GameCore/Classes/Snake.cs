using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace GameCore
{
    [Flags]
    internal enum SnakeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class Snake
    {
        private SnakeDirection SnakeDirection { get; set; } = SnakeDirection.Up;
        private bool IsAlive { get; set; } = true;
        private Point Tail { get; set; }

        public float Speed = 0.05f;

        public List<Point> SnakeBody { get; set; } = new List<Point>();

        public Snake(Vector2D spawnOrigin, int snakeSize = 3)
        {
            for (int i = 0; i < snakeSize; i++)
            {
                Point point = new Point(spawnOrigin + new Vector2D(0, i), '*');
                point.Draw();

                SnakeBody.Add(point);
            }

            GameThread.Thread(SnakeMovement());
            GameThread.Thread(SnakeControl());
        }

        public IEnumerator SnakeControl()
        {
            void ChangeDirection(ConsoleKey key)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (SnakeDirection == SnakeDirection.Up || SnakeDirection == SnakeDirection.Down)
                            break;

                        SnakeDirection = SnakeDirection.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (SnakeDirection == SnakeDirection.Up || SnakeDirection == SnakeDirection.Down)
                            break;

                        SnakeDirection = SnakeDirection.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (SnakeDirection == SnakeDirection.Left || SnakeDirection == SnakeDirection.Right)
                            break;

                        SnakeDirection = SnakeDirection.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (SnakeDirection == SnakeDirection.Left || SnakeDirection == SnakeDirection.Right)
                            break;

                        SnakeDirection = SnakeDirection.Right;
                        break;
                }
            }

            while (IsAlive)
            {
                yield return GameThread.KeyPressWaiter(ChangeDirection, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
            }

            yield break;
        }

        public IEnumerator SnakeMovement()
        {
            while (IsAlive)
            {
                yield return GameThread.Wait(Speed);

                Tail = SnakeBody[SnakeBody.Count - 1];
                SnakeBody.Remove(Tail);

                switch (SnakeDirection)
                {
                    case SnakeDirection.Up:
                        if (SnakeBody[0].Origin.Y == 0)
                            Tail.Origin = new Vector2D(SnakeBody[0].Origin.X, GameInitializer.Height - 0);
                        else
                            Tail.Origin = SnakeBody[0].Origin - new Vector2D(0, 1);
                        break;
                    case SnakeDirection.Down:
                        if (SnakeBody[0].Origin.Y == GameInitializer.Height)
                            Tail.Origin = new Vector2D(SnakeBody[0].Origin.X, 0);
                        else
                            Tail.Origin = SnakeBody[0].Origin + new Vector2D(0, 1);
                        break;
                    case SnakeDirection.Left:
                        if (SnakeBody[0].Origin.X == 0)
                            Tail.Origin = new Vector2D(GameInitializer.Width, SnakeBody[0].Origin.Y);
                        else
                            Tail.Origin = SnakeBody[0].Origin - new Vector2D(1, 0);
                        break;
                    case SnakeDirection.Right:
                        if (SnakeBody[0].Origin.X == GameInitializer.Width)
                            Tail.Origin = new Vector2D(0, SnakeBody[0].Origin.Y);
                        else
                            Tail.Origin = SnakeBody[0].Origin + new Vector2D(1, 0);
                        break;
                }

                if (IsHit())
                    break;

                if (IsEat())
                {
                    SnakeBody.Add(new Point(SnakeBody[SnakeBody.Count - 1].Origin, '*'));
                    GameInitializer.Map[ObjectType.Eat][0].Origin = GameFunctions.RandomXY();
                }

                SnakeBody.Insert(0, Tail);
            }

            yield break;
        }

        public bool IsHit()
        {
            bool IsHitSelf = SnakeBody.Where(snakePoint => snakePoint.Origin.Equals(Tail.Origin)).Count() != 0;
            bool IsHitBarrier = GameInitializer.Map[ObjectType.Barrier].Where(barrierPoint => barrierPoint.Origin.Equals(Tail.Origin)).Count() != 0;

            return IsHitSelf || IsHitBarrier;
        }

        public bool IsEat() => Tail.Origin.Equals(GameInitializer.Map[ObjectType.Eat][0].Origin);
    }
}
