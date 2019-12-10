using System;
using System.Linq;
using System.Xml;

namespace Strapkint
{
    static class Extensions
    {
        public static string Repeat(this string input, int count)
        {
            return new string('X', count).Replace("X", input);
        }

        public static char[,] To2DCharArray(this string input, int width)
        {
            int height = input.Length / width;
            char[,] output = new char[width, height];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    output[x, y] = input[(y * width) + x];
                }
            }
            return output;
        }

        public static void ReplaceAt(this char[,] array, string value, Position position)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                array[position.X + i, position.Y] = value[i];
            }
        }

        public static GameControl? ToKeyAction(this ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    return GameControl.Left;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    return GameControl.Right;

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                case ConsoleKey.Spacebar:
                    return GameControl.Jump;

                default:
                    return null;
            }
        }

        public static Axis GetAxis(this Direction direction)
        {
            return direction.HasFlag(Direction.Vertical) ? Axis.Vertical : Axis.Horizontal;
        }

        public static int GetSense(this Direction direction)
        {
            return direction.HasFlag(Direction.Plus) ? 1 : -1;
        }

        public static void Fill<T>(this T[,] array, T value)
        {
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    array[i, j] = value;
                }
            }
        }

        public static int GetIntegerAttribute(this XmlNode node, string attribute)
        {
            return Int32.Parse(node.Attributes[attribute].Value);
        }

        public static Position GetPosition(this XmlNode node, string attributeName = "position")
        {
            string[] stringPosition = node.Attributes[attributeName].Value.Split(',');
            return new Position(Int32.Parse(stringPosition[0]), Int32.Parse(stringPosition[1]));
        }

        public static Size GetSize(this XmlNode node)
        {
            string[] stringSize = node.Attributes["size"].Value.Split(',');
            return new Size(Int32.Parse(stringSize[0]), Int32.Parse(stringSize[1]));
        }

        public static char[,] GetText(this XmlNode node)
        {
            XmlNodeList rows = node.SelectNodes("row");
            int longestWidth = rows.Cast<XmlNode>().Select(row => row.InnerText.Length).Aggregate((longest, current) => longest > current ? longest : current);
            char[,] output = new char[longestWidth, rows.Count];
            for (int y = 0; y < rows.Count; ++y)
            {
                for (int x = 0; x < rows[y].InnerText.Length; ++x)
                {
                    output[x, y] = rows[y].InnerText[x];
                }
            }
            return output;
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static Directions ParseDirections(this string input)
        {
            string[] stringDirections = input.Split(',');
            Directions directions = Directions.None;
            foreach (string direction in stringDirections)
            {
                directions |= direction.ToEnum<Directions>();
            }
            return directions;
        }
    }
}
