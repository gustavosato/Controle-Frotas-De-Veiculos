// Create the data table progress *********************************************************************
//*****************************************************************************************************
//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 200, height = 200, isStacked percent or number
var @varDataName = google.visualization.arrayToDataTable([
	//['Item', 'Work', { role: 'style' }, 'Impact', { role: 'style' }, 'Remaining', { role: 'style' }],
	@data
]);
// Set chart options
var @optionsName = {
    colors: @colors,
    title: '@title',
    height: @height,
    width: @width,
    chartArea: {width: '70%'},
    legend: {position: 'right', maxLines: 3},
    isStacked: '@isStacked',
    hAxis: {
        title: '',
        minValue: 0,
        //ticks: [0, .3, .6, .9, 1]
    },
    titleTextStyle: { color: '#ABB2B9',    // any HTML string color ('red', '#cc00cc')
        fontName: 'Calibri', // i.e. 'Times New Roman'
        fontSize: 22, // 12, 18 whatever you want (don't specify px)
        bold: false,    // true or false
        italic: false   // true of false
    }
};
var @chartName = new google.visualization.BarChart(document.getElementById('@div'));
@chartName.draw(@varDataName, @optionsName);