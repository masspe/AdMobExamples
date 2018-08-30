using Android.Content;
using Android.OS;
using Android.Widget;
using RenderEngine.Screens;
using System;

namespace AdModExamples
{
    [BroadcastReceiver]
    public class ToastBroadcast : BroadcastReceiver
    {
        
          
        public override void OnReceive(Context context, Intent intent)
        {
            Console.WriteLine("timer---------------");
            Toast.MakeText(context, string.Format("THE TIME IS {0}", DateTime.Now.ToShortTimeString()), ToastLength.Long).Show();
            Vibrator vibrator = (Vibrator)context.GetSystemService(Context.VibratorService);
            vibrator.Vibrate(500);

           
            context.StartActivity(InterOperAct.activity1.Intent);

          
    }
    }
}