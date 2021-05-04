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
    
    public abstract class Entity {
        public Position Position;
        public World World;

        private int _health;

        public int Health => _health;

        private Size _size;
        public Size Size => _size;

        protected Entity(Position position, World world, Size size, int health) {
            Position = position;
            World = world;
            _health = health;
            _size = size;
        }
        
        protected Entity(Position position, World world, int health): this(position, world, new Size(1, 1), health) {
            
        }

        protected Entity(World world): this(new Position(0, 0), world, 0) {
        }

        public void Attack(int damage) {
            _health -= damage;
        }

        public abstract void OnTick(int currentTick);

        public bool EntityCollisionCheck(Enemy b) //비행기본체와 적 피격판정
        {
            //true => 피격성공
            if( this.Position.Y - this.Size.Height >= b.Position.Y  + b.Size.Height || this.Position.Y + this.Size.Height <= b.Position.Y - b.Size.Height)
                return false;

            else if(this.Position.X + this.Size.Width <= b.Position.X - b.Size.Width || this.Position.X - this.Size.Width >= b.Position.X + b.Size.Width )
                return false;
            
            else
                
                return true;

        }
               
    }
}
