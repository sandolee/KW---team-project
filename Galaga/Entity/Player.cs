using Galaga.Game;
using System.Windows.Forms;

#nullable enable

namespace Galaga.Entity
{
    public class Player : Entity
    {
        public Player(World world) : this(new Position(0, 0), world, 10){}
        public void Move(int x, int y)
        {
            if (x>0 && this.Position.X <= World.Size.Width - this.Size.Width)
                Position.X += x;
            if ( x<0&& this.Position.X >= this.Size.Width)
                Position.X += x;
        }

        public Player(Position position, World world, int health) : base(
            new Position(world.Size.Width / 2, (int)(world.Size.Height)),
            world,
            new Size(10, 10),
            health
        )
        { }

        public override void OnTick(int currentTick) {
            foreach(var entity in World.EntityManager.Entities) {
				if(entity is Enemy enemy) {
					if(EntityCollisionCheck(enemy) == true){
						enemy.Attack(1);
                        this.Attack(1);
						
					}
				}		
			}
            
        }
    }
}
