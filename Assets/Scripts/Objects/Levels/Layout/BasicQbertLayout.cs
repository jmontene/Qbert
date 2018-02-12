using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelLayout", menuName = "Qbert/Layouts/Original Qbert Layout", order = 1)]
public class BasicQbertLayout : LevelLayout {

	public override void Init(){
		rows = new int[9][] {
			new int [9] { 0, 0, 0, 0, 0, 2, 0, 0, 0 },
			new int [9] { 0, 1, 1, 1, 1, 1, 1, 1, 0 },
			new int [9] { 0, 1, 1, 1, 1, 1, 1, 0, 0  },
			new int [9] { 0, 1, 1, 1, 1, 1, 0, 0, 0  },
			new int [9] { 0, 1, 1, 1, 1, 0, 0, 0, 0  },
			new int [9] { 2, 1, 1, 1, 0, 0, 0, 0, 0  },
			new int [9] { 0, 1, 1, 0, 0, 0, 0, 0, 0  },
			new int [9] { 0, 1, 0, 0, 0, 0, 0, 0, 0  },
			new int [9] { 0, 0, 0, 0, 0, 0, 0, 0, 0  },
		};

		bottomPositions = new List<Vector2Int>() {
			new Vector2Int (7, 1),
			new Vector2Int (6, 2),
			new Vector2Int (5, 3),
			new Vector2Int (4, 4),
			new Vector2Int (3, 5),
			new Vector2Int (2, 6),
			new Vector2Int (1, 7)
		};
	}
}
