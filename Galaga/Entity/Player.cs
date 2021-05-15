using Galaga.Game;

#nullable enable

namespace Galaga.Entity
{
    public class Player : Entity
    {
        public Player(World world) :
            this(new Position(world.Size.Width / 2, world.Size.Height - 5), world, 10) {
            
        }
        
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

        public void Shoot() {
            World.EntityManager.AddEntity(new Ammo(new Position(Position.X + 4, Position.Y), World, 1));
            World.EntityManager.AddEntity(new Ammo(new Position(Position.X - 4, Position.Y), World, 1));
        }

        public Player(Position position, World world, int health) : base(
            position,
            world,
            new Size(10, 10),
            health
        ) {
            
        }

        public override void OnTick(int currentTick) {
            foreach (var entity in World.EntityManager.Entities)
            {
                if (entity is Enemy enemy)
                {
                    if (this.EntityCollisionCheck(enemy)) {
                        enemy.Attack(1);
                        Attack(1);
                        //attack 확인
                        UpdateScore();
                    }
                }
            }

            //50 tic 이후로 godMode 해제 
            if (GodMode.IsGodMode && currentTick > GodMode.GodModeStartTick + 50)
            {
                GodMode.EndGodMode();
            }
        }
    }
}
