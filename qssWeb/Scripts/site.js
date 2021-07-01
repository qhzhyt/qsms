// Write your Javascript code.
function shakeItem(obj,time,wh,fx)
{
    $(function(){
        var $panel = $(obj);
        var offset = $panel.offset()-$panel.width();
        var x= offset.left;
        var y= offset.top;
        for(var i=1; i<=time; i++){
            if(i%2==0)
            {
                $panel.animate({left:'+'+wh+'px'},fx);
            }else
            {
                $panel.animate({left:'-'+wh+'px'},fx);
            }

        }
        $panel.animate({left:0},fx);
        $panel.offset({ top: y, left: x });

    })
}