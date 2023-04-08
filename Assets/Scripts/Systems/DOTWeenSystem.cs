using Components;
using Unity.Entities;
using static Components.DOTWeenAbility;

namespace Systems
{
    public class DOTWeenSystem : ComponentSystem
    {
        private EntityQuery _dotWeenQuery;
        protected override void OnCreate()
        {
            _dotWeenQuery = GetEntityQuery(ComponentType.ReadOnly<DOTWeenData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_dotWeenQuery).ForEach((Entity entity, DOTWeenAbility dotWeenAbility) =>
            {
                dotWeenAbility.Execute();
            });
        }
    }
}