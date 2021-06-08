using Galaga.Entity.AmmoEntity;
using Galaga.Game;

#nullable enable

namespace Galaga.Entity.EnemyEntity {
	public class StraightEnemy: Enemy {
		private int _lastShoot = -1;
		
		public StraightEnemy(Position position, World world, Size size, int health) : base(position, world, size, health) {
		}

		public override void OnTick(int currentTick) {
			Position.Y += 1;

			if (currentTick - _lastShoot > 20) {
				World.EntityManager.AddEntity(new StraightEnemyAmmo(new Position(Position.X, Position.Y), World));
				_lastShoot = currentTick;
			}
		}
	}
}
