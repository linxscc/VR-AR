//
//  TAppDelegate.m
//  Unity-iPhone
//  Author:夕阳如风
//  Created by lacost on 10/11/17.


#import "TAppDelegate.h"

@interface TAppDelegate ()

@end

static TAppDelegate *instance = nil;
@implementation TAppDelegate
+(TAppDelegate *)sharedInstance{
    @synchronized(self) {
        if(instance == nil) {
            instance = [[[self class] alloc] init];
        }
    }
    return instance;
}
@end

