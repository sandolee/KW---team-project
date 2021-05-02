﻿using System;
using System.Timers;
using Galaga.Entity;

#nullable enable

namespace Galaga.Game {
	public class GameManager {
		internal IGame Game { get; private set; }

		internal IGameState State;

		public int Stage { get; private set; }

		public GameManager() {
			Stage = 1;
			Game = GetGameByStage(Stage);

			State = new IntermediateState(this);
		}

		public int GetMaxStage() {
			return 1;
		}

		public void SetStage(int newStage) {
			Game = GetGameByStage(newStage);

			Stage = newStage;
		}

		private IGame GetGameByStage(int stage) {
			return stage switch {
				1 => new Stage1Game(),
				_ => throw new ArgumentOutOfRangeException(
					$"stage cannot be larger than {GetMaxStage()}, given {stage}")
			};
		}

		public void MovePlayer(int dx, int dy) {
			State.MovePlayer(dx, dy);
		}

		public void Shoot() {
			State.Shoot();
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

		public void MovePlayer(int dx, int dy);

		public void Shoot();

		public void Tick(int currentTick);
	}

	public abstract class BaseState : IGameState {
		public virtual void Start() {
			
		}

		public virtual void Stop() {
			
		}

		public virtual void Pause() {
			
		}

		public virtual void MovePlayer(int dx, int dy) {
			
		}

		public virtual void Shoot() {
			
		}

		public virtual void Tick(int currentTick) {
			
		}
	}

	// 게임을 플레이 중인 상태
	class PlayingState : BaseState {
		private readonly GameManager manager;
		
		public PlayingState(GameManager manager) {
			this.manager = manager;
		}

		public override void Start() {
			throw new NotSupportedException("cannot start game while running");
		}

		public override void Stop() {
			manager.State = new IntermediateState(manager);
		}

		public override void Pause() {
			manager.State = new PausedState(manager);
		}

		public override void MovePlayer(int dx, int dy) {
			manager.Game.GetPlayer().Move(dx, dy);
		}

		public override void Shoot() {
			var player = manager.Game.GetPlayer();
			player.Shoot();
		}

		public override void Tick(int currentTick) {
			manager.Game.OnTick(currentTick);

			var player = manager.Game.GetPlayer();
			if (player.Health <= 0) {
				manager.State = new GameOverState(manager);
			} else {
				if (!HasGameCleared()) return;
				
				if (manager.Stage >= manager.GetMaxStage()) {
					// 모든 스테이지를 클리어
					manager.State = new CompleteAllState(manager);
					return;
				}

				var nextState = new CompleteState(manager);

				var timer = new Timer {
					Interval = 5000, // 5초 후에 스테이지 준비 상태로 변경
					Enabled = true
				};
				timer.Elapsed += (sender, args) => {
					if (manager.State == nextState) {
						manager.State = new IntermediateState(manager);
					}
				};
					
				manager.State = nextState;
					
				manager.SetStage(manager.Stage + 1);
			}
		}

		private bool HasGameCleared() {
			// TODO 게임 클리어 조건 정의

			//return new Random().NextDouble() < 0.01;
			return false;
		}
	}

	// 일시정지 버튼 같은 것으로 인해 게임이 중단된 상태
	public class PausedState : BaseState {
		private readonly GameManager manager;

		internal PausedState(GameManager manager) {
			this.manager = manager;
		}

		public override void Start() {
			manager.State = new PlayingState(manager);
		}

		public override void Stop() {
			manager.State = new IntermediateState(manager);
		}

		public override void Pause() {
			throw new NotSupportedException("cannot pause game while game is paused");
		}

		public override void Tick(int currentTick) {
			// no op
		}
	}

	// 스테이지 시작 전 또는 스테이지 종료 후 다음 게임을 시작하기 전 상태
	public class IntermediateState : BaseState {
		private readonly GameManager manager;
		
		internal IntermediateState(GameManager manager) {
			this.manager = manager;
		}

		public override void Start() {
			manager.State = new PlayingState(manager);
		}

		public override void Stop() {
			throw new NotSupportedException("cannot stop game that is not running");
		}

		public override void Pause() {
			throw new NotSupportedException("cannot pause while game is not running");
		}

		public override void Tick(int currentTick) {
			// no op
		}
	}

	// 스테이지 실패한 상태
	public class GameOverState : BaseState {
		private readonly GameManager manager;

		internal GameOverState(GameManager manager) {
			this.manager = manager;
		}

		public override void Start() {
			throw new NotSupportedException("could not start from game over state");
		}

		public override void Stop() {
			throw new NotSupportedException("game is already stopped");
		}

		public override void Pause() {
			throw new NotSupportedException("cannot pause stopped game");
		}

		public override void Tick(int currentTick) {
			// no op
		}
	}

	// 스테이지를 클리어 한 상태
	public class CompleteState : BaseState {
		private readonly GameManager manager;

		internal CompleteState(GameManager manager) {
			this.manager = manager;
		}

		public override void Stop() {
			throw new NotSupportedException("cannot stop complete game");
		}

		public override void Pause() {
			throw new NotSupportedException("cannot pause completed game");
		}

		public override void Tick(int currentTick) {
			// no op
		}
	}

	// 게임의 모든 스테이지를 클리어한 상태
	public class CompleteAllState : BaseState {
		private readonly GameManager manager;

		internal CompleteAllState(GameManager manager) {
			this.manager = manager;
		}
		
		public override void Start() {
			throw new NotSupportedException("cannot start finished game");
		}

		public override void Stop() {
			throw new NotSupportedException("cannot stop finished game");
		}

		public override void Pause() {
			throw new NotSupportedException("cannot pause finished game");
		}
	}
}
