#nullable enable

namespace Galaga.Game {
    public class World {
        private IEntitySpawner? _entitySpawner;
        public readonly Size Size;

        public EntityManager EntityManager { get; }

        public World(IEntitySpawner? entitySpawner, Size size) {
            EntityManager = new EntityManager();

            Size = size;

            _entitySpawner = entitySpawner;

        }

        public World(IEntitySpawner? entitySpawner = null): this(entitySpawner, new Size(100, 100)) {
            
        }

        public void SetEntitySpawner(IEntitySpawner spawner) {
            _entitySpawner = spawner;
        }
        
        public void OnTick(int currentTick) {
            if (_entitySpawner != null) {
                foreach (var entity in _entitySpawner.GetSpawnEntities(currentTick)) {
                    EntityManager.AddEntity(entity);
                }
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
