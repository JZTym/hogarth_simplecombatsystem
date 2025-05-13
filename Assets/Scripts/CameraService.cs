using UnityEngine;

public static class CameraService
{
  private static CameraManager cameraManager;

  public static void SetManager(CameraManager c)
  {
    cameraManager = c;
  }

  public static Vector3 Min {
    get {
      if (cameraManager == null)
        return Vector3.zero;
      return cameraManager.Min;
    }
  }

  public static Vector3 Max {
    get {
      if (cameraManager == null)
        return Vector3.zero;
      return cameraManager.Max;
    }
  }
}
