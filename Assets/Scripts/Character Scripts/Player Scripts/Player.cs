using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Dictionary<Type, IPlayerBehavior> behaviorsMap;
    private IPlayerBehavior behaviourCurrent;

    [HideInInspector] public CharacterController controller;
    [HideInInspector] public Animator animator;

    [SerializeField] public Transform cam;
    [SerializeField] public Transform cameraTarget;

    public Vector3 playerDirection;

    //Player Conditions
    [HideInInspector] public bool isTrading;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isSeating;
    [HideInInspector] public bool isWalking;


    public string PLAYER_LOCATION_TAG = "Lobby";

    /// <summary>
    /// Loading player components
    /// </summary>
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Initializing state machine
    /// </summary>
    private void Start()
    {
        this.InitBehaviors();
        this.SetBehaviorByDefault();
    }

    private void InitBehaviors()
    {
        this.behaviorsMap = new Dictionary<Type, IPlayerBehavior>();

        this.behaviorsMap[typeof(PlayerBehaviorIdle)] = new PlayerBehaviorIdle();
        this.behaviorsMap[typeof(PlayerBehaviorMove)] = new PlayerBehaviorMove();
        this.behaviorsMap[typeof(PlayerBehaviorTrade)] = new PlayerBehaviorTrade();
        this.behaviorsMap[typeof(PlayerBehaviorSeat)] = new PlayerBehaviorSeat();
    }

    private void SetBehavior(IPlayerBehavior newBehavior)
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.Exit(this);

        this.behaviourCurrent = newBehavior;
        this.behaviourCurrent.Enter(this);
    }

    private void SetBehaviorByDefault()
    {
        this.SetBehaviorIdle();
    }

    private IPlayerBehavior GetBehavior<T>() where T : IPlayerBehavior
    {
        var type = typeof(T);
        return this.behaviorsMap[type];
    }

    /// <summary>
    /// Update Input and Condition Parameters
    /// </summary>
    private void Update()
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.Update(this);
    }

    /// <summary>
    /// Fixed update to maintain physics
    /// </summary>
    private void FixedUpdate()
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.FixedUpdate(this);
    }

    public void SetBehaviorIdle()
    {
        var behavior = this.GetBehavior<PlayerBehaviorIdle>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorWalk()
    {
        var behavior = this.GetBehavior<PlayerBehaviorMove>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorTrade()
    {
        var behavior = this.GetBehavior<PlayerBehaviorTrade>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviourSeat()
    {
        var behavior = this.GetBehavior<PlayerBehaviorSeat>();
        this.SetBehavior(behavior);
    }

}
