using Unity.Entities;
using UnityEngine;

public class PickUp : MonoBehaviour, IConvertGameObjectToEntity
{
    private Entity _entity;
    private EntityManager _entityManager;

    protected void DestroyPickUp()
    {
        _entityManager.DestroyEntity(_entity);
        Destroy(gameObject);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _entityManager = dstManager;
    }
}