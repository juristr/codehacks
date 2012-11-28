define [
  'can'
  ], (can) ->
    Testmodel = can.Model(
      init: ->
        console.log 'test'
    )