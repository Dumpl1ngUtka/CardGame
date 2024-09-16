using UnityEngine;
using Battleground;
using UnityEngine.AI;
using System.IO;
using UI.Marker;

public class Test : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] GameObject boxPref;
    [SerializeField] LineRenderer markerPref;
    private NavMeshPath path;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        //NavMesh.CalculatePath(transform.position, target.position, ~0, path);
        path = agent.path;
        //agent.Move(target.position)
        Debug.Log(agent.path.corners.Length);

        foreach (var item in agent.path.corners)
        {
            Debug.Log(item);
        }
    }

    private void Start()
    {
        foreach (var item in agent.path.corners)
        {
            Debug.Log(item);
        }
    }

    private void Update()
    {
        //markerPref.
        //Debug.Log(agent.path.corners.Length);
    }
}
