using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayPurseScript : MonoBehaviour {

    public static PlayPurseScript Instance { get; set; }
    public int Balance { get; private set; }

    private const string PREFIX = "Balance: ";
    public TextMeshPro UIElement;
    public int InitialBalance;
    private Object _balanceLock = new Object();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePurse();
        }
        else if (Instance != this)
            Destroy(this);
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
        UIElement.SetText($"{PREFIX}{Balance}");
    }

    private void InitializePurse()
    {
        Balance = InitialBalance;
    }

    public void ChangeBalance(int value)
    {
        lock(_balanceLock)
        {
            Balance += value;
        }
    }

}
