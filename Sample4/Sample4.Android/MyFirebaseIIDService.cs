using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using Android.App;
using Android.Content;
using Newtonsoft.Json;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Android.Support.V4.App;

namespace Sample4.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseIIDService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);

            Log.Debug(TAG, "Refreshed token: " + p0);
            try
            {
                if (Xamarin.Forms.Application.Current.Properties.ContainsKey("Fcmtocken"))
                {
                    Xamarin.Forms.Application.Current.Properties["Fcmtocken"] = p0 ?? "";
                    Xamarin.Forms.Application.Current.SavePropertiesAsync();

                }
            }
            catch (Exception ex)
            { }

            SendRegistrationToServer(p0);
        }

        void SendRegistrationToServer(string token)
        {


        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            SendNotification(message);
                      
        }
        void SendNotification(RemoteMessage message)
        {
            var intent = new Intent(this, typeof(MainActivity));





            var pendingIntent = PendingIntent.GetActivity(this,
                                                          MainActivity.NOTIFICATION_ID,
                                                          intent,
                                                          PendingIntentFlags.OneShot);



            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.abc_list_selector_holo_light)
                                      .SetContentTitle(message.GetNotification().Title)
                                      .SetContentText(message.GetNotification().Body)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());

        }
    }
}
