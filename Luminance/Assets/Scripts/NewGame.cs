using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {

    public GameObject lPanel;
    public Image lPicture;
    public Text lText;

	public void LoadLevel(int sceneIndex)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadAsync(sceneIndex));
        
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        lPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            lPicture.fillAmount = progress;
            lText.text = progress * 100 + "%";  

            yield return null;
        }
    }


	


}
