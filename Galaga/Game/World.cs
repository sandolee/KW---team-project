#nullable enable

namespace Galaga.Game {
    public class World {
        private readonly IEnemySpawner _enemySpawner;
        
        public EntityManager EntityManager { get; }

        public World(IEnemySpawner enemySpawner) {
            EntityManager = new EntityManager();

            _enemySpawner = enemySpawner;
            _enemySpawner.SetWorld(this);
        }

        public void OnTick(int currentTick) {
            foreach (var entity in _enemySpawner.GetSpawnEntities(currentTick)) {
                EntityManager.AddEntity(entity);
            }

            EntityManager.OnTick(currentTick);
        }
    }
}
