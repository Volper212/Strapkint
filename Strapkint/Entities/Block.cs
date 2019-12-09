using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strapkint.Entities
{
    class Block : Entity
    {
        const char wall = '▓';

        public Block(Position position, Size size, Directions spikeDirections, Level level)
            : base(new string(wall, size.Width).Repeat(size.Height).To2DCharArray(size.Width), position, level)
        {
            if (spikeDirections.HasFlag(Directions.Left))
            {
                Spikes.Add(new Spike(Direction.Left, Height, Position - new Position(1, 0), Level));
            }
            if (spikeDirections.HasFlag(Directions.Down))
            {
                Spikes.Add(new Spike(Direction.Down, Width, Position - new Position(0, 1), Level));
            }
            if (spikeDirections.HasFlag(Directions.Right))
            {
                Spikes.Add(new Spike(Direction.Right, Height, Position + new Position(Width, 0), Level));
            }
            if (spikeDirections.HasFlag(Directions.Up))
            {
                Spikes.Add(new Spike(Direction.Up, Width, Position + new Position(0, Height), Level));
            }
        }

        protected List<Spike> Spikes { get; } = new List<Spike>();
    }
}
