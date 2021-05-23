#nullable enable

using System;
using Galaga.Game;

namespace Galaga.Entity {
    public class Position {
        public int X;
        public int Y;

        public Position(int x, int y) {
            X = x;
            Y = y;
        }

        public double Distance(Position other) {
            return Math.Sqrt((this.X - other.X) * (this.X - other.X) + (this.Y - other.Y) * (this.Y - other.Y));
        }
    }
    
    public class GodMode
    {
        public bool IsGodMode { get; private set; }
        public  int GodModeStartTick { get; private set; }
        public GodMode()
        {
            IsGodMode = false;
            GodModeStartTick = 0;
        }
        public void StartGodMode(int tick)
        {
            IsGodMode = true;
            GodModeStartTick = tick;
        }
        public void EndGodMode()
        {
            IsGodMode = false;
            GodModeStartTick = 0;
        }

    }
    
    public abstract class Entity {
        public Position Position;
        public World World;
        public GodMode GodMode;
        
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        private readonly Size _size;

        public Size Size => _size;

        protected Entity(Position position, World world, Size size, int health): this(position, world, size, health, health) {
        }

        protected Entity(Position position, World world, Size size, int health, int maxHealth) {
            Position = position;
            World = world;
            Health = health;
            MaxHealth = maxHealth;
            _size = size;
            GodMode = new GodMode();
        }

        protected Entity(Position position, World world, int health): this(position, world, new Size(1, 1), health, health) {
            
        }

        protected Entity(World world): this(new Position(0, 0), world, 1) {
        }
        
        public void Kill()
        {
            Health = 0;
        }
        public void Heal(int heal) {
            Health = Math.Min(MaxHealth, Health + heal);
        }

        public void Attack(int damage) {
            if (!GodMode.IsGodMode)
                Health -= damage; 
            World.EntityManager.OnEntityAttacked.ForEach(attacked=>attacked(this));
        }

        public abstract void OnTick(int currentTick);
        
        public bool CollidesWith(Entity entity) //비행기본체와 적 피격판정
        {
            //true => 피격성공
            if( Position.Y - Size.Height/2 >= entity.Position.Y  + entity.Size.Height/2 || Position.Y + Size.Height/2 <= entity.Position.Y - entity.Size.Height/2)
                return false;

            if(Position.X + entity.Size.Width/2 <= entity.Position.X - entity.Size.Width/2 || Position.X - Size.Width/2 >= entity.Position.X+entity.Size.Width/2)
                return false;
            
            return true;
        }

        public bool ItemCollisionCheck(Player player)
        {
            if (this.Position.Y - this.Size.Height/2 >= player.Position.Y + player.Size.Height/2 || this.Position.Y + this.Size.Height/2 <= player.Position.Y - player.Size.Height/2)
                return false;

            else if (this.Position.X +this.Size.Width/2 <= player.Position.X - player.Size.Width/2 || this.Position.X - this.Size.Width/2 >= player.Position.X+player.Size.Width/2)
                return false;

            else return true;
        }
    }
}
