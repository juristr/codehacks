'use strict'
require.config
  baseUrl: 'js/lib',
  # pathes with inline callbacks, 
  # if the CDN location fails, it loads from local path
  # @see: http://requirejs.org/docs/api.html#pathsfallbacks
  paths:

    jquery: [
      'http://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min',
      # If the CDN location fails, load from this location
      'libs/jquery-1.8.0.min'
    ],
    boostrap:[
      'assets/bootstrap/js/bootstrap.min.js'
    ],
    json: [
      'http://cdnjs.cloudflare.com/ajax/libs/json2/20110223/json2',
      'libs/json2'
    ],
    app: '../app'

  # Increasing the the default 7 sec. to 15
  # @see: http://requirejs.org/docs/api.html#config-waitSeconds
  #waitSeconds: 15


require([
  'jquery',
  'json',
  'can'
  #'models/Todo',
  #'models/TodoList',
  #'controllers/Todos'
], ($, can, json) ->
  # bootstrap the app
  $ () ->
    console.log "success"
)