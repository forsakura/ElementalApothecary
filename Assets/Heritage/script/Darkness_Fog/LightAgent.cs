using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAgent : MonoBehaviour
{
    LightManager lightManager;

    GameObject YourLight;
    List<GameObject> OtherLights;

    [SerializeField]
    bool updateOnlyOnMove = false;
    [SerializeField]
    int defaultLightRange;
    
    private void Update()
    {
        if(YourLight != null)
        {
            YourLight.transform.position = gameObject.transform.position;
        }
    }

    private void OnDestroy()
    {
        if (YourLight != null)
        {
            StopCastLight();
        }
    }

    public void CastLight()//������
    {
        lightManager.CreateSingleLight(
            gameObject.transform.position, 
            defaultLightRange, 
            updateOnlyOnMove,
            out YourLight);
    }

    public void SetCastRange(int range)
    {
        lightManager.SetLightRange(YourLight, range);
    }

    public void StopCastLight()
    {
        lightManager.DeleteSingleLight(YourLight);
    }

    public void createEmptyLight(Vector3 LightPosition,int LighRange,bool updateOOM)//��ʱ������,���ܾ���Ҫ��Ҳ�����ȡ�������
    {
        GameObject EmptyLight;

        lightManager.CreateSingleLight(
            LightPosition,
            LighRange,
            updateOOM,
            out EmptyLight);

        OtherLights.Add(EmptyLight);
    }
}
