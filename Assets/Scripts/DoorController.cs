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

        private Door currentDoor = null;
        private List<Door> doorList = new List<Door>();

        private void Awake()
        {
            float doorSpacing = (CameraController.TopRight.x - CameraController.TopLeft.x) / 4;

            for (int i = 0; i < doorCount; i += 1)
            {
                Door door = Instantiate(doorPrefab, CameraController.TopLeft + new Vector3(doorSpacing * (i + 1), -2), Quaternion.identity, transform);
                door.Setup(doors[Random.Range(0, doors.Count)]);
                doorList.Add(door);
            }

            currentDoor = doorList[0];
        }

        public Door CurrentDoor { get => currentDoor; }
    }
}
