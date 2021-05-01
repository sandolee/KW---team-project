#nullable enable

using System.Collections.Generic;

namespace Galaga.Game {
    public interface IEnemySpawner {
        void SetWorld(World world);
        
        IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick);
    }

    public class EnemySpawnerImpl: IEnemySpawner {
        private IEnemySpawnerStrategy? enemySpawnerStrategy;
        private World? world;

        public EnemySpawnerImpl(IEnemySpawnerStrategy? enemySpawnerStrategy = null) {
            this.enemySpawnerStrategy = enemySpawnerStrategy;
        }

        public void SetWorld(World w) {
            this.world = w;
        }

        public IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick) {
            if (world == null) return new Entity.Entity[0];
            return enemySpawnerStrategy?.GetSpawnEntities(currentTick, world) ?? new Entity.Entity[0];
        }

        public void SetEnemySpawnerStrategy(IEnemySpawnerStrategy strategy) {
            this.enemySpawnerStrategy = strategy;
        }
    }

    public interface IEnemySpawnerStrategy {
        Entity.Entity[] GetSpawnEntities(int currentTick, World world);
    }
}
