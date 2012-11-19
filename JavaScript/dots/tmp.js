steal
.css("css/ajaxprogress")
.views('loading.ejs')
.plugins(
    'jquery',
    'jquery/class',
    'jquery/view/ejs')
.then(function () {

    var animateChars = function() {
        var positions = [];
        var delay = 0;

        $('span', '#wrapper').css('opacity', 0);

        $('span', '#wrapper').each(function(i) {

            var $span = $(this);

            $span.queue('chars', function(next) {
                
                $span.delay(delay).animate({
                    opacity: 1
                }, 1000, next);
                
            });

            $span.dequeue('chars');
            
            delay += 500;

        });
    };

    animateChars();​

    $.fn.loadingDots = function(){

    };

    /**
    @page siag.progress Siag Progress
    @tag home
    @author Juri Strumpflohner

    This plugin hooks into the `ajaxstart` and `ajaxstop` events for showing
    a loading indicator at the top of the page.

    */



        //        var handledEvent = false;
        //        OpenAjax.hub.subscribe('i18n.languageChanged', function (ev, lang) {
        //            if (handledEvent === false) {
        //                handledEvent = true;
        //                $.i18n.setLang({
        //                    language: lang,
        //                    name: "test",
        //                    path: "../siag/ajax/i18n/"
        //                });
        //            }
        //        });

        var showLoading,
            LOADING_DELAY = 500,
            displayLoader = function () {
                $("body").prepend($.View("//siag/ajaxprogress/views/loading"));
                setTimeout(function(){
                    $("body > .loading > .dots").loadingDots({
                        speed: 400,
                        maxDots: 3
                    });
                },50);
            },
            hideLoader = function () {
                $("body > .loading > .dots").loadingDots("Stop");
                $("body > .modal-background, body > .loading").remove();
            };

        $(document)
            .ajaxStart(function (xhr, settings) {
                showLoading = setTimeout(displayLoader, LOADING_DELAY);
            })
            .ajaxError(function(e, request, settings){
                hideLoader();
                clearTimeout(showLoading);
            })
            .ajaxStop(function (e, xhr, settings) {
                hideLoader();
                clearTimeout(showLoading);
            });

        window.onerror = function(){
            setTimeout(hideLoader, LOADING_DELAY + 10);
        };

        $.Class.extend("Siag.Progress",
        {
            start: function (workerFunc) {
                //setTimeout(displayLoader, 10);
                displayLoader();
                setTimeout(workerFunc, 30);
                //this.stop();
            },

            stop: function () {
                hideLoader();
            }
        },
        {});
    });