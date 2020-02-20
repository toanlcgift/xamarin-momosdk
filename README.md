### xamarin-momosdk


## Android

```C#
private void requestPayment()
        {
            AppMoMoLib.Instance.SetEnvironment(AppMoMoLib.ENVIRONMENT.Development);
            AppMoMoLib.Instance.SetAction(AppMoMoLib.ACTION.Payment);
            AppMoMoLib.Instance.SetActionType(AppMoMoLib.ACTION_TYPE.GetToken);
            //Set token tracking
            AppMoMoLib.Instance.SetToken(MOMO_TOKEN_HERE);


            var eventValue = new Dictionary<string, Java.Lang.Object>();
            //client Required
            eventValue.Add(MoMoParameterNamePayment.MerchantName, MOMO_MERCHANTNAME_HERE);
            eventValue.Add(MoMoParameterNamePayment.MerchantCode, MOMO_MERCHANTCODE_HERE);
            eventValue.Add(MoMoParameterNamePayment.Amount, "0");
            eventValue.Add(MoMoParameterNamePayment.Description, "test");
            //client Optional
            eventValue.Add(MoMoParameterNamePayment.Fee, "0");
            eventValue.Add(MoMoParameterNamePayment.MerchantNameLabel, "MerchantNameLabel");

            eventValue.Add(MoMoParameterNamePayment.RequestId, "merchantCode" + "-" + UUID.RandomUUID().ToString());
            eventValue.Add(MoMoParameterNamePayment.PartnerCode, "CGV19072017");

            JSONObject objExtraData = new JSONObject();
            try
            {
                objExtraData.Put("site_code", "008");
                objExtraData.Put("site_name", "CGV Cresent Mall");
                objExtraData.Put("screen_code", 0);
                objExtraData.Put("screen_name", "Special");
                objExtraData.Put("movie_name", "Kẻ Trộm Mặt Trăng 3");
                objExtraData.Put("movie_format", "2D");
                objExtraData.Put("ticket", "{\"ticket\":{\"01\":{\"type\":\"std\",\"price\":110000,\"qty\":3}}}");
            }
            catch (JSONException e)
            {
                e.PrintStackTrace();
            }
            eventValue.Add(MoMoParameterNamePayment.ExtraData, objExtraData.ToString());
            eventValue.Add(MoMoParameterNamePayment.RequestType, "payment");
            eventValue.Add(MoMoParameterNamePayment.Language, "vi");
            eventValue.Add(MoMoParameterNamePayment.Extra, "");
            //Request momo app
            AppMoMoLib.Instance.RequestMoMoCallBack(this, eventValue);
        }
    }
```

## iOS

```C#

            NSNotificationCenter.DefaultCenter.RemoveObserver(this, aName: "NoficationCenterTokenReceived", null);
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("NoficationCenterTokenReceived"), new Action<NSNotification>(OnNotify));
            NativeLibrary.MoMoPayment.ShareInstant.InitializingAppBundleId(package_name_here, Momo_Merchant_Code_Here, Momo_Merchant_Name_Here, @"test", @"test");
            NSMutableDictionary dict =  new NSMutableDictionary();
            dict.Add(new NSString("amount"), new NSNumber(1000));
            dict.Add(new NSString("fee"), new NSNumber(1000));
            dict.Add(new NSString("extra"), new NSString("{\"key1\":\"value1\",\"key2\":\"value2\"}"));
            dict.Add(new NSString("appScheme"), new NSString(Momo_Merchant_Code_Here));
            dict.Add(new NSString("language"), new NSString("vi"));
            dict.Add(new NSString("username"), new NSString("toanlcgift@gmail.com"));
            dict.Add(new NSString("description"), new NSString("test momo"));
            NativeLibrary.MoMoPayment.ShareInstant.InitPaymentInformation(dict, "momo", NativeLibrary.MOMO_ENVIRONTMENT.MOMO_SDK_DEVELOPMENT);
            NativeLibrary.MoMoPayment.ShareInstant.RequestToken();
            
```
