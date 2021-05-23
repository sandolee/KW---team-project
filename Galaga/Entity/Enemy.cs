using System;
using Galaga.Game;
using Galaga.Entity.AmmoEntity;


#nullable enable

namespace Galaga.Entity {
	public abstract class Enemy: Entity {
		public Enemy(World world) : this(new Position(0, 0), world, new Size(1, 1), 1) {
		}

		public Enemy(Position position, World world, Size size, int health) : base(position, world, size, health) {
		}
	}

	public class Test1Enemy: Enemy{
		private int tempTick = -1;

		
		public Test1Enemy(World world) : this(new Position(0, 0), world, 1){}
		
		public Test1Enemy(Position position, World world, int health) : base(
            position,
            world,
            new Size(10, 10),
            health
        ){}

		 public override void OnTick(int currentTick) {
			Position.Y= Position.Y + 1;
			Position.X= Position.X + 1;

			if (currentTick - tempTick > 10) {
				World.EntityManager.AddEntity(new StraightEnemyAmmo(new Position(Position.X, Position.Y), World));
				tempTick = currentTick;
			}
			
				
		 }
	}

	public class Test2Enemy: Enemy{
		private int tempTick = -1;
		public Test2Enemy(World world) : this(new Position(0, 10), world, 10){}
		
		public Test2Enemy(Position position, World world, int health) : base(position, world, new Size(10, 10),health){}

		public override void OnTick(int currentTick) {
      		Position.X= Position.X + 1;
 		
			if (currentTick - tempTick > 10) {
				World.EntityManager.AddEntity(new StraightEnemyAmmo(new Position(Position.X, Position.Y), World));
				tempTick = currentTick;
			}
		
		
		}
	}

	public class TestBossEnemy: Enemy{
		private int tempTick = -1;
		public TestBossEnemy(World world) : this(new Position(30, 20), world, 10){}
		
		public TestBossEnemy(Position position, World world, int health) : base(position, world, new Size(10, 10),health){}

		public override void OnTick(int currentTick) {
      		Position.X= Position.X + 2;
			Position.Y= Position.Y + 1;
 		
			
			
			if (currentTick - tempTick > 10) {
				World.EntityManager.AddEntity(new StraightEnemyAmmo(new Position(Position.X-5, Position.Y), World));
				World.EntityManager.AddEntity(new StraightEnemyAmmo(new Position(Position.X+5, Position.Y), World));
				World.EntityManager.AddEntity(new Test1EnemyAmmo(new Position(Position.X, Position.Y), World));
				World.EntityManager.AddEntity(new Test2EnemyAmmo(new Position(Position.X, Position.Y), World));
				tempTick = currentTick;
				Position.X= Position.X - 22;
				Position.Y= Position.Y - 11;
																																																																					
			}

		}
	}
}
