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
            var dots = $('<span>.</span><span>.</span><span>.</span>');
            dots.each(function(){ $(this).addClass('dots'); });
            
            $this.append(dots);
        }

        return (function(){
            performAnimation($this);
            
            interval = setInterval(function(){
                console.log('performing animation iteration');
                if($this.find('span.dots').length > 0){
                    performAnimation($this);
                }else{
                    clearInterval(interval);   
                }
            }, 2500);
        })();
    };
    
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
    };

    $.fn.dots.stop = function(){
        clearInterval(interval);
    }
})(jQuery);