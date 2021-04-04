using Galaga.Game;

#nullable enable

namespace Galaga.Entity {
    public class Player: Entity {
        public Player(World world): this(new Position(0, 0), world, 10) {
            
        }

        public Player(Position position, World world, int health) : base(
            new Position(world.Size.Width / 2, (int) (world.Size.Height)),
            world,
            new Size(10, 10),
            health
        ) { }

        public override void OnTick(int currentTick) {
            
        }
    }
}
