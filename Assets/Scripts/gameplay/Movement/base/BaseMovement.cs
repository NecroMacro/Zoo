using UnityEngine;
using ZooWorld.Gameplay.Configs;

namespace ZooWorld.Gameplay.Movement
{
	public abstract class BaseMovement : MonoBehaviour, IMovable
	{
		public abstract void ApplyConfig(MovementConfig config);
		public abstract void ApplyDirectionChanger(IMovementController directionChanger);
		public abstract void Move();
		public abstract void Reset();
	}
	
	public abstract class BaseMovement<TConfig> : BaseMovement where TConfig : MovementConfig
	{
		private IMovementController movementController;
		protected TConfig Config { get; private set; }
		protected Rigidbody RigidBody { get; private set; }
		protected bool IsOutOfBounds => movementController.IsNeedChangeDirection(transform);
		protected Vector3 GetReturnDirection() => movementController.GetReturnDirection(transform);
		
		protected Vector3 TargetDirection;
		
		private void Start()
		{
			if (!Config)
				throw new System.NullReferenceException("Config is null");
			
			RigidBody = GetComponent<Rigidbody>();
			
			if (!RigidBody) 
				throw new System.Exception("No RigidBody attached");
			
			OnStart();
		}

		public override void Reset()
		{
			if (!RigidBody) return;
			
			RigidBody.linearVelocity = Vector3.zero;
			RigidBody.angularVelocity = Vector3.zero;
			TargetDirection = Vector3.zero;
		}
		
		public override void ApplyConfig(MovementConfig config)
		{
			if (config is not TConfig typedConfig) 
				throw new System.Exception("Config is not of type " + typeof(TConfig).Name);
			
			Config = typedConfig;
		}
		
		public override void ApplyDirectionChanger(IMovementController directionChanger)
		{
			this.movementController = directionChanger;
		}
		
		protected virtual void FixedUpdate()
		{
			Move();
		}
		
		protected virtual Vector3 PickRandomDirection()
		{
			float angle = Random.Range(0f, 360f);
			float rad = angle * Mathf.Deg2Rad;

			return new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)).normalized;
		}
		
		protected void SetNewRandomDirection()
		{
			TargetDirection = PickRandomDirection();
		}
		
		protected abstract void OnStart();
		protected abstract void OnRestart();
	}
}