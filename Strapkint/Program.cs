using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;
using Strapkint.IO;
using Strapkint.Entities;

namespace Strapkint
{
    static class Program
    {
        const int tickLength = 15;

        static readonly Scoreboard scoreboard = new Scoreboard();
        static readonly Viewport viewport = new Viewport(170, 50);
        static readonly HashSet<FixedEntity> fixedEntities = new HashSet<FixedEntity> { scoreboard };
        static int currentLevelIndex = 0;
        static readonly List<Level> levels = GetXmlLevels();
        static Level currentLevel = levels[0];
        static readonly Player player = new Player(currentLevel, scoreboard);
        static readonly ConcurrentDictionary<GameControl, int> controlsPressed = new ConcurrentDictionary<GameControl, int>();

        static Program()
        {
            EntityBase.Viewport = viewport;
            foreach (GameControl control in Enum.GetValues(typeof(GameControl)))
            {
                controlsPressed.TryAdd(control, 0);
            }
        }

        static List<Level> GetXmlLevels()
        {
            const string xmlFile = "Levels.xml";

            var output = new List<Level>();

            var document = new XmlDocument();

            document.Load(xmlFile);

            XmlNodeList xmlLevels = document.DocumentElement.ChildNodes;

            foreach (XmlNode xmlLevel in xmlLevels)
            {
                var level = new Level(xmlLevel.GetIntegerAttribute("width"), viewport.Height, xmlLevel.GetPosition("spawnPosition"));
                output.Add(level);
                foreach (XmlNode xmlEntity in xmlLevel.ChildNodes)
                {
                    switch (xmlEntity.Name)
                    {
                        case "Block":
                            new Block(
                                xmlEntity.GetPosition(),
                                xmlEntity.GetSize(),
                                xmlEntity.Attributes["spikes"]?.Value.ParseDirections() ?? Directions.None,
                                level);
                            break;
                        case "Coin":
                            new Coin(xmlEntity.GetPosition(), level);
                            break;
                        case "Spike":
                            new Spike(xmlEntity.Attributes["direction"].Value.ToEnum<Direction>(), xmlEntity.GetIntegerAttribute("length"), xmlEntity.GetPosition(), level);
                            break;
                        case "BackgroundEntity":
                            new BackgroundEntity(xmlEntity.GetText(), xmlEntity.GetPosition(), level);
                            break;
                        case "Entity":
                            new Entity(xmlEntity.GetText(), xmlEntity.GetPosition(), level);
                            break;
                    }
                }
            }

            return output;
        }

        static void Main()
        {
            Console.Title = "Strapkint";
            Console.CursorVisible = false;
            Console.TreatControlCAsInput = true;

            Window.DeleteMenus(WindowMenu.Resize, WindowMenu.Maximize);
            Input.DisableModes(InputConsoleModes.QuickEditMode);
            Output.CorrectScrollbars();
            Output.SetBetterColors();

            using var gameLoop = new Timer(_ =>
            {
                Update();
                Draw();
            }, null, 0, tickLength);

            var input = new Input();

            input.OnKeyDown += OnKeyDown;
            input.OnKeyUp += OnKeyUp;

            input.Run(ConsoleKey.Escape);
        }

        static void HandleControls()
        {
            foreach (GameControl control in controlsPressed
                .Where(control => control.Value != 0)
                .Select(control => control.Key))
            {
                switch (control)
                {
                    case GameControl.Left:
                        player.Walk(Direction.Left);
                        if (player.Position.X - viewport.Position.X + 2 == viewport.Width / 3)
                        {
                            viewport.Move(Direction.Left, currentLevel);
                        }
                        else if (player.Position.X == 0)
                        {
                            PreviousLevel();
                        }
                        break;

                    case GameControl.Right:
                        player.Walk(Direction.Right);
                        if (player.Position.X - viewport.Position.X == viewport.Width / 3)
                        {
                            viewport.Move(Direction.Right, currentLevel);
                        }
                        else if (player.Position.X + player.Width == currentLevel.Width)
                        {
                            NextLevel();
                        }
                        break;

                    case GameControl.Jump:
                        if (player.CanJump)
                        {
                            player.Jump();
                        }
                        break;
                }
            }
        }

        static void OnKeyDown(ConsoleKey key)
        {
            var action = key.ToKeyAction();
            if (action == null)
            {
                switch (key)
                {
                    case ConsoleKey.Subtract:
                        PreviousLevel();
                        player.PositionF = currentLevel.SpawnPosition;
                        break;
                    case ConsoleKey.Add:
                        NextLevel();
                        player.PositionF = currentLevel.SpawnPosition;
                        break;
                    case ConsoleKey.T:
                        viewport.IsDarkTheme ^= true;
                        break;
                    case ConsoleKey.L:
                        Console.WindowWidth += Console.WindowWidth == Console.BufferWidth ? -2 : 2;
                        break;
                }
            }
            else
            {
                ++controlsPressed[action.Value];
                HandleControls();
            }
        }

        static void OnKeyUp(ConsoleKey key)
        {
            var action = key.ToKeyAction();
            if (action != null)
            {
                --controlsPressed[action.Value];
            }
        }

        static void Update()
        {
            HandleControls();
            player.Update();
        }

        static void Draw()
        {
            viewport.Clear();

            foreach (EntityBase entity in currentLevel.BackgroundEntities.Union<EntityBase>(currentLevel.Entities).Union(fixedEntities))
            {
                viewport.Write(entity);
            }

            viewport.Redraw();
        }

        static void PreviousLevel()
        {
            if (currentLevelIndex > 0)
            {
                player.Level = currentLevel = levels[--currentLevelIndex];
                viewport.Position.X = currentLevel.Width - viewport.Width;
                player.PositionF.X = currentLevel.Width - player.Width;
                --scoreboard.LevelCount;
            }
        }

        static void NextLevel()
        {
            if (currentLevelIndex < levels.Count - 1)
            {
                player.Level = currentLevel = levels[++currentLevelIndex];
                viewport.Position.X = 0;
                player.PositionF.X = 0;
                ++scoreboard.LevelCount;
            }
        }
    }
}
