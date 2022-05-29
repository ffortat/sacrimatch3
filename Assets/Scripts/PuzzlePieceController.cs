using UnityEngine;

namespace Sacrimatch3
{
    public class PuzzlePieceController
    {
        private Grid<PuzzlePieceController> grid;
        private int x;
        private int y;
        private PuzzlePiece puzzlePiece;

        public PuzzlePieceController(Grid<PuzzlePieceController> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void Update()
        {
            if (puzzlePiece)
            {
                Vector3 targetPosition = grid.GetWorldPosition(x, y);
                Vector3 moveDir = (targetPosition - puzzlePiece.transform.position);
                float moveSpeed = 10f;
                puzzlePiece.transform.position += moveDir * moveSpeed * Time.deltaTime;

                Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
                Vector3 scaleDir = (targetScale - puzzlePiece.transform.localScale);
                float scaleSpeed = 10f;
                puzzlePiece.transform.localScale += scaleDir * scaleSpeed * Time.deltaTime;
            }
        }
     
        public PuzzlePiece PuzzlePiece { get => puzzlePiece; set => puzzlePiece = value; }
    }
}
