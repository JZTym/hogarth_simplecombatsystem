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
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  #endregion // MonoBehaviour

  #region Targetting

  private void FindTarget ()
  {
    if (target != null)
      return;

  }

  #endregion // Targetting

}
