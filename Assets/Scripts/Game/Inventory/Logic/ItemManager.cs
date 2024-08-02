using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
    public class ItemManager : MonoBehaviour
    {
        public LegacyItem itemPrefab;
        private Transform itemParent;

        //记录场景Item
        private Dictionary<string, List<LegacyPickableItem>> sceneItemDict = new Dictionary<string, List<LegacyPickableItem>>();

        private void OnEnable()
        {
            //EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
            EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        }

        private void OnDisable()
        {
            //EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
            EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
            EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        }

        private void OnBeforeSceneUnloadEvent()
        {
            GetAllSceneItems();
        }

        private void OnAfterSceneLoadedEvent()
        {
            itemParent = GameObject.FindWithTag("ItemParent").transform;
            RecreateAllItems();
        }



        private void OnInstantiateItemInScene(ItemID ID, Vector3 pos)
        {
            var item = Instantiate(itemPrefab, pos, Quaternion.identity);
            item.itemID = ID;
        }
        /// <summary>
        /// 获取当前场景所有item
        /// </summary>
        private void GetAllSceneItems()
        {
            List<LegacyPickableItem> currentSceneItems = new List<LegacyPickableItem>();

            foreach (var item in FindObjectsOfType<LegacyItem>())
            {
            LegacyPickableItem sceneItem = new LegacyPickableItem
            {
                itemID = item.itemID,
                position = item.transform.position
                };
                currentSceneItems.Add(sceneItem);
            }

            if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                //找到数据就更新item数据列表
                sceneItemDict[SceneManager.GetActiveScene().name] = currentSceneItems;
            }
            else //如果是新场景
            {
                sceneItemDict.Add(SceneManager.GetActiveScene().name, currentSceneItems);
            }
        }
        /// <summary>
        /// 刷新重建场景中的物体
        /// </summary>
        private void RecreateAllItems()
        {
            List<LegacyPickableItem> currentSceneItems = new List<LegacyPickableItem>();

            if (sceneItemDict.TryGetValue(SceneManager.GetActiveScene().name, out currentSceneItems))
            {
                if (currentSceneItems != null)
                {
                    //清场
                    foreach (var item in FindObjectsOfType<LegacyItem>())
                    {
                        Destroy(item.gameObject);
                    }
                    foreach (var item in currentSceneItems)
                    {
                        LegacyItem newItem = Instantiate(itemPrefab, item.position, Quaternion.identity, itemParent);
                        newItem.Init(item.itemID);
                    }
                }
            }
        }
        private void OnDropItemEvent(ItemID ID, Vector3 pos)/*, ItemType type)*/
        {
            LegacyItem item = Instantiate(itemPrefab, pos, Quaternion.identity);
            item.itemID = ID;
        }
    }