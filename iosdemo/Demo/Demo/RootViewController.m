//
//  RootViewController.m
//  Demo
//
//  Created by Juri on 07.03.13.
//  Copyright (c) 2013 Juri. All rights reserved.
//

#import "RootViewController.h"

//class extension; definitions here are not visible "outside", basically internal class visiblity
@interface RootViewController () <UITableViewDataSource, UITableViewDelegate> //these are the protocol definitions, for fetching data and for allowing to click on the list entry

@property(nonatomic, strong) UITableView *_myTableView;
@property(nonatomic, strong) NSArray *_myArray;

@end

@implementation RootViewController


// "nib" files are for the interface builder; otherwise this would be deleted
/*
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
  return self;
}
*/

- (id)init{
    self = [super init];
    if(self){
        //custom init -> only non-UI components
        self.title = @"Demo";
        NSLog(@"Root controller loaded");
        
        self._myArray = @[@"Test", @"1", @"bla"];
    }
    
    return self;
}

// is responsible for doing the view setup; is called after the base view is being loaded
- (void)viewDidLoad
{
    [super viewDidLoad];

    //CG stands for CoreGraphics -> see under Frameworks
    //The 0, 0, 320, 416 are the hardcoded width of a standard iPhone; in real world these should be read out dynamically
    self._myTableView = [[UITableView alloc] initWithFrame:CGRectMake(0, 0, 320, 416) style:UITableViewStylePlain];

    //register this controller as a datasource for the table view; note that we implemented the according protocol "UITableViewDataSource"
    self._myTableView.dataSource = self;
    self._myTableView.delegate = self;
    [self.view addSubview:self._myTableView];
}

#pragma mark - Table View Data Source

-(NSInteger)numberOfSectionsInTableView:(UITableView *)tableView{
    return 1; //the number of sections of the table; in this example here we just want a single one
}

-(NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    return [self._myArray count]; //return the number of elements of the array, namely x cells;
}

//this function is called for each entry in associated datasource
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    static NSString *MyIdentifier = @"MyIdentifier";
    
    //try to reuse the cell; this is for performance reasons to only have as much cell objects in
    //memory as currently displayed on the screen
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:MyIdentifier];
    
    //the cell is not nil if we got a reused one
    if(cell == nil){
        //only create cells if necessary, otherwise they get reused (performance enhancements)
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:MyIdentifier];
        cell.accessoryType = UITableViewCellAccessoryDisclosureIndicator; //displays an arrow at the right side
    }
    
    //update the text label
    cell.textLabel.text = [self._myArray objectAtIndex:indexPath.row]; //the indexPath.row is the current index (invoked for each element of the datasource)
    
    return cell;
}

#pragma mark - Table View Delegate

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    RootViewController *rvc = [[RootViewController alloc] init];
    [self.navigationController pushViewController:rvc animated:YES];
}



- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
