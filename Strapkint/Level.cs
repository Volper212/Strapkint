using System.Collections.Generic;
using Strapkint.Entities;

namespace Strapkint
{
    class Level
    {
        public Level(int width, int height, Position spawnPosition)
        {
            SpawnPosition = spawnPosition;
            Width = width;
            Height = height;
            Size = new Size(Width, Height);
        }

        public Position SpawnPosition { get; }
        public int Width { get; }
        public int Height { get; }
        public Size Size { get; }
        public HashSet<Entity> Entities { get; } = new HashSet<Entity>();
        public HashSet<BackgroundEntity> BackgroundEntities { get; } = new HashSet<BackgroundEntity>();
    }
}
