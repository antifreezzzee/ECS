using Components;
using Unity.Entities;

namespace Systems
{
    public class AIBehaveSystem : ComponentSystem
    {
        private EntityQuery _behaveQuery;

        protected override void OnCreate()
        {
            _behaveQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_behaveQuery).ForEach(
                (Entity entity, BehaviourManager behaviourManager) =>
                {
                    behaviourManager.ActiveBehaviour?.Behave();
                });
        }
    }
}