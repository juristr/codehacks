
(function() {

  define('app/models/testmodel',['can'], function(can) {
    var Testmodel;
    return Testmodel = can.Model({
      init: function() {
        return console.log('test');
      }
    });
  });

}).call(this);

(function() {

  define('app/builder/builder',['can/control', 'can/view', 'app/models/testmodel', 'can/view/ejs'], function(Control, canView, Testmodel) {
    var Builder;
    return Builder = Control({
      defaults: {
        view: '//js/app/builder/views/main.ejs'
      }
    }, {
      init: function() {
        this.someModel = new Testmodel({
          name: 'Juri'
        });
        return this.element.html(canView.view(this.options.view, this.someModel));
      },
      '.js-save click': function(el, ev) {
        return this.someModel.attr('name', el.siblings('input[type=text]').val());
      }
    });
  });

}).call(this);

(function() {
  

  require.config({
    paths: {
      jquery: ['http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min']
    }
  });

  require(['app/builder/builder'], function(Builder) {
    return new Builder($('.js-main-content'));
  });

}).call(this);

define("app", function(){});
