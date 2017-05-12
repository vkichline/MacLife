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
	                for(var i = 0; i < height; i++)
	                {
	                    str += "<div>"
	                    var row = table[i];
	                    for(var j = 0; j < width; j++)
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
    var mount = document.getElementById("world");
    var generation = document.getElementById("generation");
    var timing = document.getElementById("timing");
    refresh();
    setInterval(refresh, 100);
});
