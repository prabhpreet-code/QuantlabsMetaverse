using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBehavior
{
    void Enter(Player player, InterfaceManager interfaceManager);
    void Exit(Player player, InterfaceManager interfaceManager);
    void Update(Player player, InterfaceManager interfaceManager);
    void FixedUpdate(Player player, InterfaceManager interfaceManager);
}
