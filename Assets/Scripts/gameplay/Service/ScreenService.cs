using JetBrains.Annotations;
using UnityEngine;
using ZooWorld.Gameplay;
using ZooWorld.Gameplay.Movement;

[UsedImplicitly]
public class ScreenService : IMovementController
{
	private readonly Camera currentCamera;
	private readonly EnvironmentData environment;
	public bool IsNeedChangeDirection(Transform target) => IsOutOfScreen(target);
	public Camera CurrentCamera => currentCamera;
	public Vector3 GetReturnDirection(Transform target) => GetCenterScreenDirection(target);

	public ScreenService(EnvironmentData environment)
	{
		this.environment = environment;
		currentCamera = environment.EnvironmentCamera;
	}

	public Vector3 GetRandomGroundPoint()
	{
		float x = Random.Range(0f, Screen.width);
		float y = Random.Range(0f, Screen.height);
		Vector3 screenPos = new Vector3(x, y, 0);
		
		Ray ray = currentCamera.ScreenPointToRay(screenPos);
		return environment.GroundCollider.Raycast(ray, out RaycastHit hit, Mathf.Infinity) ?
			hit.point : Vector3.zero;
	}
	
	private Vector3 GetCenterScreenDirection(Transform target)
	{
		var forward = currentCamera.transform.forward;
		var position = currentCamera.transform.position;
    
		Vector3 targetPoint = new Vector3(
			position.x + forward.x * 10f,
			target.transform.position.y,
			position.z + forward.z * 10f
		);

		return (targetPoint - target.position).normalized;
	}
	
	private bool IsOutOfScreen(Transform target, float visibleArea = 0.9f)
	{
		Vector3 viewportPos = currentCamera.WorldToViewportPoint(target.position);

		float min = (1f - visibleArea) / 2f;
		float max = 1f - min;

		bool result = viewportPos.x < min || viewportPos.x > max ||
			viewportPos.y < min || viewportPos.y > max ||
			viewportPos.z <= 0;
		
		return result;
	}
}