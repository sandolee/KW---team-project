using Galaga.Game;
using System.Windows.Forms;

#nullable enable

namespace Galaga.Entity
{
    public class Player : Entity
    {
        public Player(World world) : this(new Position(0, 0), world, 10){}
        public void Move(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (this.Position.X <= World.Size.Width - this.Size.Width)
                        this.Position.X += 7;
                    break;
                case Keys.Left:
                    if (this.Position.X >= this.Size.Width) 
                        this.Position.X -= 7;
                    break;
                case Keys.Space:
                    World.EntityManager.AddEntity(new Ammo(new Position(Position.X + 4, Position.Y - 4), World, 1));
                    World.EntityManager.AddEntity(new Ammo(new Position(Position.X - 4, Position.Y - 4), World, 1));
                    break;
                default:
                    break;
            }

        }
        public Player(Position position, World world, int health) : base(
            new Position(world.Size.Width / 2, (int)(world.Size.Height)),
            world,
            new Size(10, 10),
            health
        )
        { }

        public override void OnTick(int currentTick) {
            
        }
    }
}
