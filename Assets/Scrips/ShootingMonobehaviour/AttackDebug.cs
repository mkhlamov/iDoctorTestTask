using UnityEngine;

namespace iDoctorTestTask
{
    public class AttackDebug : MonoBehaviour, IAttackable
    {
        public void OnAttack(GameObject attacker, Attack attack)
        {
            Debug.Log($"{name} attacked by {attacker.name} damage {attack.Damage}");
            if (attack.IsCritical)
            {
                Debug.Log("Critical");
            }
        }
    }
}