using Galaga.Game;
using System.CodeDom.Compiler;

#nullable enable

namespace Galaga.Entity {
	public class Ammo: Entity {
		public Ammo(World world): this(new Position(0, 0), world, 0) {
		}

		public Ammo(Position position, World world, int health) : base(position, world, new Size(3, 3), health) {
		}


		public bool AmmoCollisionCheck(Enemy b) //총알과 적 피격판정
        {
            //true => 피격 성공
			return b.Position.X + b.Size.Width >= this.Position.X && b.Position.X - b.Size.Width <= this.Position.X && 
				b.Position.Y + b.Size.Height >= this.Position.Y && b.Position.Y - b.Size.Width <= this.Position.Y;
        }
		
		public override void OnTick(int currentTick) {
			
			Position.Y= Position.Y - 5;
			foreach(var entity in World.EntityManager.Entities) {
				if(entity is Enemy enemy) {
					if(AmmoCollisionCheck(enemy)){
						enemy.Attack(1);
           			}
				}		
			}
		}
	}
}
