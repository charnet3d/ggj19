using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    float pathDurationStep = 0.2f;

    float nodeDurationStep;

    Transform thisTransform;
    Animator thisAnimator;
    PathNode[] nodes;

    int currentNode;
    float timer;
    Vector3 targetNodePosition;
    Quaternion targetNodeRotation;
    Vector3 startNodePosition;
    Quaternion startNodeRotation;

    float pathDistance;


    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        thisAnimator = GetComponent<Animator>();

        GameObject path = GameObject.Find("Path");
        nodes = path.GetComponentsInChildren<PathNode>();
        currentNode = 0;

        // Calculate total path distance
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            pathDistance += Vector3.Distance(nodes[i].GetPosition(), nodes[i + 1].GetPosition());
        }

        AdvanceNode();
    }

    void Update()
    {
        if (thisTransform.position != targetNodePosition)
        {
            timer += nodeDurationStep * Time.deltaTime;

            thisTransform.position = Vector2.Lerp(startNodePosition, targetNodePosition, timer);
            thisTransform.rotation = Quaternion.Lerp(startNodeRotation, targetNodeRotation, timer * 1.2f);
        }
        else
            if (currentNode < nodes.Length - 1)
                AdvanceNode();
            else
                thisAnimator.SetBool("walking", false);
    }

    void AdvanceNode()
    {
        timer = 0;

        startNodePosition = thisTransform.position;
        startNodeRotation = thisTransform.rotation;

        currentNode++;

        targetNodePosition = nodes[currentNode].GetPosition();
        targetNodeRotation = Quaternion.Euler(0, 0, 
            Vector3.Angle(targetNodePosition - startNodePosition, Vector3.right));  // Angle between X-Axis and Current/Next node vector

        // Adapt the movement speed so that it's the same throughout all nodes
        // long node distance => small time steps => bigger movement steps => same speed
        // short node distance => longer time steps => smaller movement steps => same speed
        float nodeDistance = Vector2.Distance(startNodePosition, targetNodePosition);
        nodeDurationStep = pathDurationStep * pathDistance / nodeDistance;
    }
}
