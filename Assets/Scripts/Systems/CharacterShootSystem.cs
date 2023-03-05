using Components;
using Components.Interfaces;
using Unity.Entities;

namespace Systems
{
    public class CharacterShootSystem : ComponentSystem
    {
        private EntityQuery _shootQuery;

        protected override void OnCreate()
        {
            _shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<ShootData>(), ComponentType.ReadOnly<UserInputData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_shootQuery).ForEach(
                (Entity entity, UserInputData userInputData, ref InputData inputData) =>
                {
                    if (inputData.Shoot > 0f && userInputData.ShootAction != null &&
                        userInputData.ShootAction is IAbility ability)
                        ability.Execute();
                });
        }
    }
}