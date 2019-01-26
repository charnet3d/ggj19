using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0;

    [SerializeField]
    GameObject path;

    Transform tr;
    PathNode[] nodes;
    int currentNode = 0;
    float timer;
    static Vector3 currentNodePosition;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        nodes = path.GetComponentsInChildren<PathNode>();
        CheckNode();
    }

    void CheckNode()
    {
        timer = 0;
        currentNodePosition = nodes[currentNode].GetPosition();
        startPosition = tr.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * moveSpeed;

        if (tr.position != currentNodePosition)
        {
            tr.position = Vector3.Lerp(startPosition, currentNodePosition, timer);
        }
        else
        {
            if (currentNode < nodes.Length - 1)
            {
                currentNode++;
                CheckNode();
            }
        }
    }
}
