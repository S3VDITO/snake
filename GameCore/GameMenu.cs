using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

using static GameCore.GameCoroutine;
using static GameCore.GameFunctions;

namespace GameCore
{
    internal class GameMenu : IDisposable
    {
        private int xOffset = 30;
        private int yOffset = 8;

        public int Cursor { private set; get; } = 1;
        public string SelectedSection { private set; get; }

        public MenuType MenuType { private set; get; }

        private Dictionary<MenuType, Dictionary<string, List<GamePoint>>> MenuDrawMap = new Dictionary<MenuType, Dictionary<string, List<GamePoint>>>()
        {
            [MenuType.InGame] = new Dictionary<string, List<GamePoint>>()
            {
                ["START"] = new List<GamePoint>(),
                ["CREATOR"] = new List<GamePoint>(),
                ["EXIT"] = new List<GamePoint>()
            },

            [MenuType.Pause] = new Dictionary<string, List<GamePoint>>()
            {
                ["RESUME"] = new List<GamePoint>(),
                ["RESTART"] = new List<GamePoint>(),
                ["EXIT"] = new List<GamePoint>()
            },
        };

        private bool menuIsOpen = true;

        public GameMenu(MenuType menuType)
        {
            MenuType = menuType;
            SelectedSection = MenuDrawMap[menuType].Keys.ElementAt(Cursor - 1);

            for (int menu_stroke = 0; menu_stroke < MenuDrawMap[MenuType].Count; menu_stroke++)
            {
                if (menu_stroke == Cursor - 1)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;


                for (int j = xOffset; j < 19 + xOffset; j++)
                    MenuDrawMap[MenuType][MenuDrawMap[MenuType].Keys.ElementAt(menu_stroke)].Add(new GamePoint(new Vector2D(j, menu_stroke * 4 + yOffset), '#'));

                for (int i = 6; i < 6 + MenuDrawMap[MenuType].Keys.ElementAt(menu_stroke).Length; i++)
                    MenuDrawMap[MenuType][MenuDrawMap[MenuType].Keys.ElementAt(menu_stroke)].Add(new GamePoint(new Vector2D(i + xOffset, menu_stroke * 4 + yOffset + 1), MenuDrawMap[menuType].Keys.ElementAt(menu_stroke)[i - 6]));


                MenuDrawMap[MenuType][MenuDrawMap[MenuType].Keys.ElementAt(menu_stroke)].Add(new GamePoint(new Vector2D(xOffset, menu_stroke * 4 + yOffset + 1), '#'));
                MenuDrawMap[MenuType][MenuDrawMap[MenuType].Keys.ElementAt(menu_stroke)].Add(new GamePoint(new Vector2D(xOffset + 18, menu_stroke * 4 + yOffset + 1), '#'));

                for (int j = xOffset; j < 19 + xOffset; j++)
                    MenuDrawMap[MenuType][MenuDrawMap[MenuType].Keys.ElementAt(menu_stroke)].Add(new GamePoint(new Vector2D(j, menu_stroke * 4 + yOffset + 2), '#'));
            }

            IEnumerator enumerator()
            {
                while (menuIsOpen)
                {
                    yield return KeyPressWaiter(key =>
                    {
                        ChangeSelector(key);

                        if (key == ConsoleKey.Enter)
                            EnterSelected();

                    }, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter);
                }

            }

            Thread(enumerator());
        }

        public void UpdateMenu()
        {
            for (int menu_stroke = 0; menu_stroke < MenuDrawMap[MenuType].Count; menu_stroke++)
            {
                if (menu_stroke == Cursor - 1)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;

                MenuDrawMap[MenuType][MenuDrawMap[MenuType].Keys.ElementAt(menu_stroke)].ForEach(point => point.Draw());
            }
        }

        public void CloseMenu()
        {
            Dispose();
        }

        public void Dispose()
        {
            menuIsOpen = false;

            foreach (var menuDrawMap in MenuDrawMap[MenuType].Keys)
            {
                MenuDrawMap[MenuType][menuDrawMap].ForEach(point => point.Clear());
                MenuDrawMap[MenuType][menuDrawMap].Clear();
            }
            MenuDrawMap.Clear();

            GC.SuppressFinalize(this);
            Console.Clear();
        }

        public void ChangeSelector(ConsoleKey dir)
        {
            switch (dir)
            {
                case ConsoleKey.UpArrow:
                    Cursor = Cursor - 1 == 0 ? MenuDrawMap[MenuType].Keys.Count : Cursor - 1;
                    break;
                case ConsoleKey.DownArrow:
                    Cursor = Cursor + 1 > MenuDrawMap[MenuType].Keys.Count ? 1 : Cursor + 1;
                    break;
            }

            SelectedSection = MenuDrawMap[MenuType].Keys.ElementAt(Cursor - 1);
            UpdateMenu();
        }

        public void EnterSelected()
        {
            switch (SelectedSection)
            {
                case "EXIT":
                    CloseMenu();
                    Environment.Exit(1337);
                    return;
            }
        }
    }
}
