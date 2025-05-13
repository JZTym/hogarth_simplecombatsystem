using UnityEngine;
using UnityEngine.Events;

public static class CombatService
{

  private static CombatManager combatManager;

  public static void SetManager(CombatManager c)
  {
    combatManager = c;
  }

  public static Character GetValidTarget(Character self)
  {
    if (combatManager == null)
      return null;
    return combatManager.GetValidTarget(self);
  }

  public static void SubscribeToEndCombat(UnityAction<EndState, Character> action)
  {
    if (combatManager == null)
      return;
    combatManager.SubscribeToEndCombat(action);
  }
}
