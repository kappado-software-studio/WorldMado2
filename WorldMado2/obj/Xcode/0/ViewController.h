// WARNING
// This file has been generated automatically by Visual Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <AppKit/AppKit.h>


@interface ViewController : NSViewController {
	NSTextField *_textTimer;
	NSTextField *_textURL;
}

@property (nonatomic, retain) IBOutlet NSTextField *textTimer;

@property (nonatomic, retain) IBOutlet NSTextField *textURL;

- (IBAction)buttonStart:(id)sender;

- (IBAction)buttonTest:(id)sender;

@end
