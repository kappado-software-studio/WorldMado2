using System;
using System.IO;
using System.Diagnostics;
using System.Threading;



using AppKit;
using Foundation;

namespace WorldMado2
{
    public partial class ViewController : NSViewController
    {
        private string txtTimer;
        private string txtURL;
        private string exFile = "";
        private string nowFile = "";
		private bool ThreadRunning = false;


		public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

		partial void buttonTest(Foundation.NSObject sender)
		{
			txtTimer = textTimer.StringValue;
			txtURL = textURL.StringValue;
			bool Urlstatus = TestURL();
			dispAlert(Urlstatus);
		}

		partial void buttonStart(Foundation.NSObject sender)
		//private void buttonStart_Clicked(object sender, EventArgs e)
		{
			if (ThreadRunning == false)
			{
				txtTimer = textTimer.StringValue;
				txtURL = textURL.StringValue;
				bool Urlstatus = TestURL();
				if (Urlstatus == false)
				{
					dispAlert(Urlstatus);
                }
                else
                {
					ThreadRunning = true;
					//(sender as buttonStart).Text = "You pressed me!";
					StartThread();
					//DLLabel.StringValue = "Now download is";
					//this.Title = "Stop";
				}
			}
            else
            {
				ThreadRunning = false;
				//this.Title = "Start";

				//DLLabel.StringValue = "Download URL:";

			}
		}


		//----------------------------------
		//main Thread
		public void ThreadTask()
		{
			int i_txtTimer = int.Parse(txtTimer);
			int timerval = 1000 * 60 * i_txtTimer;
			DateTime nextUpdate = DateTime.Now.AddMinutes(int.Parse(txtTimer));
			/*
			int swapTiming = timerval / 50 ;
			int waitTimes=0;
			bool Urlstatus;
			while(ThreadRunning == true){
				//Application.DoEvents();//--Not work in MonoMac
				System.Threading.Thread.Sleep (50);
				waitTimes ++ ;
				if( waitTimes < swapTiming ){
					continue;
				}
				Urlstatus = TestURL ();
				if( Urlstatus == true && ThreadRunning == true){
					PictureToWallpaer ();
					waitTimes=0;
				}

			}
			*/
			//int swapTiming = timerval / 50 ;
			int waitTimes = 0;
			bool Urlstatus;
			while (ThreadRunning == true)
			{
				//Application.DoEvents();//--Not work in MonoMac
				System.Threading.Thread.Sleep(1000);
				waitTimes++;
				if (DateTime.Now < nextUpdate)
				{
					continue;
				}
				Urlstatus = TestURL();
				if (Urlstatus == true && ThreadRunning == true)
				{
					PictureToWallpaer();
					waitTimes = 0;
					nextUpdate = DateTime.Now.AddMinutes(int.Parse(txtTimer));


				}

			}



		}
		//Thread Starter
		public void biginThread()
		{
			Thread trd = new Thread(new ThreadStart(this.ThreadTask));
			//trd.IsBackground = true;
			trd.Start();
		}

		//Pre-Thread Stater(Only First Time)
		public void StartThread()
		{
			bool Urlstatus = TestURL();
			if (Urlstatus == true && ThreadRunning == true)
			{
				PictureToWallpaer();
				biginThread();
			}
		}

		//URL TetstResult Display 
		public void dispAlert(bool Urlstatus)
		{
			string alertText;
			if (Urlstatus == true)
			{
				alertText = "URL is OK.";
			}
			else
			{
				alertText = "URL is NG.";
			}
			alertDisplay(alertText);
		}

		//Set Alert Text.
		public void alertDisplay(string alertText)
		{
			var alert = new NSAlert()
			{
				MessageText = alertText,
				InformativeText = ""
			};
			alert.RunModal();

		}

		//Test URL 
		public bool TestURL()
		{
			String myURL = txtURL;
			bool ret = PictureDownload(myURL, "/Applications/WorldMado/now.jpg");
			return ret;
		}

		//Picture Downloading
		public bool PictureDownload(string myURL, string Destfile)
		{
			bool ret = false;
			System.Net.WebClient wc = new System.Net.WebClient();

			try
			{
				nowFile = "";
				DateTime dt = DateTime.Now;
				//string format = @"yyyyMMddhhmmss";
				nowFile = dt.ToString(@"yyyyMMddhhmmss");
				string dl_file = @"/Applications/WorldMado/now_" + nowFile + ".jpg";

				//wc.DownloadFile (myURL, "/Users/yone/Pictures/now.jpg");
				//wc.DownloadFile (myURL, "/Applications/WorldMado/now.jpg");
				wc.DownloadFile(myURL, dl_file);
				//wc.DownloadFile (myURL, Destfile);//Bug of MonoMac??
				ret = true;
				if (File.Exists(exFile))
					File.Delete(exFile);
				exFile = dl_file;
			}
			catch (InvalidOperationException ex)
			{
				ret = false;
			}
			wc.Dispose();
			System.Threading.Thread.Sleep(1);

			return ret;
		}
		public bool PictureToWallpaer()
		{
			bool ret = false;

			//Can not get Current folder by MonoMac	
			string stCurrentDir = System.IO.Directory.GetCurrentDirectory();

			//string cdr = MonoMac.AppKit.NSWorkspace.
			string stc = Environment.CurrentDirectory;


			//string script = @"set /Users/yone/Pictures/now.jpg to POSIX file ""{0}"" as text tell application ""Finder"" set desktop picture to {{/Users/yone/Pictures/now.jpg}} as alias end tell";
			/*
			 string script = @"tell application ""Finder"" set desktop picture to file ""/Users/yone/Pictures/now.jpg"" file end tell";

			NSDictionary error = null;
			NSAppleScript scr = new NSAppleScript(script);
			NSAppleEventDescriptor result = scr.ExecuteAndReturnError(out error);
			string retval = result.StringValue;			
			*/

			/**/
			//string myScript= @"/Users/yone/Desktop/develop/Wallpicture/SetWallPicture.app";
			//string myScript= stCurrentDir +  @"SetWallPicture.app";
			//string myScript=  @"./SetWallPicture.app";
			//string myScript2= @"/Users/yone/Desktop/develop/Wallpicture/all_purge.app";
			string myScript = @"/Applications/WorldMado/SetWallPicture.app";
			string myScript2 = @"/Applications/WorldMado/SetWallPicture3.scpt " + nowFile;


			//string myScript= @"SetWallPicture.app";
			try
			{
				//MonoMac.MonoNativeFunctionWrapperAttribute = "/Users/yone/Pictures/now.jpg" ;
				//MonoMac.MonoNativeFunctionWrapperAttribute.Equals
				//System.
				//MacInterop.AppleScript.Run(string.Format(script, "/Users/yone/Pictures/now.jpg"));
				//Process.Start(myScript);
				Process.Start(@"osascript", myScript2);
				System.Threading.Thread.Sleep(1);
				//Process.Start(myScript2);
				ret = true;
			}
			catch (InvalidOperationException ex)
			{
				ret = false;
			}
			/**/

			return ret;
		}






		//----------------------------------
	}
}
