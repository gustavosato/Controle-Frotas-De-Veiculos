//varibles varDataName, optionsName, chartName, data, div, width= '100%' 0r 200, height = 180
     var @varDataName = new google.visualization.DataTable();
      @varDataName.addColumn('string', 'Task ID');
      @varDataName.addColumn('string', 'Task');
      @varDataName.addColumn('string', 'Resource');
      @varDataName.addColumn('date', 'Start Date');
      @varDataName.addColumn('date', 'End Date');
      @varDataName.addColumn('number', 'Duration');
      @varDataName.addColumn('number', 'Percent Complete');
      @varDataName.addColumn('string', 'Dependencies');
      data.addRows([
	  @data      
	  ]);

      var @optionsName = {
        height: @height,
        gantt: {
          trackHeight: 30
        }
      };

      var @chartName = new google.visualization.Gantt(document.getElementById('@div'));

      @chartName.draw(@varDataName, @optionsName);