using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObserver", menuName = "Custom Scriptables/Event Observer")]
public class EventObserver : ScriptableObject
{
    public delegate void IntCallback(int i);
    public delegate void FloatCallback(float f);

    public delegate void VoidGameCallback();

    public IntCallback OnAddScore;
    public IntCallback OnRestScore;
    public VoidGameCallback OnResetGame;
    public VoidGameCallback OnStartGame;
    public VoidGameCallback OnEndGame;

    public void AddScore(int i)
    {
        OnAddScore?.Invoke(i);
    }

    public void RestScore(int i)
    {
        OnRestScore?.Invoke(i);
    }

    public void ResetGame()
    {
        Debug.Log("reset");
        OnResetGame?.Invoke();
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public void EndGame()
    {
        OnEndGame?.Invoke();
    }
}
