using System;
using System.Collections.Generic;
using System.Linq;

namespace Strapkint.Entities
{
    class Player : Entity
    {
        const double horizontalSpeed = 1;
        const double desiredJumpHeight = 9;
        const double desiredJumpHeightX = 12;
        const int fallCooldown = 3;

        const double jumpSpeed = 2 * desiredJumpHeight * horizontalSpeed / desiredJumpHeightX;
        const double gravity = jumpSpeed * horizontalSpeed / desiredJumpHeightX;

        const string text =
            "\0O\0" + //  O
            "/|\\" +  // /|\
            "/\0\\";  // / \

        const int width = 3;

        readonly Scoreboard scoreboard;
        
        double verticalSpeed = 0;
        bool hasAlreadyJumped = false;
        bool isOnGround = true;
        int currentFallCooldown = 0;

        public Player(Level level, Scoreboard scoreboard)
            : base(text.To2DCharArray(width), Position.Zero, level, true)
        {
            this.scoreboard = scoreboard;
            PositionF = Level.SpawnPosition;
        }

        public override Position Position => (Position)PositionF;
        public bool CanJump => isOnGround || (currentFallCooldown > 0 && !hasAlreadyJumped);
        public PositionF PositionF { get; set; }

        public void Jump()
        {
            hasAlreadyJumped = true;
            isOnGround = false;
            verticalSpeed = jumpSpeed;
        }

        public void Update()
        {
            if (!isOnGround)
            {
                Move(Axis.Vertical, verticalSpeed - (gravity / 2));
                verticalSpeed -= gravity;
                --currentFallCooldown;
            }

            if (Collides(Axis.Vertical, offsetY: -0.01))
            {
                hasAlreadyJumped = false;
                isOnGround = true;
                PositionF.Y = Math.Floor(PositionF.Y);
                verticalSpeed = 0;
            }
            else if (isOnGround)
            {
                currentFallCooldown = fallCooldown;
                isOnGround = false;
            }
        }

        public void Walk(Direction direction)
        {
            if (direction.HasFlag(Direction.Vertical))
            {
                throw new ArgumentException("Cannot walk vertically");
            }
            Move(direction, horizontalSpeed);
        }

        void Die()
        {
            Viewport.Position.X = 0;
            PositionF = Level.SpawnPosition;
            isOnGround = true;
        }

        void Move(Axis axis, double distance)
        {
            int sense = Math.Sign(distance);

            for (int i = 0; i < Math.Abs((int)distance); ++i)
            {
                PositionF[axis] += sense;
                if (Collides(axis))
                {
                    if (PositionF[axis] % 1 == 0)
                    {
                        PositionF[axis] -= sense;
                    }
                    else
                    {
                        PositionF[axis] = sense == 1 ? Math.Floor(PositionF[axis]) : Math.Ceiling(PositionF[axis]);
                    }
                    if (axis == Axis.Vertical)
                    {
                        verticalSpeed = 0;
                    }
                    return;
                }
            }
            PositionF[axis] += distance % 1;
            if (Collides(axis))
            {
                PositionF[axis] = sense == 1 ? Math.Floor(PositionF[axis]) : Math.Ceiling(PositionF[axis]);
            }
        }

        void Move(Direction direction, double distance)
        {
            Move(direction.GetAxis(), distance * direction.GetSense());
        }

        bool Collides(Axis axis, double offsetX = 0, double offsetY = 0)
        {
            PositionF position = PositionF + new PositionF(offsetX, offsetY);
            
            if (position[axis] < 0 || position[axis] > Level.Size[axis] - Size[axis])
            {
                return true;
            }

            var toRemove = new HashSet<IDisposable>();
            bool shouldDie = false;

            foreach (Entity entity in Level.Entities.Where(entity => entity != this))
            {
                if (position < entity.Position + entity.Size && position + Size > entity.Position)
                {
                    if (entity is Spike && !(isOnGround && axis == Axis.Vertical))
                    {
                        shouldDie = true;
                    }
                    else if (entity.IsCollidable)
                    {
                        return true;
                    }
                    else if (entity is Coin coin)
                    {
                        ++scoreboard.Coins;
                        toRemove.Add(coin);
                    }
                }
            }

            if (shouldDie)
            {
                Die();
            }

            foreach (IDisposable entity in toRemove)
            {
                entity.Dispose();
            }

            return false;
        }
    }
}
