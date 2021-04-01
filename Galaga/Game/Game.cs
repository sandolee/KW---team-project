﻿#nullable enable

namespace Galaga.Game {
    public class Game {
        private IGameDelegate? _gameDelegate;
        private int _stage = 0;

        public Game(IGameDelegate? gameDelegate = null) {
            _gameDelegate = gameDelegate;
        }

        public void SetStage(int stage) {
            _stage = stage;
            
            // TODO set IGameDelegate, IEnemySpawner to match stage value
        }

        private void SetGameDelegate(IGameDelegate gameDelegate) {
            _gameDelegate = gameDelegate;
        }

        public void OnTick(int currentTick) {
            _gameDelegate?.OnTick(currentTick);
        }

        public EntityManager? GetEntityManager() {
            return _gameDelegate?.GetEntityManager();
        }
    }

    public interface IGameDelegate {
        EntityManager GetEntityManager();

        void OnTick(int currentTick);
    }
}
