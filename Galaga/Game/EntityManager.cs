#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Galaga.Game {
    public class Score
    {
        public bool scoreFlag { get; private set; }

        public void SetScore() {
            scoreFlag = true;
        }
        public void EndScore()
        {
            scoreFlag = false;
        }

    }
    public class EntityManager {
        public Score score=new Score();
        private readonly List<Entity.Entity> _entities = new List<Entity.Entity>();

        public ReadOnlyCollection<Entity.Entity> Entities;

        public EntityManager() {
            Entities = _entities.AsReadOnly();
        }
        
        public delegate void OnEntityKillDelegate(Entity.Entity entity);
        public readonly List<OnEntityKillDelegate> OnEntityKill = new List<OnEntityKillDelegate>();
        
        //틱마다 list에 delegate가 추가되는 문제 
        public delegate void OnEntityAttackedDelegate(Entity.Entity entity);
        public readonly List<OnEntityAttackedDelegate> OnEntityAttacked = new List<OnEntityAttackedDelegate>();

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
