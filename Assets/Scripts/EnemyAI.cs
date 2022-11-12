using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;
    public float stoppingDistance;

    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;

    [SerializeField] private Image healthBar;
  
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Cover[] avaliableCovers;
    
    public TextMeshProUGUI statusText; 

    private Transform bestCoverSpot;
    private NavMeshAgent agent;

    private Node topNode;

    private bool isAttack = true;

    private Vector3 offset;

    public EnemyData enemyData;

    private float _currentHealth;

    AudioManager audioManager;

	public float currentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        _currentHealth = startingHealth;
        ConstructBehahaviourTree();

        offset = new Vector3(0, 3.5f, 0);
    }

    private void ConstructBehahaviourTree()
    {
        IsCovereAvaliableNode coverAvaliableNode = new IsCovereAvaliableNode(avaliableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        ShootNode shootNode = new ShootNode(agent, this, playerTransform);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });
    }

    private void Update()
    {
        topNode.Evaluate();
        if(topNode.nodeState == NodeState.FAILURE)
        {
            statusText.text = "Not Detected";
            agent.isStopped = true;
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount -= (damage/100f);
        Instantiate(enemyData.hitEffect, transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            Instantiate(enemyData.destroyEffect, transform.position, Quaternion.identity);
            audioManager.blast.Play();
            Destroy(this.gameObject);
        }
    }

    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
        healthBar.fillAmount += Time.deltaTime * (healthRestoreRate / 5);
        currentHealth += Time.deltaTime * healthRestoreRate;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

    public void Attack()
    {
        if (isAttack)
        {         
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        isAttack = false;
        GameObject projectile = Instantiate(enemyData.weapon, this.transform.position + offset, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        yield return new WaitForSeconds(2f);
        isAttack = true;       
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "playerweapon")
        {
            Destroy(other.gameObject);
        }
    }
}
