#nullable enable

using System;

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
        
        private int _health;

        public int Health => _health;

        protected Entity(Position position, int health) {
            Position = position;
            _health = health;
        }

        protected Entity(): this(new Position(0, 0), 0) {
        }

        public void Attack(int damage) {
            _health -= damage;
        }

        public abstract void OnTick(int currentTick);
    }
}
