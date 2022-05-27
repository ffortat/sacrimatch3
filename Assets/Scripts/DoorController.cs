using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private Door doorPrefab = null;
        [SerializeField]
        private int doorCount = 3;
        [SerializeField]
        private List<SODoor> doors = new List<SODoor>();

        private List<Door> doorList = new List<Door>();

        private void Awake()
        {
            for (int i = 0; i < doorCount; i += 1)
            {
                Door door = Instantiate(doorPrefab, transform);
                door.Setup(doors[Random.Range(0, doors.Count)]);
                doorList.Add(door);
            }
        }

        private void Start()
        {
        }
    }
}
