using Study_Camera.CombatSystem;

namespace Study_Camera.Study_ObjectPool
{
    public class CombatEventBinder
    {
        public void Enable()
        {
            CombatSystem.CombatSystem.Instance.Subscribe.OnSomeoneTakeDamage += OnSomeoneTakeDamage;
        }

        public void Disable()
        {
            CombatSystem.CombatSystem.Instance.Subscribe.OnSomeoneTakeDamage -= OnSomeoneTakeDamage;
        }
        
        private void OnSomeoneTakeDamage(CombatEvent combatEvent)
        {
            string key = combatEvent.HitInfo.parameter == 1 ? "Red" : "Yellow";
            ObjectPoolManager.Instance.SpawnFxObject(key, combatEvent.HitInfo.position);
        }
    }
}