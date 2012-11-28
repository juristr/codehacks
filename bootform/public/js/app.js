(function() {
  'use strict';

  require.config({
    paths: {
      jquery: ['http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min']
    }
  });

  require(['app/builder/builder'], function(Builder) {
    return new Builder($('.js-main-content'));
  });

}).call(this);
