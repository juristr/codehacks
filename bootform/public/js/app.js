(function() {
  'use strict';

  require.config({
    paths: {
      bootstrap: ['../assets/bootstrap/js/bootstrap.min']
    }
  });

  require(['app/builder/builder', 'bootstrap'], function(Builder) {
    return new Builder($('.js-main-content'));
  });

}).call(this);
