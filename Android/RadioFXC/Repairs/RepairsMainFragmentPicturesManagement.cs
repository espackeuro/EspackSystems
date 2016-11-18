using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CommonAndroidTools;
using com.refractored.fab;
using Java.IO;
using System;
using System.Collections.Generic;
//using Java.Lang;
using Uri = Android.Net.Uri;
using AccesoDatosNet;
using Android.Graphics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RadioFXC
{


    [Activity(Label = "RepairManagement")]
    //[IntentFilter(new[] {Action = new[] {Intent.Action,"" })]
    public class FragmentPicturesManagement : Android.Support.V4.App.Fragment, IScrollDirectorListener, AbsListView.IOnScrollListener 
    {
        public static Button cBtnAddPictures { get; set; }
        private ListView cListView;
        private ListImageAdapter cImageAdapter;
        private static Button cBtnAddPart { get; set; }
        private static File cDir;
        private static File cFile;
        private string cUnitNumber;
        public string cRepairCode { get; set; }
        private int cNUM;
        private int cCount;
        public static int cOldPictures { get; set; }
        private DynamicRS cRSOld = new DynamicRS();
        public ListImageFile cImageFileList;

        public FragmentPicturesManagement() 
            : base()
        {
            cImageFileList = new ListImageFile();
            cUnitNumber = UnitRepair.cUnitNumber;
            cRepairCode = UnitRepair.cRepairCode;
            cCount = 0;
            cRSOld.Open("Select FileName,FilePath,Thumbnail,len=len(Thumbnail),IdPicture from PicturesRepairs where RepairCode='" + cRepairCode+"' and UnitNumber='"+cUnitNumber+"' order by xfec", Values.gDatos );
            while (!cRSOld.EOF)
            {
                Bitmap _bm = BitmapFactory.DecodeByteArray((byte[])cRSOld["Thumbnail"], 0, Convert.ToInt32(cRSOld["len"]));
                ImageFile elFile = new ImageFile(cRSOld["FileName"].ToString(), _bm);
                elFile.IdPicture = cRSOld["IdPicture"].ToString();
                cImageFileList.Add(elFile);
                cRSOld.MoveNext();
            }
            cRSOld.Close();
        }
        public int cWidthThumbnail
        {
            get
            {
                return (Math.Min(Resources.DisplayMetrics.WidthPixels, Resources.DisplayMetrics.HeightPixels) - 100) / 3;
            }
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.fragment_RepairPictures, container, false);
            root.FindViewById<TextView>(Resource.Id.lblUnitNumber).Text = cUnitNumber;
            root.FindViewById<TextView>(Resource.Id.lblRepairNumber).Text = cRepairCode;
            cListView = root.FindViewById<ListView>(Resource.Id.listPictures);
            //cGridview.LayoutParameters= new GridView.LayoutParams(cGridview.Width, (Resources.DisplayMetrics.WidthPixels - 100) / 3 + 100);

            cImageAdapter = new ListImageAdapter(Activity, cRepairCode, cUnitNumber,cWidthThumbnail,cImageFileList);
            cListView.Adapter = cImageAdapter;
            //while (!cRSOld.EOF)
            //{
            //    Bitmap _bm = BitmapFactory.DecodeByteArray((byte[])cRSOld["Thumbnail"], 0, Convert.ToInt32(cRSOld["len"]));
            //    ImageFile elFile = new ImageFile(cRSOld["FileName"].ToString(), _bm);
            //    elFile.IdPicture = cRSOld["IdPicture"].ToString();
            //    cImageAdapter.AddImage(elFile);
            //    cRSOld.MoveNext();
            //}
            //cRSOld.Close();
            var fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab);
            cOldPictures = Convert.ToInt32(cCount);
            CreateDirectoryForPictures();
            fab.AttachToListView(cListView, this, this);
            fab.Click += TakeAPicture;
            cListView.ItemLongClick += cListView_ItemLongClick;
            return root;
        }

        private void cListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);

            var position = e.Position;
            
            var _idFile = cImageAdapter[position];
            if (_idFile.Uploaded != true)
                return;

            var _idPic = cImageAdapter.GetPictureId(position).ToString();
            builder.SetTitle("Warning");
            builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            builder.SetMessage("Do you want to remove this picture?");
            builder.SetNegativeButton("No", delegate
            {
            });
            builder.SetPositiveButton("Yes", delegate
            {

                var _SP = new SP(Values.gDatos, "pDelPicturesRepairs");
                _SP.AddParameterValue("IdPicture", _idPic);
                _SP.Execute();
                if (_SP.LastMsg != "OK")
                {
                    throw (new Exception(_SP.LastMsg));
                }

                Activity.RunOnUiThread(() => Toast.MakeText(Activity, "Picture removed.", ToastLength.Long).Show());
                cImageAdapter.RemoveImage(_idPic);

                cImageAdapter.NotifyDataSetChanged();
                //cListView.InvalidateViews();

            });
            builder.Create().Show();
        }


        private void CreateDirectoryForPictures()
        {
            cDir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "RadioFXC");
            if (!cDir.Exists())
            {
                cDir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                Activity.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            var _SP = new SP(Values.gDatos, "pGetRepairPictureNumber");
            int _num = 0;
            _SP.AddParameterValue("RepairCode", cRepairCode);
            _SP.AddParameterValue("Num", _num);
            try
            {
                _SP.Execute();
            }
            catch
            {
                throw;
            }
            cNUM = Convert.ToInt32(_SP.ReturnValues()["@Num"]);

            Intent intent = new Intent(MediaStore.ActionImageCapture);

            cFile = new File(cDir, String.Format(cUnitNumber + "_" + cRepairCode + "_{0}.jpg",cNUM));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(cFile));
            this.StartActivityForResult(intent, 4);
        }

        public async override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            //base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            if (cFile.IsFile != false)
            {
                Uri contentUri = Uri.FromFile(cFile);
                mediaScanIntent.SetData(contentUri);
                Activity.SendBroadcast(mediaScanIntent);
                Bitmap bitmap = cFile.Path.LoadAndResizeBitmap(cWidthThumbnail, cWidthThumbnail);
                ImageFile elFile = new ImageFile(cFile, cDir, bitmap, cNUM, Activity);
                
                await cImageAdapter.AddImage(elFile);
                //cImageFileList.Add(elFile);
                cImageAdapter.NotifyDataSetChanged();
                cListView.InvalidateViews();

                // Dispose of the Java side bitmap.
                GC.Collect();
            }
            else
            {
                cFile.Dispose();
            }
        }

        void IScrollDirectorListener.OnScrollDown()
        {
        }

        void IScrollDirectorListener.OnScrollUp()
        {
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
        }
    }
    public class ListImageAdapter : BaseAdapter<ImageFile>
    {
        private ListImageFile cImageFileList;
        private readonly Context context;
        private string cRepairCode;
        private string cUnitNumber;
        private int cImageWidth;
        private int cNUM;
        public ListImageAdapter(Context c, string pRepairCode, string pUnitNumber, int pImageWidth, ListImageFile pImagefileList)
        {
            context = c;
            //cImageFileList = new ListImageFile((Activity)context, pRepairCode, pUnitNumber);
            //cImageFileList.AfterFTP += CImageFileList_AfterFTP;
            cRepairCode = pRepairCode;
            cUnitNumber = pUnitNumber;
            cImageWidth = pImageWidth;
            cImageFileList = pImagefileList;
            cImageFileList.cParentActivity = (Activity)context;
            cImageFileList.cRepairCode = cRepairCode;
            cImageFileList.cUnitNumber = cUnitNumber;

        }

        

        public override int Count
        {
            get { return cImageFileList.Count; }
        }

        public override ImageFile this[int position]
        {
            get
            {
                return cImageFileList[position];
            }
        }


        public override long GetItemId(int position)
        {
            return position;
        }

        public string GetPictureId(int position)
        {
            return cImageFileList[position].IdPicture;
        }

        public async Task AddImage(ImageFile pFile)
        {
            await cImageFileList.Add(pFile);
        }

        public void RemoveImage(string IdPicture)
        {
            cImageFileList.Remove(cImageFileList[IdPicture]);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView _image;
            TextView _text;

            var view = convertView;
            if (convertView == null)
                view = LayoutInflater.From(context).Inflate(Resource.Layout.ListImageProgress, null);
            var _progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            this[position].Progress = _progress;
            _image = view.FindViewById<ImageView>(Resource.Id.ImageP);
            _text = view.FindViewById<TextView>(Resource.Id.Text);
            // if it's not recycled, initialize some attributes
            int width = (context.Resources.DisplayMetrics.WidthPixels - 100) / 3;
            _image.LayoutParameters = new RelativeLayout.LayoutParams(cImageWidth, cImageWidth);
            //_image.SetScaleType(ImageView.ScaleType.CenterCrop);
            //_image.SetPadding(8, 8, 8, 8);
            _image.SetImageBitmap(cImageFileList[position].Bitmap);
            _text.Text = cImageFileList[position].Text;
            this[position].Progress.Progress = cImageFileList[position].intProgress;
            return view;
        }
    }
    public class ListImageFile : List<ImageFile>, IDisposable
    {
        
        public Activity cParentActivity { get; set; }
        public string cRepairCode { get; set; }
        public string cUnitNumber { get; set; }
        public List<ImageFile> pendingItems
        {
            get
            {
                return this.Where(x => x.Uploaded == false).ToList();
            }
        }

        public ImageFile this[string SearchKey]
        {
            get
            {
                // When IdPicture (length 10), search by IdPicture, else search by FileName
                if (SearchKey.Length == 10)
                {
                    return this.FirstOrDefault(x => x.IdPicture != null && x.IdPicture == SearchKey);
                }
                else
                {
                    return this.FirstOrDefault(x => x.File != null && x.File.Name == SearchKey);
                }
                
            }
        }
        public ListImageFile()
        {

        }
        public ListImageFile(Activity parent = null, string pRepairCode = "", string pUnitNumber="")
        {
            cParentActivity = parent;
            cRepairCode = pRepairCode;
            cUnitNumber = pUnitNumber;
        }

        public async new Task Add(ImageFile item)
        {
            //if (null != OnAdd)
            //{
            //    OnAdd(this, null);
            //}
            //base.Add(item);
            Insert(0, item);
            item.Activity = cParentActivity;
            item.cRepairCode = cRepairCode;
            item.cUnitNumber = cUnitNumber;
            if (!item.Uploaded)
            {
                string dateString = DateTime.Today.ToString("yyyyMMdd");
                string directory = Values.gFTPDir + dateString + "/" + cRepairCode + "/";
                item.ServerPath = directory;
                new Thread(new ThreadStart(item.FtpImage)).Start();
                //ThreadPool.QueueUserWorkItem(o => item.FtpImage(directory));
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.ForEach(x => { x.Dispose(); x = null; });
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ListImageFile() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion



        //private async Task LaunchFtpQueue()
        //{
        //    string dateString = DateTime.Today.ToString("yyyyMMdd");
        //    string directory = Values.gFTPDir + dateString + "/" + cExtraInfo + "/";
        //    //create directory for day/repair code
        //    MakeFTPDir(Values.gFTPServer, directory, Values.gFTPUser, Values.gFTPPassword);
        //    isBusySending = true;
        //    bool error = false;
        //    while (this.pendingItems.Count != 0)
        //    {
        //        foreach (ImageFile pendingImage in this.pendingItems)
        //        {
        //            await FtpImage(pendingImage, directory);
        //        }
        //    }
        //}

    }
    public class ImageFile: IDisposable
    {
        public Java.IO.File File { get; set; }
        public Java.IO.File Dir { get; set; }
        public Bitmap Bitmap { get; set; }
        public bool Uploaded { get; set; }
        public string Status { get; set; }
        public string ServerPath { get; set; }
        public string Text { get; set; }
        public string IdPicture { get; set; }
        public int NumPicture { get; set; }
        public ProgressBar Progress;
        public Activity Activity;
        public string cRepairCode;
        public string cUnitNumber;
        public int intProgress { get; set; }
        public ImageFile(Java.IO.File pFile, Java.IO.File pDir, Bitmap pBitmap, int pNum, Activity MainActivity)
        {
            File = pFile;
            Dir = pDir;
            Bitmap = pBitmap;
            Uploaded = false;
            Status = "PENDING";
            Text = pFile.Name;
            NumPicture = pNum;
            intProgress = 0;
            Activity = MainActivity;
        }



        public ImageFile(string pFileName, Bitmap pBitmap)
        {
            Bitmap = pBitmap;
            Uploaded = true;
            Status = "OLD";
            Text = pFileName;
            intProgress = 100;
        }
        public async void FtpImage()
        {
            
            //create directory for day/repair code
            MakeFTPDir(Values.gFTPServer, ServerPath, Values.gFTPUser, Values.gFTPPassword);
            //create request for image uploading
            string uri = "ftp://" + Values.gFTPServer + ServerPath + File.Name;
            // Create FtpWebRequest object from the Uri provided
            FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new System.Uri(uri));
            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(Values.gFTPUser, Values.gFTPPassword);
            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;
            reqFTP.UsePassive = true;
            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // Specify the data transfer type.
            reqFTP.UseBinary = true;
            // Notify the server about the size of the uploaded file

            long FileLenght = File.Length();
            reqFTP.ContentLength = FileLenght;
            //string transmissions = (this.Count - pendingItems.Count).ToString() + "/" + this.Count.ToString();
            //int pending_old = pendingItems.Count;
            //cParentActivity.RunOnUiThread(() => cTextView.Text = "Upload " + transmissions + ": " + pendingImage.File.Name + "...");
            // The buffer size is set to 2kb
            int buffLength = 1024;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileInputStream fs = new FileInputStream(File);
            // Stream to which the file to be upload is written
            try
            {
                System.IO.Stream strm = await reqFTP.GetRequestStreamAsync();
                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);
                // Till Stream content ends
                int totalRead = 0;
                while (contentLen != -1)
                {
                    totalRead += contentLen;
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    //Java.Lang.Thread.Sleep(5);
                    //updates the progress bar
                    intProgress = (int)(100 * totalRead / FileLenght);
                    if (null != Progress)
                        Activity.RunOnUiThread(() => Progress.Progress = intProgress);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
                Uploaded = true;

                string FileName =File.Name;
                byte[] rawBitmap;
                using (var stream = new System.IO.MemoryStream())
                {
                    Bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                    rawBitmap = stream.ToArray();
                }
                Status = "UPLOADED";

                SP pAddPicturesRepairs = new SP(Values.gDatos, "pAddPicturesRepairsNEW");
                pAddPicturesRepairs.AddParameterValue("RepairCode", cRepairCode);
                pAddPicturesRepairs.AddParameterValue("UnitNumber", cUnitNumber);
                pAddPicturesRepairs.AddParameterValue("FileName", FileName);
                pAddPicturesRepairs.AddParameterValue("FilePath", ServerPath);
                pAddPicturesRepairs.AddParameterValue("Thumbnail", rawBitmap);
                pAddPicturesRepairs.AddParameterValue("IdPicture", "");
                pAddPicturesRepairs.AddParameterValue("Num", NumPicture);
                pAddPicturesRepairs.Execute();
                if (pAddPicturesRepairs.LastMsg != "OK")
                {
                    throw (new Exception(pAddPicturesRepairs.LastMsg));
                }

                    //delete the local file once transmitted
                IdPicture = pAddPicturesRepairs.Parameters["@IdPicture"].Value.ToString();
                bool deleted = File.Delete();
                File.Dispose();
                File = null;
                return;
            }
            catch (System.Exception ex)
            {
                Activity.RunOnUiThread(() =>
                {
                    Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
                    AlertDialog alertDialog = builder.Create();
                    alertDialog.SetTitle("ERROR");
                    alertDialog.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    alertDialog.SetMessage("Error: " + ex.Message);
                    alertDialog.SetButton("OK", (s, ev) =>
                    {
                        Activity.Finish();
                    });
                    alertDialog.Show();
                });
                return;
            }



    }
        public static void MakeFTPDir(string ftpAddress, string pathToCreate, string login, string password)
        {
            FtpWebRequest reqFTP = null;
            System.IO.Stream ftpStream = null;
            string[] subDirs = pathToCreate.Split('/');

            string currentDir = string.Format("ftp://{0}", ftpAddress);

            foreach (string subDir in subDirs)
            {
                try
                {
                    currentDir = currentDir + "/" + subDir;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(login, password);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    ftpStream.Close();
                    response.Close();
                }
                catch 
                {
                    //directory already exist I know that is weak but there is no way to check if a folder exist on ftp...
                }
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                Bitmap.Dispose();
                Bitmap = null;
                File.Dispose();
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ImageFile() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}