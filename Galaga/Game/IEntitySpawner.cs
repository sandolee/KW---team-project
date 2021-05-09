#nullable enable

using System.Collections.Generic;

namespace Galaga.Game {
    public interface IEntitySpawner {
        IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick);
    }
}
