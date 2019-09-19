//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 400, height = 300, isStacked percent or number, title, series=6 e.g.
var @varDataName = google.visualization.arrayToDataTable([
        @data
]);

var @optionsName = {
    height: @height,
    title : '@title',
    vAxis: {title: 'Amount'},
    hAxis: {title: 'Period'},
    seriesType: 'bars',
    //series: {@series: {type: 'line'}}, //indica qual serie sera a linha e.g. 1 or 2
    colors: @colors,
    titleTextStyle: { color: '#ABB2B9', 
        fontName: 'Calibri',
        fontSize: 22,
        bold: false,
        italic: false 
    }
};

var @chartName = new google.visualization.ComboChart(document.getElementById('@div'));
@chartName.draw(@varDataName, @optionsName);