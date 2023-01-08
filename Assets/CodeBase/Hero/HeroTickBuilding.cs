using System.Collections.Generic;
using System.Linq;
using CodeBase.Buildings;

namespace CodeBase.Hero
{
    public class HeroTickBuilding
    {
        public HeroTickBuilding()
        {
            _currentBuildings = new List<IBuildingIntakes>();
        }

        private readonly List<IBuildingIntakes> _currentBuildings;

        public List<IBuildingIntakes> GetCurrentProcessBuildings() => _currentBuildings;

        public void AddNewBuildings(List<IBuildingIntakes> last)
        {
            foreach (IBuildingIntakes unit in last.Where(enemy => !_currentBuildings.Contains(enemy)))
            {
                unit.Intake();

                _currentBuildings.Add(unit);
            }
        }

        public void RemoveOldBuilding(List<IBuildingIntakes> current)
        {
            foreach (IBuildingIntakes buildingIntakes in _currentBuildings.ToList()
                         .Where(enemyIntakes => !current.Contains(enemyIntakes)))
            {
                buildingIntakes.OutTake();
                _currentBuildings.Remove(buildingIntakes);
            }
        }

        public void ClearBuildings()
        {
            if (_currentBuildings.Count == 0) return;

            _currentBuildings.ForEach((unit) => unit.OutTake());

            _currentBuildings.Clear();
        }
    }
}