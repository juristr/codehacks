# Bootform #
The aim of this mini-project is mainly educational with the hope to build at the same time something useful as well. When I started this project, my goal was to

- **learn CanJS** - in order to be able to see what are the major differences to JavaScriptMVC.
- **learn RequireJS** - because it is the most used JavaScript dependency managing tool as well as to see the differences to JMVC's steal.
- **experiment with a gruntjs setup**
- **use CoffeeScript** - in a real project rather than just for playing around simple (standard) code snippets.

This document tries to document all the steps necessary to build this app. As such it is targeted to all of you which want to learn some of the above points as well.

## Project Content and Goal ##
The project targets is simple, namely to create a more advanced version of the current [bootstrap form builder](http://bootstrap-forms.heroku.com/).

## Environment Setup ##
ONe of the first steps is to properly setup the environment.

### Code Editor ###
I'm using Sublimetext 2 as my text editor with the following plugins:

- ...
- ...

### Folder Structure ###
The project is structured as follows:

- **build** - the gruntjs build configuration
- **public** - the folder where all the stuff lies that is needed to run the app (i.e. the html, css, compiled JavaScript files etc..)
- **src** - the folder where the coffeescript files are

### Gruntjs setup ###

- nodejs packages
- Setup of require-js

#### GruntJS CoffeeScript compilation ####
When you code in CoffeeScript you need a mecchanism which watches your file system for changes in the CS files and accordingly launches the compilation process. 

One possibility is to install the coffeescript node package with `npm install -g coffee-script` and then to execute `coffee -cw src -o js` to instruct the package to continuously watch for changes in the `src` folder and to eventually compile and copy them over to the `js` folder. More [here](/blog/2012/08/its-time-to-learn-coffeescript/#Setup).

The 2nd possibility is to let gruntjs do this job. In order to set this up it is necessary to install the **grunt-contrib-coffee** package ([GitHub repo here](https://github.com/gruntjs/grunt-contrib-coffee/)). The tasks needs then to be configured like

    coffee: { 
        compile: {
          files: {
            '../public/js/*.js': [
              '../src/*.coffee',
              '../src/app/*.coffee',
              '../src/app/builder/*.coffee',
              '../src/app/models/*.coffee'
            ]
          }
        }
    }
    ...
    watch: {
      files: ['<%= pkg.folders.src %>/**/*.coffee'],
      tasks: 'coffee requirejs'
    }

## Creating the Application Shell ##
This section describes the very basic setup of the application shell. Basically the job which usually [Yeoman](http://yeoman.io) would do (if a CanJS setup would exist at the moment of this writing).

### Bootstap ###
The design: with Bootstrap obviously. It's enough to download the latest version from the [official site](http://twitter.github.com/bootstrap/) and copy the contents of the zip into the `public/assets` folder.

Then just follow the examples provided on the main site to create the minimal layout as desired.

### Installing CanJS ###

#### Download CanJS ####
Download the [CanJS AMD](http://canjs.us/#using_canjs-amd) version. That allows to easily integrate with RequireJS.

### RequireJS with jQuery and Bootstrap ###


---

### Loading Views ###
http://www.youtube.com/watch?feature=player_detailpage&v=GdT4Oq6ZQ68#t=1095s


## Setup Build Environment ##

One of the first steps is to setup a proper build environment. In the case of this app this means:

- minifying the JavaScript source files using RequireJS
- compiling CoffeeScript to JavaScript files

---

## Resources ##

### RequireJS + jQuery + Bootstrap ###
Necessary to create a require-jquery.js, basically a concatenation of require.js followed by jQuery

### CoffeeScript ###
- http://juristr.com/blog/2012/08/its-time-to-learn-coffeescript/

### Dependency Management with RequireJS ###

- http://www.websector.de/blog/2012/08/26/modular-todomvc-app-with-canjs-and-requirejs/
- https://github.com/sectore/todomvc-canjs-requirejs-coffeescript

### Grunt build tasks ###

- https://github.com/sectore/todomvc-canjs-requirejs-coffeescript/blob/master/build/grunt.js
- https://github.com/asciidisco/grunt-requirejs
- 