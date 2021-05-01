#nullable enable

namespace Galaga.Game {
    public class Game {
        private IGameDelegate? gameDelegate;
        private int stage = 0;

        public Game(IGameDelegate? gameDelegate = null) {
            this.gameDelegate = gameDelegate;
        }

        public void SetStage(int value) {
            this.stage = value;
            
            // TODO set IGameDelegate, IEnemySpawner to match stage value
        }

        private void SetGameDelegate(IGameDelegate gd) {
            this.gameDelegate = gd;
        }

        public void OnTick(int currentTick) {
            gameDelegate?.OnTick(currentTick);
        }

        public World? GetWorld() {
            return gameDelegate?.GetWorld();
        }
    }

    public interface IGameDelegate {
        World GetWorld();

        void OnTick(int currentTick);
    }

    public class SimpleGameDelegate : IGameDelegate {
        private World world;

        public SimpleGameDelegate(World world) {
            this.world = world;
        }
        
        public World GetWorld() {
            return world;
        }

        public void OnTick(int currentTick) {
            world.OnTick(currentTick);
        }
    }
}
