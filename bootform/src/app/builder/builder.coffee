define [
  'can/control',
  'can/view',
  'app/models/testmodel',
  'can/view/ejs'
  ], (Control, canView, Testmodel) ->
    Builder = Control(
      defaults: 
        view: '//js/app/builder/views/main.ejs'
    ,
      init: ->
        @someModel = new Testmodel( name: 'Juri' )
        @element.html canView.view @options.view, @someModel

      '.js-save click': (el, ev) ->
        @someModel.attr 'name', el.siblings('input[type=text]').val()
    )