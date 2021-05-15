using Galaga.Game;

namespace Galaga.Entity.AmmoEntity {
	public class StraightEnemyAmmo: Entity {
		private const int AmmoHealth = int.MaxValue;

		public StraightEnemyAmmo(Position position, World world) : 
			base(position, world, new Size(3, 3), AmmoHealth) {
		}

		public override void OnTick(int currentTick) {
			Position.Y += 5;

			foreach(var entity in World.EntityManager.Entities) {
				if (!(entity is Player)) continue;

				if (CollidesWith(entity)) {
					entity.Attack(1);
					Kill();
					return;
				}
			}
		}
	}
}
