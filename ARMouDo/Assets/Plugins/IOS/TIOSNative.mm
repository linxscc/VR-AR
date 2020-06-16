//
//  TIOSNative.m
//  Unity-iPhone
//  Author:夕阳如风
//  Created by lacost on 10/11/17.


#import "TAppDelegate.h"
#import "TIOSNative.h"

@interface TIOSNative ()
@end
@implementation TIOSNative :NSObject
void FooPluginFunction(){
    TAppDelegate *app = [TAppDelegate sharedInstance];
   if(app.winUp==nil){
    app.winUp = [[UIWindow alloc]initWithFrame:CGRectMake(172, 621, 70, 70)];
  //  app.winUp.windowLevel = UIWindowLevelAlert+1;
    app.winUp.windowLevel = UIWindowLevelNormal;
    app.winUp.backgroundColor = [UIColor whiteColor];
  //  app.winUp.alpha = 0.5;
    app.winUp.hidden = NO;
    app.winUp.layer.cornerRadius = 35;
    app.winUp.layer.masksToBounds = YES;
    TViewController *fooOCVC= [[TViewController alloc] initWithNibName:nil bundle:nil];
    fooOCVC.view.userInteractionEnabled = YES;
    [app.winUp addSubview:fooOCVC.view];
    [UnityGetGLViewController() addChildViewController: fooOCVC];   
    UIButton*_button = [UIButton buttonWithType:UIButtonTypeCustom];
    //  [_button setTitle:@"click" forState:UIControlStateNormal];
    _button.frame = CGRectMake(0, 0, 70, 70);
    UIImage *img = [UIImage imageNamed:@"recording.png"];
    [_button setBackgroundImage:img forState:UIControlStateNormal];    
    [_button addTarget:fooOCVC action:@selector(resignWindow) forControlEvents:UIControlEventTouchUpInside];
    [fooOCVC.view addSubview:_button];
    [ app.winUp makeKeyAndVisible];
   }
}
@end


