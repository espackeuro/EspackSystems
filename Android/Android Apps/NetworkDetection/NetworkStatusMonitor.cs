using Android.Telephony;
using Android.Renderscripts;
using Android.Util;
using Android.Nfc;

namespace NetworkDetection
{
    using Android.App;
    using Android.Content;
    using Android.Net;
    using Android.OS;
    using Android.Widget;
    using System;

    public class NetworkStatusMonitor
    {
        private NetworkState _state;
        private NetworkStatusBroadcastReceiver _broadcastReceiver;
        private Context context;
        public NetworkStatusMonitor(Context _context)
        {
            context = _context;
        }
        public NetworkState State
        {
            get
            {
                UpdateNetworkStatus();
                return _state;
            }
        }
        public void UpdateNetworkStatus()
        {
            if (context==null)
            {
                throw new Exception("Context not set.");
            }
            _state = NetworkState.Unknown;
            // Retrieve the connectivity manager service
            var connectivityManager = (ConnectivityManager)
                context.GetSystemService(
                    Context.ConnectivityService);
            // Check if the network is connected or connecting.
            // This means that it will be available,
            // or become available in a few seconds.
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;

            if (activeNetworkInfo!=null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                // Now that we know it's connected, determine if we're on WiFi or something else.
                _state = activeNetworkInfo.Type == ConnectivityType.Wifi ?
                    NetworkState.ConnectedWifi : NetworkState.ConnectedData;
            }
            else
            {
                _state = NetworkState.Disconnected;
            }
        }
        public event EventHandler NetworkStatusChanged;
        public void Start()
        {
            if (context == null)
            {
                throw new Exception("Context not set.");
            }
            if (_broadcastReceiver != null)
            {
                throw new InvalidOperationException(
                    "Network status monitoring already active.");
            }
            // Create the broadcast receiver and bind the event handler
            // so that the app gets updates of the network connectivity status
            _broadcastReceiver = new NetworkStatusBroadcastReceiver();
            _broadcastReceiver.ConnectionStatusChanged += OnNetworkStatusChanged;
            // Register the broadcast receiver
            context.RegisterReceiver(_broadcastReceiver,
              new IntentFilter(ConnectivityManager.ConnectivityAction));
        }
        void OnNetworkStatusChanged(object sender, EventArgs e)
        {
            var currentStatus = _state;
            UpdateNetworkStatus();
            if (currentStatus != _state && NetworkStatusChanged != null)
            {
                NetworkStatusChanged(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            if (_broadcastReceiver == null)
            {
                throw new InvalidOperationException(
                    "Network status monitoring not active.");
            }
            // Unregister the receiver so we no longer get updates.
            context.UnregisterReceiver(_broadcastReceiver);
            // Set the variable to nil, so that we know the receiver is no longer used.
            _broadcastReceiver.ConnectionStatusChanged -= OnNetworkStatusChanged;
            _broadcastReceiver = null;
        }
    }
    public enum NetworkState
    {
        Unknown,
        ConnectedWifi,
        ConnectedData,
        Disconnected
    }

    [BroadcastReceiver()]
    public class NetworkStatusBroadcastReceiver : BroadcastReceiver
    {
        public event EventHandler ConnectionStatusChanged;
        public override void OnReceive(Context context, Intent intent)
        {
            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(this, EventArgs.Empty);
        }
    }



}