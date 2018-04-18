using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clearCache : MonoBehaviour {

    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    public void BackLobby()
    {
        SceneManager.LoadScene(0);
    }
}
