    /// this property indicate from which js file draw chart
    // 0 = nothing
    // 1 = chartread.js
    // 2 = i1read.js
    // 3 = isisread.js
    var chartDrawFrom = 0;

    var area = new Object();
    area["W"] = 0;
    area["H"] = 0;

    var distance = new Object();
    distance["R"] = 0;
    distance["C"] = 0;

    var w = parseInt(jQuery("[id*='Canvas']").width());
    var h = parseInt(jQuery("[id*='Canvas']").height());

    var startPosition = 1;

    var col = 0;
    var row = 0;

    var drawCells = 0;
    var cId = 0;
    var x1 = 0;
    var y1 = 0;

    function CallDrawChart(chart) {
        switch (chartDrawFrom) {
            case 1:
                this.Initilize_Common(chart);
                this.Initilize_ChartRead();
                this.drawChart_For_ReadChart(chart);
                break;
            case 2:
                //this.Initilize_Common(chart);
                this.Initilize_I1Read();
                this.drawChart_For_I1_And_ISIRead();
                break;
            case 3:
                this.Initilize_Common(chart);
                this.Initilize_ISIRead();
                this.drawChart_For_I1_And_ISIRead();
                break;
        }
    }

    //Assign chart read variable 
    function Initilize_ChartRead() {
        if (row != 1) {
            area["H"] = ((h / row) / 10) + '';
            distance["R"] = ((h / row) / 10) + '';
        }
        else {
            area["H"] = 20;
            distance["R"] = 1;
        }
        drawCells = 0;
        cId = 0;
        x1 = 0;
        y1 = 0;
    }

    //Assign i1read variable 
    function Initilize_I1Read() {
        if (row != 1) {
            area["H"] = ((h / row) / 10) + '';
            distance["R"] = ((h / row) / 10) + '';
        }
        else {
            area["H"] = 20;
            distance["R"] = 1;
        }

        if (y1 == 0) {
            y1 = parseInt(area["H"]);
        }
        y1 = y1 + (parseInt(area["H"])) + parseInt(distance["R"] * 2);

        x1 = 0;
        col = results.length;
    }

    //Assign isiread variable assign
    function Initilize_ISIRead() {
        area["H"] = ((h / row) / 10) + '';
        distance["R"] = ((h / row) / 10) + '';
        if (y1 == 0) {
            y1 = parseInt(area["H"]);
        }
        y1 = y1 + (parseInt(area["H"])) + parseInt(distance["R"] * 2);
        x1 = 0;
        col = results.length;
    }

    //Assign common variable 
    function Initilize_Common(chart) {
        area["W"] = 0;
        area["H"] = 0;
        distance["R"] = 0;
        distance["C"] = 0;
        w = parseInt(jQuery("[id*='Canvas']").width());
        h = parseInt(jQuery("[id*='Canvas']").height());
        startPosition = 1;
        col = chart["NumberOfColumns"];
        row = chart["NumberOfRows"];
        area["W"] = ((w / col) / 5) + '';
        distance["C"] = ((w / col) / 4) + '';
        noOfCells = chart["Size"];
    }

    //use for draw chart in chartread.js 
    function drawChart_For_ReadChart(chart) {
        var c2 = document.getElementById("Canvas");
        if (c2.getContext) 
        {
            var c2_context = c2.getContext("2d");
            for (i = 0; i < row; i++) {
                y1 = y1 + (parseInt(area["H"])) + parseInt(distance["R"] * 2);
                x1 = 0;
                for (j = 0; j < col; j++) {
                  if (noOfCells > drawCells) {
                        x1 = x1 + parseInt(area["W"]) + parseInt(distance["C"]);
                        var xx1 = startPosition + x1;
                        var yy1 = startPosition + y1;                        
                        var rgbx = chart["RefBase"][cId]["HTMLColor"];
                        c2_context.fillStyle = rgbx;
                        c2_context.fillRect(xx1, yy1, area["W"], area["H"]);
                        drawCells++;
                        cId++;
                    }
                }
            }
        }
    }

    //use for draw chart in i1read.js  and isiread.js
    function drawChart_For_I1_And_ISIRead() {
        for (j = 0; j < col; j++) {
            if (noOfCells > drawCells) {
                x1 = x1 + parseInt(area["W"]) + parseInt(distance["C"]);
                var xx1 = startPosition + x1;
                var yy1 = startPosition + y1;
                var srgb = results[j]["sRGB"];
                var rgbx = "#" + dec2hex(srgb["R"]) + dec2hex(srgb["G"]) + dec2hex(srgb["B"]);
                c2_context.fillStyle = rgbx;
                c2_context.fillRect(xx1, yy1, area["W"], area["H"]);
                drawCells++;
            }
        }
    }