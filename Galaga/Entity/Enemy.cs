using Galaga.Game;

#nullable enable

namespace Galaga.Entity {
	public abstract class Enemy: Entity {
		public Enemy(World world) : base(world) {
		}

		public Enemy(Position position, World world) : base(world) {
		}
	}
}
