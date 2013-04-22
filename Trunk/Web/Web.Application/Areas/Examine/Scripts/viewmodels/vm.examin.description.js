define('vm.examine.description',
    ['vm.examine.container', 'underscore', 'vm.examine.description.area', 'ko', 'config', 'model.area.component.collection'],
    function (container, _, DescriptionArea, ko, config, ComponentCollection) {

        var areaTemplate = 'description.area';
        var selectedAreas = ko.observableArray();

        container.selectedAreas.subscribe(function(newSelectedAreas) {
            selectedAreas.removeAll();

            _.each(newSelectedAreas, function (area) {
                var areaComponents = new ComponentCollection();
                areaComponents.url = $.format('{0}{1}', config.apiUris.componentsByArea, area.id());
                areaComponents.fetch();
                
                selectedAreas.push(new DescriptionArea(area, areaComponents));
            });
            
        });


        return {
            selectedAreas: selectedAreas,
            areaTemplate: areaTemplate
        };
    });