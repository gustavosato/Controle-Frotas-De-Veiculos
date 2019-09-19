//chart area ***************************************************************************************************************************
//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 400, height = 300, isStacked percent or number, title
var @varDataName = google.visualization.arrayToDataTable([
  @data
]);

var @optionsName = {
    title: '@title',
    hAxis: {title: '',  titleTextStyle: {color: '#333'}},
    vAxis: {minValue: 0},
    colors: @colors,
    titleTextStyle: { color: '#ABB2B9', 
        fontName: 'Calibri',
        fontSize: 22,
        bold: false,
        italic: false 
    }
};
var chartName = new google.visualization.AreaChart(document.getElementById('@div'));
chartName.draw(@varDataName, @optionsName);