#nullable enable

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

        public World? GetWorld() {
            return _gameDelegate?.GetWorld();
        }
    }

    public interface IGameDelegate {
        World GetWorld();

        void OnTick(int currentTick);
    }

    public class SimpleGameDelegate : IGameDelegate {
        private World _world;

        public SimpleGameDelegate(World world) {
            _world = world;
        }
        
        public World GetWorld() {
            return _world;
        }

        public void OnTick(int currentTick) {
            _world.OnTick(currentTick);
        }
    }
}
