using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace GetVersionSample
{
	public partial class GetVersionSampleViewController : UIViewController
	{
		public GetVersionSampleViewController () : base ("GetVersionSampleViewController", null)
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

			button.TouchUpInside += (sender, e) => {

				// バージョン情報を取得する
				var bundle = NSBundle.MainBundle;
				var version = bundle.ObjectForInfoDictionary("CFBundleVersion").ToString();

				Console.WriteLine("version: {0}", version);
			};
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

