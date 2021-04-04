using Galaga.Game;

#nullable enable

namespace Galaga.Entity {
	public abstract class Enemy: Entity {
		public Enemy(World world) : this(new Position(0, 0), world, new Size(1, 1), 1) {
		}

		public Enemy(Position position, World world, Size size, int health) : base(position, world, size, health) {
		}
	}
}
