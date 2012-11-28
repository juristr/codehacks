(function() {

  define(['can'], function(can) {
    var Testmodel;
    return Testmodel = can.Model({
      init: function() {
        return console.log('test');
      }
    });
  });

}).call(this);
