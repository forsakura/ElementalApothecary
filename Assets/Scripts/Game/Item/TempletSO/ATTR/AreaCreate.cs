using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "AeraCreate", menuName = "ATTR/AeraCreate")]
public class AreaCreate : BaseAttribute
{
    [SerializeField]
    Vector3 Pos_target;
    [SerializeField]
    HitInstance hitData;
    [SerializeField]
    GameObject AreaPrefab;
    [SerializeField]
    BulletControl bulletCrtl;

    GameObject Origin;
    DataItem itemData;


    public Vector2 Target { get => Pos_target; set => Pos_target = value; }

    public override void OnUpdate(GameObject target, float deltaTime)
    {
        if (Origin != target) 
        {
            bulletCrtl = target.GetComponent<BulletControl>();
            itemData = target.GetComponent<ItemEntityCtrl>().Data;
            initHit();
            if (bulletCrtl != null)
            {
                Debug.LogError("gameObject don't have BulletControl!!");
            }
        }
        Pos_target = bulletCrtl.Target;
        if (target.transform.position == Pos_target && Pos_target != null)  
        {
            CreateAera();
        }
    }

    void initHit()
    {
        if (hitData == null)
        {
            hitData = new HitInstance()
            {
                Source = null,//直接由物品决定应该是不需要角色作为“源”
                Damage = 0,
                elementState = new ElementVector()
                {
                    elementVector = itemData.ReturnElementVector()
                }
            };
        }
    }

    public void CreateAera()
    {
        GameObject area = Instantiate(AreaPrefab);
        area.GetComponent<EffectAreaCrtl>().Hit = hitData;
        //Debug.Log("CreateAera!!!");
        Destroy(Origin);//删除的是记录的目标
    }
}
