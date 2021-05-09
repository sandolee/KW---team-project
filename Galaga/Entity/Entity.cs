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
        public void EndGodMode(int tick)
        {
            if (IsGodMode && tick > GodModeStartTick + 50)
            {
                IsGodMode = false;
                GodModeStartTick = 0;
            }
        }

    }
    
    public abstract class Entity {
        public Position Position;
        public World World;
        public GodMode GodMode;
        public int Health { get; private set; }

        private Size _size;
        public Size Size => _size;

        protected Entity(Position position, World world, Size size, int health) {
            Position = position;
            World = world;
            Health = health;
            _size = size;
            GodMode = new GodMode();
        }
        
        protected Entity(Position position, World world, int health): this(position, world, new Size(1, 1), health) {
            
        }

        protected Entity(World world): this(new Position(0, 0), world, 0) {
        }

        public void Attack(int damage) {
            if (!GodMode.IsGodMode)
                Health -= damage;
        }

        public abstract void OnTick(int currentTick);

        public bool EntityCollisionCheck(Enemy b) //비행기본체와 적 피격판정
        {
            //true => 피격성공
            if( this.Position.Y - this.Size.Height/2 >= b.Position.Y  + b.Size.Height/2 || this.Position.Y + this.Size.Height/2 <= b.Position.Y - b.Size.Height/2)
                return false;

            else if(this.Position.X + b.Size.Width/2 <= b.Position.X - b.Size.Width/2 || this.Position.X - this.Size.Width/2 >= b.Position.X+b.Size.Width/2)
                return false;
            
            else
                return true;

        }

        public bool ItemCollisionCheck(Player _plyaer)
        {
            if (this.Position.Y - this.Size.Height/2 >= _plyaer.Position.Y + _plyaer.Size.Height/2 || this.Position.Y + this.Size.Height/2 <= _plyaer.Position.Y - _plyaer.Size.Height/2)
                return false;

            else if (this.Position.X +this.Size.Width/2 <= _plyaer.Position.X - _plyaer.Size.Width/2 || this.Position.X - this.Size.Width/2 >= _plyaer.Position.X+_plyaer.Size.Width/2)
                return false;

            else return true;
        }

        public void Heal(int heal)
        {
            Health += heal;
        }
               
    }

}
