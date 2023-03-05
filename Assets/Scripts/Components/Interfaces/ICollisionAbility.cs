using System.Collections.Generic;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public interface ICollisionAbility : IAbility

    {
        List<Collider> Collisions { get; set; }
    }
}