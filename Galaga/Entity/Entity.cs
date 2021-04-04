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

        protected Entity(Position position, World world, int health) {
            Position = position;
            World = world;
            _health = health;
        }

        protected Entity(World world): this(new Position(0, 0), world, 0) {
        }

        public void Attack(int damage) {
            _health -= damage;
        }

        public abstract void OnTick(int currentTick);
    }
}
