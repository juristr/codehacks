//
//  main.m
//  Demo
//
//  Created by Juri on 07.03.13.
//  Copyright (c) 2013 Juri. All rights reserved.
//

#import <UIKit/UIKit.h>

#import "AppDelegate.h"

int main(int argc, char *argv[])
{
    //within the region of @autoreleasepool the ARC can work/manage memory
    @autoreleasepool {
        return UIApplicationMain(argc, argv, nil, NSStringFromClass([AppDelegate class]));
    }
}
