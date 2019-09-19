// Create the data table details **********************************************************************
//*****************************************************************************************************
//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 200, height = 180
var @varDataName = new google.visualization.DataTable();
							//['Start Date', '20/10/2017' ],
		@data
	]);
	// Set chart options
var @optionsName = {showRowNumber: false, width: @width, height: @height};
var @chartName = new google.visualization.Table(document.getElementById('@div'));
@chartName.draw(@varDataName, @optionsName);