using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;

namespace LogSample
{
	public partial class LogSampleViewController : UIViewController
	{
		public LogSampleViewController () : base ("LogSampleViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			 
			buttonLog.TouchUpInside += (object sender, EventArgs e) => outputLog1();
			buttonDebug.TouchUpInside += (object sender, EventArgs e) => outputLog2();
		}

		private void outputLog1()
		{
			Console.WriteLine("called outputLog1.");

			var strValue1 = "I'm ch3cooh";
			Console.WriteLine(strValue1);

			var strValue2 = "酢酸だよー "; 
			Console.WriteLine(strValue2);

			var intValue = 393;
			Console.WriteLine(intValue); 

			var floatValue = 393.339;
			Console.WriteLine(floatValue); 
		}

		private void outputLog2()
		{ 
			Console.WriteLine("called outputLog2.");

			var strValue1 = "I'm ch3cooh";
			Debug.WriteLine(strValue1);
			
			var strValue2 = "酢酸だよー "; 
			Debug.WriteLine(strValue2);
			
			var intValue = 393;
			Debug.WriteLine(intValue); 
			
			var floatValue = 393.339;
			Debug.WriteLine(floatValue); 
		} 
	}
} 

