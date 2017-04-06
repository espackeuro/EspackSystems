using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.Threading.Tasks;

namespace CommonAndroidTools
{
    public static class cSounds
    {
        public static Task Error(Context context)
        {
            return Play(context, Resource.Raw.Antares);
        }

        public static Task Scan(Context context)
        {
            return Play(context, Resource.Raw.decodeshort);
        }
        public static Task Warning(Context context)
        {
            return Play(context, Resource.Raw.Gallium);
        }
        public static Task EndOfProcess(Context context)
        {
            return Play(context, Resource.Raw.TaDa);
        }
        public static Task Correct(Context context)
        {
            return Play(context, Resource.Raw.Tejat);
        }
        private static Task Play(Context context, int Resource)
        {
            return Task.Run(() =>
            {
                MediaPlayer _player = MediaPlayer.Create(context, Resource);
                _player.Start();
            });
        }
    }
}