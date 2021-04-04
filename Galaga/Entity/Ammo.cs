using Galaga.Game;

#nullable enable

namespace Galaga.Entity {
	public class Ammo: Entity {
		public Ammo(World world): base(world) {
		}

		public Ammo(Position position, World world, int health) : base(position, world, health) {
		}
		
		public override void OnTick(int currentTick) {
			
		}
	}
}
