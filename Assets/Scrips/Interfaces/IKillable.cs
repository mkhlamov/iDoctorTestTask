using UnityEngine;

namespace iDoctorTestTask
{
    public interface IKillable
    {
        void OnDeath(GameObject killer);
    }
}