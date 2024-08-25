using ProjectBase.UI;
using UnityEngine;
using UnityEngine.UI;

public class HomeMainPannel : BasePanel<HomeMainPannel>
{
    public Button flaskBtn;
    public Button distillerBtn;
    public Button inventroyBtn;
    public Button furnaceBtn;
    public Button potBtn;
    public Button beginBtn;
    public SceneLoadEventSO begin;
    public Vector3 posTo;
    public InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();
    public override void Init()
    {
        beginBtn.onClick.AddListener(() =>
        {
            GameStart();
            PortableBag.Instance.InitBag();
        });
        flaskBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<FlaskPannel>("FlaskPannel", E_UI_Layer.top);
            inventoryUI.OpenBoxUI();
        });
        distillerBtn.onClick.AddListener(() =>
        {
            //DistillerPanel.Instance.ShowMe();
            inventoryUI.OpenBoxUI();
        });
        inventroyBtn.onClick.AddListener(() =>
        {
            inventoryUI.OpenBagUI();
            inventoryUI.OpenBoxUI();
        });
        potBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<PotPanel>("PotPanel", E_UI_Layer.top);
            inventoryUI.OpenBoxUI();
        });
        furnaceBtn.onClick.AddListener(() =>
        {
            
        });
        
    }

    private void Start()
    {
        SetContain();
        Init();
    }
    public void GameStart()
    {
        // SceneMgr.Instance.LoadNextScene("EntrancePlace");
        // UIManager.Instance.fightUI.SetActive(true);
        begin.RaiseLoadScenetEvent("Home","EntrancePlace",posTo);
        Debug.Log("L");
    }

    public void SetContain()
    {
        flaskBtn.gameObject.SetActive(InventoryManager.Instance.haveFlask);
        distillerBtn.gameObject.SetActive(InventoryManager.Instance.haveDistiller);
        furnaceBtn.gameObject.SetActive(InventoryManager.Instance.haveFurnace);
        potBtn.gameObject.SetActive(InventoryManager.Instance.havePot);
    }
}
