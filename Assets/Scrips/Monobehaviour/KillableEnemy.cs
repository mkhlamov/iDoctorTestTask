using UnityEngine;
using UnityEngine.AI;

namespace iDoctorTestTask
{
    public class KillableEnemy : MonoBehaviour, IKillable
    {
        private Collider _collider;

        public void OnDeath(GameObject killer)
        {
            _collider.enabled = false;
        }

        private void Start()
        {
            _collider = GetComponent<Collider>();
        }
    }
}