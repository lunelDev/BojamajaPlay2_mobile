using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    #region 상품ID
    // 상품ID는 구글 개발자 콘솔에 등록한 상품ID와 동일하게 해주세요.
    public const string productId1 = "crystal_10";
    public const string productId2 = "onedayfree";
    public const string productId3 = "removead";

    #endregion

    void Start()
    {
        InitializePurchasing();
    }

    private bool IsInitialized()
    {
        return (storeController != null && extensionProvider != null);
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var module = StandardPurchasingModule.Instance();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(productId1, ProductType.Consumable, new IDs
        {
            { productId1, AppleAppStore.Name },
            { productId1, GooglePlay.Name },
        });

        builder.AddProduct(productId2, ProductType.Consumable, new IDs
        {
            { productId2, AppleAppStore.Name },
            { productId2, GooglePlay.Name }, }
        );

        builder.AddProduct(productId3, ProductType.Consumable, new IDs
        {
            { productId3, AppleAppStore.Name },
            { productId3, GooglePlay.Name },
        });


        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProductID(string productId)
    {
        try
        {
            if (IsInitialized())
            {
                Product p = storeController.products.WithID(productId);

                if (p != null && p.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
                    storeController.InitiatePurchase(p);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }

    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
    {
        Debug.Log("OnInitialized : PASS");

        storeController = sc;
        extensionProvider = ep;
    }

    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        switch (args.purchasedProduct.definition.id)
        {
            // 결제되는 다이아 : 10개
            case productId1:
                TimeManager.instance.diamondSu += 10;
                PlayerPrefs.SetInt("Diamond", TimeManager.instance.diamondSu);
                break;
            //1일 무제한 플레이
            case productId2:
                GameManager.HaveOneDayFree = true;
                PlayerPrefs.SetString("FreeTime", GameManager.HaveOneDayFree.ToString()); //1일 무제한 쿠폰이 존재
                PlayerPrefs.SetString("BuyEndOneDayFree", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                break;
            //광고 제거권
            case productId3:
                GameManager.HaveAdRemoveCount += 100;
               // PlayerPrefs.SetString("RemoveAD", GameManager.HaveAdRemove.ToString()); //str : true
                PlayerPrefs.SetInt("RemoveADCount", GameManager.HaveAdRemoveCount); //int  ; 100
                break;

        }
       //구매 완료 했을 경우,
        PopUpSystem.Instance.ShowPopUp(ShopAppManager.Instance.PurchaseCompleteOb);
        return PurchaseProcessingResult.Complete;
    }

    //구매 실패 했을 경우,
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        PopUpSystem.Instance.ShowPopUp(ShopAppManager.Instance.PurchasefailedOb); //구매에 실패 
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}