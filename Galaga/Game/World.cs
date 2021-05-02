#nullable enable

namespace Galaga.Game {
    public class World {
        private readonly IEntitySpawner entitySpawner;
        public readonly Size Size;

        public EntityManager EntityManager { get; }

        public World(IEntitySpawner entitySpawner, Size size) {
            EntityManager = new EntityManager();

            Size = size;

            this.entitySpawner = entitySpawner;
        }

        public World(IEntitySpawner entitySpawner): this(entitySpawner, new Size(100, 100)) {
            
        }

        public void OnTick(int currentTick) {
            foreach (var entity in entitySpawner.GetSpawnEntities(currentTick)) {
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
