# xamarin-momosdk

## Packages ##

Platform/Feature               | Package name                              | Stable      |
-----------------------|-------------------------------------------|-----------------------------|
Momo Wallet             | `Xamarin.Momo` | [![NuGet](https://badge.fury.io/nu/Xamarin.Momo.svg)](https://www.nuget.org/packages/Xamarin.Momo/) |

## Android

### Step 1. Config AndroidMainfest
```xml
<uses-permission android:name="android.permission.INTERNET" />
```
### Step 2. Get token & request payment
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
### Step 3:Get token callback from MoMo app an submit to server side

```C#
protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            AppMoMoLib.Instance.TrackEventResult(this, data);//request tracking result data
            if (requestCode == AppMoMoLib.Instance.RequestCodeMomo && (int)resultCode == -1)
            {
                if (data != null)
                {
                    if (data.GetIntExtra("status", -1) == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("message: " + "Get token " + data.GetStringExtra("message"));

                        if (data.GetStringExtra("data") != null && !data.GetStringExtra("data").Equals(""))
                        {
                            // TODO:

                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("message: " + "not_receive_info");
                        }
                    }
                    else if (data.GetIntExtra("status", -1) == 1)
                    {
                        string message = data.GetStringExtra("message") != null ? data.GetStringExtra("message") : "Thất bại";
                        System.Diagnostics.Debug.WriteLine("message: " + message);
                    }
                    else if (data.GetIntExtra("status", -1) == 2)
                    {
                        System.Diagnostics.Debug.WriteLine("message: " + "not_receive_info");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("message: " + "not_receive_info");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("message: " + "not_receive_info");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("message: " + "not_receive_info_err");
            }
        }
```

## iOS

### STEP 1: Config file Plist (CFBundleURLTypes and LSApplicationQueriesSchemes)

 ```xml
 <key>CFBundleURLTypes</key>
 <array>
   <dict>
     <key>CFBundleURLName</key>
     <string></string>
     <key>CFBundleURLSchemes</key>
     <array>
       <string>partnerSchemeId</string>
     </array>
   </dict>
 </array>
 <key>LSApplicationQueriesSchemes</key>
 <array>
   <string>momo</string>
 </array>
 <key>NSAppTransportSecurity</key>
 <dict>
   <key>NSAllowsArbitraryLoads</key>
   <true/>
 </dict>
 ```
- CFBundleURLTypes: add scheme <partnerSchemeId> . Note: partnerSchemeId provided by MoMo , get from business.momo.vn
- LSApplicationQueriesSchemes: add the scheme as "momo"
        
### STEP 2: Implement following methods:
        
```C#

void InitMomo
{
            NSNotificationCenter.DefaultCenter.RemoveObserver(this, aName: "NoficationCenterTokenReceived", null);
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("NoficationCenterTokenReceived"), new Action<NSNotification>(OnNotify));
}


void RequestMomo
{
           
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
}

void OnNotify(NSNotification notification)
{
       var response = notification.Object.ToString();
       if (response.Contains("message=Successful"))
       {
        //TODO: implement your code here
        }
}
```

### Step 3: Handle OpenURL in AppDelegate.cs

```C#
public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {   
            if (url.AbsoluteString.StartsWith(MOMO_MERCHANT_CODE_HERE))
            {
                NativeLibrary.MoMoPayment.ShareInstant.HandleOpenUrl(url);
                return true;
            }
            return base.OpenUrl(app, url, options);
        }

        public override bool HandleOpenURL(UIApplication application, NSUrl url)
        {
            NativeLibrary.MoMoPayment.ShareInstant.HandleOpenUrl(url);
            return true;
        }
```
