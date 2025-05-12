using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{

  public CharacterData data;
  public Character target;

  #region MonoBehaviour

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (data == null)
    {
      Debug.LogWarning("Character data is null!");
      return;
    }
    target = CombatService.GetValidTarget(this);
    Debug.Log($"{gameObject.name} is targetting {(target == null ? "null" : target.gameObject.name)}");
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  #endregion // MonoBehaviour

}
