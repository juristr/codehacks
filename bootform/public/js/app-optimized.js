
(function() {

  define('app/models/testmodel',['can'], function(can) {
    var Testmodel;
    return Testmodel = can.Model({
      init: function() {
        return console.log('model initialized');
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
        this.element.html(canView.view(this.options.view, this.someModel));
        return $.getScript('js/libs/fb.js');
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
      bootstrap: ['../assets/bootstrap/js/bootstrap.min']
    }
  });

  require(['app/builder/builder', 'bootstrap'], function(Builder) {
    return new Builder($('.js-main-content'));
  });

}).call(this);

define("app", function(){});
