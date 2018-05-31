using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of the player for AI #ILOVESINGLETONS
/// </summary>
public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player; // When we spawn players in will have to set it in a script
}
