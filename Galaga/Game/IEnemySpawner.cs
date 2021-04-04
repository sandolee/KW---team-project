#nullable enable

using System.Collections.Generic;

namespace Galaga.Game {
    public interface IEnemySpawner {
        void SetWorld(World world);
        
        IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick);
    }

    public class EnemySpawnerImpl: IEnemySpawner {
        private IEnemySpawnerStrategy? _enemySpawnerStrategy;
        private World? _world;

        public EnemySpawnerImpl(IEnemySpawnerStrategy? enemySpawnerStrategy = null) {
            _enemySpawnerStrategy = enemySpawnerStrategy;
        }

        public void SetWorld(World world) {
            _world = world;
        }

        public IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick) {
            if (_world == null) return new Entity.Entity[0];
            return _enemySpawnerStrategy?.GetSpawnEntities(currentTick, _world) ?? new Entity.Entity[0];
        }

        public void SetEnemySpawnerStrategy(IEnemySpawnerStrategy enemySpawnerStrategy) {
            _enemySpawnerStrategy = enemySpawnerStrategy;
        }
    }

    public interface IEnemySpawnerStrategy {
        Entity.Entity[] GetSpawnEntities(int currentTick, World world);
    }
}
