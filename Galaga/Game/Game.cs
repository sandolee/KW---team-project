using System;
using System.Collections.Generic;
using Galaga.Entity;
using Galaga.Entity.EnemyEntity;

#nullable enable

namespace Galaga.Game {
    public interface IGame {
        public World GetWorld();

        public Player GetPlayer();

        // 플레이어가 게임을 클리어 했는지 여부
        public bool IsCleared();

        // 플레이어가 패배했는지 여부를 반환
        public bool IsOver();

        public void OnTick(int currentTick);

        public int GetScore();
        public void SetScore(int score);
    }

    public abstract class BaseGame : IGame {
        private readonly World _world;
        private readonly Player _player;
        private int _score;
        protected BaseGame(World world) {
            this._world = world;

            _player = new Player(world);
            world.EntityManager.AddEntity(_player);
            _score = 0;
        }

        public abstract bool IsCleared();

        public abstract bool IsOver();

        public World GetWorld() {
            return _world;
        }

        public Player GetPlayer() {
            return _player;
        }

        public void OnTick(int currentTick) {
            _world.OnTick(currentTick);
        }

        public void SetScore(int score)
         {
            _world.EntityManager.OnEntityAttacked.Add(
                entity =>
                {
                    if (entity is Enemy)
                    {
                        _score += score;
                    }
                });
        }
        public int GetScore()
        {
            return this._score;
        }
    }
    
    internal class Stage1Game : BaseGame {
        public Stage1Game() : base(new World()) {
            var world = GetWorld();
            world.SetEntitySpawner(new EntitySpawner(world));

            //피격시 10점 
            SetScore(10);
        }

        public override bool IsCleared() {
            return false;
        }

        public override bool IsOver() {
            return false;
        }

        // Stage 1의 엔티티 소환을 관리
        // item 소환, form tab 순서 관리, 이미지 크기 줄이기
        private class EntitySpawner : IEntitySpawner {
            private int _lastSpawn = -1;

            private readonly World _world;

            public EntitySpawner(World world) {
                _world = world;
            }
            
            public IEnumerable<Entity.Entity> GetSpawnEntities(int currentTick) {

                var worldHeight = _world.Size.Height;
                var worldWidth = _world.Size.Width;


                if (currentTick - _lastSpawn > 100) {
                    var entities = new List<Entity.Entity>();

                    //30% 확률로 item 생성
                    var items = new List<Entity.Entity>();//적 생성 후 entities에 추가 
                    Random p = new Random();
                    Random randomX = new Random();
                    if (p.Next(1, 101) <= 30)
                    {
                        items.Add(new Potion(
                        new Position(randomX.Next(5, worldWidth - 5), worldHeight - 5),
                        _world, new Size(10, 10),
                        1));
                    }

                    if (p.Next(1, 101) <= 30)
                    {
                        items.Add(new Heart(
                        new Position(randomX.Next(5, worldWidth - 5), worldHeight - 5),
                        _world, new Size(10, 10),
                        1));
                    }

                    for (var i = 0; i < 4; ++i) {
                        entities.Add(new StraightEnemy(
                            new Position(worldWidth / 5 * (i + 1), 0), 
                            _world, new Size(10, 10),
                            5
                        ));

                        _lastSpawn = currentTick;
                    }
                    
                    //item 추가 
                    entities.AddRange(items);

                    return entities;
                }
                
                return new Entity.Entity[0];
            }

        }
    }
}
