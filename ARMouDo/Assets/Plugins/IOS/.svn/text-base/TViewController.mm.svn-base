//
//  TViewController.m
//  Unity-iPhone
//  Author:夕阳如风
//  Created by lacost on 10/11/17.



#import "TAppDelegate.h"
#import "TViewController.h"

@interface TViewController ()

@end
@implementation TViewController
- (void)viewDidLoad {
    [super viewDidLoad];
}
- (void)resignWindow
{
    UnitySendMessage("TIOSNative", "IOSButtonClick", "");
      TAppDelegate *app = [TAppDelegate sharedInstance];
     [app.winUp resignKeyWindow];
    app.winUp = nil;
}
 @end
