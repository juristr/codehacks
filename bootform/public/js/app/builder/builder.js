(function() {

  define(['can/control', 'can/view', 'app/models/testmodel', 'can/view/ejs'], function(Control, canView, Testmodel) {
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
