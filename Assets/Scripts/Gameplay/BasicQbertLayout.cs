using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelLayout", menuName = "Qbert/Layouts/Original Qbert Layout", order = 1)]
public class BasicQbertLayout : LevelLayout {

	public override void Init(){
		rows = new int[8][] {
			new int [8] { 0, 0, 0, 0, 0, 2, 0, 0 },
			new int [8] { 0, 1, 1, 1, 1, 1, 1, 1 },
			new int [8] { 0, 1, 1, 1, 1, 1, 1, 0 },
			new int [8] { 0, 1, 1, 1, 1, 1, 0, 0 },
			new int [8] { 0, 1, 1, 1, 1, 0, 0, 0 },
			new int [8] { 2, 1, 1, 1, 0, 0, 0, 0 },
			new int [8] { 0, 1, 1, 0, 0, 0, 0, 0 },
			new int [8] { 0, 1, 0, 0, 0, 0, 0, 0 }
		};
	}
}
