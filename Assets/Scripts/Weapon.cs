using UnityEngine;

public class Weapon : MonoBehaviour
{
  public WeaponData Data;
  public bool IsFiring = false;
  [SerializeField] private GameObject bulletPrefab;
  [SerializeField] private Transform muzzlePosition;
  private float elapsedTime;

  public enum State
  {
    Idle,
    Firing,
    Cooldown
  }

  private State state;

  #region Firing

  void UpdateFire()
  {
    switch (state)
    {
      case State.Idle:
        if (IsFiring)
          state = State.Firing;
        break;
      case State.Firing:
        Fire();
        state = State.Cooldown;
        break;
      case State.Cooldown:
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= Data.AttackSpeed)
        {
          if (IsFiring)
            state = State.Firing;
          else
            state = State.Idle;
          elapsedTime = 0;
        }
        break;
    }
  }

  void Fire()
  {
    if (bulletPrefab == null)
      return;
    ObjectPoolService.SpawnObject(bulletPrefab, muzzlePosition.position, muzzlePosition.rotation);
  }

  #endregion // Firing

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    UpdateFire();
  }
}
