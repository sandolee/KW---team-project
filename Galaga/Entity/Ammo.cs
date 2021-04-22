using Galaga.Game;

#nullable enable

namespace Galaga.Entity {
	public class Ammo: Entity {
		public Ammo(World world): this(new Position(0, 0), world, 0) {
		}

		public Ammo(Position position, World world, int health) : base(position, world, new Size(3, 3), health) {
		}
		
		public override void OnTick(int currentTick) {
			
			Position.Y= Position.Y - 5;

			if(ammo_enemy_check(this) == true){
				//Position.Y = -5;
				//Position.X = -5;
			}
				
		}
	}
}
