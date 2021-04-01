#nullable enable

namespace Galaga.Game {
    public class Game {
        private IGameDelegate? _gameDelegate;

        public Game(IGameDelegate? gameDelegate = null) {
            _gameDelegate = gameDelegate;
        }

        public void SetGameDelegate(IGameDelegate gameDelegate) {
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
