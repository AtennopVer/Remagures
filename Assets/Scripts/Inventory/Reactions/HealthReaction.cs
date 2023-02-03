using Remagures.SO;
using UnityEngine;

namespace Remagures.Inventory
{
    public class HealthReaction : MonoBehaviour
    {
        [SerializeField] private FloatValue _playerHealth;
        [SerializeField] private Signal _healthSignal;
    
        public void Use(int amountToIncrease)
        {
            _playerHealth.Value += amountToIncrease;
            _healthSignal.Invoke();
        }
    }
}
