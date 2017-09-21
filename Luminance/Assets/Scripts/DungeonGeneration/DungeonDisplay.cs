using UnityEngine;
using System.Collections;

public class DungeonDisplay : MonoBehaviour {
	public GameObject[] shapes;
	private MapGenerator mapGenerator;
	public float minimumMazePercentage = 0.8f;

    public float roomSize = 5f;


    // Use this for initialization
    void Start () {
		mapGenerator = GetComponent<MapGenerator> ();

		int visitedCellCount = 0;
		bool[,] visitedCells = new bool[mapGenerator.mapRows, mapGenerator.mapColumns];

		int minimumMazeCells = Mathf.FloorToInt((mapGenerator.mapRows - 2) * (mapGenerator.mapColumns - 2) * minimumMazePercentage);

		while (visitedCellCount < minimumMazeCells) {
			Debug.Log ("Current dungeon size = " + visitedCellCount + " which is less than the required " + minimumMazeCells + ". Retrying");
			mapGenerator.InitializeMap ();
			visitedCells = mapGenerator.TraverseMap ();
			visitedCellCount = GetVisitedCellsCount (visitedCells);
			Debug.Log ("visited cell count = " + visitedCellCount);
		}

		mapGenerator.DisplayMap ();

		for (int r = 1; r < mapGenerator.mapRows-1; r++) {
			for (int c = 1; c < mapGenerator.mapColumns - 1; c++) {
				string ch = mapGenerator.map [r, c].ToString();
				int charPos = mapGenerator.boxCharacters.IndexOf (ch);

				if (charPos < 0 || !visitedCells[r,c]) {
					continue;
				}

				Instantiate (shapes [charPos], new Vector3 (r * roomSize, 0, c * roomSize), shapes[charPos].transform.rotation);
			}
		}
	}

	private int GetVisitedCellsCount(bool[,] visitedCells) {
		int visitedCellsCount = 0;

		for (int r = 1; r < mapGenerator.mapRows - 1; r++) {
			for (int c = 1; c < mapGenerator.mapColumns - 1; c++) {
				if (visitedCells [r, c]) {
					visitedCellsCount++;
				}
			}
		}

		return visitedCellsCount;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(roomSize, 2, roomSize));
    }
}