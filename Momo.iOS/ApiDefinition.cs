using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace NativeLibrary
{
        // @interface MoMoPayment : NSObject
        [BaseType(typeof(NSObject))]
        interface MoMoPayment
        {
            // +(MoMoPayment *)shareInstant;
            [Static]
            [Export("shareInstant")]
            //[Verify(MethodToProperty)]
            MoMoPayment ShareInstant { get; }

            // -(void)initializingAppBundleId:(NSString *)bundleid merchantId:(NSString *)merchantId merchantName:(NSString *)merchantname merchantNameTitle:(NSString *)merchantNameTitle billTitle:(NSString *)billTitle;
            [Export("initializingAppBundleId:merchantId:merchantName:merchantNameTitle:billTitle:")]
            void InitializingAppBundleId(string bundleid, string merchantId, string merchantname, string merchantNameTitle, string billTitle);

            // -(void)requestToken;
            [Export("requestToken")]
            void RequestToken();

            // -(void)handleOpenUrl:(NSURL *)url;
            [Export("handleOpenUrl:")]
            void HandleOpenUrl(NSUrl url);

            // -(void)createPaymentInformation:(NSMutableDictionary *)info;
            [Export("createPaymentInformation:")]
            void CreatePaymentInformation(NSMutableDictionary info);

            // -(void)addMoMoPayDefaultButtonToView:(UIView *)parrentView;
            [Export("addMoMoPayDefaultButtonToView:")]
            void AddMoMoPayDefaultButtonToView(UIView parrentView);

            // -(UIButton *)addMoMoPayCustomButton:(UIButton *)button forControlEvents:(UIControlEvents)controlEvents toView:(UIView *)parrentView;
            [Export("addMoMoPayCustomButton:forControlEvents:toView:")]
            UIButton AddMoMoPayCustomButton(UIButton button, UIControlEvent controlEvents, UIView parrentView);

            // -(NSString *)getAction;
            [Export("getAction")]
            //[Verify(MethodToProperty)]
            string Action { get; }

            // -(void)setAction:(NSString *)action;
            [Export("setAction:")]
            void SetAction(string action);

            // -(void)updateAmount:(long long)amt;
            [Export("updateAmount:")]
            void UpdateAmount(long amt);

            // -(void)setSubmitURL:(NSString *)submitUrl;
            [Export("setSubmitURL:")]
            void SetSubmitURL(string submitUrl);

            // -(NSMutableDictionary *)getPaymentInfo;
            [Export("getPaymentInfo")]
            //[Verify(MethodToProperty)]
            NSMutableDictionary PaymentInfo { get; }

            // -(void)requestWebpaymentData:(NSMutableDictionary *)dataPost requestType:(NSString *)requesttype;
            [Export("requestWebpaymentData:requestType:")]
            void RequestWebpaymentData(NSMutableDictionary dataPost, string requesttype);

            // -(NSString *)getDeviceInfoString;
            [Export("getDeviceInfoString")]
            //[Verify(MethodToProperty)]
            string DeviceInfoString { get; }

            // -(void)setMoMoAppScheme:(NSString *)bundleId;
            [Export("setMoMoAppScheme:")]
            void SetMoMoAppScheme(string bundleId);

            // -(void)initPaymentInformation:(NSMutableDictionary *)info momoAppScheme:(NSString *)bundleId environment:(MOMO_ENVIRONTMENT)type_environment;
            [Export("initPaymentInformation:momoAppScheme:environment:")]
            void InitPaymentInformation(NSMutableDictionary info, string bundleId, MOMO_ENVIRONTMENT type_environment);

            // -(void)setEnvironment:(MOMO_ENVIRONTMENT)type_environtment;
            [Export("setEnvironment:")]
            void SetEnvironment(MOMO_ENVIRONTMENT type_environtment);

            // -(BOOL)getEnvironment;
            [Export("getEnvironment")]
            //[Verify(MethodToProperty)]
            bool Environment { get; }
        }
}

