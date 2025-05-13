public interface IObserver
{
  void OnNotify(int data, object context);
}

public enum CharacterState
{
  OnDamage,
  OnDeath,
}
public interface ICharacterObserver : IObserver
{
  void OnNotify(CharacterState state, CharacterSubject context);
}
