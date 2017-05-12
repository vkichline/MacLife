$(function() {
    function refresh()
    {
        if(mount) {
            $.getJSON("api/life", function(data){
                var table    = data.table;
                var height   = data.height;
                var width    = data.width;
                var gen      = data.generation;
                var elapsed  = data.elapsed;

                if(table) {
                    var str = "";
                    for(var i = 0; i < width; i++)
                    {
                        str += "<div>"
                        var row = table[i];
                        for(var j = 0; j < height; j++)
                        {
                            var cell = row[j];
                            var val = cell.value;
                            str += val ? "&nbsp;O&nbsp;" : "&nbsp;&nbsp;&nbsp;";
                        }
                        str += "</div>\n"
                    }
                    mount.innerHTML = str;
                    generation.innerText = gen;
                    timing.innerText = elapsed;
                }
            });
        }
    }

    function pause()
    {
        clearInterval(timer);
        timer = null;
    }

    function resume()
    {
        if(!timer)
        {
            timer = setInterval(refresh, 100);
        }
    }

    function reset()
    {
        pause();
        $.post("api/life", { action: "reset"});
        resume();
    }

    var mount = document.getElementById("world");
    var generation = document.getElementById("generation");
    var timing = document.getElementById("timing");
    $("#PauseButton").on('click', pause);
    $("#ResumeButton").on('click', resume);
    $("#ResetButton").on('click', reset);

    refresh();
    var timer = setInterval(refresh, 100);
});
