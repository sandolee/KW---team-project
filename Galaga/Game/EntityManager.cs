#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Galaga.Game {
    public class EntityManager {
        private readonly List<Entity.Entity> _entities = new List<Entity.Entity>();
        public ReadOnlyCollection<Entity.Entity> Entities => _entities.AsReadOnly();

        public delegate void OnEntityKillDelegate(Entity.Entity entity);
        public readonly List<OnEntityKillDelegate> OnEntityKill = new List<OnEntityKillDelegate>();

        public void OnTick(int currentTick) {
            var destroy = _entities.FindAll(entity => entity.Health <= 0);
            foreach (var entity in destroy) {
                _entities.Remove(entity);
                
                OnEntityKill.ForEach(del => del(entity));
            }
        }

        public void AddEntity(Entity.Entity entity) {
            if(entity.Health <= 0) return;
            
            _entities.Add(entity);
        }
    }
}
