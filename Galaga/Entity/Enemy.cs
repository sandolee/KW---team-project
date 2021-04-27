using Galaga.Game;

#nullable enable

namespace Galaga.Entity {
	public abstract class Enemy: Entity {
		public Enemy(World world) : this(new Position(0, 0), world, new Size(1, 1), 1) {
		}

		public Enemy(Position position, World world, Size size, int health) : base(position, world, size, health) {
		}
	}

	public class TestEnemy: Enemy{
		public TestEnemy(World world) : this(new Position(0, 0), world, 10){}
		
		public TestEnemy(Position position, World world, int health) : base(
            position,
            world,
            new Size(10, 10),
            health
        ){}

		 public override void OnTick(int currentTick) {
         		Position.Y= Position.Y + 1;
				Position.X= Position.X + 1;
		 }
	}
}
