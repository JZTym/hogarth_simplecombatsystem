using UnityEngine;

public static class ObjectPoolService
{
  public static ObjectPoolManager objectPoolManager;

  public static void SetManager(ObjectPoolManager opm)
  {
    objectPoolManager = opm;
  }

  public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
  {
    if (objectPoolManager == null)
      return null;
    return objectPoolManager.SpawnObject(prefab, position, rotation);
  }

  public static void DespawnObject(GameObject obj)
  {
    if (objectPoolManager == null)
      return;
    objectPoolManager.DespawnObject(obj);
  }
}
