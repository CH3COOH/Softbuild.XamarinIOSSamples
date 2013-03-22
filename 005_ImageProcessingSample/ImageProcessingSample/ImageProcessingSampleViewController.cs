using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ImageProcessingSample
{
	public partial class ImageProcessingSampleViewController : UIViewController
	{
		public ImageProcessingSampleViewController () : base ("ImageProcessingSampleViewController", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			UIImage grayImage = GrayScale(imageView.Image);
			imageView.Image = grayImage;
		}

		/// <summary>
		/// Graies the scale.
		/// </summary>
		/// <returns>The scale.</returns>
		/// <param name="srcImage">Source image.</param>
		UIImage GrayScale (UIImage srcImage)
		{
			var cgImage = srcImage.CGImage;

			int width = cgImage.Width;
			int height = cgImage.Height;
			int bitsPerComponent = cgImage.BitsPerComponent;
			int bitsPerPixel = cgImage.BitsPerPixel;
			int bytesPerRow = cgImage.BytesPerRow;
			var colorSpace = cgImage.ColorSpace;
			var bitmapInfo = cgImage.BitmapInfo;
			var shouldInterpolate = cgImage.ShouldInterpolate;
			var intent = cgImage.RenderingIntent; 

			// データプロバイダを取得する
			byte[] bytes;
			using (var dataProvider = cgImage.DataProvider)
			using (var data = dataProvider.CopyData())
			{
				// ビットマップデータを取得する
				var butter = data.Bytes;
				bytes = new byte[data.Length];
				System.Runtime.InteropServices.Marshal.Copy(
					butter, bytes, 0, bytes.Length);
			}

			// グレースケール処理をおこなう
			int pixelCount = width * height;
			for (int i = 0; i < pixelCount; i++)
			{
				var index = i * 4;
				
				// 単純平均法で輝度を求める
				var sum = bytes[index + 0] + bytes[index + 1] + bytes[index + 2];
				var y = (double)sum / 3;
				
				bytes[index + 0] = (byte)Math.Min(255, Math.Max(0, y));
				bytes[index + 1] = (byte)Math.Min(255, Math.Max(0, y));
				bytes[index + 2] = (byte)Math.Min(255, Math.Max(0, y));
				bytes[index + 3] = bytes[index + 3];
			}

			// 画像処理後のbyte配列を元にデータプロバイダーを作成する
			var effectedDataProvider
				= new MonoTouch.CoreGraphics.CGDataProvider(bytes, 0, bytes.Length);

			// データプロバイダーからCGImageを作成し、CGImageからUIImageを作成する
			var effectedCgImage = new MonoTouch.CoreGraphics.CGImage(
				width, height, 
				bitsPerComponent, bitsPerPixel, bytesPerRow, 
				colorSpace, bitmapInfo, effectedDataProvider, 
				null, shouldInterpolate, intent);
			return new UIImage(effectedCgImage);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

