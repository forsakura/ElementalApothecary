using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Warning:
//     �˽ű�����Ϊչʾ��ɫ֪ͨUI���²��ֵĽӿڣ�����и��õģ��Ǿͱ�������������е��ҡ�
// ����ȱһ��ʣ��ҩˮ�����ֿ⣬�������UIû��ϵ��
// ��Ϊ���ں���û����ͨ�� ID �����زĺ�ҩˮ���࣬���滹Ҫ��΢�ģ���һЩд�������ֺ�ͼƬ�ĳɲ��ҽ����Ӧ�����ݡ�

public class AnnounceToUI : MonoBehaviour
{

    // <potionID, count>
    Dictionary<int, int> potions = new Dictionary<int, int>();

    int remainingBullet;

    [SerializeField]
    GameObject _bulletPrefab;
    [SerializeField]
    InventoryBag_SO potionBag;

    private void Start()
    {
        var itemlist = InventoryManager.Instance.playerBag.itemList;

        potions = new Dictionary<int, int>();
        foreach (var item in itemlist)
        {
            if (item.itemID != null)
            {
                //potions.Add(item.itemID, item.itemAmount);
            }
        }
        Init(potions);
    }

    
    // 初始化携带的药水背包
    public void Init(Dictionary<int, int> carriedPotions)
    {
        potions = carriedPotions;
        // portableBag.InitBag(carriedPotions);
        PortableBag.Instance.InitBag(potions);
    }

    public GameObject UseOnePotion()
    {
        // 关于药水选择几号位这个就放在UI里点了，不然还要UI反过来提醒当前选择药剂变了

        int id = 0;

        potions[id]--;
        //InventoryManager.Instance.RemoveBagItem(id, 1);
        PortableBag.Instance.SetPotionCount(potions[id]);
        // ToDo，子弹Prefab的位置
        //return Resources.Load<GameObject>("");
        return _bulletPrefab;
    }

    // ��ȡ�ز�����Ļ���Ͻ���ʾ
    public void GetMaterial(int materialID, int count)
    {
        MaterialFloatingContainer.Instance.GetMaterial(materialID, count);
    }

    // ��ȡ�����ز���ͣ��Ϸ����ʾ���м�
    public void GetSpecialMaterial(int materialID)
    {
        StartAndStop.Instance.GetSpecialMaterial(materialID);
    }

    // װ�������½ǵ�ҩ��UI��Ҫ����
    public GameObject FillTheGun()
    {
        GameObject bulletPfb = UseOnePotion();
        if(bulletPfb == null)
        {
            return null;
        }
        //BulletConsumption.Instance.Fill(PortableBag.Instance.GetCurrentPotionID());
        return bulletPfb;
    }

    // ����һ�Σ����ʣ�൯ҩΪ0���򷵻�false��ʾʧ�ܣ�����ʣ�൯ҩ�����UI������true��
    public bool ShootOnce()
    {
        if (remainingBullet == 0)
        {
            return false;
        }
        remainingBullet--;
        BulletConsumption.Instance.UpdateCover(remainingBullet);
        return true;
    }

    // ���½��л�������Ŀǰֻд�������Ͷ���ֻ������滹Ҫ����ҩ����ʲô�ط������ֲ���˳���ŷ�
    public void SwitchCurrentWeapon()
    {
        SwitchWeapon.Instance.SwitchWeaponToAnother();
    }

    // �ؿ���������ʣ��ҩ�����������ֿ⻹�ǲֿ������߼�����Ҫ����
    public Dictionary<int, int> ReturnRemainingPotions()
    {
        return potions;
    }
}
