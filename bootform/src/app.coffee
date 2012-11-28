'use strict'
require.config
  # pathes with inline callbacks, 
  # if the CDN location fails, it loads from local path
  # @see: http://requirejs.org/docs/api.html#pathsfallbacks
  paths:
    #jquery: [
    #  'http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min',
    #  'libs/jquery.min'
    #],
    bootstrap:[
       '../assets/bootstrap/js/bootstrap.min'
    ]
  #   json: [
  #     'http://cdnjs.cloudflare.com/ajax/libs/json2/20110223/json2'
  #   ]

  # Increasing the the default 7 sec. to 15
  # @see: http://requirejs.org/docs/api.html#config-waitSeconds
  #waitSeconds: 15


require([
  'app/builder/builder',
  'bootstrap'
], (Builder) ->
  new Builder $('.js-main-content')
  #can.$('.js-main-content').html '<h1>Test</h1>'
  #new Builder can.$('.js-main-content')
)