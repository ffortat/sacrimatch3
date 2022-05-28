using UnityEngine;

namespace Sacrimatch3
{
    public class CameraController : MonoBehaviour
    {
        private static CornerPositions cornerPositions = new CornerPositions();

        private void Awake()
        {
            GetCornerPositions();
        }

        public void Zoom()
        {

        }

        public void Unzoom()
        {

        }

        private void GetCornerPositions()
        {
            cornerPositions = new CornerPositions();
            cornerPositions.topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
            cornerPositions.topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
            cornerPositions.bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
            cornerPositions.bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10));
        }

        public static Vector3 TopLeft { get => cornerPositions.topLeft; }
        public static Vector3 TopRight { get => cornerPositions.topRight; }
        public static Vector3 BottomLeft { get => cornerPositions.bottomLeft; }
        public static Vector3 BottomRight { get => cornerPositions.bottomRight; }
    }

    public struct CornerPositions
    {
        public Vector3 topLeft;
        public Vector3 topRight;
        public Vector3 bottomLeft;
        public Vector3 bottomRight;
    }
}
