using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{

  public CharacterData data;
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
    if (data == null)
    {
      Debug.LogWarning("Character data is null!");
      return;
    }
    AcquireNewTarget();
  }

  // Update is called once per frame
  void Update()
  {

  }

  #endregion // MonoBehaviour

  #region Targetting

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
