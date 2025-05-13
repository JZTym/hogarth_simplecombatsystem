using UnityEngine;

public class CameraManager : MonoBehaviour
{
  private Camera mainCamera;
  public Vector3 Min;
  public Vector3 Max;

  private void Start()
  {
    mainCamera = GetComponent<Camera>();
    if (mainCamera == null)
      mainCamera = Camera.main;
    if (mainCamera == null)
    {
      Debug.LogWarning("No camera found!");
      return;
    }

    // Calculate bounds
    Min = mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, -0.1f, mainCamera.transform.position.y));
    Max = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, mainCamera.transform.position.y));
    Debug.Log($"Min: {Min} | Max: {Max}");

    // Set CameraService
    CameraService.SetManager(this);
  }
}