using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    private Dictionary<Type, IShopkeeperBehavior> shopkeeperBehaviorsMap;
    private IShopkeeperBehavior behaviourCurrent;

    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterController controller;

    //Shopkeeper conditions
    [HideInInspector] public bool isTrading;
    public bool isSeating;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        this.InitBehaviors();
        this.SetBehaviorIdle();
    }

    private void InitBehaviors()
    {
        this.shopkeeperBehaviorsMap = new Dictionary<Type, IShopkeeperBehavior>();

        this.shopkeeperBehaviorsMap[typeof(ShopkeeperBehaviorIdle)] = new ShopkeeperBehaviorIdle();
        this.shopkeeperBehaviorsMap[typeof(ShopkeeperBehaviorTrade)] = new ShopkeeperBehaviorTrade();
        this.shopkeeperBehaviorsMap[typeof(ShopkeeperBehaviorSeat)] = new ShopkeeperBehaviorSeat();
    }

    private void SetBehavior(IShopkeeperBehavior newBehavior)
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

    private IShopkeeperBehavior GetBehavior<T>() where T : IShopkeeperBehavior
    {
        var type = typeof(T);
        return this.shopkeeperBehaviorsMap[type];
    }

    // Update is called once per frame
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
        var behavior = this.GetBehavior<ShopkeeperBehaviorIdle>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorTrade()
    {
        var behavior = this.GetBehavior<ShopkeeperBehaviorTrade>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorSeat()
    {
        var behavior = this.GetBehavior<ShopkeeperBehaviorSeat>();
        this.SetBehavior(behavior);
    }
}
