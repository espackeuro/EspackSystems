//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.Content.PM;
//using Android.OS;
//using Android.Provider;
//using Android.Graphics;
//using Android.Widget;
//using Android.Runtime;
//using Android.Views;
//using Java.IO;
////using Java.Lang;
//using Uri = Android.Net.Uri;
//using BitmapHelpers;
//using System.ComponentModel;
//using cAccesoDatosAndroid;
//using DataWedge;


//namespace RadioFXC
//{
//    [Activity(Label = "RepairManagement")]
//    //[IntentFilter(new[] {Action = new[] {Intent.Action,"" })]
//    public class RepairManagement : Activity
//    {
//        public static Button cBtnAddPictures { get; set; }
//        private GridView cGridview;
//        private ListImageAdapter cImageAdapter;
//        private static Button cBtnAddPart { get; set; }
//        private static File cDir;
//        private static File cFile;
//        private string cUnitNumber;
//        public string cRepairCode { get; set; }

//        public static int cOldPictures { get; set; }

//        public int cWidthThumbnail
//        {
//            get
//            {
//                return (Math.Min(Resources.DisplayMetrics.WidthPixels, Resources.DisplayMetrics.HeightPixels) - 100) / 3;
//            }
//        }
//        protected override void OnCreate(Bundle bundle)
//        {
//            base.OnCreate(bundle);
//            SetContentView(Resource.Layout.RepairManagement);
//            cUnitNumber = Intent.GetStringExtra("UnitNumber");
//            cRepairCode = Intent.GetStringExtra("RepairCode");
//            string lOldPicturesCount= Intent.GetStringExtra("PicturesCount");
//            FindViewById<TextView>(Resource.Id.lblUnitNumber).Text = cUnitNumber;
//            FindViewById<TextView>(Resource.Id.lblRepairNumber).Text = cRepairCode;
//            cGridview = FindViewById<GridView>(Resource.Id.gridPictures);
//            //cGridview.LayoutParameters= new GridView.LayoutParams(cGridview.Width, (Resources.DisplayMetrics.WidthPixels - 100) / 3 + 100);
//            cImageAdapter = new ListImageAdapter(this, FindViewById<ProgressBar>(Resource.Id.ftpProgressBar),FindViewById<TextView>(Resource.Id.ftpTextProgressBar));
//            cGridview.Adapter = cImageAdapter;
//            cGridview.ItemClick += CGridview_ItemClick;
//            cBtnAddPictures = FindViewById<Button>(Resource.Id.btnAddPictures);
//            cBtnAddPictures.Text = "Add Picture(" + lOldPicturesCount + ")";
//            cBtnAddPart = FindViewById<Button>(Resource.Id.btnAddPart);
//            cOldPictures = Convert.ToInt32(lOldPicturesCount);
//            CreateDirectoryForPictures();
//            cBtnAddPictures.Click += TakeAPicture;
//            cBtnAddPart.Click += CBtnAddPart_Click;
//            // Create your application here
//        }

//        private void CBtnAddPart_Click(object sender, EventArgs e)
//        {
//            Intent i = new Intent(cDataWedge.ACTION_SOFTSCANTRIGGER);
//            i.PutExtra(cDataWedge.EXTRA_PARAM, cDataWedge.DWAPI_TOGGLE_SCANNING);
//            //DWDemoActivity.this.sendBroadcast(i);
//            //this.StartActivityForResult(i, 5);
//            this.SendBroadcast(i);
//            Toast.MakeText(this, "Soft scan trigger toggled.", ToastLength.Short).Show();
//        }

//        protected override void OnNewIntent(Intent intent)
//        {
//            var txt= cDataWedge.HandleDecodeData(intent);
//        } 

//        private void CGridview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
//        {
//            Toast.MakeText(this, e.Position.ToString(), ToastLength.Short).Show();
//        }

//        private void CreateDirectoryForPictures()
//        {
//            cDir = new File(
//                Android.OS.Environment.GetExternalStoragePublicDirectory(
//                    Android.OS.Environment.DirectoryPictures), "RadioFXC");
//            if (!cDir.Exists())
//            {
//                cDir.Mkdirs();
//            }
//        }

//        private bool IsThereAnAppToTakePictures()
//        {
//            Intent intent = new Intent(MediaStore.ActionImageCapture);
//            IList<ResolveInfo> availableActivities =
//                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
//            return availableActivities != null && availableActivities.Count > 0;
//        }
//        private void TakeAPicture(object sender, EventArgs eventArgs)
//        {
//            Intent intent = new Intent(MediaStore.ActionImageCapture);
//            cFile = new File(cDir, String.Format(cUnitNumber + "_" + cRepairCode + "_{0}.jpg", (cImageAdapter.Count +1+ cOldPictures).ToString()));
//            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(cFile));
//            StartActivityForResult(intent, 4);
//        }

//        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
//        {
//            base.OnActivityResult(requestCode, resultCode, data);

//            // Make it available in the gallery

//            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
//            Uri contentUri = Uri.FromFile(cFile);
//            mediaScanIntent.SetData(contentUri);
//            SendBroadcast(mediaScanIntent);
//            Bitmap bitmap = cFile.Path.LoadAndResizeBitmap(cWidthThumbnail, cWidthThumbnail);
//            ImageFile elFile = new ImageFile(cFile, cDir, bitmap);
//            cImageAdapter.AddImage(elFile);
//            //cImageFileList.Add(elFile);
//            cImageAdapter.NotifyDataSetChanged();
//            cGridview.InvalidateViews();       

//            // Dispose of the Java side bitmap.
//            GC.Collect();
//        }
        

//    }
//    public class ListImageAdapter : BaseAdapter<ImageFile>
//    {
//        private ListImageFile cImageFileList;
//        private readonly Context context;

//        public ListImageAdapter(Context c, ProgressBar pProgressBar = null, TextView pTextView = null)
//        {
//            context = c;
//            cImageFileList = new ListImageFile((Activity)context, pProgressBar, pTextView, ((Activity)context).FindViewById<TextView>(Resource.Id.lblRepairNumber).Text);
//            cImageFileList.AfterFTP += CImageFileList_AfterFTP;
//        }

//        private void CImageFileList_AfterFTP(object sender, ListImageFileEventArgs e)
//        {
//            string FileName = e.ImageFileString;
//            string FilePath = cImageFileList[FileName].ServerPath;
//            try
//            {
//                if (e.Status != "OK")
//                {
//                    throw new Exception(e.Status);
//                }
//                cImageFileList[FileName].Status = "UPLOADED";
//                SP pAddPicturesRepairs = new SP(Values.gDatos, "pAddPicturesRepairs");
//                pAddPicturesRepairs.AddParameterValue("RepairCode", ((RepairManagement)context).cRepairCode);
//                pAddPicturesRepairs.AddParameterValue("FileName", FileName);
//                pAddPicturesRepairs.AddParameterValue("FilePath", FilePath);
//                pAddPicturesRepairs.Execute();
//                if (pAddPicturesRepairs.LastMsg != "OK")
//                {
//                    throw (new Exception(pAddPicturesRepairs.LastMsg));
//                }
//                //delete the local file once transmitted
//                bool deleted = cImageFileList[FileName].File.Delete();
//                ((Activity)context).RunOnUiThread(() => 
//                {
//                    RepairManagement.cBtnAddPictures.Text = "Add Picture (" + RepairManagement.cOldPictures + ")";
//                });
//            }
//            catch (System.Exception ex)
//            {
//                ((Activity)context).RunOnUiThread(() =>
//                {
//                    cImageFileList[FileName].Status = "ERROR_DB";
//                    Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(context);
//                    AlertDialog alertDialog = builder.Create();
//                    alertDialog.SetTitle("ERROR");
//                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
//                    alertDialog.SetMessage("Error: " + ex.Message);
//                    alertDialog.SetButton("OK", (s, ev) =>
//                    {
//                        ((Activity)context).Finish();
//                    });
//                    alertDialog.Show();
//                });
//                return;
//            }
//        }

//        public override int Count
//        {
//            get { return cImageFileList.Count; }
//        }

//        public override ImageFile this[int position]
//        {
//            get
//            {
//                return cImageFileList[position];
//            }
//        }


//        public override long GetItemId(int position)
//        {
//            return position;
//        }

//        public void AddImage(ImageFile pFile)
//        {
//            cImageFileList.Add(pFile);
//        }

//        public override View GetView(int position, View convertView, ViewGroup parent)
//        {
//            ImageView imageView;

//            if (convertView == null)
//            {
//                // if it's not recycled, initialize some attributes
//                imageView = new ImageView(context);
//                int width = (context.Resources.DisplayMetrics.WidthPixels - 100) / 3;
//                imageView.LayoutParameters = new AbsListView.LayoutParams(((RepairManagement)context).cWidthThumbnail, ((RepairManagement)context).cWidthThumbnail);
//                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
//                imageView.SetPadding(8, 8, 8, 8);
//            }
//            else
//            {
//                imageView = (ImageView)convertView;
//            }
//            imageView.SetImageBitmap(cImageFileList[position].Bitmap);
//            return imageView;
//        }
//    }
//}