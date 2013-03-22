// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace LogSample
{
	[Register ("LogSampleViewController")]
	partial class LogSampleViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton buttonLog { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonDebug { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (buttonLog != null) {
				buttonLog.Dispose ();
				buttonLog = null;
			}

			if (buttonDebug != null) {
				buttonDebug.Dispose ();
				buttonDebug = null;
			}
		}
	}
}
