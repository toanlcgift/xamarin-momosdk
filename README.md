### xamarin-momosdk


##Android

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
