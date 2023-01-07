using CodeBase.Infrastructure.Factory.Interfaces;
using CodeBase.Unit;
using CodeBase.Unit.Scriptables;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.Scriptables
{
    [CreateAssetMenu(fileName = "UnitConfigContainer", menuName = "Containers/UnitConfigContainer")]
    public class UnitConfigContainer : ConfigContainer<UnitConfig, UnitType>, IUnitConfigContainer
    {
    }
}