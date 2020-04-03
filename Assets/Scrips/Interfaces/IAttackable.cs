using UnityEngine;

namespace iDoctorTestTask
{
    public interface IAttackable
    {
        void OnAttack(GameObject attacker, Attack attack);
    }
}