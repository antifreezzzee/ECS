namespace Components
{
    public class TrapAbility : CollisionAbility, ICollisionAbility
    {
        public int Damage = 10;

        public void Execute()
        {
            foreach (var collider in Collisions)
            {
                var colliderHealth = collider?.gameObject.GetComponent<CharacterHealth>();
                if (colliderHealth != null) colliderHealth.Health -= Damage;
            }
        }
    }
}