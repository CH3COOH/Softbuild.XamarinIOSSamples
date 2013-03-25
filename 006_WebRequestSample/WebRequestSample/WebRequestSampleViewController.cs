using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.IO;

namespace WebRequestSample
{
	public partial class WebRequestSampleViewController : UIViewController
	{
		public WebRequestSampleViewController () : base ("WebRequestSampleViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			button.TouchUpInside += DownloadUsingNSUrlRequest;
		}

		public void DidDownloaded (object sender, EventArgs e)
		{
			var customDelegate = sender as CustomDelegate;
			label.Text = customDelegate.GetResultText();
		}

		void DownloadUsingNSUrlRequest (object sender, EventArgs e)
		{
			var downloadedDelegate = new CustomDelegate(this);

			var req = new NSUrlRequest(new NSUrl("http://ch3cooh.hatenablog.jp/"));
			NSUrlConnection connection = new NSUrlConnection(req, downloadedDelegate);
			connection.Start();
		} 

		void DownloadUsingHttpWebRequest (object sender, EventArgs e)
		{  
 			var text = default(string);

			var req = (HttpWebRequest)HttpWebRequest.Create("http://ch3cooh.hatenablog.jp/");
			using (var res = req.GetResponse())
			using (var strm = res.GetResponseStream())
			using (var reader = new StreamReader(strm))
			{
				text = reader.ReadToEnd();
			}

			label.Text = text;
		} 

		void DownloadUisnHttpClient (object sender, EventArgs e)
		{
			WebClient client = new WebClient();
			client.DownloadStringCompleted += (sx, ex) => {
				this.BeginInvokeOnMainThread(() => {
					label.Text = ex.Result;
				});
			};
			client.DownloadStringAsync(new Uri("http://ch3cooh.hatenablog.jp/"));
		}
		 
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}

	public class CustomDelegate : NSUrlConnectionDelegate
	{
		public byte[] ResponseData { get; set; }
		private long _receivedDataLength { get; set; }
		private WebRequestSampleViewController _delegate;

		public CustomDelegate(WebRequestSampleViewController viewController)
		{
			_delegate = viewController;
		}

		public override void ReceivedResponse (NSUrlConnection connection, NSUrlResponse response)
		{
			long length = response.ExpectedContentLength;
			if (length == -1) {
				length = 1* 1024 * 1024;
			}
			ResponseData = new byte[length];
			_receivedDataLength = 0;
		}

		public override void ReceivedData (NSUrlConnection connection, NSData data)
		{
			System.Runtime.InteropServices.Marshal.Copy(
				data.Bytes, ResponseData, (int)_receivedDataLength, (int)data.Length);
			_receivedDataLength += data.Length;
		}

		public override void FinishedLoading (NSUrlConnection connection)
		{
			_delegate.DidDownloaded(this, new EventArgs());
		}

		public string GetResultText()
		{
			return System.Text.UTF8Encoding.UTF8.GetString(
				ResponseData, 0,  (int)_receivedDataLength);
		}
	}
}

