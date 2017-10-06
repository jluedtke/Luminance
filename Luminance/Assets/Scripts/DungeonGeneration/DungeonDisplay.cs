using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DungeonDisplay : MonoBehaviour {
	public GameObject[] hallways;
    public GameObject[] rooms;
    public GameObject deadEnd;
	private MapGenerator mapGenerator;
	public float minimumMazePercentage = 0.8f;
    private bool[,] visitedCells;

    public float roomSize = 5f;

    [Header("AfterMapModels")]
    public GameObject endPortal;
    public GameObject crystal;
    public GameObject corruptedWisp;
    public GameObject mage;

    public float minimumCrystalPercentage;
    public float minimumCWPercentage;
    public float minimumMagePercentage;



    // Use this for initialization
    void Start () {
		mapGenerator = GetComponent<MapGenerator> ();

		int visitedCellCount = 0;
		visitedCells = new bool[mapGenerator.mapRows, mapGenerator.mapColumns];

		int minimumMazeCells = Mathf.FloorToInt((mapGenerator.mapRows - 2) * (mapGenerator.mapColumns - 2) * minimumMazePercentage);

		while (visitedCellCount < minimumMazeCells) {
			//Debug.Log ("Current dungeon size = " + visitedCellCount + " which is less than the required " + minimumMazeCells + ". Retrying");
			mapGenerator.InitializeMap ();
			visitedCells = mapGenerator.TraverseMap ();
			visitedCellCount = GetVisitedCellsCount (visitedCells);
			//Debug.Log ("visited cell count = " + visitedCellCount);
		}

		mapGenerator.DisplayMap ();

		for (int r = 1; r < mapGenerator.mapRows-1; r++) {
			for (int c = 1; c < mapGenerator.mapColumns - 1; c++) {
				string ch = mapGenerator.map [r, c].ToString();
				int charPos = mapGenerator.boxCharacters.IndexOf (ch);

				if (ch == "O")
                {
                    Instantiate(deadEnd, new Vector3(r * roomSize, 0, c * roomSize), deadEnd.transform.rotation);
                    continue;
                }
                else if (charPos < 0 || !visitedCells[r,c]) {
					continue;
				}

                if ((r + c) % 2 == 0)
                    Instantiate(rooms[charPos], new Vector3(r * roomSize, 0, c * roomSize), rooms[charPos].transform.rotation);
                else
                    Instantiate(hallways[charPos], new Vector3 (r * roomSize, 0, c * roomSize), hallways[charPos].transform.rotation);
			}
		}

        //Object Spawning
        SpawnEndPortal(mapGenerator.mapRows-2, mapGenerator.mapColumns-2);
        SpawnCrystals();
        SpawnEnemies();


        //NavMesh Stuff
        gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
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

    // Object Spawning //

    private void SpawnEnemies()
    {
        //Grab arrays of all enemy waypoints
        GameObject[] cwWaypoints = GameObject.FindGameObjectsWithTag("CWWaypoint");
        GameObject[] mageWaypoints = GameObject.FindGameObjectsWithTag("MageWaypoint");

        //Corrupted Wisp, default rate is 80%
        for (int i = 0; i < cwWaypoints.Length; i++)
        {
            if (Random.value < minimumCWPercentage)
                Instantiate(corruptedWisp, cwWaypoints[i].transform.position + ( Vector3.up * 3 ), Quaternion.identity);
        }

        //Mage, default rate is 100%
        for (int i = 0; i < mageWaypoints.Length; i++)
        {
            if (Random.value < minimumMagePercentage)
                Instantiate(mage, mageWaypoints[i].transform.position, Quaternion.identity);
        }
    }

    private void SpawnCrystals()
    {
        //Collect all possible locations for crystals
        GameObject[] crystalWaypoints = GameObject.FindGameObjectsWithTag("CrystalWaypoint");

        //Instantiate a crystal if it passes a range test. Roughly 80% at base.
        for (int i = 0; i < crystalWaypoints.Length; i++)
        {
            //Instantiate at random rotation
            if (Random.value < minimumCrystalPercentage)
                Instantiate(crystal, crystalWaypoints[i].transform.position, Quaternion.Euler(0, Random.Range(90, 270), 0));
        }
    }

    private void SpawnEndPortal(int r, int c)
    {
        //Grab character location in case of dead ends
        string ch = mapGenerator.map[r, c].ToString();

        //If it's not a visited cell, randomly move up or left
        if (!visitedCells[r, c] || ch == "O")
        {
            if (Random.value < 0.5f)
            {
                SpawnEndPortal(r - 1, c);
                return;
            }
            else
            {
                SpawnEndPortal(r, c - 1);
                return;
            }
        }
        //Move existing end portal to middle of room
        endPortal.transform.position = new Vector3(r * roomSize, 0.1f, c * roomSize);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(roomSize, 2, roomSize));
    }
}