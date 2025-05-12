using UnityEngine;

public static class CombatService
{

  private static CombatManager combatManager;

  public static void SetManager(CombatManager c)
  {
    combatManager = c;
  }

  public static Character GetValidTarget (Character self)
  {
    if (combatManager == null)
      return null;
    return combatManager.GetValidTarget(self);
  }
}
