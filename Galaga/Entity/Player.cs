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
            if (x > 0 && this.Position.X < World.Size.Width - this.Size.Width / 2)
                Position.X += x;
            else if (x < 0 && this.Position.X > this.Size.Width / 2)
                Position.X += x;
            //else if (y < 0 && this.Position.Y > this.Size.Height / 2)
            //    Position.Y += y;
            //else if (y > 0 && this.Position.Y < World.Size.Height - this.Size.Height / 2)
            //    Position.Y += y;
        }

        public Player(Position position, World world, int health) : base(
            new Position(world.Size.Width / 2, (int)(world.Size.Height-5)),
            world,
            new Size(10, 10),
            health
        )
        { }
        public int GodModeStartTic { get; set; }

        public override void OnTick(int currentTick) {
            foreach (var entity in World.EntityManager.Entities)
            {
                if (entity is Enemy enemy)
                {
                    if (this.EntityCollisionCheck(enemy))
                    {
                        Attack(1);
                    }
                }

            }

            //50 tic 이후로 godMode 해제 
            if (GodMode && currentTick > GodModeStartTic + 50)
            {
                GodMode = false;
            }
        }
    }
}
