using R3;

namespace ZooWorld.Gameplay.Collision
{
	public class AnimalPhysicsCollisionTracker : BasePhysicsCollisionTracker<AnimalPhysicsCollisionTracker>
	{
		private readonly Subject<(AnimalPhysicsCollisionTracker, AnimalPhysicsCollisionTracker)> subject = new();
		public Observable<(AnimalPhysicsCollisionTracker, AnimalPhysicsCollisionTracker)> OnCollision => subject;
		
		protected override void OnCollisionEnterHandler(AnimalPhysicsCollisionTracker other)
		{
			subject.OnNext((this, other));
		}
	}
}