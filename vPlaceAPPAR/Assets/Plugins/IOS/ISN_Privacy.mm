////////////////////////////////////////////////////////////////////////////////
//
// @module IOS Native Plugin
// @author Osipov Stanislav (Stan's Assets)
// @support support@stansassets.com
// @website https://stansassets.com
//
////////////////////////////////////////////////////////////////////////////////


#import "ISN_NativeCore.h"

#import <Foundation/Foundation.h>
#import <Photos/Photos.h>
#import <Contacts/Contacts.h>
#import <EventKit/EventKit.h>
#import <CoreLocation/CoreLocation.h>

NSString * const UNITY_SPLITTER = @"|";
NSString * const UNITY_SPLITTER2 = @"|%|";
NSString * const UNITY_EOF = @"endofline";

typedef NS_ENUM(NSInteger, PermissionStatus)
{
    PermissionStatusNotDetermined = 0, //Explicit user permission is required for photo library access, but the user has not yet granted or denied such permission.
    PermissionStatusRestricted, //Your app is not authorized to access the library / service, and the user cannot grant such permission.
    PermissionStatusDenied, //The user has explicitly denied your app access to the library / service.
    PermissionStatusAuthorized //The user has explicitly granted your app access to the library / service.
};

@interface PermissionsManager : NSObject<CLLocationManagerDelegate>
+ (PermissionsManager *)sharedInstance;
- (PermissionStatus)checkPermissionsFor:(NSString *)service;
- (void)requestPermissionsFor:(NSString *)service;
@property NSString* subscribedForService;
@property CLLocationManager *locationManager;
@end


@implementation PermissionsManager

+ (PermissionsManager *)sharedInstance
{
    // structure used to test whether the block has completed or not


    // initialize sharedObject as nil (first call only)
    __strong static PermissionsManager *_sharedObject = nil;

    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _sharedObject = [[self alloc]init];
    });


    // returns the same object each time
    return _sharedObject;
}

- (PermissionStatus)checkPermissionsFor:(NSString *)service {

    if ([service isEqualToString:@"NSCalendars"]) {
        EKAuthorizationStatus authStatus = [EKEventStore authorizationStatusForEntityType:EKEntityTypeEvent];
        switch (authStatus) {
            case EKAuthorizationStatusAuthorized:
                return PermissionStatusAuthorized;
                break;
            case EKAuthorizationStatusDenied:
                return PermissionStatusDenied;
                break;
            case EKAuthorizationStatusRestricted:
                return PermissionStatusRestricted;
                break;
            default:
                return PermissionStatusNotDetermined;
                break;
        }
    } else if ([service isEqualToString:@"NSReminders"]) {
        EKAuthorizationStatus authStatus = [EKEventStore authorizationStatusForEntityType:EKEntityTypeReminder];
        switch (authStatus) {
            case EKAuthorizationStatusAuthorized:
                return PermissionStatusAuthorized;
                break;
            case EKAuthorizationStatusDenied:
                return PermissionStatusDenied;
                break;
            case EKAuthorizationStatusRestricted:
                return PermissionStatusRestricted;
                break;
            default:
                return PermissionStatusNotDetermined;
                break;
        }
    } else if ([service isEqualToString:@"NSCamera"]) {
        AVAuthorizationStatus authStatus = [AVCaptureDevice authorizationStatusForMediaType:AVMediaTypeVideo];

        switch (authStatus) {
            case AVAuthorizationStatusAuthorized:
                return PermissionStatusAuthorized;
                break;
            case AVAuthorizationStatusDenied:
                return PermissionStatusDenied;
                break;
            case AVAuthorizationStatusRestricted:
                return PermissionStatusRestricted;
                break;
            default:
                return PermissionStatusNotDetermined;
                break;
        }

    } else if ([service isEqualToString:@"NSMicrophone"]) {

        AVAuthorizationStatus authStatus = [AVCaptureDevice authorizationStatusForMediaType:AVMediaTypeAudio];

        switch (authStatus) {
            case AVAuthorizationStatusAuthorized:
                return PermissionStatusAuthorized;
                break;
            case AVAuthorizationStatusDenied:
                return PermissionStatusDenied;
                break;
            case AVAuthorizationStatusRestricted:
                return PermissionStatusRestricted;
                break;
            default:
                return PermissionStatusNotDetermined;
                break;
        }

    } else if ([service isEqualToString:@"NSPhotoLibrary"]) {
        PHAuthorizationStatus authStatus = [PHPhotoLibrary authorizationStatus];

        switch (authStatus) {
            case PHAuthorizationStatusAuthorized:
                return PermissionStatusAuthorized;
                break;
            case PHAuthorizationStatusDenied:
                return PermissionStatusDenied;
                break;
            case PHAuthorizationStatusRestricted:
                return PermissionStatusRestricted;
                break;
            default:
                return PermissionStatusNotDetermined;
                break;
        }
    } else if ([service isEqualToString:@"NSContacts"]) {
        CNAuthorizationStatus authStatus = [CNContactStore authorizationStatusForEntityType:CNEntityTypeContacts];

        switch (authStatus) {
            case CNAuthorizationStatusAuthorized:
                return PermissionStatusAuthorized;
                break;
            case CNAuthorizationStatusDenied:
                return PermissionStatusDenied;
                break;
            case CNAuthorizationStatusRestricted:
                return PermissionStatusRestricted;
                break;
            default:
                return PermissionStatusNotDetermined;
                break;
        }
    } else if ([service isEqualToString:@"NSLocationAlways"]) {
        if ([CLLocationManager locationServicesEnabled]) {
            CLAuthorizationStatus authStatus = [CLLocationManager authorizationStatus];

            switch (authStatus) {
                case kCLAuthorizationStatusAuthorizedAlways:
                    return PermissionStatusAuthorized;
                    break;
                case kCLAuthorizationStatusDenied:
                    return PermissionStatusDenied;
                    break;
                case kCLAuthorizationStatusRestricted:
                    return PermissionStatusRestricted;
                    break;
                default:
                    return PermissionStatusNotDetermined;
                    break;
            }
        } else {
            return PermissionStatusNotDetermined;
        }
    } else if ([service isEqualToString:@"NSLocationWhenInUse"]) {
        if ([CLLocationManager locationServicesEnabled]) {
            CLAuthorizationStatus authStatus = [CLLocationManager authorizationStatus];

            switch (authStatus) {
                case kCLAuthorizationStatusAuthorizedWhenInUse:
                    return PermissionStatusAuthorized;
                    break;
                case kCLAuthorizationStatusDenied:
                    return PermissionStatusDenied;
                    break;
                case kCLAuthorizationStatusRestricted:
                    return PermissionStatusRestricted;
                    break;
                default:
                    return PermissionStatusNotDetermined;
                    break;
            }
        } else {
            return PermissionStatusNotDetermined;
        }
    } else {
        return PermissionStatusNotDetermined;
    }

}

- (void) requestPermissionsFor:(NSString *)service {

    if ([service isEqualToString:@"NSCalendars"]) {
        EKEventStore *eventStore = [[EKEventStore alloc]init];
        [eventStore requestAccessToEntityType:EKEntityTypeEvent completion:^(BOOL granted, NSError * _Nullable error) {
            if (error == nil) {
                if (granted) {
                    [self sendResponseFor:service withStatus:PermissionStatusAuthorized];
                } else {
                    [self sendResponseFor:service withStatus:PermissionStatusDenied];
                }
            } else {
                [self sendResponseFor:service withStatus:PermissionStatusNotDetermined];
            }
        }];

    } else if ([service isEqualToString:@"NSReminders"]) {
        EKEventStore *eventStore = [[EKEventStore alloc]init];
        [eventStore requestAccessToEntityType:EKEntityTypeReminder completion:^(BOOL granted, NSError * _Nullable error) {
            if (error == nil) {
                if (granted) {
                    [self sendResponseFor:service withStatus:PermissionStatusAuthorized];
                } else {
                    [self sendResponseFor:service withStatus:PermissionStatusDenied];
                }
            } else {
                [self sendResponseFor:service withStatus:PermissionStatusNotDetermined];
            }
        }];

    } else if ([service isEqualToString:@"NSCamera"]) {
        [AVCaptureDevice requestAccessForMediaType:AVMediaTypeVideo completionHandler:^(BOOL granted) {
            if (granted) {
                [self sendResponseFor:service withStatus:PermissionStatusAuthorized];
            } else {
                [self sendResponseFor:service withStatus:PermissionStatusDenied];
            }
        }];
    } else if ([service isEqualToString:@"NSMicrophone"]) {
        [AVCaptureDevice requestAccessForMediaType:AVMediaTypeAudio completionHandler:^(BOOL granted) {
            if (granted) {
                [self sendResponseFor:service withStatus:PermissionStatusAuthorized];
            } else {
                [self sendResponseFor:service withStatus:PermissionStatusDenied];
            }
        }];
    } else if ([service isEqualToString:@"NSPhotoLibrary"]) {
        [PHPhotoLibrary requestAuthorization:^(PHAuthorizationStatus status) {
            switch (status) {
                case PHAuthorizationStatusAuthorized:
                    [self sendResponseFor:service withStatus:PermissionStatusAuthorized];
                    break;
                case PHAuthorizationStatusDenied:
                    [self sendResponseFor:service withStatus:PermissionStatusDenied];
                    break;
                case PHAuthorizationStatusRestricted:
                    [self sendResponseFor:service withStatus:PermissionStatusRestricted];
                    break;
                default:
                    [self sendResponseFor:service withStatus:PermissionStatusNotDetermined];
                    break;
            }
        }];

    } else if ([service isEqualToString:@"NSContacts"]) {
        CNContactStore *contactStore = [[CNContactStore alloc]init];
        [contactStore requestAccessForEntityType:CNEntityTypeContacts completionHandler:^(BOOL granted, NSError * _Nullable error) {
            if (error == nil) {
                if (granted) {
                    [self sendResponseFor:service withStatus:PermissionStatusAuthorized];
                } else {
                    [self sendResponseFor:service withStatus:PermissionStatusDenied];
                }
            } else {
                [self sendResponseFor:service withStatus:PermissionStatusNotDetermined];
            }
        }];
    } else if ([service isEqualToString:@"NSLocationAlways"]) {
        if ([CLLocationManager locationServicesEnabled]) {
            if (_locationManager == nil) {
                _locationManager = [[CLLocationManager alloc] init];
            }

            _locationManager.delegate = self;
            _subscribedForService = service;
            [_locationManager requestAlwaysAuthorization];
        } else {
            [self sendResponseFor:service withStatus:PermissionStatusNotDetermined];
        }

    } else if ([service isEqualToString:@"NSLocationWhenInUse"]) {
        if ([CLLocationManager locationServicesEnabled]) {
            if (_locationManager == nil) {
                _locationManager = [[CLLocationManager alloc] init];
            }
            _locationManager.delegate = self;
            _subscribedForService = service;
            [_locationManager requestWhenInUseAuthorization];

        } else {
            [self sendResponseFor:service withStatus:PermissionStatusNotDetermined];
        }
    } else {

    }
}

- (void)sendResponseFor:(NSString*)service withStatus:(PermissionStatus)status {

    NSString *responseData = [NSString stringWithFormat:@"%@%@%d%@%@", service, UNITY_SPLITTER2, (int)status, UNITY_SPLITTER2, UNITY_EOF];

    UnitySendMessage("SA.IOSNative.Privacy.NativeReceiver", "PermissionRequestResponseReceived", [ISN_DataConvertor NSStringToChar:responseData]);
}

- (void)locationManager:(CLLocationManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status {
    switch (status) {
        case kCLAuthorizationStatusAuthorizedWhenInUse:
            [self sendResponseFor:_subscribedForService withStatus:PermissionStatusAuthorized];
            break;
        case kCLAuthorizationStatusAuthorizedAlways:
            [self sendResponseFor:_subscribedForService withStatus:PermissionStatusAuthorized];
            break;
        case kCLAuthorizationStatusDenied:
            [self sendResponseFor:_subscribedForService withStatus:PermissionStatusDenied];
            break;
        case kCLAuthorizationStatusRestricted:
            [self sendResponseFor:_subscribedForService withStatus:PermissionStatusRestricted];
            break;
        default:
            [self sendResponseFor:_subscribedForService withStatus:PermissionStatusNotDetermined];
            break;
    }
}

@end

extern "C" {

    int _ISN_CheckPermissions(char* descriptionKey) {

        NSString *usageString = [ISN_DataConvertor charToNSString:descriptionKey];
        return [[PermissionsManager sharedInstance] checkPermissionsFor:usageString];
    }

    void _ISN_RequestPermissions(char* descriptionKey) {

        NSString *usageString = [ISN_DataConvertor charToNSString:descriptionKey];
        [[PermissionsManager sharedInstance] requestPermissionsFor:usageString];
    }
    
}
