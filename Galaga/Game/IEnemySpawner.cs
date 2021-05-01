#nullable enable

using System.Collections.Generic;

namespace Galaga.Game {
    public interface IEnemySpawner {
        IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick);
    }
}
