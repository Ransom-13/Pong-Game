using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AgentController : Agent
{

    [SerializeField] private Transform target;
    public float moveSpeed;
    Rigidbody rb;
    public GameManager manager;
    public Text sideSelect;

    private float myScore, oppScore, scoreDiff;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        if (sideSelect.gameObject.tag == "left")
        {
            transform.localPosition = new Vector3(0f, 0.3f, 9f);
            
        }
        else
        {
            transform.localPosition = new Vector3(0f, 0.3f, -9f);
        }
        target.localPosition = new Vector3(0f, 0.3f, 0f);
    }

    private void Update()
    {
        if (sideSelect.gameObject.tag == "left")
        {
            myScore = (float)this.manager.leftScore;
            oppScore = (float)this.manager.rightScore;
        }
        else
        {
            myScore = (float)this.manager.rightScore;
            oppScore = (float)this.manager.leftScore;
        }
        scoreDiff = myScore - oppScore;

        if (myScore >= 5f || oppScore >= 5f)
        {
            CheckScore();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float direction = actions.ContinuousActions[0];

        transform.localPosition += new Vector3(direction, 0f) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (sideSelect.gameObject.tag == "left")
        {
            var continuousActions = actionsOut.ContinuousActions;
            continuousActions[0] = Input.GetAxisRaw("Horizontal");
        }
  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            AddReward(1f);
        }
        else if (other.gameObject.tag == "Wall" && transform.localPosition.x < 0 && sideSelect.gameObject.tag == "left")
        {
            //AddReward(-0.05f);
            transform.localPosition = new Vector3(transform.localPosition.x + 0.2f, 0.3f, 9f);
        }
        else if (other.gameObject.tag == "Wall" && transform.localPosition.x > 0 && sideSelect.gameObject.tag == "left")
        {
            //AddReward(-0.05f);
            transform.localPosition = new Vector3(transform.localPosition.x - 0.2f, 0.3f, 9f);
        }
        else if (other.gameObject.tag == "Wall" && transform.localPosition.x < 0 && sideSelect.gameObject.tag == "right")
        {
            //AddReward(-0.05f);
            transform.localPosition = new Vector3(transform.localPosition.x + 0.2f, 0.3f, -9f);
        }
        else if (other.gameObject.tag == "Wall" && transform.localPosition.x > 0 && sideSelect.gameObject.tag == "right")
        {
            //AddReward(-0.05f);
            transform.localPosition = new Vector3(transform.localPosition.x - 0.2f, 0.3f, -9f);
        }
    }

    public void CheckScore()
    {
        if (scoreDiff < 0f)
        {
            AddReward(0.01f * scoreDiff);
        }
        else
        {
            AddReward(0.1f - (0.01f * scoreDiff));
        }
        EndEpisode();
    }
}