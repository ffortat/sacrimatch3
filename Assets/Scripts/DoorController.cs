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
        private Grid<PuzzlePieceController> grid = null;

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

        private void Update()
        {
            UpdateVisuals();
        }

        public void ClearDoorPiece(PuzzlePiece doorPiece)
        {
            currentDoor.UnlockTile(doorPiece.Index);
            currentDoor.GetTileCoordinates(doorPiece.Index, out int x, out int y);

            grid.GetValue(x, y).PuzzlePiece = doorPiece;
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

                grid = new Grid<PuzzlePieceController>(currentDoor.TilesWidth, currentDoor.TilesHeight, 0.5f, new Vector3(10, -(currentDoor.TilesHeight / 4)), (Grid<PuzzlePieceController> grid, int x, int y) => new PuzzlePieceController(grid, x, y));
                grid.ShowDebug = true;
            }
            else
            {
                // TODO trigger end level
            }
        }

        private void UpdateVisuals()
        {
            grid.ForEach((int x, int y, PuzzlePieceController puzzlePieceController) =>
            {
                puzzlePieceController.Update();
            });
        }

        public Door CurrentDoor { get => currentDoor; }
    }
}
