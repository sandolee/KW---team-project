﻿#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Galaga.Game {
    public class EntityManager {
        private readonly List<Entity.Entity> _entities = new List<Entity.Entity>();
        public ReadOnlyCollection<Entity.Entity> Entities => _entities.AsReadOnly();

        public delegate void OnEntityKillDelegate(Entity.Entity entity);
        public readonly List<OnEntityKillDelegate> OnEntityKill = new List<OnEntityKillDelegate>();

        public void OnTick(int currentTick) {
            foreach (var entity in _entities) {
                if (entity.Health <= 0) {
                    OnEntityKill.ForEach(del => del(entity));
                } else {
                    entity.OnTick(currentTick);
                }
            }

            _entities.RemoveAll(entity => entity.Health <= 0);
        }

        public void AddEntity(Entity.Entity entity) {
            if(entity.Health <= 0) return;
            
            _entities.Add(entity);
        }
    }
}
