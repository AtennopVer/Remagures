using System;
using System.Linq;
using Remagures.View.MapSystem;

namespace Remagures.Model.MapSystem
{
    public sealed class GoalTransitionMarker
    {
        private readonly IMarkerView _markerView;
        private readonly IMap _map;

        public GoalTransitionMarker(IMarkerView markerView, IMap map)
        {
            _markerView = markerView ?? throw new ArgumentNullException(nameof(markerView));
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public bool IsActive()
        {
            _markerView.UnDisplay();
            
            if (!ContainsInMapHierarchy(_map)) 
                return false;

            _markerView.Display();
            return true;
        }

        private bool ContainsInMapHierarchy(IMap map)
        {
            if (ContainsOnMap(map))
                return true;
            
            return map.Transitions.Any(transition =>
            {
                var childMap = transition.MapToTransit;
                return ContainsInMapHierarchy(childMap);
            });
        }

        private bool ContainsOnMap(IMap map)
            => map.Markers.GoalMarkers.Any(marker => marker.IsActive());
    }
}
