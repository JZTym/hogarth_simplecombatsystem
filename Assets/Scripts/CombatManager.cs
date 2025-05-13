using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour, ICharacterObserver
{
  public List<Character> activeCharacters = new List<Character>();

  public Character GetValidTarget(Character self)
  {
    List<Character> others = activeCharacters.Where(x => x != self).ToList();
    if (others.Count <= 0) return null; // No valid target
    int i = UnityEngine.Random.Range(0, others.Count);
    return others[i];
  }

  #region Character Observer

  public void OnNotify(CharacterState state, CharacterSubject context)
  {
    Character character = (Character)context;
    if (character == null)
      return;
    activeCharacters.Remove(character);
  }

  public void OnNotify(int data, object context)
  {
    OnNotify((CharacterState) data, (Character) context);
  }

  #endregion // Character Observer

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
  {
    CombatService.SetManager(this);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
