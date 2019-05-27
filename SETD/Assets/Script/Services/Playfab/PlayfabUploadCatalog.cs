using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.Json;
using PlayFab.AdminModels;

namespace PlayfabServices.Admin
{

#if UNITY_EDITOR
    public class PlayfabUploadCatalog
    {
        public void UploadCatalog(string targetCatalog, List<CatalogItem> catalogToUpload)
        {
            var updateCatalogRequest = new UpdateCatalogItemsRequest
            {
                Catalog = catalogToUpload,
                CatalogVersion = targetCatalog,
            };

            PlayFabAdminAPI.SetCatalogItems(updateCatalogRequest, reuslt => Debug.Log("Upload Complete"), null);

            // PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            // {
            //     FunctionName = "updateCatalogs",
            //     FunctionParameter = new
            //     {
            //         catalogName = targetCatalog,
            //         catalogData = catalogToUpload
            //     },
            //     GeneratePlayStreamEvent = true
            // }, reuslt => Debug.Log("Upload Complete"), OnError);
        }

        public List<CatalogItem> SetupCatalogJson(List<ItemData> items)
        {
            if (items.Count <= 0)
                return null;

            List<CatalogItem> catalog = new List<CatalogItem>();

            for (int i = 0; i < items.Count; i++)
            {
                CatalogItem newItem = new CatalogItem();
                JsonObject customData = new JsonObject();
                newItem.ItemId = items[i].id.ToString();
                newItem.ItemClass = "ItemData";
                newItem.DisplayName = items[i].itemName;
                newItem.Description = items[i].itemDesc;
                newItem.IsStackable = items[i].isStackable;
                newItem.Consumable = new CatalogItemConsumableInfo { UsageCount = 1 };

                customData.Add("Price", items[i].price.ToString());
                customData.Add("Capacity", items[i].capacity.ToString());

                newItem.CustomData = customData.ToString();

                catalog.Add(newItem);
            }

            return catalog;
        }
        // public JsonObject SetupCatalogJson(List<ClassData> classes)
        // {

        // }

        // public JsonObject SetupCatalogJson(List<Modifier> modifiers)
        // {

        // }
    }
#endif
}
