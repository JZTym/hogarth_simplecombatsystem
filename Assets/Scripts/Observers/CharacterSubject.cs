using System.Collections.Generic;
using UnityEngine;

public class CharacterSubject : MonoBehaviour
{
  private List<ICharacterObserver> observers;
  public void AddObserver(ICharacterObserver observer)
  {
    if (observers == null)
      observers = new List<ICharacterObserver>();
    observers.Add(observer);
  }

  public void RemoveObserver (ICharacterObserver observer)
  {
    observers.Remove(observer);
  }

  public void NotifyObservers(CharacterState state)
  {
    observers.ForEach(x => x.OnNotify(state, this));
    observers.Clear();
  }
}
