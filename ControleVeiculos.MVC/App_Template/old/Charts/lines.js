// Create the data multi lines ************************************************************************
//*****************************************************************************************************
//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 200, height = 300, isStacked percent or number, title, colors
var @varDataName = new google.visualization.DataTable();
//[new Date(2014, 5, 21), 3, 2, 4, 2, 1, 2],
	@data

var @optionsName = {
    height: @height,
    width: @width,
    title: '@title',
    hAxis: {
        title: ''
    },
    vAxis: {
        title: 'Amount'
    },
    colors: @colors,
    trendlines: {
        //0: {type: 'exponential', color: '#333', opacity: 1},
        //1: {type: 'linear', color: '#111', opacity: .5}
    },
    titleTextStyle: { color: '#ABB2B9', 
        fontName: 'Calibri',
        fontSize: 22,
        bold: false,
        italic: false 
    }
};
var @chartName = new google.visualization.LineChart(document.getElementById('@div'));
	@chartName.draw(@varDataName, @optionsName);