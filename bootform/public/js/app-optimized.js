


require.config({
  baseUrl: 'js/lib',
  paths: {
    jquery: ['http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min', 'libs/jquery-1.8.0.min'],
    boostrap: ['assets/bootstrap/js/bootstrap.min.js'],
    json: ['http://cdnjs.cloudflare.com/ajax/libs/json2/20110223/json2', 'libs/json2'],
    app: '../app'
  }
});

require(['jquery', 'json', 'can'], function($, can, json) {
  return $(function() {
    return console.log("success");
  });
});

define("app", function(){});
