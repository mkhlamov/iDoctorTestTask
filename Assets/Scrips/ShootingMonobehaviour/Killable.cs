using UnityEngine;

namespace iDoctorTestTask
{
    public class Killable : MonoBehaviour, IKillable
    {
        public void OnDeath(GameObject killer)
        {
            Destroy(gameObject);
        }
    }
}