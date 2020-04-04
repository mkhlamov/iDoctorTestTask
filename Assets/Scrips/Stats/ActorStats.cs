using UnityEngine;

namespace iDoctorTestTask
{
    public class ActorStats : MonoBehaviour
    {
        #region VariableDeclaration
        public ActorStatsSO _actorStatsSOdefault;
        public ActorStatsSO _actorStatsSO;
        #endregion

        #region Monobehaviour
        private void Start()
        {
            // Here we just set default stats every time
            // but this can be modified to save stats between 
            //game sessions and so on
            if (_actorStatsSOdefault != null)
            {
                _actorStatsSO = Instantiate(_actorStatsSOdefault);
            }
        }
        #endregion

        #region Health
        public void AddHealth(int health)
        {
            _actorStatsSO.AddHealth(health);
        }

        public void TakeDamage(int damage)
        {
            _actorStatsSO.TakeDamage(damage);
        }

        public int GetHealth()
        {
            return _actorStatsSO._currentHealth;
        }

        public int GetDamage()
        {
            return _actorStatsSO._damage;
        }

        public bool IsDead()
        {
            return _actorStatsSO._currentHealth <= 0;
        }

        public void ResetStats()
        {
            _actorStatsSO._damage = _actorStatsSOdefault._damage;
            _actorStatsSO._currentHealth = _actorStatsSOdefault._currentHealth;
            _actorStatsSO._maxHealth = _actorStatsSOdefault._maxHealth;
        }
        #endregion
    }
}