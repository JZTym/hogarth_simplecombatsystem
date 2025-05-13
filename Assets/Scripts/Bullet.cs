using UnityEngine;

public class Bullet : MonoBehaviour
{
  public BulletData data;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag != "Player")
      return;
    var damageable = other.GetComponent<IDamageable>();
    damageable.Damage(data.Damage);
    Destroy(gameObject);
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = transform.position + (transform.forward * data.Speed * Time.deltaTime);
    if (transform.position.x < CameraService.Min.x || transform.position.x > CameraService.Max.x ||
      transform.position.z < CameraService.Min.z || transform.position.z > CameraService.Max.z)
    {
      Destroy(gameObject);
    }
  }
}
