using UnityEngine;

public class Heart : PowerUp
{
    [SerializeField] private FloatValue _playerHealth;
    [SerializeField] private FloatValue _heartContainers;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger)
        {
            _playerHealth.Value += 4;
            if (_playerHealth.Value > _heartContainers.Value * 4)
                _playerHealth.Value = _heartContainers.Value * 4;

            PowerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
