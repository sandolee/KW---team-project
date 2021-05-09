using Galaga.Game;

namespace Galaga.Entity
{
    public abstract class Item : Entity
    {
        public Item(World world) : this(new Position(0, 0), world, new Size(1,1),1)
        {
        }

        public Item(Position position, World world, Size size, int health) : base(position, world, size, health)
        {
        }
        
    }
    public class Heart : Item
    {
        public Heart(World world) : this(new Position(0, 0), world, new Size(1, 1), 1)
        {
        }
        public Heart(Position position, World world, Size size, int health) : base(
            new Position(world.Size.Width-10, (int)(world.Size.Height - 5)),
            world,
            new Size(10, 10),
            health)
        {
        }
        public override void OnTick(int currentTick)
        {
            foreach (var entity in World.EntityManager.Entities)
            {
                if (entity is Player player)
                {
                    if (this.ItemCollisionCheck(player))
                    {
                        player.Heal(1);
                        Kill();//피격시 아이템 삭제 
                    }
                }
            }
        }
    }
    public class Poition : Item
    {
        public Poition(World world) : this(new Position(0, 0), world, new Size(1, 1), 1)
        {
        }
        public Poition(Position position, World world, Size size, int health) : base(
            new Position(world.Size.Width-20, (int)(world.Size.Height - 5)),
            world,
            new Size(10, 10),
            health)
        {
        }
        public override void OnTick(int currentTick)
        {
            foreach (var entity in World.EntityManager.Entities)
            {
                if (entity is Player player)
                {
                    if (this.ItemCollisionCheck(player))
                    {
                        player.GodMode.StartGodMode(currentTick);
                        Kill();//피격시 아이템 삭제 
                    }
                }
            }
        }
    }
}
