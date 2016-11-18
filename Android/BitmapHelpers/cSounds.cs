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

namespace CommonAndroidTools
{
    public static class cSounds
    {
        public static void Error(Context context)
        {
            MediaPlayer _player = MediaPlayer.Create(context, Resource.Raw.Antares);
            _player.Start();
        }

        public static void Scan(Context context)
        {
            MediaPlayer _player = MediaPlayer.Create(context, Resource.Raw.decodeshort);
            _player.Start();
        }

        public static void EndOfProcess(Context context)
        {
            MediaPlayer _player = MediaPlayer.Create(context, Resource.Raw.TaDa);
            _player.Start();
        }
        public static void Correct(Context context)
        {
            MediaPlayer _player = MediaPlayer.Create(context, Resource.Raw.Tejat);
            _player.Start();
        }
    }
}