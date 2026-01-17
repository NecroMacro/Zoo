using UnityEngine;

public interface IPhysicsCollisionHandler
{
	void OnCollisionEnterHandler(Collision collision);
	void OnCollisionStayHandler(Collision collision);
	void OnCollisionExitHandler(Collision collision);

	void OnTriggerEnterHandler(Collider other);
	void OnTriggerStayHandler(Collider other);
	void OnTriggerExitHandler(Collider other);
}