using System.Collections.Generic;
using Galaga.Entity;

#nullable enable

namespace Galaga.Game {
    public interface IGame {
        public World GetWorld();

        public Player GetPlayer();

        public void OnTick(int currentTick);
    }

    public abstract class BaseGame : IGame {
        private readonly World world;
        private readonly Player player;

        protected BaseGame(World world) {
            this.world = world;
            
            player = new Player(world);
            world.EntityManager.AddEntity(player);
        }

        public World GetWorld() {
            return world;
        }

        public Player GetPlayer() {
            return player;
        }

        public void OnTick(int currentTick) {
            world.OnTick(currentTick);
        }
    }

    class Stage1Game : BaseGame {
        public Stage1Game() : base(new World(new EntitySpawner())) {
            
        }

        // Stage 1의 엔티티 소환을 관리
        private class EntitySpawner : IEntitySpawner {
            public IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick) {
                return new Entity.Entity[0];
            }
        }
    }
}
