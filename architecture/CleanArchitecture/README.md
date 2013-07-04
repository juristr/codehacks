Clean Architecture
---

This is my playground on trying to implement Uncle Bob's description of a so-called [Clean Architecture](http://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html) which he also mentions in his keynote about "Architecture the Lost Years".

<iframe width="560" height="315" src="http://www.youtube.com/embed/WpkDN78P884" frameborder="0" allowfullscreen="allowfullscreen"> </iframe>


The implementation happens to be in C# .Net using WebApi as the Frontend (or Delivery Mechanism as Uncle Bob calls it). However you should equally be able to prot this to any desired language.

### State

This is in a very early state. That's also the reason why it isn't in a separate GitHub project but under a folder of my "Codehacks" repository I use for open experiments.

### Contributions

Contributions are always welcome. Feel free to submit pull request or simply open any issues which could be used for an open discussion.

### Disclaimer

All of the pictures used here are from the Uncle Bob's keynote (basically nothing than screenshots) to visually evidence the different parts of the architecture he describes.

# The Architecture

From here onwards I collect some notes from the keynote, just to have them in one place to keep the big picture.

## Flow of Information

User executes a request/command and the **delivery mechanism** creates a new **Request Model** which pushes it through the boundaries.

This means that in an **MVC environment** we'd have some web mechanism which instantiates the controller which in turn instantiates the Request Model.

![](./docs/imgs/flow_mvc_http.png)

As mentioned, it would then pass it on through the **Boundaries** (having an interface of it) into the **Interactors** (the core part of the application).

![](./docs/imgs/flow_mvc_interactor.png)

Zooming into that scenario we'd have

![](./docs/imgs/flow_requestmodel_creation.png)


The Interactors **interact** with the Request Model by means of the **Application Business Rules** and adapt it using the **Entities** which contain the **high level Enterprise Rules**. Out of that we get a **ResultModel** which is _pushed back_ through the boundaries using an "out-bound" boundary which has been injected as an interface to the Interactors. 

![](./docs/imgs/flow_resultmodel.png)

## The Whole

![](./docs/imgs/wholepicture.png)


## Database

Database should not be in the middle, but rather a plugin that provides persistent storage. This allows to leave it away when not needed (i.e. unit tests) or easily exchange it with some alternative storage mechanism.

![](./docs/imgs/pluggabledatabase.png)


## References

- http://www.infoq.com/news/2013/07/architecture_intent_frameworks
- http://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html
- http://jeffreypalermo.com/blog/the-onion-architecture-part-1/
- [Keynote: Architecture the Lost Years](http://www.youtube.com/watch?feature=player_detailpage&v=WpkDN78P884#t=1672s)