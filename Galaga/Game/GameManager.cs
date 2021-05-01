using System;
using Galaga.Entity;

#nullable enable

namespace Galaga.Game {
	public class GameManager {
		internal IGame Game { get; private set; }

		internal IGameState State;

		public int Stage { get; private set; }

		public GameManager(IGame game) {
			Game = game;

			Stage = 1;

			State = new IntermediateState(this);
		}

		public int GetMaxStage() {
			return 1;
		}

		public void SetStage(int newStage) {
			Game = newStage switch {
				1 => new Stage1Game(),
				_ => throw new ArgumentOutOfRangeException(
					$"stage cannot be larger than {GetMaxStage()}, given {newStage}")
			};

			Stage = newStage;
		}

		// 스테이지가 바뀔 때마다 Game 인스턴스가 바뀜에 따라 Player 인스턴스도 바뀌기 때문에
		// Player 인스턴스를 사용할 일이 있을 때에는 이 함수를 이용하여 얻어야 함
		public Player GetPlayer() {
			return Game.GetPlayer();
		}

		// 스테이지가 바뀔 때마다 Game 인스턴스가 바뀜에 따라 World 인스턴스도 바뀌기 때문에
		// World 인스턴스를 사용할 일이 있을 때에는 이 함수를 이용하여 얻어야 함
		public World GetWorld() {
			return Game.GetWorld();
		}

		public void Tick(int currentTick) {
			State.Tick(currentTick);
		}

		public void Start() {
			State.Start();
		}

		public void Stop() {
			State.Stop();
		}

		public void Pause() {
			State.Pause();
		}
	}

	internal interface IGameState {
		public void Start();

		public void Stop();

		public void Pause();

		public void Tick(int currentTick);
	}

	class PlayingState : IGameState {
		private GameManager manager;
		
		public PlayingState(GameManager manager) {
			this.manager = manager;
		}
		
		public void Start() {
			// no op
		}

		public void Stop() {
			
		}

		public void Pause() {
			manager.State = new PausedState(manager);
		}

		public void Tick(int currentTick) {
			manager.Game.OnTick(currentTick);
		}
	}

	class PausedState : IGameState {
		private GameManager manager;

		public PausedState(GameManager manager) {
			this.manager = manager;
		}

		public void Start() {
			manager.State = new PlayingState(manager);
		}

		public void Stop() {
			manager.State = new IntermediateState(manager);
		}

		public void Pause() {
			throw new NotSupportedException("cannot pause game while game is paused");
		}

		public void Tick(int currentTick) {
			// no op
		}
	}

	class IntermediateState : IGameState {
		private GameManager manager;
		
		public IntermediateState(GameManager manager) {
			this.manager = manager;
		}
		
		public void Start() {
			manager.State = new PlayingState(manager);
		}

		public void Stop() {
			throw new NotSupportedException("cannot stop game that is not running");
		}

		public void Pause() {
			throw new NotSupportedException("cannot pause while game is not running");
		}

		public void Tick(int currentTick) {
			// no op
		}
	}

	class GameOverState : IGameState {
		public void Start() {
			throw new NotSupportedException("could not start from game over state");
		}

		public void Stop() {
			throw new NotSupportedException("game is already stopped");
		}

		public void Pause() {
			throw new NotSupportedException("cannot pause stopped game");
		}

		public void Tick(int currentTick) {
			// no op
		}
	}

	class CompleteState : IGameState {
		public void Start() {
			
		}

		public void Stop() {
			
		}

		public void Pause() {
			
		}

		public void Tick(int currentTick) {
			
		}
	}
}
