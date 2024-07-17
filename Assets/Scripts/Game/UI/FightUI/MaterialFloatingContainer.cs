using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class MaterialFloatingContainer : Singleton<MaterialFloatingContainer>
{
    public Queue<MaterialFloating> floatings = new Queue<MaterialFloating>();

    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        UpdateRect();
    }

    public void Test()
    {
        GetMaterial(301, 6);
    }

    public void GetMaterial(int materialID, int count)
    {
        GameObject floatingPrefab = Resources.Load<GameObject>("Prefab/UI/FightUI/MaterialFloating");
        MaterialFloating floating = GameObject.Instantiate(floatingPrefab).GetComponent<MaterialFloating>();
        floating.transform.SetParent(transform);
        floating.InitSet(materialID, count, this);
        if(floatings.Count >= 5)
        {
            MaterialFloating older = floatings.Dequeue();
            Destroy(older.gameObject);
            Debug.Log("Overflow Dequeue.");
        }
        floatings.Enqueue(floating);
        Debug.Log("Enqueue new.");
        UpdateRect();
    }

    public void UpdateRect()
    {
        rect.anchorMin = new(rect.anchorMin.x, 1 - floatings.Count * 0.1f);
    }
}
