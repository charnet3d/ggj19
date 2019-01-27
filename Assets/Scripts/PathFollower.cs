using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0;

    [SerializeField]
    GameObject path = null;

    Transform tr;
    PathNode[] nodes;
    Animator anim;

    int currentNode = 0;
    float timer;
    static Vector3 currentNodePosition;
    Quaternion currentNodeRotation;
    Vector3 startPosition;
    Vector3 previousPosition;
    Quaternion startRotation;


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        nodes = path.GetComponentsInChildren<PathNode>();
        anim = GetComponent<Animator>();

        previousPosition = Vector3.zero;
        CheckNode();
    }

    void CheckNode()
    {
        timer = 0;
        currentNodePosition = nodes[currentNode].GetPosition();
        startPosition = tr.position;
        startRotation = tr.rotation;
        currentNodeRotation = Quaternion.Euler(0, 0, Vector3.Angle(currentNodePosition - startPosition, 
            Vector3.right));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * moveSpeed;

        if (tr.position != currentNodePosition)
        {
            tr.position = Vector3.Lerp(startPosition, currentNodePosition, timer);
            tr.rotation = Quaternion.Lerp(startRotation, currentNodeRotation, timer);
        }
        else
        {
            if (currentNode < nodes.Length - 1)
            {
                currentNode++;

                if (currentNode > 0)
                    previousPosition = startPosition;

                CheckNode();
            }
        }

        if (currentNode >= nodes.Length - 1)
        {
            anim.Play("Idle");
        }
    }
}
