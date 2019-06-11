using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject m_on, m_off;
    public Sprite layer_white, layer_black;
    public Text level;
  
    public GameObject obj;
    void Start()
    {
        if (gameObject.name == "Ads") {
        if (!PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Level", 1);
        level.text = "Level " + PlayerPrefs.GetInt("Level");
        }

        if (gameObject.name == "Audio") {
            if (PlayerPrefs.GetString("Music") == "no") {
                m_on.SetActive(false);
                m_off.SetActive(true);
            } else {
                m_on.SetActive(true);
                m_off.SetActive(false);
            }
        }
    }
    void OnMouseDown() => GetComponent<SpriteRenderer>().sprite = layer_black;
    void OnMouseUp() => GetComponent<SpriteRenderer> ().sprite = layer_white;
    void OnMouseUpAsButton()
    {
        if (PlayerPrefs.GetString("Music") != "no") {
            GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
        }
        switch (gameObject.name) {
            case "Play":
            SceneManager.LoadScene("GamePlay");
            break;

            case "Back":
            SceneManager.LoadScene("Main");
            break;

            case "Star":
            Application.OpenURL("https://www.youtube.com/user/sthxnp");
            break;

            case "Store":
            Instantiate(obj, new Vector3(-0.02f ,0.15f , -0.65f), Quaternion.Euler(0, 180, 10));
            break;

            case "Ads":
            if (PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") == 5 ? 1 : PlayerPrefs.GetInt("Level") + 1);
            else PlayerPrefs.SetInt("Level", 1);
            level.text = "Level " + PlayerPrefs.GetInt("Level");
            break;

            case "Audio":
            if (PlayerPrefs.GetString("Music") != "no") {
                PlayerPrefs.SetString("Music", "no");
                m_on.SetActive(false);
                m_off.SetActive(true);
            }
            else {
                PlayerPrefs.SetString("Music", "yes");
                m_on.SetActive(true);
                m_off.SetActive(false);
            }
            break;
        }
    }
}
