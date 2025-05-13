using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
  public List<PooledObjectData> ObjectPools = new List<PooledObjectData>();

  private void Start()
  {
    ObjectPoolService.SetManager(this);
  }

  public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
  {
    // Get pool if existing
    PooledObjectData pool = ObjectPools.Find(p => p.ID == prefab.name);


    // Create new pool if it doesn't exist
    Transform poolParent = null;
    if (pool == null)
    {
      pool = new PooledObjectData()
      {
        ID = prefab.name,
      };
      ObjectPools.Add(pool);
      poolParent = new GameObject($"Pool: {prefab.name}").transform;
      poolParent.parent = transform;
    }
    else
    {
      poolParent = transform.Find($"Pool: {prefab.name}");
    }

    // Check for inactive objects
    GameObject objectToSpawn = pool.Inactive.FirstOrDefault();

    // If no object available, instantiate one
    if (objectToSpawn == null)
    {
      objectToSpawn = Instantiate(prefab, position, rotation);
      objectToSpawn.name = prefab.name;
      objectToSpawn.transform.parent = poolParent;
      return objectToSpawn;
    }

    objectToSpawn.transform.position = position;
    objectToSpawn.transform.rotation = rotation;
    pool.Inactive.Remove(objectToSpawn);
    objectToSpawn.SetActive(true);

    return objectToSpawn;
  }

  public void DespawnObject(GameObject obj)
  {
    PooledObjectData pool = ObjectPools.Find(p => p.ID == obj.name);

    if (pool == null)
      return;

    obj.SetActive(false);
    pool.Inactive.Add(obj);
  }
}

public class PooledObjectData
{
  public string ID;
  public List<GameObject> Inactive = new List<GameObject>();
}
