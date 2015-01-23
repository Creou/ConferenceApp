using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Creou.ConferenceApp.Server.Models
{
	public static class ReallyHackyEnumerableExtensionsDoNotUseInProduction
	{
		private static Random rng = new Random();

		public static T Random<T>(this IEnumerable<T> data)
		{
			int index = rng.Next(0, data.Count());

			return data.Skip(index).First();
		}
	}

	public class CreouConferenceAppDBInitializer : DropCreateDatabaseIfModelChanges<CreouConferenceAppServerContext>
	{
		private class Detail
		{
			public string Title { get; set; }
			public string Description { get; set; }
		}

		protected override void Seed(CreouConferenceAppServerContext context)
		{
			// Add some dummy data for prototyping the app.
			List<Track> tracks = new List<Track>();
			List<Room> rooms = new List<Room>();
			List<Speaker> speakers = new List<Speaker>();

			var DDD = new Conference { ConferenceName = "DDD" };
			var companySizeOption = new DropDownOption { Conference = DDD, OptionName = "CompanySize" };
			companySizeOption.Values = new List<DropDownOptionValue>
			{
				new DropDownOptionValue{Option=companySizeOption, Text="1"},
				new DropDownOptionValue{Option=companySizeOption, Text="2 - 10"},
				new DropDownOptionValue{Option=companySizeOption, Text="11 - 50"},
				new DropDownOptionValue{Option=companySizeOption, Text="51 - 250"},
				new DropDownOptionValue{Option=companySizeOption, Text="251 - 1000"},
				new DropDownOptionValue{Option=companySizeOption, Text="1000+"}
			};
			var statusOption = new DropDownOption { Conference = DDD, OptionName = "Status" };
			statusOption.Values = new List<DropDownOptionValue>
			{
				new DropDownOptionValue{Option=statusOption, Text="Hobby"},
				new DropDownOptionValue{Option=statusOption, Text="Student"},
				new DropDownOptionValue{Option=statusOption, Text="Professional"}
			};

			context.Conferences.Add(DDD);
			context.DropDownOptions.AddRange(new[] { companySizeOption, statusOption });

			tracks.Add(new Track() { Name = "Track 1" });
			tracks.Add(new Track() { Name = "Track 2" });
			tracks.Add(new Track() { Name = "Track 3" });
			tracks.Add(new Track() { Name = "Track 4" });
			tracks.Add(new Track() { Name = "Track 5" });

			foreach (var track in tracks)
			{
				context.Tracks.Add(track);
			}

			rooms.Add(new Room() { RoomName = "Room 1" });
			rooms.Add(new Room() { RoomName = "Room 2" });
			rooms.Add(new Room() { RoomName = "Room 3" });
			rooms.Add(new Room() { RoomName = "Room 4" });
			rooms.Add(new Room() { RoomName = "Room 5" });

			foreach (var room in rooms)
			{
				context.Rooms.Add(room);
			}

			speakers.Add(new Speaker() { Name = "Bob" });
			speakers.Add(new Speaker() { Name = "Jim" });
			speakers.Add(new Speaker() { Name = "JimBob" });

			foreach (var speaker in speakers)
			{
				context.Speakers.Add(speaker);
			}

			var startTimes = new List<Tuple<TimeSpan, TimeSpan>> {
			   new Tuple<TimeSpan, TimeSpan>( new TimeSpan(9, 0, 0),  new TimeSpan(9, 50, 0)  ),
			   new Tuple<TimeSpan, TimeSpan>( new TimeSpan(10, 0, 0), new TimeSpan(10, 50, 0) ),
			   new Tuple<TimeSpan, TimeSpan>( new TimeSpan(11, 0, 0), new TimeSpan(11, 50, 0) ),
			   new Tuple<TimeSpan, TimeSpan>( new TimeSpan(14, 0, 0), new TimeSpan(14, 50, 0) ),
			   new Tuple<TimeSpan, TimeSpan>( new TimeSpan(15, 0, 0), new TimeSpan(15, 50, 0) ),
			   new Tuple<TimeSpan, TimeSpan>( new TimeSpan(16, 0, 0), new TimeSpan(16, 50, 0) )
			};

			var details = new List<Detail>
			{
				new Detail
				{
					Title = @"An Introduction to SOLID",
					Description =
						@"An introduction to the five S.O.L.I.D. principles (Single Reponsibility Principle, Open/Closed Principle, Liskov Substitution Principle, Interface Segregation Principle, and Dependency Inversion Principle). In this session we'll take basic OO concepts and expand on them to give a grounding in the SOLID principles.
The examples are in C#, but the concepts can apply to any OO language."
				},
				new Detail
				{
					Title = @"Super charging your JavaScript development experience",
					Description =
						@"With the release of V8 and subsequently NodeJs, JavaScript has started to grow up.  In this session we will look at how you can super charge your JavaScript development lifecycle and deliver better written, cleaner and more coherent JavaScript with and without VisualStudio
We will look at the many awesome frameworks for infrastructural support when developing JavaScript applications like Yeoman, Gulp, grunt, Browserfy and a few other handy libraries"
				},
				new Detail
				{
					Title = @"Compiling to JavaScript",
					Description =
						@"The range of programming languages that compile to JavaScript is enormous.  From brand new languages like CoffeeScript, to templating languages like Jade all the way to pre-existing languages like F# and Java. It seems like everyone wants to use JavaScript as a compile target these days. This talk will cover all the steps to writing a clear, well structured compiler in JavaScript. It will also discuss a few optimizations you may want to perform, using Jade as an example."
				},
				new Detail
				{
					Title = @"GitHub Automation",
					Description =
						@"GitHub has one of the best REST APIs you'll ever see. Pretty much any task in GitHub can be automated. Tired of updating code after a feature gets deprecated? Write a bot for that. Need to add the repository field to all your package.json files? Script it. You can even use GitHub as the backend for an entire application to take advantage of its built in collaboration features.  This talk will take you through how to write you own GitHub automation code in JavaScript and give you some ideas on how to use your new-found powers for good."
				},
				new Detail
				{
					Title = @"RavenDB: an introduction to document databases",
					Description =
						@"RavenDB rapidly moved from being one of the emerging kids to a stable and mature platform to built scalable and reliable applications.
In this talk we'll start introducing document databases concepts, such as schema-less and eventual consinstency, and then we'll see how to create applications using RavenDB as the storage backend."
				},
				new Detail
				{
					Title = @"NServiceBus: introduction to a message based distributed architecture",
					Description =
						@"SOA and distributed had been buzzwords for a long time, a message based architecture that embrace the SOA principles is the real solution to a scalable and distributed environment where HA or/and temporal decoupling are a must.
In this session we will introduce messaging concepts and see how NServiceBus, a powerfull toolkit to rule all the aspects of a messaging transport, can dramatically simplify the development process."
				},
				new Detail
				{
					Title = @"Using F# for Line of Business Applications",
					Description =
						@"C# is a great language for developing Line of Business applications but F# is even better! If you want to write code that expresses your requirements clearly, ensures correctness and supports rapid, and fun, development then guess what, F# does all that and more!

No prior experience of F# is necessary for this session but be warned, once you've seen what F# can do, you won't want to go back to C#!"
				},
				new Detail
				{
					Title = @"Beyond responsive design - UI for the modern web application",
					Description =
						@"Applications written for the modern web are being consumed not just on desktop browsers, but also on a myriad of other devices... even watches and glasses. If you design your application with a pc screen in mind, at worst you're either cutting your userbase in half or setting yourself up for an expensive redesign.

In this talk I'll introduce you to some modern web design constructs, and the technologies that bring them to life. Learn how to create apps that work just as well on phone, mobile and desktop with no extra effort, and without restrictive layout frameworks. Who knows... you may even even see things that begin to rival native apps!"
				},
				new Detail
				{
					Title = @"Decoupling from ASP.NET - Hexagonal Architectures in .NET",
					Description =
						@"The term 'hexagonal architecture' has come back and forth in popularity since Alistair Cockburn first mooted it, with the Rails community's recent soul searching over its importance or threat just the latest. So what is a hexagonal architecture, why might you want to use one, and why is the 'Rails just falls away' (https://www.youtube.com/watch?v=tg5RFeSfBM4) threat so discomforting to web framework builders. How can we make 'ASP.NET just fall away'.

In this presentation we will look at the Layered Architectural style - when we would want to use one (as opposed to the alternatives) and when it is appropriate how to implement one. We will look at how to implement the Ports & Adapters (Hexagonal's 'proper' name) style, explaining what the different layers are. 

We will look at the value the command pattern for implementing our ports , explain why Netflix uses it in Hystrix for reliability. On the way we will discuss Retry, Timeout and Circuit Breaker and explain how we can do better than Hystrix with a Command Dispatcher and Command Processor.

We'll show code throughout, including a look at the Paramore. Brighter framework, an OSS version of the platform we use at Huddle to build this kind of architecture.

As a bonus we will round off showing you how easy it is go from sync to async with this approach."
				},
				new Detail
				{
					Title = @"Functional DDD",
					Description =
						@"Very often we approach, more or less unconsciously, DDD's principles only with object-oriented paradigm, without exploring if other ""style"" can live better with aggregates, value objects, commands and domain events. Well, yes: there is 'other' out there... 
In this session we will see how a functional language as F# can lead to a more intuitive and compact implementation of our domains."
				},
				new Detail
				{
					Title =
						@"An Actor's Life for Me – An introduction to the TPL Dataflow Library and asynchronous programming blocks",
					Description =
						@"Every version of the .NET Framework has brought improvements to asynchronous and concurrent programming. While .NET 4.0 brought the async/await model which is useful for improving UI responses and server applications, it can sometimes still be tricky to marshal multiple threads within longer processing pipelines.

The Dataflow Library consists of a Nuget package built on top of the Task Parallel Library (TPL). It harnesses the actor-based programming model to provide a set of dataflow blocks data structures that buffer and process data, which you can connect together to form custom pipelines with messages passed between the blocks.

By using the Dataflow Library you can concentrate on the messages and actions being performed, while the blocks marshal the messages, provide concurrent message processing and buffering as well as supporting cancellation and exception handling."
				},
				new Detail
				{
					Title = @"EventSourcing in a ""not-always"" connected world",
					Description =
						@"In a ""not-always"" connected world we have a problem: merge business transactions across network boundaries. In this context, event sourcing is useful because we can think to our system's evolution with state transition. 
Synchronize (only) data is not so easy because we lose why something happens. Events are the answer to trace and synchronize business process and identify ""merge conflicts""."
				},
				new Detail
				{
					Title = @"No Backend - Creating Javascript apps without needing a server",
					Description =
						@"So you've had a killer idea and you need to get a site up quickly but why should you spend time creating the same old code to handle registering your users, storing data, etc?

Come along to this session and I'll show you an alternative way to build apps that don't need you to write any server side code at all yet still provide the functionality you need and possibly more."
				},
				new Detail
				{
					Title = @"Effective Disagreement - turning conflict into growth",
					Description = @"How many times have you heard team members say things like:

	He/She doesn't get it!
	We tried but they don't listen to us!
	I don't have the power to change things!
	The business (other outside entity) don't know what they want!

If you've ever seethed with rage as you look at the code that other team members have written. If you've come to the conclusion that it's quicker and easier for you to change the code rather than talk to the people around you about the problems it presents then this talk is for you. I will present how we get ourselves into these situations of mistrust and disrespect for our colleagues. I will also show some simple but effective techniques for communicating effectively that enables better decisions based on differing opinions without conflict.

You will learn how to explain the great technical ideas you get from DDD East Anglia to the rest of your team without feeling the frustration of being surrounded by people ""who don't get it!"". Imagine going back to your team from a conference and actually have the ideas be adopted!"
				},
				new Detail
				{
					Title = @"Not just layers! What can pipelines and events do for you?",
					Description =
						@"When developers reach into their toolkit for architectural styles, they often explicitly uses layers to separate their domain from their presentation logic or infrastructure. They often implicitly use the repository style, with independent components updating the database. But there are many more styles out there, which can help you build your applications. In this talk we look at two of them: pipelines and events. Pipelines let us deal with streams of data effectively, and events provide significant advantages for loose coupling.

 We discuss where these styles are appropriate and how to implement them in .NET. As both approaches can be used in-process or out-of-process we'll show examples of both, leading to an understanding of how distributed systems communicate using ideas such a SEDA - the staged event driven architecture."
				},
				new Detail
				{
					Title = @"All your types are belong to us!",
					Description =
						@"Big Data tasks typically require acquiring and analysing data from a wide variety of data sources, visualizing the data and applying a barrage of statistical algorithms. This talk will show how this can be accomplished in Visual Studio on Windows or Xamarin Studio on Mac and Linux using F#'s REPL and Type Providers. 
Type Providers give typed access to a wide range of data sources from CSV, JSON and XML to SQL, OData and Web Services, instantly without a code generation step. The Type Provider mechanism can also be used to analyse data with direct access to statistical packages like R and MATLAB as well as all the existing .Net libraries.
Finally visualizations can be generated using F#'s desktop charting libraries, or with ASP.Net and even JavaScript libraries like HighCharts.
Expect a sprinkling of anecdotes drawn from experiences working on large machine learning systems at Microsoft, and plenty of live demos."
				},
				new Detail
				{
					Title = @"F# for fun and games",
					Description =
						@"It's already been shown that F# is a great language for so many different types of applications. One area in which F# is quickly gaining popularity is in games development for both the mobile and PC markets. This session will cover some of the key features which make F# a brilliant language for this area of development. Expect to see plenty of demos of what you can actually achieve in just a few hundred lines of code whilst also going up to AAA games rewritten in F#."
				},
				new Detail
				{
					Title = @"Building Skynet: Machine learning for software developers",
					Description =
						@"How does Netflix know that I'd like that new movie which just released? How does Google know which ads to serve to me? How do games like Halo and Titanfall put me in game lobbies to create even matches? All these questions are answered with machine learning algorithms. Machine learning can sometimes look difficult. This session aims to break down the barrier to entry for machine learning and show how powerful even the most simple algorithms can be. Expect plenty of sample code to show just how quick and easy these basic algorithms can be."
				},
				new Detail
				{
					Title = @"Keeping it responsive - cross-platform MVVM with ReactiveUI",
					Description =
						@"Building an awesome user interface is hard work. You've got the complexities of real life to deal with - handling user inputs, dealing with slow network connections and managing background workers. There's also this testing thing that people keep going on about!

We need a way of handling the real world, whilst behaving in a predictable and responsive manner. Enter ReactiveUI, a fusion of MVVM and the Reactive Extensions (Rx) for .NET applications.

In this session I'll introduce ReactiveUI, show you some tips and tricks, discuss the benefits and tradeoffs of the framework and show how it can be used as a solid foundation for building cross-platform .NET applications."
				},
				new Detail
				{
					Title = @"Lean Software Factories - (Stop trying to scale Agile!)",
					Description =
						@"The idea that traditional manufacturing practices could be used to manage complexity in large software operations has been tested and evaluated several times with varying degrees of success.

The first Software Factory experiment over was over 40 years ago in a venture that involved some 3000 personnel and spanned two decades. The fundamental differences between software development and mass-production of physical products shaped the software factory into something entirely unexpected. This talk examines the evolution and outcome. It also draws links to the Software Craftsmanship movement and confronts the challenges the Agile community has had with scaling."
				},
				new Detail
				{
					Title = @"What Developers Need To Know About Visual Design ",
					Description =
						@"The world has become a very design sensitive meaning it’s now even more critical that developers build products that look amazing. Sadly frameworks like twitter’s bootstrap can only take us so far and even with designers on the team developers need to understand the key principals of good design to make effective decisions.

In this session Ben will explore the five key topics around design that can make or break an application and website. The key topics are Layout and the golden ratio, Typography, Imaginary, Colours and User Feedback. With these topics attendees will come away with a deeper understanding about why certain elements look good while others don’t and what developers really should know about design. It will explore the cognitive science and research to move beyond personal options about design to data and research driven insights."
				},
				new Detail
				{
					Title = @"Real World Lessons on the Anti-Patterns of Node.js Applications ",
					Description =
						@"The Node.js is a vibrant and passionate community, pushing the boundaries and exploring the best ways to build applications. This has resulted in some amazing frameworks but has also made it difficult for people to understand how to best architecture and build Node.js applications. 

This session will highlight the anti-patterns of Node.js, such as Callback Hell, how to write better applications and the lessons Ben has learned while building Node.js applications. 

**The key discussion points will include:**

Controlling an asynchronous world of Callbacks, Async and Promises. 

SOA/MicroServices and keeping things small

Error handling, monitoring and deploying to production 

Testing and debugging to move beyond console.log"
				},
				new Detail
				{
					Title = @"DDD: Disney Driven Development",
					Description =
						@"Disney parks are built using their Four Keys of The Kingdom: Safety, Courtesy, Show and Efficiency. This talk will show examples of how Disney have applied these four keys everywhere in their parks and how we can extend those principles to our work as web developers."
				},
				new Detail
				{
					Title = @"How aspects clean your code?",
					Description =
						@"Aspect-Oriented Programming (AOP) is useful whenever there are so-called cross-cutting concerns. Aspects lead to cleaner code and a consistent design for business logic. 

They promote the Single Responsibility Principle and let software engineers focus on application features. In this talk, I will cover lesser-known use cases of aspects and, using real-life scenarios, demonstrate how AOP results in a cleaner implementation that is easier to test. 

In the end, I hope to show how AOP helps going from model to code."
				},
				new Detail
				{
					Title = @"Embracing DevOps at JUST EAT, within a Microsoft platform",
					Description =
						@"JUST EAT changed its culture towards embracing DevOps principles, and heavily leveraged AWS to achieve it.

We're a successful online takeaway ecommerce website running on a Microsoft-based platform.

Come learn how we:

 - re-organised our teams and our platform to loosely couple them
 - re-organised our architecture to be more modular
 - made it possible for developers to operate their code in production directly, starting with shoot-it-in-the-head debugging
 - made it possible for developers to continuously ship changes
 - eliminated most differences between production and qa environments
 - became more resilient as a happy by-product"
				},
				new Detail
				{
					Title = @"Moving JUST EAT to AWS - what we broke to make it work",
					Description =
						@"JUST EAT is a high volume ecommerce operation, running on a Microsoft platform. We successfully migrated from a single data centre into AWS. Along the way, we:

 - increased availability and resilience - multi-AZ by default, and resilient to loss of an AZ
 - reduced cycle time between releases - ~600 in 2014 to date, ~300 in 2013, ~100 in 2012
 - removed software integration patterns not appropriate for cloud-native platforms
   - replaced shared filesystems with S3
   - replaced database poll-and-wait with SNS+SQS eventing
 - removed single points of failure in favour of auto-scaling instances that come up clean every time
 - cheated at load-balancing apps that couldn't be load-balanced

This session will describe some of the obstacles we overcame (read ""hacks we employed to get us going""*), within the context of a Microsoft platform.

\* some of which we then removed."
				},
				new Detail
				{
					Title = @"A Unit Testing Swiss Army Knife",
					Description =
						@"Putting all of *#IsTddDeadOrNotQuiteYet* discussion aside, there are a lot of things to be said about more technical side of writing tests. Instead of big important questions like ""How"" or ""Why"", I would like to present you a couple of tricks, patterns and libraries that help in what is usually of secondary interest - readability, maintainability.

The leading motive for this talk will be approaching our tests like living documentation - and what we can possibly do to make it better.

So, have you ever wondered what is Bulider Pattern about? What is all the fuss in being ""fluent""? Or maybe you wondered if you can effectively integration test you MVC app? I hope you will find useful learning this, and some more. 
"
				},
				new Detail
				{
					Title = @"Tonight We're Gonna Code Like It's 1999: Creating Responsive Emails",
					Description =
						@"It's the moment you’ve been dreading: the project of redesigning all consumer facing emails AND making them responsive becomes yours. And you've heard the rumors: designing emails means coding like it’s 1999, creating tables and adding styles inline (heaven forbid!), and throwing best practices and hopes of compatibility out the window. BREATHE. In this session, I’ll help you get your emails in shape for 2014 and ready for the responsive spotlight by showing you:

 - Why you need a reusable, maintainable template (or three) and how to
   do that.
 - When and how to use media queries and the ever controversial !important.
 - How to make desktop, mobile, and web-based clients play nicely, and which ones to watch out for (sneaky devils). 
 - Third-party tools and knowledgeable blogs that weed through the
   ugliness and what    parts you still need to code by hand. Zurb Ink,
   Litmus, Campaign    Monitor, Style Campaign [hide list of resources
   for talk    description]."
				},
				new Detail
				{
					Title = @"Owin: The great asp.net reboot",
					Description =
						@"Asp.net has a long history of delivering web applications; unfortunately this history comes with a fair amount of baggage. This all changed with the introduction of owin. Owin lets us achieve a level of efficiency and portability using asp.net that would be inconceivable using the existing .net infrastructure. 

In this session I will cover:

 - The whats and whys of owin, how's it different to what we already
   have? What do we get out of it? 
 - Owin hosts and middleware
 - Running owin on multiple platforms using mono 
 - Fast and simple in-memory integration tests using the owin testing infrastructure 
 - Owin in .net vNext

In short: Owin - it's really fast, extremely portable and you should use it; here's why."
				},
				new Detail
				{
					Title = @"A Brief Introduction to Making your own (Internet of Things) Thing.",
					Description =
						@"The Internet of Things is exploding and it's a great time to join in: more and more devices like the Arduino, Netduino and Gadgeteer are becoming available. The question is, how do I get started?

We will look at what is available in terms of popular hardware for building your Thing, and a demo of how to develop for the Arduino, followed by an introduction to the Gadgeteer and .Net Micro Framework, hopefully finishing up with a fairly simple but connected Gadgeteer based Thing (Wifi Allowing!)."
				},
				new Detail
				{
					Title = @"Linking apps",
					Description =
						@"Having data locked in apps isn't what many people want. Nor is the inability for apps to work together. 

Deep links and custom URI schemes are a way for apps to work together but it's often more frustrating and complicated than it needs to be. This means it's hard to provide the best experience for users and understand how and when apps should send the user to another app.

[AppLinks][1], an initiative started by Facebook but with lots of big names backing it, is an attempt to make things better and a whole lot easier for developers who want to provide a better app linking experience.  
It's a solution that is truly cross-platform and this session will be a chance to see how it works and you can link apps together


  [1]: http://applinks.org/"
				},
				new Detail
				{
					Title = @"Mobile app security with OWASP",
					Description =
						@"If you've ever done any web development you're hopefully very aware of the issues highlighted in the Open Web Application Security Project (OWASP).

If you're developing mobile (or other client) apps then security probably (and unfortunately) isn't a major concern. What you're probably not aware of is that OWASP also have a mobile security project. The OWASP mobile security project aims to classify mobile security risks and provide guidance to reduce impact and likelihood of exploitation.

Regardless of the platform, or platforms, you're building for there are lessons and tips we can learn for the apps we're building or to check the security of the apps we're using.  
And if you ask me nicely, I might even tell you about the mobile banking app that sends personal information over the internet in the clear."
				},
				new Detail
				{
					Title = @"8 tips for WP8.1 Dev",
					Description =
						@"What makes a great Windows Phone app? How does the introduction of version 8.1 change things for anyone who has previously worked with Windows Phone or Windows Store? How can a phone app be ""universal""? What are the common things that can be done to improve existing apps?

All this and more as we look at building awesome apps for Windows Phone."
				},
				new Detail
				{
					Title = @"Single Code base, multiple platforms. Mobile app development made smart",
					Description =
						@"Successful mobile apps target multiple mobile platforms through a process of costly repetition – by recreating versions of the same app with zero code reuse, incompatible tooling and no skills share – and that’s before you get into support, team utilisation, etc.

But building multiple native apps is not the only way. Hybrid app development can bridge the gap.

Come learn about two approaches to building cross-platform mobile apps: AppBulder, an Apache Cordova based IDE, and NativeScript, a pure native API to JavaScript framework. These tools will help you build mobile apps for Android, iOS and Windows Phone using only JavaScript and HTML5.

I will share some of my best practices on project structure, code hints and more."
				},
				new Detail
				{
					Title = @"JavaScript for .Net Developers. How to get in the mood",
					Description =
						@"Picking up JavaScript if you’re a C# developer is not straightforward – on top of my pet peeves list are switching from a strongly typed language to a dynamically typed one and syntax resolution before build vs. after failure. This being said – I’m always excited about learning something new.

In this talk I’ll take you on a journey of discovery, show you some of the things that made my head explode from a safe distance and share my recipe for pain minimisation.

And just for fun I’ll show you how to migrate a WPF’s MVVM concept to a Kendo UI MVVM project."
				},
				new Detail
				{
					Title = @"The vNext Big Thing",
					Description =
						@"At TechEd this year, various Scotts and a David announced ASP.NET vNext, the biggest thing to happen to the Microsoft web developers story since ASP.NET 1.0.

It's a moving target, so I can't say exactly what the talk will include, but I can guarantee it will cover:

- The Core CLR, the K Runtime, and the project.json file
- How Roslyn fits into the story
- What (and why) OWIN is, and how to write middleware for it
- The merging of MVC and Web API into a single, streamlined, uber-framework
- Why I'm never going to finish Simple.Web

Look, it's all awesome. Just come and see."
				},
				new Detail
				{
					Title = @"Hadoop Kickstarter for Microsoft Developers",
					Description =
						@"Big Data is the new shiny thing right now, and if you read the blogosphere you'd be forgiven for thinking it was a tool for Linux devs only, (or worse, only for those annoying hipsters with their shiny Macs). Nothing could be further from the truth however. Windows makes an excellent platform for Hadoop and in this session I'll show you everything you need to know to get started. From downloading and installation, to writing our your first map reduce job, using both the streaming API and the SDK, this session will cover it all, so come along and join the big data wave!"
				},
				new Detail
				{
					Title = @"Data Science for Fun and Profit",
					Description =
						@"Make no mistake, data science can be hard, but it can also be fun. In this session I'll introduce you to Classic and Bayesian Statistics and Machine Learning, all through the medium of predicting horse racing results. We'll explore a number of techniques for making such predictions and we'll finish by combining them into a powerful ""mixed model"" prediction engine, that's sure to pick the next big winner. This session won't only improve your knowledge, it'll improve your bank balance too! Note: probably won't do the latter though. :-)"
				},
				new Detail
				{
					Title = @"Introduction to the R Programming Language",
					Description =
						@"R is the programming language of choice for data scientists. In this session I'll give you an introduction to the language, covering all the main areas as well as a good set of coding standards, after which, you'll have all the knowledge you need to write the next great data science prediction engine and get bought out by Google. When you do, remember who taught you the basics! :-)"
				},
				new Detail
				{
					Title = @"Build Great Software for the Enterprise and Love it",
					Description =
						@"You want to write great code - taking the time to create an application that is cleanly written, easy to test, performs well, and delights users.  Your company wants it done yesterday.  **How do you get the time and budget you need to build it right instead of just pounding out the next feature**?

In this talk, we'll cover how to:

 1. Never skip coded tests again.
 2. Communicate security risks effectively so your business isn't the next Target.
 3. Pay down technical debt while delivering the features your users are waiting for.

Sound incredible? Come to the talk to find out how you can go home happy with the work you've done each day, even in an enterprise."
				},
				new Detail
				{
					Title = @"Introducing Agile to the Enterprise",
					Description =
						@"**Still Living in a Waterfall World? You're Not Alone!** Convincing your company to use an Agile development approach for application development can be surprisingly difficult.  While the initial benefits excite everyone, that enthusiasm often disappears when the true trade-offs of Agile become apparent.  You may even find that many members of the development team aren't willing to give up their waterfall patterns of development.  How do you convince the business that good software doesn't begin and end with a giant Microsoft Project Plan? In this session we'll discuss how you can get your company moving down the path to more effective software development and bridge the gap between traditional practices and the current software development landscape

 - **How to discuss Agile with the Business**: It's their money, you need to make sure they're on board with how you're going to spend it.
 - **Convincing the Team to Work Differently**: Without the right patterns of development within your team, your first Agile project may be the last for a while.
 - **Useful Compromises**: When you can't change the world overnight, how do you at least get the ship turning in the right direction?

Presented by one of the founders of Gibraltar Software, we'll discuss how we've worked with many enterprise customers to marry agile practices into traditional teams.  Sometimes successful  sometimes a brilliant failure - come and learn from our mistakes so you can produce better results and have happier customers and team members. We won't be focusing on a specific technology or Agile approach but rather how you get businesses to accept **giving up the Gantt chart and learn to love software development**."
				},
				new Detail
				{
					Title = @"Peaks and Pitfalls in Creating Commercial Software",
					Description =
						@"Kendall Miller, Co-founder of Gibraltar Software (A US-based Independent Software Vendor) pulls back the curtain and talks about the challenges and difficult decisions that come along with creating a business around commercial software.

 - **Creating Product Strategy**: One of these three doors is the path to greatness, the other two go nowhere. Guess which.
 - **Connecting with Customers**: Trying to not be the best product no one ever heard of.
 - **Finding the Right Talent**: Collecting the team to knock it out of the park.

We’ll go through real examples from the history of our software products – things that have worked out and things that have gone awry. Sometimes comical, sometimes tragic – it’s never boring when you’re building your reputation and your company."
				},
				new Detail
				{
					Title = @"Taking your craft seriously with F#",
					Description =
						@"Many standard F# libraries and tools, including the compiler itself, are developed as open-source and have a large number of contributors. To successfully build such projects, you need to be serious about your craft. This includes comprehensive testing, using automated build tools, continuous integration, as well as creating great documentation and tutorials. In this talk, I'll talk about what I learned as an open-source F# contributor.

Along the way, we'll look a number of risk-free ways of introducing F# into your workflow:

 * How to use F# Interactive for explorative programming and writing code that works on the first try
 * Using FAKE - an F# build tool - to automate everything in your build process
 * Writing readable unit tests with F# and using FsCheck for property-based testing
 * Generating great documentation using F# Formatting tools

In summary, this talk is a walkthrough covering some of the software engineering aspects of programming that have been working extremely well for the F# open-source ecosystem. After the talk, you'll have a good idea how to use some of the techniques in your daily job - but you may as well become an F# contributor! 
"
				},
				new Detail
				{
					Title = @"FsLab: Doing data science with F#",
					Description =
						@"How to get knowledge from data? We need to access CSV files and REST services, combine the data while handling missing values, try different analyses and machine learning algorithms and then build visualizations to make our point. We need to explore data interactively, but end up with reproducible scripts that can be easily deployed in production. 


I’ll demonstrate end-to-end data analysis using FsLab – a cross-platform set of data science libraries and tools based on F# that make it easy to perform the entire process with a single tool. Type providers turn external data sources into inherent part of your language; integration with tools like R gives you immediate access to professional packages and HTML5-based visualization tools produce beautiful results.
 

Along the way, we’ll explore correlations between countries using the WorldBank, we’ll look at survival rate of different passengers on Titanic and we’ll look how different political parties contribute to country’s debt.
"
				},
				new Detail
				{
					Title = @"A Journey into CQRS/ES/DDD",
					Description =
						@"For years I used to live and die by the CRUD sword. Building complex repositories and service layers to update my anaemic models, often with complex validation that tried to encompass all scenarios. Then, a while back I discovered the CQRS/ES/DDD sword, the Excalibur. 

But this way is not easy, especially for the beginner. Lots of new terminology such as ubiquitous languages and eventually consistency make it a difficult field to enter at first. In this talk I will attempt to demonstrate a simple architecture. Along the way explaining the basic concepts and pointing out any pitfalls that I encountered as a beginner. The aim of which is to provide you with a basic implementation in which to start your own journey into this CQRS/ES/DDD world."
				},
				new Detail
				{
					Title = @"ASP.NET you're doing it wrong: An Introduction into Nancy",
					Description =
						@"ASP.NET sucks. Well it's not that bad, but there is a better way. The Nancy way. For those of you who haven't heard of it, Nancy is a lightweight framework for building HTTP based services and it's a great alternative to the typical Microsoft web stack.  In this talk I will give an introduction to the framework showing you how to create a basic application and highlighting some of Nancy's advantages along the way. I will be going over the basics of:

- Routing and route arguments
- Model binding and validation
- View engine and how to return views.
- Content negotiation
- Dependency injection
- Testing

I'll then finish up by showing you how to integrate Nancy into an existing ASP.NET Application so you can start using it in production straight away :)."
				},
				new Detail
				{
					Title = @"Experience Eventstorming first hand",
					Description =
						@"During this hands-on session you will get the opportunity to experience the power of event storming yourself. Instead of listening to someone explaining you how it works, you will get to practice it. I strongly believe in learning by doing. You will model the domain of a hypothetical problem space using concepts like domain events, commands, queries, bounded contexts, etc. After a short introduction of the good practices and things to keep in mind, you will split up in small groups and experience event storming firsthand. At the end of the session, we will do a very brief debrief to see what you experienced and want to share with the group."
				},
				new Detail
				{
					Title = @"It's all about the User, man! (Authentication and Authorization with OWIN)",
					Description =
						@"Google, Facebook, Twitter, even Office265 - these days getting a user to sign in to your web application is all about federating identity. And with OWIN, it's easier than ever to integrate with these identity providers. 

In this talk I demonstrate hooking these providers up, how to avoid rolling your own user database, and what other great libraries are available to help.
"
				},
				new Detail
				{
					Title = @"Automating deployment and testing with Visual Studio Online and OctopusDeploy",
					Description = @"Last year I showed how OctopusDeploy was a great (and simple) deployment platform. 

Now, I'll show how the latest V12 build templates in Visual Studio Online can let you automate building, deploying and testing software - whether it's a Nugettable class library or your brand new social website.
"
				},
				new Detail
				{
					Title = @"So you want to be a Tech Lead? 10 things you need to do to succeed.",
					Description =
						@"""Tech Lead"" is an amorphous job title - is it all about the technology, or all about leadership? What should the balance really be?

And it can also be a complex and thankless role too - particularly if you find yourself becoming the go-to guy (or gal) for everyone from the intern to the product manager.

In this talk I'll cover (at least) 10 things that I think are essential to success in both areas, including how to address technical debt, herd your PMs and make sure your development team has a steady flow of work (and beer, pizza or other ""motivationals"").
"
				},
				new Detail
				{
					Title = @"Performance is a Feature!",
					Description =
						@"Starting with the premise that *""Performance is a Feature""*, this session will look at how to measure, what to measure and how get the best performance from your .NET code. We will look at real-world examples from the Roslyn code-base, StackOverflow and my personal experience of trying (but ultimately failing) to break a world record."
				},
				new Detail
				{
					Title = @"An introduction to AngularJS",
					Description =
						@"JavaScript and HTML has recently come to a new life, in this talk we'll see how we can develop a Single Page Application (SPA) using one of the most powerful SPA toolkit out there: AngularJS.
We'll start introducing AngularJS concepts and core components and then we'll start building our sample application using ASP.Net WebAPI as the backend technology.
"
				},
				new Detail
				{
					Title = @"AngularJS directives",
					Description =
						@"Directives in AngularJS are one of the most powerful concepts and one of the hardest things to understand and manage.
In this session we will start introducing directives basic concepts and we will deep dive into the programming model, and challenges, understanding how to build custom directives starting with a simple breadcrumb to move to a much more complex typeahead."
				},
				new Detail
				{
					Title = @"Highly Strung",
					Description =
						@"Strings suck. I mean, seriously, they're awful. They can be quite literally *anything*. How do you know what a string is? Look inside it. But first, run your program, because you *can't* look inside it until then.

Want to change the way a string behaves? OK, go change all the functions that deal with that string. How do you know which functions? Well, it could be any of them. Better go fix them all.

Say you've got two strings. We want one string out. So we can stick them together with the `+` operator! Except wait, one of them is SQL (or HTML, or JavaScript, or…). So better escape the other one first! Which one? Probably the second one. It's usually the second one.

This all makes me go **AAAAAAAAARRRRGGHHHHHHHHHH**.

Let's do this better. I want to talk to you about using your type system to make all these problems go away (at least from the core of your code), and as a bonus, we'll end up with much more expressive, readable, maintainable and most importantly, correct code."
				},
				new Detail
				{
					Title = @"Efficient Coding",
					Description =
						@"Learn how to save time in the mechanics of coding and become a more efficient developer by taking advantage of productivity tools and practices. As software craftsmen, we are as good as our tools and mastering the use of them. In this session we will discuss:

 - Windows and Visual Studio productivity tools.
 - Hardware such as development machines, screens, keyboards and other devices.
 - Techniques such as Code Katas, Pomodoro and Golden Hour.
 - Performance analysis and tracking - [QuantifiedDev][1] platform.


  [1]: http://www.quantifieddev.org/"
				},
				new Detail
				{
					Title = @"Get on board the release train: how we ship confident code every week",
					Description =
						@"You’ve heard the story: Our customers really do rely on our software to manage their production infrastructure, so we can't afford to get it wrong. We want to get new features out to them as quickly as possible, so we need to develop rapidly yet ship every week with supreme confidence. Now let’s see how to make that continuous delivery a reality.
 
This session will dive into the various technical, procedural and cultural techniques we use to make this possible (painless, even). We’ll cover everything from branching strategies and continuous integration to tactical whiteboards and post-release monitoring.  And of course the biggies: how we use a layered approach to testing, and how we found the right amount of process utilizing the best parts of Scrum and Kanban.
 
All this means that when we ship every Wednesday, we know that our customers are safe."
				},
				new Detail
				{
					Title = @"OWIN, Katana and ASP.NET vNext: eliminating the pain of IIS",
					Description =
						@"I first encountered OWIN when I added SignalR to a legacy ASP.NET MVC app, and had to write a piece of OWIN middleware to get SignalR to play nicely with our legacy authentication.

It was a thoroughly impressive experience, so I built my next greenfield project on OWIN & Katana as a single-page app using static files & Web API, finally ditching IIS for good.  The glad tidings continue for Microsoft web developers, with ASP.NET vNext promising even more goodness on the horizon.

There’s a lot of changes coming for those of us working on the .NET web stack, so this talk will show you what things look like today:

 - What are OWIN & Katana, and why you should care
 - What middleware is, as well as why and how you write it
 - The advantages this brings for testing
 - How Helios lets you host on IIS (if you really *really really* want to)

As well as what's changing in ASP.NET vNext:

 - How Roslyn comes into play
 - The what and the why of the K runtime
 - Why you should care about the Core CLR
 - What’s shiny about ASP.NET MVC 6

There’s a lot to cover, so we’ll move fast. You'll come away knowing why and how you should start using this on your own projects."
				},
				new Detail
				{
					Title = @"How I ruined a team's productivity for years to come",
					Description =
						@"It was 2009.  I'd just left university and started my first real job.  I was smart and I got things done, but I didn't know how software engineering worked in the real world.  It turns out there are these things called SOLID and TDD and, because I didn't know about them, the team paid the price long after I’d left it.

This is a talk about how, even with the best intentions, things can go awry.  We're going to look back at the greenfield project that I helped build over my first 28 months as a professional software developer, dive into the code, and learn how not to repeat my mistakes.

We'll look at all of the poor architectural and code decisions that came back to bite the team after I'd left it, the impact these had on their productivity, and how you really do have to SOLID and TDD like you mean it."
				},
				new Detail
				{
					Title = @"Implementing an Event Sourced Bounded Context - Live!!",
					Description =
						@"Event sourcing, CQRS, DDD - quite often they are referred to as the stuff for architecture astronauts. This is quite ironic, seeing that one of the core benefits of these approaches is fast, iterative, feedback driven development. In this session, we will take a problem space, carve out a bounded context, devise some use cases, and their consequences. The problem will involve temporal queries, and complex logic. We will then write specifications for it, and implement it using event sourcing. While we will draw on tools and libraries, we will do all the coding from scratch. We will have a working system, with executable specifications producing Word document outputs. At least that's the goal....Stuff for outer space? Come and find out :)"
				},
				new Detail
				{
					Title = @"Cassandra - Indicium, Versatilis",
					Description =
						@"Cassandra is an extremely versatile NoSQL store. It is an amalgam of features from Dynamo and BigTable. It can deal with a lot of data, very fast - assuming some care is taken in data modelling. It is very efficient, reliable, and provides *good* guarantees. It can be used both for OLTP *and* OLAP. It is quite simple to set up, and use - even from .NET. In this session, we will vagrant up a Cassandra cluster, and use it from C#. In addition, we will discuss some of what makes Cassandra tick, and why you should care. We will discuss some modelling approaches and identify potential use cases (and look at a real case study). If you're frustrated by *some* NoSQL stores that promise quite a bit, but fall over in production, have a look at Cassandra :)"
				},
				new Detail
				{
					Title = @"When to NoSQL and when to Know SQL",
					Description =
						@"With NoSQL, NewSQL and plain old SQL, there are so many tools around it’s not always clear which is the right one for the job.
This is a look at a series of NoSQL technologies, comparing them against traditional SQL technology. I’ll compare real use cases and show how they are solved with both NoSQL options, and traditional SQL servers, and then see who wins. 
We’ll look at some code and architecture examples that fit a variety of NoSQL techniques, and some where SQL is a better answer. We’ll see some big data problems, little data problems, and a bunch of new and old database technologies to find whatever it takes to solve the problem.
By the end you’ll hopefully know more NoSQL, and maybe even have a few new tricks with SQL, and what’s more how to choose the right tool for the job."
				},
				new Detail
				{
					Title = @"Riding the Elephant: Hadoop 2.0 and the death of Map Reduce",
					Description = @"Hadoop is about much more than MapReduce these days. 

This session introduces the power of YARN and how you can write applications on top of it, as well as some of the new capabilities it brings to Hadoop which make it a powerful operating systems for your data.

We will cover both what the community is doing, and how established tools like Hive and MapReduce have benefitted from YARN, as well as how you can use this new platform to write and deploy new kinds of applications on a Hadoop cluster. As a special bonus, we'll also look at new data processing tools like spark and open up a whole new world for everyone's favourite Elephant inspired Data OS."
				},
				new Detail
				{
					Title = @"Designers can’t help you: How devs are essential for great user experiences",
					Description =
						@"Good user experiences are key to successful products today. Unfortunately, there is a big misconception about designers, and only designers, being responsible for the user experience. Designers undoubtedly have a lot to contribute to a product’s user experience, but they are far from the only people who make the UX better.

As a product director in a design-led organization, I’ll share examples about how engineers and developers are responsible for many great user experiences we see today. I'll discuss why a design team will only address a portion of the UX issues in your project. I will share some tips about working with designers and summarize the key organizational elements for delivering amazing user experiences."
				},
				new Detail
				{
					Title = @"Version Control – Patterns & Practices",
					Description =
						@"After the text editor and programming language the next most valuable, hotly debated and often poorly used tool is probably the version control system. Some treat it as nothing more than an ad-hoc backup of their source code whilst others endeavour to create a narrative that describes the evolution of their entire software product from inception to decommission.

This session takes a walk through the software development lifecycle and examines the role the source control system plays – what we store, why we store it, how we change it and then later how we reconstruct what was changed and why. We’ll look at the various forces that dictate our branching (and subsequent merging) strategies along with some of the less contentious policies we can adopt to ensure our system continues to evolve reliably whilst maintaining a traceable narrative.

Despite the range of commercial and open source SCM products out there the patterns and practices I will discuss here are almost universal. For the Software Archaeologist preserving history across file/folder moves and renames is just one aspect where tool specific knowledge matters. But before we can get there we need to deal with their lack of agreement on a common vernacular…"
				},
				new Detail
				{
					Title = @"Test-Driven SQL",
					Description =
						@"Nobody ever sets out to write a 500-line stored procedure; they often just end up that way. Writing SQL code is often perceived as “simple” which is why we start out with short simple SQL statements and don’t realise they’ve turned into a behemoth until it’s far too late. It’s likely no one ever wrote any unit tests for the functionality either so the cost, and risks, of change are only going to keep rising.

But it’s just the same story that we’ve been hearing for years about our client and server-side code. And yet SQL unit testing frameworks have been around for 10 years, so why doesn’t it get adopted along with other modern development practices such as Test-Driven Development (TDD)?

For a closed, tightly-coupled system the database framework will take away much of the need to write raw SQL in the first place. However there are a huge number of systems out there, both old and new, that still rely on the RDBMS to provide an independent subsystem that is accessed by a formal, SQL-based public interface.

The complexity of the modern RDBMS product means that refactoring is a necessity if it is to support future changes in safety; even if that change is to eventually move logic outside it. This in turn requires a good suite of automated tests and a Continuous Integration style build pipeline if changes are to be made in a reasonable time frame.

This session looks at applying the same principles and disciplines used in other areas of system development to tame the ever increasing complexity that has arisen from the maturity of the RDBMS. To show how easy it can be to apply TDD/Unit Testing to SQL development, part of the talk will involve coding up a procedure in a test-first manner using a freely available T-SQL based test framework."
				},
				new Detail
				{
					Title = @"Architecture - why so serious?",
					Description =
						@"What comes to developer's mind when he hears the phrase 'software architecture'? Is it clean design or rather heavy and unusable overhead? Nowadays there are many approaches to follow while building the software, but they often sound to pompous and lead to overcomplicating things. Programmers refuse to consider them, when all they need is making some simple functionalities work. They hear the word architecture and they get uptight in seconds. And that's not how it's supposed to be. Good practices should help, not introduce unnecessary problems and disturbance.

What if thinking about architecture doesn't make the software too heavy and introduces actual value? What if some of its concepts could be used easily, even in non complex projects, simplifying the process of creation at the same time?

The talk is to illustrate how architecture is not about ivory towers, but actual coding, and on what those coding architects should do. It will center on showing some habits developed through years of building different kinds of software systems. Using them can help reducing work, while focusing on what's most important - getting the job done that brings concrete value to the client. It will be demonstrated by real (but simple in the same time) code and fully functional web application. One that can be used as an outline for further usage, as patterns to apply. Presented examples will highlight power of more abstract approach, but in the same time will consider hands on code."
				},
				new Detail
				{
					Title =
						@"As in Life, so in Memory Management: Premature Promotion Produces Poor Performance and a Menagerie of Other Grotesque Blunders",
					Description =
						@"Both the JVM and CLR provide automatic memory management with garbage collection. Developers are encouraged to write their code and forget about memory management entirely but, whilst ignorance is bliss, it can also lead to a host of problems further down the line. And if you move back and forth between platforms the differences can sometimes trip you up!

In this session we’ll compare and contrast memory management in both the JVM and CLR. We’ll look at some of the “classic” blunders that cause trouble and how to avoid them. We’ll also talk about the tools that can help you get to the root of problems when they do occur."
				},
				new Detail
				{
					Title = @"Scream if you want to go faster: speed up .NET and SQL Server web apps",
					Description =
						@"We all know that websites need to be fast. But how do you juice up creaking web apps that have been around for a while without deploying the thermonuclear option (i.e., the costly and much-maligned ground-up rewrite)?

Unfortunately this can still prove tricky, especially when the issues lie in the database layer. Help is at hand though: I’ll show you the techniques that will help you hunt down performance problems in your database, and relate them back to your .NET code. But that's only half the story: we'll also talk about the strategies you can use to fix them from the relatively simple, to the much more involved.

You should leave with an arsenal of optimisation tricks for every occasion!"
				},
				new Detail
				{
					Title = @"Millenial SPAs - have your cake and eat it",
					Description =
						@"Aaah, HTML5 and Javascript. The buzzwords that send fear into the hearts of web developers everywhere when a client starts using them.

Your client will tell you tomorrow morning that they would like a responsive mobile site. And an app. And all their content on their regular website needs to be in the mobile website... and the app. Half of you is screaming “YES! Six months’ work and a stable paycheck”. The other half is screaming “Run away! Run away!”. Sound familiar?

In this talk we’ll take an existing desktop website, redesign it using Twitter Bootstrap, and add Single Page Application (SPA) functionality using AngularJS, whilst still retaining SEO so that your website can still be found. One codebase, all platforms, no loss in SEO."
				},
				new Detail
				{
					Title = @"Design fundamentals for developers",
					Description =
						@"A lot of developers think they can’t design, but they just don’t know what the rules are. I’ll show you how to think about design the same way you think about programming. We’ll cover some basic design and UX patterns you can apply to make your products better looking and easier to use. Expect to learn skills and techniques you can immediately apply to your next project, be it mobile, desktop or web.
"
				},
				new Detail
				{
					Title = @"Ember.js — More productive out of the box",
					Description =
						@"Ember.js is a framework for building web applications, but why should I learn yet *another* front-end framework? What makes it better than Angular, Knockout and all the other frameworks before? Why is it so opinionated? Why is the logo a hamster?

We’ll write a real application from scratch and see how Ember’s conventions dramatically reduce the amount of code you have to write. 

And that’s a good thing!"
				},
				new Detail
				{
					Title = @"Analysing social networks with F#",
					Description =
						@"Online social media connect us all. How can we use the information that is hidden in our social networks? For example, do you know who is your most influential follower on Twitter? 

I’ll show you how to perform the whole social network analysis: from downloading connections using Twitter REST-based API, to implementing your own PageRank algorithm which finds the most central followers. In the process, you’ll see how the F# type providers give an easy access to data from JSON files, and how we can use them to harness the power of the statistical language R to run some machine learning algorithms.

At the end, you’ll know how to run your own social network analysis on Twitter and how to use data science tools to find out more about your followers."
				},
				new Detail
				{
					Title = @"The Internals of the Modern IDE",
					Description =
						@"Modern IDEs are getting more and more capable, providing smarter navigation, editing and debugging. But have you ever stopped to wonder what's going on under the surface? With the increasing wealth and breadth of frameworks and languages, the importance of effective developer tools is only growing. This talk will pull back the curtain and explore how the modern IDE knows so much about your code. We'll take a look at parsing syntax trees, static analysis, code navigation, refactoring, debugging, and we can leverage this knowledge to create powerful, integrated language tools.
"
				}
			};

			SetDetails(context, tracks, rooms, details, startTimes, speakers);

			context.SaveChanges();

			base.Seed(context);
		}

		private static void SetDetails(
			CreouConferenceAppServerContext context,
			List<Track> tracks,
			List<Room> rooms,
			List<Detail> details,
			List<Tuple<TimeSpan, TimeSpan>> startTimes,
			List<Speaker> speakers)
		{
			var roomEnum = rooms.GetEnumerator();
			var detailEnum = details.GetEnumerator();

			detailEnum.MoveNext();
			foreach (var track in tracks)
			{
				foreach (var startTime in startTimes)
				{
					if (!roomEnum.MoveNext())
					{
						roomEnum = rooms.GetEnumerator();
						roomEnum.MoveNext();
					}
					var detail = detailEnum.Current;
					context.Sessions.Add(
						new Session(startTime)
						{
							Track = track,
							Room = roomEnum.Current,
							Speaker = speakers.Random(),
							Title = detail.Title,
							Description = detail.Description
						});
					if (!detailEnum.MoveNext())
					{
						return;
					}
				}
			}
		}
	}
}