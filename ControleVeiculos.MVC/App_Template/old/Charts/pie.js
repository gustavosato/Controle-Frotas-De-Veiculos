//new pie *****************************************************************************
//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 400, height = 300, isStacked percent or number, title
var @varDataName = google.visualization.arrayToDataTable([
 @data
]);

var @optionsName = {
    title: '@title',
    height: @height,
    width: @width,
    legend: {position: 'rigth', maxLines: 4},
    pieSliceText: '@isStacked',
    slices: { 1: {offset: 0.0},
        2: {offset: 0.3},
        3: {offset: 0.1},
        4: {offset: 0.1},
        5: {offset: 0.1},
    },
    colors: @colors,
    is3D: true,
    titleTextStyle: { color: '#ABB2B9', 
        fontName: 'Calibri',
        fontSize: 22,
        bold: false,
        italic: false 
    }
};

var @chartName = new google.visualization.PieChart(document.getElementById('@div'));
@chartName.draw(@varDataName, @optionsName);