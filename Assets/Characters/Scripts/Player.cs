using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Dictionary<Type, IPlayerBehavior> behaviorsMap;
    private IPlayerBehavior behaviourCurrent;

    public CharacterController controller;
    public Animator animator;

    [SerializeField]
    public Transform cam;

    public Vector3 playerDirection;

    public bool isTrading;

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
        this.behaviorsMap[typeof(PlayerBehaviorWalk)] = new PlayerBehaviorWalk();
        this.behaviorsMap[typeof(PlayerBehaviorJump)] = new PlayerBehaviorJump();
        this.behaviorsMap[typeof(PlayerBehaviorTrade)] = new PlayerBehaviorTrade();
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

    private void Update()
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.Update(this);
    }

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

    public void SetBehaviorJump()
    {
        var behavior = this.GetBehavior<PlayerBehaviorJump>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorWalk()
    {
        var behavior = this.GetBehavior<PlayerBehaviorWalk>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorTrade()
    {
        var behavior = this.GetBehavior<PlayerBehaviorTrade>();
        this.SetBehavior(behavior);
    }

}
