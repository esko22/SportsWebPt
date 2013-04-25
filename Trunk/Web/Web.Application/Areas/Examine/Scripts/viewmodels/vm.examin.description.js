define('vm.examine.description',
    ['vm.examine.container', 'underscore', 'ko'],
    function (container, _, ko) {

        var areaTemplate = 'description.area';
        var componentTemplate = 'description.components';
        var selectedAreas = ko.observableArray();

        container.selectedAreas.subscribe(function(newSelectedAreas) {
            selectedAreas.removeAll();
            _.each(newSelectedAreas, function (area) {
                selectedAreas.push(area);
            });
        });

        return {
            selectedAreas: selectedAreas,
            areaTemplate: areaTemplate,
            componentTemplate: componentTemplate
        };
    });