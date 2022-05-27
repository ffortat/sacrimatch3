using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private Door doorPrefab = null;
        [SerializeField]
        private List<SODoor> doors = new List<SODoor>();
    }
}
