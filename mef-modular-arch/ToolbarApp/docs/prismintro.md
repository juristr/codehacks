
# Introduction to Modularity With PRISM

## MEF - Managed Extension Framework

The Managed Extensibility Framework (MEF) is a composition layer for .NET that allows for creating modular applications. This article is going to cover the very basics of developing a application that can be dynamically extended with plugins by using MEF.

From the [MEF homepage](http://mef.codeplex.com/):

> The Managed Extensibility Framework (MEF) is a composition layer for .NET that improves the flexibility, maintainability and testability of large applications. MEF can be used for third-party plugin extensibility, or it can bring the benefits of a loosely-coupled plugin-like architecture to regular applications. <cite><a href="http://mef.codeplex.com">http://mef.codeplex.com</a></cite>

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

What is important to know is that for **importing a list of dependencies** the `ImportMany` attribute has to be used like

    [ImportMany]
    public IEnumerable<IAction> Actions { get; set; }

>  An ordinary ImportAttribute attribute is filled by one and only one ExportAttribute. If more than one is available, the composition engine produces an error. <cite>From the official MSDN docs</cite>

**InheritedExport**  
http://randomactsofcoding.blogspot.it/2010/01/making-part-declarations-easier-with.html

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

## Creating a Module

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

## Links

[mef_nuget]:http://nuget.org/packages/Prism.MEFExtensions/
[expfactory_port]:https://skydrive.live.com/?cid=f8b2fd72406fb218&id=F8B2FD72406FB218!238