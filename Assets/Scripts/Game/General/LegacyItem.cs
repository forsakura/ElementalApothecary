using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class LegacyItem : MonoBehaviour
{
    public ItemID itemID;//加个id分辨物品
    [HideInInspector]public PlayerControll playerControll;
    private EPlayerAttackState currentAttackState;
    public Vector2 startPosition;
    [Tooltip("对应鼠标的点击点")]public Vector2 movePosition;
    public GameObject effectPre;

    
    [Header("投掷模式")]
    public AnimationCurve curve;
    [Tooltip("A到B的时间")]public float direction;
    public float maxHeight;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public LegacyItemDetails itemDetails;
    private BoxCollider2D coll;
    [Header("------")]
    public bool isOnBag;



    public virtual void Start() 
    {
        playerControll = FindAnyObjectByType<PlayerControll>();

        currentAttackState = playerControll.currentAttackState;

        rb = GetComponent<Rigidbody2D>();
        Init(itemID);
    }

    public virtual void Update() 
    {
        if(isOnBag )
        {
            gameObject.layer = 2;
        }
        else
        {
            
            SwitchAttackState(currentAttackState);
        }

            
    }
    //TODO:销毁后生成粒子效果预制体 播放相应动画
    public void SwitchAttackState(EPlayerAttackState ePlayerAttackState)
    {
        switch (ePlayerAttackState)
        {
            case EPlayerAttackState.Throwing:
                ThrowModelDestroyObj();
                StartCoroutine(Curve(playerControll.rightHand.position,movePosition));
                break;
            case EPlayerAttackState.Shooting:
                ShootModelDestroyObj();
                SetMoveDirection();
                
                break;
        }
    }
    public void SetMoveDirection()
    {
        Vector2 shootDirection = (movePosition - startPosition).normalized;

        rb.velocity = shootDirection*playerControll.character.shootSpeed;
        
    }

    IEnumerator Curve(Vector3 startPoint,Vector3 endPoint)
    {
        var timePast = 0f;

        while (timePast < direction)
        {
            timePast += Time.deltaTime;

            var curveTime = timePast / direction;//curve中对应的x值

            var heightTime = curve.Evaluate(curveTime);//curve中对应的y值

            var height = Mathf.Lerp(0,maxHeight, heightTime);

            transform.position = Vector2.Lerp(startPoint,endPoint,curveTime) + new Vector2(0,height);

            yield return null;
        }

        
    }

    public virtual void ThrowModelDestroyObj()
    {
        float dis = Vector2.Distance(transform.position,playerControll.transform.position);

        if (dis > playerControll.character.throwDistance)
        {
            
            Destroy(gameObject);
            
        }
    }

    public virtual void ShootModelDestroyObj()
    {
        float distance = (transform.position - playerControll.transform.position).magnitude;
        if(distance >= playerControll.character.throwDistance)
        { 
            Destroy(gameObject);
        }
    }
  

    public void Init(ItemID ID)
    {
        itemID = ID;

        //Inventory获得当前数据
        itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
        if (itemDetails != null)
        {
            //spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;
            ////修改碰撞体尺寸
            //Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
            //coll.size = newSize;
            //coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject,0.1f);
        }
    }
}

    
   
