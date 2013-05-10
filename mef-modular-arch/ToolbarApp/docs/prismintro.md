
# Introduction to Modularity With PRISM

## MEF - Managed Extension Framework

The Managed Extensibility Framework (MEF) is a composition layer for .NET that allows for creating modular applications. This article is going to cover the very basics of developing a application that can be dynamically extended with plugins by using MEF.

From the [MEF homepage](http://mef.codeplex.com/):

> The Managed Extensibility Framework (MEF) is a composition layer for .NET that improves the flexibility, maintainability and testability of large applications. MEF can be used for third-party plugin extensibility, or it can bring the benefits of a loosely-coupled plugin-like architecture to regular applications. <cite><a href="http://mef.codeplex.com">http://mef.codeplex.com</a></cite>

### Mef vs Dependency Injection Containers

...


## Basics

### Export and Import
MEF is based on the concept of exports and imports. Speaking in the language of other available dependency injection containers an "export" would correspond to the definition of a dependency while an "import" corresponds to a request of a dependency.

So to define an export, we can write it as follows:

    [Export(typeof(IAction))]
    public class SaveAction : IAction
    {

        public string Name { get; set; }

    }

...and to request such dependency in any other component we use the `[Import]` attribute like

    public class SomeOtherClass
    {

        [Import]
        public IAction MyAction { get; set; }

    }

**Note**, that the import statements property could even be of _private_ visibility. Meaning, we could write

	[Import]
	private IAction MyAction { get; set; }

and it would still work.

What is important to know is that for **importing a list of dependencies** the `ImportMany` attribute has to be used like

    [ImportMany]
    public IEnumerable<IAction> Actions { get; set; }

>  An ordinary ImportAttribute attribute is filled by one and only one ExportAttribute. If more than one is available, the composition engine produces an error. <cite>From the official MSDN docs</cite>

Normally when importing dependencies into an object there are two different kind of them

- mandatory dependencies (without which the object can't live)
- optional dependencies (without which the object can still work and function but it might lack some additional functionality)

It is preferrable to import _mandatory_ dependencies directly into the constructor of the object. This is especially useful for communication purposes as a potential consumer of the object will immediately see the required dependencies he has to inject. **Constructor injection** can be done by using the `[ImportingConstructor]` attribute

	public class MyClass
	{
		private IMyDependency dependency;

        [ImportingConstructor]
        public MyClass(IMyDependency dependency)
        {
            this.dependency = dependency;
        }
    }

When needed, additional metadata can be specified in the following way:

	public class MyClass
	{
		private IMyDependency dependency;

        [ImportingConstructor]
        public MyClass(
             [Import("SpecialDependency")] 
             IMyDependency dependency)
        {
            this.dependency = dependency;
        }
    }

> **Note**, it might happen that you're forced to use property injection in case of bidirectional dependencies. For instance, if class A dependes on B and B depends on A then we cannot import both of them using constructor injection as that would result in circular references.  
>_(beside that, bidirectional references are usually a sign of a design problem)_

**InheritedExport**  
Inherited exports are a way to facilitate the declaration of export statements and to avoid redundancy. So instead of

	[Export("commands", typeof(ICommand))]
	public class Command1 : ICommand
	{
		// Implementation of the Interface
	}
	 
	[Export("commands", typeof(ICommand))]
	public class Command2 : ICommand
	{
	    // Implementation of the Interface
	}

we have the possibility to write

    [InheritedExport("commands",typeof(ICommand))]
    public interface ICommand
    {
      // Interface Declaration
    }
       
    public class Command1 : ICommand
    {
      // Implementation of the Interface
    }

    public class Command2 : ICommand
    {
      // Implementation of Interface
    }
	

Source: [http://randomactsofcoding.blogspot.it/2010/01/making-part-declarations-easier-with.html](http://randomactsofcoding.blogspot.it/2010/01/making-part-declarations-easier-with.html)

### Metadata

Export definitions allow to provide further information which might be needed by the consumer of the export for properly elaborating it. A "weekly typed" way of adding such metadata information is shown in the following piece of code:

	[Export(typeof(IView))]
	[ExportMetadata("Position", Positions.Header)]
	public class MyPluginView : IView
	{
	}

"Weekly typing" is not ideal however, hence, we try to make it strongly typed whenever possible. For doing so we first of all define an interface which defines our metadata's properties

	public interface IViewMetadata
	{
		Positions Position { get; set; } //enumeration
	}

We then create a new attribute, inheriting from `ExportAttribute` and implementing our previously defined `IViewMetadata` interface.

	public class ExportAsViewAttribute : ExportAttribute, IViewMetadata
	{
		public ExportAsViewAttribute 
			: base(typeof(IView)) //will automatically export this as an IView type
		
		public Positions Position { get; set; }
	}

Now we can exchange our weekly typed metadata information of above with the following

	[ExportAsView (Positions = Positions.Header)]
	public class MyPluginView : IView
	{
	}

Associated metadata information can then be imported by a consumer like

	[ImportMany]
    public Lazy<IView,IPluginMetadata>[] Parts { get; set; }

and once the import is satisfied we can access the metadata by simply iterating over the collection

	foreach (var plugin in Parts)
	{
	    var position = plugin.Metadata.Position;
	    
	    if (position.Equals(Positions.Header))
	    {
	        HeaderPanel.Children.Add(plugin.Value.Part);
	    }
	    else
	    {
	        FooterPanel.Children.Add(plugin.Value.Part);
	    }
	}

	
### Catalogs

[Catalogs](http://mef.codeplex.com/wikipage?title=Using%20Catalogs) allow for the dynamic discovery of parts which are basically compontents registered with a `[Export]` tag. There are different kind of catalogs with diverse capabilities:

- AssemblyCatalog - _for discovering exports in a given assembly_
- DirectoryCatalog - _for scanning assemblies in a given directory for exposed exports_
- (many more...)
- AggregateCatalog - _for combining multiple different catalogs_

When an app gets initialized it needs to configure the `CompositionContainer` with the Catalogs it needs. The first step is to create an `AggregateCatalog` which is a special catalog capable of aggregating different kinds of MEF catalogs.

	
	var aggregateCatalog = new AggregateCatalog();


An application may then register assemblies explicitly through an `AssemblyCatalog` like

	aggregateCatalog.Catalogs.add(new AssemblyCatalog(typeof(MyMefPluginClass).Assembly));

or in an even more dynamic way through a directory discovery by using an `DirectoryCatalog`:

	var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "plugins");
	if (!Directory.Exists(pluginsDirectory))
	    Directory.CreateDirectory(pluginsDirectory);
	
	aggregateCatalog.Catalogs.add(new DirectoryCatalog(pluginsDirectory));

If that's not enough, there is also the possibility to register all exports of a single type through a `TypeCatalog`

	aggregateCatalog.Catalogs.add(new TypeCatalog(typeof(MyClass));

Finally, after setting up all catalogs, the `CompositionContainer` can be instantiated with the previously created `AggregateCatalog`:

	var compositionContainer = new CompositionContainer(aggregateCatalog);

In order to resolve the dependencies the `ComposeParts(...)` method needs to be called with the top-level object, that is, the object which holds the entire object graph. Normally in a WinForms application this may be the root shell which holds the `[Import]` statements to other classes, or you might have another top-level class created by yourself, for instance

	public class MyBootstrapper
	{
		[Import]
		public Form1 MyRootForm { get; set; }

		public void Init()
		{
			//initialization of the catalogs...

			var compositionContainer = new CompositionContainer(aggregateCatalog);
			compositionContainer.ComposeParts(this);
		}

	}

The call to `ComposeParts(this)` will instantiate all imports of `this` (i.e. MyBootstrapper) and correspondingly inject all dependencies.

## Open Generics Support

MEF also supports Open Generics. A classical example is the registration of a generic repository class:

	public interface IRepository<TItem> { ... }

and an according implementation adds the corrisponding export

	[Export(typeof(IRepository<>))]
	public class Repository<TItem> : IRepository<TItem> { ... }

Note how the export is defined, it doesn't specify any specific implementation of the generic type `IRepository<TItem>` but instead leaves it open (thus "open generics"). A consumer can then specify the type and get the correct instance injected, like

	public class SomeConsumer
	{
		...
		[Import]
		public IRepository<Order> OrderRepository { get; set; }

		...
	}

## Lifetime

A fact of major importance is to know the lifetime of a certain object. In MEF this is defined by the `PartCreationPolicy` attribute which defines the "shareability" of a given part. It distinguishes

- **Shared** - the part author is telling MEF that at most one instance of the part may exist per container (aka Singleton scope)
- **NonShared** - the part author is telling MEF that each request for exports of the part will be served by a new instance of it.
- **Any** - the part author allows the part to be used as either "Shared" or "NonShared".

By default each part is created as a singleton, basically in "Shared" mode.

For example

	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class MySingletonClass { ... }


A non-singleton class instead would look like

	[PartCreationPolicy(CreationPolicy.NonShared)]
	[Export]
	public class MyNonSingletonClass { ... }

On the other side, a consumer can define constraints on its imports using the `RequiredCreationPolicy` attribute.

	[Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
	public SomeClass MyImportedClass { get; set; }

By default the `RequiredCreationPolicy` is set to `Any` s.t. shared and non-shared parts are accepted. The following table highlights the different possible scenarios of using the `RequiredCreationPolicy`:

<table>
	<thead>
		<th>-</th>
		<th>Export with CreationPolicy.Any</th>
		<th>Export with CreationPolicy.Shared</th>
		<th>Export with CreationPolicy.NonShared</th>
	</thead>
	<tbody>
		<tr>
			<td><strong>Import with CreationPolicy.Any</strong></td>
			<td>Shared</td>
			<td>Shared</td>
			<td>Non Shared</td>
		</tr>
		<tr>
			<td><strong>Import with CreationPolicy.Shared</strong></td>
			<td>Shared</td>
			<td>Shared</td>
			<td><i>No Match</i></td>
		</tr>
		<tr>
			<td><strong>Import with CreationPolicy.NonShared</strong></td>
			<td>Non Shared</td>
			<td><i>No Match</i></td>
			<td>Non Shared</td>
		</tr>
	</tbody>
</table>

More details can be found [here](http://mef.codeplex.com/wikipage?title=Parts%20Lifetime).


## ExportFactory

Often there is the need to explicitly control the lifecycle of a given object and in case to re-initialize it or get a new instance. For this purpose MEF has the `ExportFactory`.

> Note, this is included natively only starting from .Net 4.5. In the case you cannot use that, you need to rely on a port by Glenn Block (former MS Project Manager for PRISM) which can be found [here][expfactory_port].

For the export factory to work it is necessary to hook it onto the `CompositionContainer`

	var exportFactoryProvider = new ExportFactoryProvider();
	var container = new CompositionContainer(this.AggregateCatalog, exportFactoryProvider);
	exportFactoryProvider.SourceProvider = container;
	
	return container;	

Then assume you have the following dummy class with the following import

	public class OrderCommand
	{
		[Import]
		public IOrderRepository OrderRepository { get; set; }

		public void Run() 
		{
			OrderRepository.PlaceOrder(someOrder);
		}
	}

Assume for instance that `IOrderRepository` might get disposed at some time in the lifecycle for performance reasons and as such when invoking the `OrderCommand` we would have the need to retrieve a new instance of it. Using `ExportFactory` would enable such fine-grained control. Wrapping it up is relatively easy:

	public class OrderCommand
	{
		private IOrderRepository OrderRepository { get; set; }

		[Import]
		public ExportFactory<IOrderRepository> OrderRepositoryFactory { get; set; }

		public void Run() 
		{
			if(OrderRepository.IsDisposed)
			{
				OrderRepository = OrderRepositoryFactory.CreateExport().Value;
			}

			OrderRepository.PlaceOrder(someOrder);
		}
	}


## Bootstrapping an Application

In a modular application there is always a so-called "host" which is the one that governs the loading of the modules and orchestrates between them. As such, a host has a **bootstrapper** which defines the application's dependencies.

Lets add a Bootstrapper.cs and inherit from the `SimpleMefBootstrapper` class

	class Bootstrapper : SimpleMefBootstrapper
	{
		

	}

## Modules

### Creating a Module

Create a new Visual Studio Class Library project and name it accordingly. Add the necessary dependencies.

- System.ComponentModel.Composition (.Net Framework dependency)
- Prism.MEFExtensions (through [NuGet][mef_nuget])

The next step is to create the module loader

	
	public class MyFirstMefPluginModule : IModule
	{
	
	    public MyFirstMefPluginModule()
	    {
	
	    }
	
	    public void Initialize()
	    {
	            
	    }
	}

The important part here is to mark the module with the `ModuleExport` attribute. Now go to your host project and add a reference to the newly created plugin. Moreover you need to register it in the host's bootstrapper:

	class Bootstrapper : SimpleMefBootstrapper
	{

		protected override void ConfigureAggregateCatalog()
		{
			base.ConfigureAggregateCatalog();

			...
			this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MyFirstMefPluginModule).Assembly));
			...
		}

	}	


This should load up your module. Try to verify it by placing a breakpoint in your module's `Initialize()` method.

### Dependencies and On Demand Loading

Another important functionality is to specify **on-demand loading** of a module. Similar to specifying the dependencies, this can be done like

	[ModuleExport(typeof(ModuleC), InitializationMode = InitializationMode.OnDemand)]
	public class ModuleC : IModule { ... }

For **specifying dependencies** among modules we can just use the corresponding metadata in the `ModuleExport` attribute

	[ModuleExport(typeof(ModuleA), DependsOnModuleNames = new string[] { "ModuleD" })]
	public class ModuleA : IModule { ... }

In this way we specify that `ModuleA` is depends on `ModuleD` and as such when `ModuleA` is requested to load up, `ModuleD` will be instantiated and initialized as well.

> **Note**, we cannot have dependencies to a module with `InitializationMode.OnDemand`. You'll get a nice error message:  
> _Module NavigationTreePluginModule is marked for automatic initialization when the application starts, but it depends on modules that are marked as OnDemand initialization. To fix this error, mark the dependency modules for InitializationMode=WhenAvailable, or remove this validation by extending the ModuleCatalog class._


### Explicitly Load a Module

Can be achieved by getting a dependency to `IModuleManager` and then by calling

	moduleManager.LoadModule("ModuleA");

## Loosely Coupled Communication

Prism allows for loosely coupled communication between different parts of your application by using a publisher/subscriber pattern which is realized by the `IEventAggregator` interface.


### Defining the Event Type

First of all we need to define an event type which is shared among all interested subscribers. Each event inherits from the class `Prism4Winforms.Prism.Events.CompositeEvent<TPayload>` where Payload defines the object this event transports/broadcasts. Note, this is a custom class porting the functionality of the original Prism `CompositePresentationEvent<TPayload>` to WinForms.

	public class StringEvent : CompositeEvent<string>
	{

	}

### Publishing

To publish the previously defined event, we need to get a dependency of the `IEventAggregator` and invoke its `Publish(...)` method

	[Import]
	public IEventAggregator EventAggregator { get; set; }

    private void buttonOk_Click(object sender, EventArgs e)
    {
		...
        EventAggregator.GetEvent<StringEvent>().Publish("Some string text");
		...
    }

> **Note**, the registration of the concrete implementation of the `IEventAggregator` is automatically done by the `SimpleMefBootstrapper` in the `RegisterDefaultTypesIfMissing()` method.	

### Subscribing

To subscribe to a given event we again have to get a dependency to `IEventAggregator` and use its `Subscribe(...)` method.

	eventAggregator.GetEvent<StringEvent>().Subscribe((msg) => MessageBox.Show(msg));

Note that the subscribe method takes an `Action<TPayload>` delegate which is used to elaborate the received payload object defined by the event type (in our case a simple string).

The Subscribe method takes a couple of optional parameters which allow to customize the subscription

- `Action<TPayload>` - the delegate that gets invoked when the event gets published
- `ThreadOption` - enumeration that specifies on which thread to execute the subscriber will be called. Options include
	- 	`PublisherThread` - The call is done on the same thread on which the event was published.
	- 	`SubscriberAffinityThread` - The call is done on the thread the subscriber subscribes from, if a SynchronizationContext is present indicating thread affinity. Otherwise it fires on  the publisher's thread.
	- 	`BackgroundThread` - The call is done asynchronously on a background thread.
- 	`keepSubscribersAlive` - boolean indicating on whether to keep all subscribers alive s.t. they are not garbage collected. If `false` it keeps a [WeakReference](http://msdn.microsoft.com/en-us/library/system.weakreference.aspx) to the subscribers.
- 	`Predicate<TPayload>` - a filter lambda expression which allows to filter the events to subscribe to.

Applying these params looks as follows

	eventAggregator.GetEvent<StringEvent>().Subscribe(
	    (msg) => MessageBox.Show(msg),
	    ThreadOption.PublisherThread,
	    true,
	    (x) => x.ToUpper().Contains("HI"));

In such case, we would execute the subscription delegate on the publisher thread, we would keep all subscribers alive and the subscription would only fire if the string contains "hi".

## Links

[mef_nuget]:http://nuget.org/packages/Prism.MEFExtensions/
[expfactory_port]:https://skydrive.live.com/?cid=f8b2fd72406fb218&id=F8B2FD72406FB218!238
[inheritedexports]:http://randomactsofcoding.blogspot.it/2010/01/making-part-declarations-easier-with.html