module.exports = function(grunt) {

  grunt.loadNpmTasks('grunt-requirejs');
  grunt.loadNpmTasks('grunt-coffee');

  grunt.initConfig({

    pkg: '<json:package.json>',

    coffee: { 
      /*
      models: {
        src: ['<%= pkg.folders.src %>/models/*.coffee'],
        dest: '<%= pkg.folders.public %>/js/models/'
      },
      controllers: {
        src: ['<%= pkg.folders.src %>/controllers/*.coffee'],
        dest: '<%= pkg.folders.public %>/js/controllers/'
      },*/
      app: {
        src: '<%= pkg.folders.src %>/app.coffee',
        dest: '<%= pkg.folders.public %>/js/'
      },
    },
    requirejs: {
      baseUrl: "../public/js/",
      out: "../public/js/app-optimized.js",
      name: "app",
      optimize: "none",
      paths: {
        jquery: "empty:",
        can: "empty:",
        json: "empty:"
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
