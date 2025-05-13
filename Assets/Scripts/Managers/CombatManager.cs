using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum EndState
{
  Draw,
  OneWinner
}

public class CombatManager : MonoBehaviour
{
  [SerializeField] private List<Character> activeCharacters = new List<Character>();
  private UnityEvent<EndState, Character> onEndCombatEvent = new UnityEvent<EndState, Character>();

  public Character GetValidTarget(Character self)
  {
    List<Character> others = activeCharacters.Where(x => x != self).ToList();
    if (others.Count <= 0) return null; // No valid target
    int i = UnityEngine.Random.Range(0, others.Count);
    return others[i];
  }

  public void RemoveCharacter(Character character)
  {
    if (activeCharacters == null)
      return;
    if (activeCharacters.Count <= 0)
      return;
    activeCharacters.Remove(character);

    if (activeCharacters.Count <= 0)
      onEndCombatEvent?.Invoke(EndState.Draw, null);
    else if (activeCharacters.Count <= 1)
      onEndCombatEvent?.Invoke(EndState.OneWinner, activeCharacters.FirstOrDefault());
  }

  public void SubscribeToEndCombat(UnityAction<EndState, Character> action)
  {
    onEndCombatEvent.AddListener(action);
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
  {
    CombatService.SetManager(this);
  }

  void Start()
  {
    foreach (Character character in activeCharacters)
    {
      if (character == null)
        continue;
      character.SubscribeToOnDeathEvent((Character c) =>
      {
        RemoveCharacter(c);
      });
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
