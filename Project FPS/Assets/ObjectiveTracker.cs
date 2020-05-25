using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// entrance doors close once enter objetive. exit doors open once objective is finished. 
/// </summary>
public class ObjectiveTracker : MonoBehaviour
{
    [SerializeField]
    List<GameObject> entranceDoors;
    [SerializeField]
    List<GameObject> exitDoors;
    [SerializeField]
    List<GameObject> enemies;
    public bool isBeatByLeavingTrigger;
    public bool isFinalObjective;
    private bool _objectiveCompleted = false;
    private GameFlowManager _gameFlowManager;

    private void Awake()
    {
        _gameFlowManager = FindObjectOfType<GameFlowManager>();
        DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, EnemyController>(_gameFlowManager, this);

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _objectiveCompleted = true;
            for (int i = 0; i < entranceDoors.Count; i++)
            {
                if (entranceDoors[i])   entranceDoors[i].SetActive(true);
            }
            for (int i = 0; i < exitDoors.Count; i++)
            {
                if (exitDoors[i]) exitDoors[i].SetActive(true);
            }
        }
    }
    

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && isBeatByLeavingTrigger)
        {
            for (int i = 0; i < exitDoors.Count; i++)
            {
                if (exitDoors[i])   exitDoors[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        // checks based on how many enemies defeated
        if (isBeatByLeavingTrigger == false)
        {
            _objectiveCompleted = true;   
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    _objectiveCompleted = false;
                    break;
                }
            }

            if (_objectiveCompleted == true)
            {
                for (int i = 0; i < exitDoors.Count; i++)
                {
                    if (exitDoors[i]) exitDoors[i].SetActive(false);
                }
            }

            if (_objectiveCompleted && isFinalObjective)
            {
                _gameFlowManager.SetLevelComplete(true);
            }
        }
    }
}
