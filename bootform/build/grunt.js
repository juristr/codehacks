module.exports = function(grunt) {


  grunt.loadNpmTasks('grunt-requirejs');
  //grunt.loadNpmTasks('grunt-coffee');
  grunt.loadNpmTasks('grunt-contrib-coffee');

  grunt.initConfig({

    pkg: '<json:package.json>',

    coffee: { 
      compile: {
        files: {
          //'<%= pkg.folders.public %>/js/**/*.js': [ '<%= pkg.folders.src %>/**/*.coffee']
          '../public/js/*.js': [
            '../src/*.coffee',
            '../src/app/*.coffee',
            '../src/app/builder/*.coffee',
            '../src/app/models/*.coffee'
          ]
        }
      }
      /*
      models: {
        src: ['<%= pkg.folders.src %>/models/*.coffee'],
        dest: '<%= pkg.folders.public %>/js/models/'
      },
      
      app: {
        src: ['<%= pkg.folders.src %>/app/.coffee'],
        dest: '<%= pkg.folders.public %>/js/app'
      },
      appinit: {
        src: '<%= pkg.folders.src %>/app.coffee',
        dest: '<%= pkg.folders.public %>/js/'
      },
      */
    },
    requirejs: {
      baseUrl: "../public/js/",
      out: "../public/js/app-optimized.js",
      name: "app",
      optimize: "none",
      paths: {
        jquery: "empty:",
        can: "empty:",
        json: "empty:",
        boostrap: "empty"
      }
    },
    server: {
      port: 9999,
      base: '../public'
    },
    watch: {
      files: ['<%= pkg.folders.src %>/**/*.coffee'],
      tasks: 'coffee requirejs'
    }
  });
  
  grunt.registerTask('default', 'server coffee requirejs watch');

};
