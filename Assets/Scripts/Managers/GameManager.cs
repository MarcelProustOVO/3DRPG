using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public CharacterStats playerStats;

    private CinemachineVirtualCamera followCamara;

    List<IEndGameObserve> endGameObserves = new List<IEndGameObserve>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void RigisterPlayer(CharacterStats player)
    {
        playerStats = player;

        followCamara = FindObjectOfType<CinemachineVirtualCamera>();
        if (followCamara != null)
        {
            followCamara.Follow = playerStats.transform.GetChild(2);
            followCamara.LookAt = playerStats.transform.GetChild(2);
        }

    }
    public void AddObserver(IEndGameObserve observer)
    {
        endGameObserves.Add(observer);
    }
    public void RemoveObserver(IEndGameObserve obsever)
    {
        endGameObserves.Remove(obsever);
    }
    public void NotifyObservers()
    {
        foreach(var observer in endGameObserves)
        {
            observer.EndNotify();
        }
    }
}
