using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameScene : MonoBehaviour
{
    public float notWalkableAreaCost = 5f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.NavMeshSurface.BuildNavMesh();

        NavMesh.SetAreaCost(NavMesh.GetAreaFromName("Not Walkable"), notWalkableAreaCost);

        GameManager.Instance.MonsterSpawn();
    }
}
