#nullable enable

namespace Galaga.Game {
    public class World {
        private readonly IEnemySpawner enemySpawner;
        public readonly Size Size;

        public EntityManager EntityManager { get; }

        public World(IEnemySpawner enemySpawner, Size size) {
            EntityManager = new EntityManager();

            Size = size;

            this.enemySpawner = enemySpawner;
        }

        public World(IEnemySpawner enemySpawner): this(enemySpawner, new Size(100, 100)) {
            
        }

        public void OnTick(int currentTick) {
            foreach (var entity in enemySpawner.GetSpawnEntities(currentTick)) {
                EntityManager.AddEntity(entity);
            }

            EntityManager.OnTick(currentTick);
        }
    }

    public class Size {
        public readonly int Width;
        public readonly int Height;
        
        public Size(int width, int height) {
            Width = width;
            Height = height;
        }
    }
}
