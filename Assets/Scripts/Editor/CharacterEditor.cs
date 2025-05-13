using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
[CanEditMultipleObjects]
public class CharacterEditor : Editor
{

  private void OnEnable()
  {

  }

  public override void OnInspectorGUI()
  {
    serializedObject.Update();
    base.OnInspectorGUI();

    if (GUILayout.Button("Kill"))
    {
      Character character = (Character)target;
      character.Kill();
    }
    serializedObject.ApplyModifiedProperties();
  }
}
