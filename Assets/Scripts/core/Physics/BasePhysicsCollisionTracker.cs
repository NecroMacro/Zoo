using UnityEngine;

public abstract class BasePhysicsCollisionTracker : MonoBehaviour, IPhysicsCollisionHandler
{
	private void OnCollisionEnter(Collision collision) => OnCollisionEnterHandler(collision);
	private void OnCollisionStay(Collision collision) => OnCollisionStayHandler(collision);
	private void OnCollisionExit(Collision collision) => OnCollisionExitHandler(collision);

	private void OnTriggerEnter(Collider other) => OnTriggerEnterHandler(other);
	private void OnTriggerStay(Collider other) => OnTriggerStayHandler(other);
	private void OnTriggerExit(Collider other) => OnTriggerExitHandler(other);

	public virtual void OnCollisionEnterHandler(Collision collision) { }
	public virtual void OnCollisionStayHandler(Collision collision) { }
	public virtual void OnCollisionExitHandler(Collision collision) { }

	public virtual void OnTriggerEnterHandler(Collider other) { }
	public virtual void OnTriggerStayHandler(Collider other) { }
	public virtual void OnTriggerExitHandler(Collider other) { }
}

public abstract class BasePhysicsCollisionTracker<T> : BasePhysicsCollisionTracker where T : BasePhysicsCollisionTracker
{
	public override void OnCollisionEnterHandler(Collision collision)
	{
		if (!collision.gameObject.TryGetComponent<T>(out T other))
			return;
			
		if (GetInstanceID() > other.GetInstanceID())
			return;
		
		OnCollisionEnterHandler(other);
	}

	protected abstract void OnCollisionEnterHandler(T other);
}