#nullable enable

using System.Collections.Generic;

namespace Galaga.Game {
    public interface IEnemySpawner {
        IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick);
    }

    public class EnemySpawnerImpl: IEnemySpawner {
        private IEnemySpawnerDelegate? _enemySpawnerDelegate;

        public EnemySpawnerImpl(IEnemySpawnerDelegate? enemySpawnerDelegate = null) {
            _enemySpawnerDelegate = enemySpawnerDelegate;
        }

        public IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick) {
            return _enemySpawnerDelegate?.GetSpawnEntities(currentTick) ?? new Entity.Entity[0];
        }

        public void SetEnemySpawnerDelegate(IEnemySpawnerDelegate enemySpawnerDelegate) {
            _enemySpawnerDelegate = enemySpawnerDelegate;
        }
    }

    public interface IEnemySpawnerDelegate {
        Entity.Entity[] GetSpawnEntities(int currentTick);
    }
}
