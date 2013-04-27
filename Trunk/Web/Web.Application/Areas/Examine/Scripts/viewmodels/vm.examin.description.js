define('vm.examine.description',
    ['vm.examine.container', 'underscore', 'ko', 'jquery', 'kendo'],
    function (container, _, ko, $, kendo) {

        var areaTemplate = 'description.area';
        var componentTemplate = 'description.components';
        var selectedAreas = ko.observableArray();

        container.selectedAreas.subscribe(function(newSelectedAreas) {
            selectedAreas.removeAll();
            _.each(newSelectedAreas, function (area) {
                selectedAreas.push(area);
            });
        });
        
        $(document).ready(function () {
            $(".balSlider").each(function (slider) {
                slider.kendoSlider({
                    min: 1,
                    max: 5,
                    orientation: "horizontal",
                    smallStep: 1,
                    largeStep: 2
                });

                alert();
            });
        });

        return {
            selectedAreas: selectedAreas,
            areaTemplate: areaTemplate,
            componentTemplate: componentTemplate
        };
    });