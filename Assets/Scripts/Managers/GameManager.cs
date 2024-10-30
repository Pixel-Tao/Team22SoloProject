using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : ManagerSingleton<GameManager>
{
    private NavMeshSurface navMeshSurface;
    public NavMeshSurface NavMeshSurface
    {
        get
        {
            if (navMeshSurface == null)
            {
                navMeshSurface = FindObjectOfType<NavMeshSurface>();
                if (navMeshSurface == null)
                {
                    navMeshSurface = new GameObject { name = nameof(NavMeshSurface) }.AddComponent<NavMeshSurface>();
                    navMeshSurface.collectObjects = CollectObjects.All;
                }
            }
            return navMeshSurface;
        }
    }

    public void MonsterSpawn()
    {
        FindObjectOfType<MonsterSpawner>()?.Spawn();
    }
}
