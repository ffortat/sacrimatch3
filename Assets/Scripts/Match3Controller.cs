using System.Collections.Generic;
using UnityEngine;

namespace Sacrimatch3
{
    public class Match3Controller : MonoBehaviour
    {
        [SerializeField]
        private Match3Visual match3Visual = null;
        [SerializeField]
        private List<SOGem> gems = new List<SOGem>();

        private GridGenerator generator = null;

        private void Awake()
        {
            generator = new GridGenerator(gems);
            match3Visual.Setup(generator.Grid);
        }

        private void Update()
        {
            // TODO controls on the match 3
        }
    }
}
