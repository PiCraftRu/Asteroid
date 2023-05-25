using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [System.Serializable]
    public class Game
    {
        public string id;
        public string name;
    }

    [System.Serializable]
    public class Root
    {
        public User user;
        public Game game;
    }

    [System.Serializable]
    public class User
    {
        public string id;
        public string name;
        public bool isGuest;
    }
}
