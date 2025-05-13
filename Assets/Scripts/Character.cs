using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
  void Damage(float damage);
}

public class Character : MonoBehaviour, IDamageable
{
  public CharacterData Data;
  public Character target;
  private UnityEvent<Character> onDeathEvt = new UnityEvent<Character>();

  #region Events

  public void SubscribeToOnDeathEvent(UnityAction<Character> action)
  {
    onDeathEvt.AddListener(action);
  }

  public void UnsubscribeToOnDeathEvent(UnityAction<Character> action)
  {
    onDeathEvt.RemoveListener(action);
  }

  #endregion // Events

  public void Damage(float damage)
  {
    Data.Health -= damage;
    if (Data.Health <= 0)
    {
      Kill();
    }
  }

  public void Kill()
  {
    onDeathEvt.Invoke(this);
    onDeathEvt.RemoveAllListeners();

    // Do death stuff
    Destroy(gameObject);
  }

  #region MonoBehaviour

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (Data == null)
    {
      Debug.LogWarning("Character data is null!");
      return;
    }
    AcquireNewTarget();
  }

  // Update is called once per frame
  void Update()
  {
    CheckState();
    switch (state)
    {
      case State.Idle:
        break;
      case State.Rotating:
        UpdateRotation();
        break;
      case State.Moving:
        UpdateMovement();
        break;
      case State.Attacking:
        break;
    }
  }

  #endregion // MonoBehaviour

  #region States

  public State state = State.Idle;

  public enum State
  {
    Idle,
    Moving,
    Rotating,
    Attacking,
  }

  private void CheckState()
  {
    if (target == null)
    {
      StopAttacking();
      state = State.Idle;
    }
    else if (!IsAimedCorrectly())
    {
      StopAttacking();
      state = State.Rotating;
    }
    else if (!IsInRange())
    {
      StopAttacking();
      state = State.Moving;
    }
    else
    {
      StartAttacking();
      state = State.Attacking;
    }
  }

  #endregion // States

  #region Attacking

  private bool IsInRange()
  {
    if (target == null)
      return false;
    return Data.Weapon.Data.Range >= Vector3.Distance(target.transform.position, transform.position);
  }

  private void StartAttacking()
  {
    Data.Weapon.IsFiring = true;
  }

  private void StopAttacking()
  {
    Data.Weapon.IsFiring = false;
  }

  #endregion // Attacking

  #region Moving

  private void UpdateMovement()
  {
    if (target == null)
      return;
    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Data.MoveSpeed * Time.deltaTime);
  }

  #endregion // Moving

  #region Targetting

  private bool IsAimedCorrectly()
  {
    if (target == null)
      return false;
    return transform.rotation == Quaternion.LookRotation(target.transform.position - transform.position);
  }

  private void UpdateRotation()
  {
    if (target == null)
      return;

    transform.rotation = Quaternion.RotateTowards(
      transform.rotation,
      Quaternion.LookRotation(target.transform.position - transform.position),
      Data.RotateSpeed * Time.deltaTime);
  }

  private void AcquireNewTarget()
  {
    target = CombatService.GetValidTarget(this);
    Debug.Log($"{gameObject.name} is targetting {(target == null ? "null" : target.gameObject.name)}");
    if (target == null)
      return;
    target.SubscribeToOnDeathEvent((Character c) =>
    {
      AcquireNewTarget();
    });
  }

  #endregion // Targetting

}
