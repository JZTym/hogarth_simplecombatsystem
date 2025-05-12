using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
  public List<Character> activeCharacters = new List<Character>();

  public Character GetValidTarget(Character self)
  {
    List<Character> others = activeCharacters.Where(x => x != self).ToList();
    if (others.Count <= 0) return null; // No valid target
    int i = UnityEngine.Random.Range(0, others.Count);
    return activeCharacters[i];
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {

  }
}
