using TMPro;
using UnityEngine;

public class WinnerText : MonoBehaviour
{
  [SerializeField] private TMP_Text text = null;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (text == null)
      text = GetComponent<TMP_Text>();

    if (text == null)
      return;

    text.text = string.Empty;
    text.enabled = false;

    CombatService.SubscribeToEndCombat((EndState s, Character c) =>
    {
      switch (s)
      {
        case EndState.Draw:
          text.text = $"It's a draw!";
          break;
        case EndState.OneWinner:
          text.text = $"{c.name} wins!";
          break;
      }
      text.enabled = true;
    });
  }
}
