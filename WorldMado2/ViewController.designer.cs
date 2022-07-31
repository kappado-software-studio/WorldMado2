// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WorldMado2
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField textTimer { get; set; }

		[Outlet]
		AppKit.NSTextField textURL { get; set; }


		[Action ("buttonStart:")]
		partial void buttonStart (Foundation.NSObject sender);

		[Action ("buttonTest:")]
		partial void buttonTest (Foundation.NSObject sender);

		
		void ReleaseDesignerOutlets ()
		{
			if (textTimer != null) {
				textTimer.Dispose ();
				textTimer = null;
			}

			if (textURL != null) {
				textURL.Dispose ();
				textURL = null;
			}
		}
	}
}
