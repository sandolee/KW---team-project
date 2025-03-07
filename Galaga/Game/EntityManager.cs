﻿#nullable enable

using System.Collections.Generic;
using System.Linq;
using Galaga.Entity;

namespace Galaga.Game {
    public class EntityManager {
        private readonly List<Entity.Entity> _entityBuffer = new List<Entity.Entity>();
        
        private readonly List<Entity.Entity> _entities = new List<Entity.Entity>();

        public IEnumerable<Entity.Entity> Entities => _entities.Skip(0);

        public delegate void OnEntityKillDelegate(Entity.Entity entity);
        public readonly List<OnEntityKillDelegate> OnEntityKill = new List<OnEntityKillDelegate>();

        //틱마다 list에 delegate가 추가되는 문제 
        public delegate void OnEntityAttackedDelegate(Entity.Entity entity);
        public readonly List<OnEntityAttackedDelegate> OnEntityAttacked = new List<OnEntityAttackedDelegate>();

        public void OnTick(int currentTick) {
            var removing = new List<Entity.Entity>();

            foreach (var entity in _entities) {
                if (ShouldDeleteEntity(entity)) {
                    removing.Add(entity);
                    OnEntityKill.ForEach(del => del(entity));
                } else {
                    entity.OnTick(currentTick);
                }
            }

            _entities.RemoveAll(entity => removing.Contains(entity));

            if (_entityBuffer.Count > 0) {
                foreach (var entity in _entityBuffer) {
                    _entities.Add(entity);
                }
                _entityBuffer.Clear();
            }
        }

        private bool ShouldDeleteEntity(Entity.Entity entity) {
            var world = entity.World;
            
            return entity.Health <= 0
                   || (!(entity is ISelfDisposingEntity) 
                       && (entity.Position.X < 0 || world.Size.Width < entity.Position.X
                            || entity.Position.Y > world.Size.Height));
        }

        public void AddEntity(Entity.Entity entity) {
            if(entity.Health <= 0) return;
            
            _entityBuffer.Add(entity);
        }
    }
}
