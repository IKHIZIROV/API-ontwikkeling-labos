//Create a new DI container
using Ninject;
using VoorbeeldDiSamurai;

var kernel = new StandardKernel();
//Tell the container to resolve an instance of the Sword class when it's asked for an IWeapon
kernel.Bind<IWeapon>().To<Sword>();
kernel.Bind<IWeapon>().To<Dagger>();
kernel.Bind<IWeapon>().To<Gun>();
kernel.Bind<ITrigger>().To<AutomaticTrigger>();

//Ask the container to get in instance of the Samurai class and to resolve all of it's dependencies
var warrior = kernel.Get<Samurai>();
warrior.Attack("the evildoers");
Console.ReadLine();

var gun = kernel.Get<Gun>();