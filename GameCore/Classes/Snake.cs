using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace GameCore
{
    [Flags]
    public enum SnakeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Snake
    {
        private bool IsAlive { get; set; } = true;
        private Vector2D SpawnOrigin { get; set; }

        public List<Point> SnakeBody { get; set; } = new List<Point>();
        private Point Tail { get; set; }

        private SnakeDirection SnakeDirection { get; set; } = SnakeDirection.Up;

        public Snake(Vector2D spawnOrigin)
        {
            SpawnOrigin = spawnOrigin;

            for (int i = 0; i < 3; i++)
            {
                Point point = new Point(spawnOrigin + new Vector2D(0, i), '*');
                point.Draw();

                SnakeBody.Add(point);
            }

            GameThread.Thread(SnakeMovement());
            GameThread.Thread(SnakeControl());
        }

        private IEnumerator ResetSnake()
        {
            IsAlive = false;
            SnakeBody.Clear();

            for (int i = 0; i < 3; i++)
            {
                Point point = new Point(SpawnOrigin + new Vector2D(0, i), '*');
                point.Draw();

                SnakeBody.Add(point);
            }

            GameManager.CreateBarriers();

            yield return GameThread.Wait(.5f);
            foreach (var body in SnakeBody)
                body.Clear();
            yield return GameThread.Wait(.5f);
            foreach (var body in SnakeBody)
                body.Draw();
            yield return GameThread.Wait(.5f);
            foreach (var body in SnakeBody)
                body.Clear();
            yield return GameThread.Wait(.5f);
            foreach (var body in SnakeBody)
                body.Draw();

            IsAlive = true;

            GameThread.Thread(SnakeMovement());
            GameThread.Thread(SnakeControl());
        }

        public IEnumerator SnakeMovement()
        {
            while (IsAlive && !IsHit())
            {
                yield return GameThread.Wait(0.05f);
                Tail = SnakeBody[SnakeBody.Count - 1];
                SnakeBody.Remove(Tail);
                switch (SnakeDirection)
                {
                    case SnakeDirection.Up:
                        if (SnakeBody[0].Origin.Y == 0)
                            Tail.Origin = new Vector2D(SnakeBody[0].Origin.X, GameManager.Height - 0);
                        else
                            Tail.Origin = SnakeBody[0].Origin - new Vector2D(0, 1);
                        break;
                    case SnakeDirection.Down:
                        if (SnakeBody[0].Origin.Y == GameManager.Height)
                            Tail.Origin = new Vector2D(SnakeBody[0].Origin.X, 0);
                        else
                            Tail.Origin = SnakeBody[0].Origin + new Vector2D(0, 1);
                        break;
                    case SnakeDirection.Left:
                        if (SnakeBody[0].Origin.X == 0)
                            Tail.Origin = new Vector2D(GameManager.Width, SnakeBody[0].Origin.Y);
                        else
                            Tail.Origin = SnakeBody[0].Origin - new Vector2D(1, 0);
                        break;
                    case SnakeDirection.Right:
                        if (SnakeBody[0].Origin.X == GameManager.Width)
                            Tail.Origin = new Vector2D(0, SnakeBody[0].Origin.Y);
                        else
                            Tail.Origin = SnakeBody[0].Origin + new Vector2D(1, 0);
                        break;
                }
                SnakeBody.Insert(0, Tail);

                if (IsEat())
                    SnakeBody.Add(new Point(SnakeBody[1].Origin, '*'));
            }
            yield break;
        }

        public bool IsHit()
        {
            void ClearOldSnake()
            {
                foreach (var snakePart in SnakeBody)
                    snakePart.Clear();
            }

            foreach (var barrier in GameData.Barriers)
            {
                foreach (var snakePart in SnakeBody)
                {
                    if (barrier.Point.Origin.Equals(snakePart.Origin))
                    {
                        ClearOldSnake();
                        GameThread.Thread(ResetSnake());
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsEat()
        {
            foreach (var snakePart in SnakeBody)
            {
                if (snakePart.Origin.Equals(Eat.EatPoint.Origin))
                {
                    Eat.MoveEat();
                    return true;
                }
            }
            return false;
        }

        public IEnumerator SnakeControl()
        {
            while (IsAlive)
            {
                yield return GameThread.KeyPressWaiter(new Action<ConsoleKey>(key =>
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
                }), ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
            }
            yield break;
        }
    }
}
