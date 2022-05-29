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

        private int doorIndex = -1;
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

            GoToNextDoor();
        }

        public void ClearDoorPiece(PuzzlePiece doorPiece)
        {
            currentDoor.UnlockTile(doorPiece.Index);
            // TODO Move piece visually outside of match3
        }

        public void OpenDoor()
        {
            // TODO open door
            // TODO trigger party move
            GoToNextDoor();
        }

        private void GoToNextDoor()
        {
            doorIndex += 1;

            if (doorIndex < doorList.Count)
            {
                currentDoor = doorList[doorIndex];
                currentDoor.AddOnDoorUnlockedListener(OpenDoor);
            }
            else
            {
                // TODO trigger end level
            }
        }

        public Door CurrentDoor { get => currentDoor; }
    }
}
