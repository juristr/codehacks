/*
    @author Juri Strumpflohner (http://juristr.com)
    @version 0.1
*/

(function($, undefined){
    var interval;
    $.fn.defaults = {};

    $.fn.dots = function(options){
        var opts = $.extend({}, $.fn.dots.defaults, options);
        $this = $(this);
        var o = $.meta ? $.extend({}, opts, $this.data()) : opts;

        if($this.find('span.dots').length === 0){
            var dots = $('<span class="one">.</span><span class="two">.</span><span class="three">.</span>');
            dots.each(function(){ $(this).addClass('dots'); });
            
            $this.append(dots);
        }

        beginAnimation();
    };

    function beginAnimation(){
        if($.browser.webkit || $.browser.mozilla){
            var styleEl = '<style type="text/css" id="dots_animation">';

            if($.browser.webkit){
                styleEl += '.one { opacity: 0; -webkit-animation: dot 1.3s infinite; -webkit-animation-delay: 0.0s; }';
                styleEl += '.two { opacity: 0; -webkit-animation: dot 1.3s infinite; -webkit-animation-delay: 0.2s; }';
                styleEl += '.three { opacity: 0; -webkit-animation: dot 1.3s infinite; -webkit-animation-delay: 0.3s;}';
                styleEl += '@-webkit-keyframes dot { 0% { opacity: 0; } 50% { opacity: 0; } 100% { opacity: 1; } }';
            }

            if($.browser.mozilla){
                styleEl += '.one { opacity: 0; animation: dot 1.3s infinite; animation-delay: 0.0s; }';
                styleEl += '.two { opacity: 0; animation: dot 1.3s infinite; animation-delay: 0.2s; }';
                styleEl += '.three { opacity: 0; animation: dot 1.3s infinite; animation-delay: 0.3s;}';
                styleEl += '@keyframes dot { 0% { opacity: 0; } 50% { opacity: 0; } 100% { opacity: 1; } }';
            }

            styleEl += "</style>";

            $(styleEl).appendTo("head");
        }else{
            javaScriptFallbackAnimation();
        }
    }

    function javaScriptFallbackAnimation(){
        performAnimation($this);
        
        interval = setInterval(function(){
            console.log('performing animation iteration');
            if($this.find('span.dots').length > 0){
                performAnimation($this);
            }else{
                clearInterval(interval);
            }
        }, 2300);
    }
    
    function performAnimation($this){
        var positions = [];
        var delay = 0;

        $this.find('span.dots').css('opacity', 0);

        $this.find('span.dots').each(function(i) {
            var $span = $(this);
            $span.queue('dots', function(next) {
                $span.delay(delay).animate({
                    opacity: 1
                }, 1000, next);
            });

            $span.dequeue('dots');
            delay += 500;
        });
    }
})(jQuery);