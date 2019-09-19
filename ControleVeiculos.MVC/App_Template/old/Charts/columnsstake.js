// Create the data stake columns **********************************************************************
//*****************************************************************************************************
//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 200, height = 200, isStacked percent or number, @title
var @varDataName = google.visualization.arrayToDataTable([
 //['Requirement', 'No Run', 'Passed', 'Failed', 'Blocked', 'Incomplete', 'N/A'],
 //['Req 01', 10, 0, 0, 0, 0, 0],
 @data
]);

//var @view = new google.visualization.DataView(@varDataName);
var @optionsName = {
    title: '@title',
    isStacked: '@isStacked',
    height: @height,
    width: @width,
    bar: { groupWidth: "90%" },
    legend: {position: 'rigth', maxLines: 3},
    vAxis: {
        minValue: 0,
        ticks: [0, .25, .5, .75, 1]
    },
    colors: @colors,
    titleTextStyle: { color: '#ABB2B9',    // any HTML string color ('red', '#cc00cc')
        fontName: 'Calibri', // i.e. 'Times New Roman'
        fontSize: 22, // 12, 18 whatever you want (don't specify px)
        bold: false,    // true or false
        italic: false   // true of false
    }
};
var @chartName = new google.visualization.ColumnChart(document.getElementById("@div"));
@chartName.draw(@varDataName, @optionsName);