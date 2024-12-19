using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] private float healthPoints = 100f;
        [SerializeField] private bool isDead = false;

        public bool IsDead{
            get { return isDead; }
        }

        

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if (healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}