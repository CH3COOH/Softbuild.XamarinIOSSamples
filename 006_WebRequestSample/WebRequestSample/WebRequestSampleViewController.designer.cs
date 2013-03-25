// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace WebRequestSample
{
	[Register ("WebRequestSampleViewController")]
	partial class WebRequestSampleViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton button { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel label { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (button != null) {
				button.Dispose ();
				button = null;
			}

			if (label != null) {
				label.Dispose ();
				label = null;
			}
		}
	}
}
