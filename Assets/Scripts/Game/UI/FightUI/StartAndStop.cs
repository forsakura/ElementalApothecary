using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAndStop : Singleton<StartAndStop>
{
    [SerializeField]
    Image stop;
    [SerializeField]
    Image start;

    string pausePagePath = "Prefab/UI/FightUI/PausePage";
    [HideInInspector]
    public GameObject PausePage = null;
    string settingPagePath = "Prefab/UI/FightUI/Setting";
    [HideInInspector]
    public GameObject SettingPage = null;

    string specialMaterialPagePath = "Prefab/UI/FightUI/SpecialMaterialPanel";
    [HideInInspector]
    public GameObject SpecialMaterialPanel = null;

    private bool getMaterial;

    public void OnValueChanged(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0.0f;
            //stop.gameObject.SetActive(true);
            start.gameObject.SetActive(false);
            if (!getMaterial)
            {
                CreatePausePage();
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            //stop.gameObject.SetActive(false);
            start.gameObject.SetActive(true);
            if (PausePage != null)
            {
                DestroyPausePage();
            }
            if (SpecialMaterialPanel != null)
            {
                DestroyMaterialPanel();
            }
            if (SettingPage != null)
            {
                DestroySettingPage();
            }
        }
    }

    public void CreatePausePage()
    {
        if (PausePage != null)
        {
            DestroyPausePage();
        }
        if (SpecialMaterialPanel != null)
        {
            DestroyMaterialPanel();
        }
        PausePage = GameObject.Instantiate(Resources.Load<GameObject>(pausePagePath));
        PausePage.transform.SetParent(transform.parent.transform);
        PausePage.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        PausePage.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        PausePage.GetComponent<PausePageButtons>().ContinueButton.onClick.AddListener(() =>
        {
            GetComponent<Toggle>().isOn = false;
            //Time.timeScale = 1.0f;
            //Destroy(PausePage);
        });
        PausePage.GetComponent<PausePageButtons>().SettingButton.onClick.AddListener(() =>
        {
            SettingPage = GameObject.Instantiate(Resources.Load<GameObject>(settingPagePath));
            SettingPage.transform.SetParent(transform.parent.transform);
            SettingPage.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            SettingPage.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            
            SettingPage.GetComponent<VolumeSettings>().Back.onClick.AddListener(() =>
            {
                DestroySettingPage();
            });


            
        });

        

    }

    public void DestroyPausePage()
    {
        Destroy(PausePage);
        PausePage = null;
    }

    public void GetSpecialMaterial(int materialID)
    {
        if (PausePage != null)
        {
            DestroyPausePage();
        }
        if (SpecialMaterialPanel != null)
        {
            DestroyMaterialPanel();
        }
        getMaterial = true;
        GetComponent<Toggle>().isOn = true;
        SpecialMaterialPanel = GameObject.Instantiate(Resources.Load<GameObject>(specialMaterialPagePath));
        SpecialMaterialPanel.transform.SetParent(transform.parent.transform);
        SpecialMaterialPanel.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        SpecialMaterialPanel.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        SpecialMaterialPanel.GetComponent<SpecialMaterialPage>().CloseBtn.onClick.AddListener(() =>
        {
            GetComponent<Toggle>().isOn = false;
            getMaterial = false;
        });
    }

    public void DestroyMaterialPanel()
    {
        getMaterial = false;
        Destroy(SpecialMaterialPanel);
        SpecialMaterialPanel = null;
    }

    public void DestroySettingPage()
    {
        Destroy(SettingPage);
        SettingPage = null;
    }

    public void Test()
    {
        GetSpecialMaterial(0);
    }
}
