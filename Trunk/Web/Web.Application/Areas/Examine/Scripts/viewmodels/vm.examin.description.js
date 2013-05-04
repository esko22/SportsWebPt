define('vm.examine.description',
    ['vm.examine.container', 'underscore', 'ko', 'jquery', 'knockback'],
    function (container, _, ko, $, kb) {

        var areaTemplate = 'description.area';
        var componentTemplate = 'description.components';
        var selectedAreas = ko.observableArray();

        container.selectedAreas.subscribe(function(newSelectedAreas) {
            selectedAreas.removeAll();
            _.each(newSelectedAreas, function (area) {
                var skeletonArea = kb.utils.wrappedModel(area);
                _.each(skeletonArea.get('bodyParts'), function (bodyPart) {
                    bodyPart.getRelations();
                });
                selectedAreas.push(area);
            });
        });
      
        return {
            selectedAreas: selectedAreas,
            areaTemplate: areaTemplate,
            componentTemplate: componentTemplate
        };
    });