define('vm.examine.skeleton',
    ['jquery', 'model.symptomatic.region.collection', 'knockback', 'underscore', 'vm.examine.container'],
    function ($, SymptomaticRegionCollection, kb, _, container) {

        var symptomaticRegions = new SymptomaticRegionCollection();
        symptomaticRegions.reset(JSON.parse($('#skeleton-areas').val()));

        var selectArea = function (item) {
            if (container.selectedAreas.indexOf(item) == -1) {
                $('#' + item.name()).addClass('skeleton-selected');
                container.selectedAreas.push(item);
            } else {
                $('#' + item.name()).removeClass('skeleton-selected');
                container.selectedAreas.remove(item);
            }
        };
        
        var areaMouseOver = function (item) {
            if (container.selectedAreas.indexOf(item) > -1) {
                $('#' + item.name()).removeClass('skeleton-selected');
            }
            
            $('#' + item.name()).addClass('skeleton-hover');
        };

        var areaMouseOut = function (item) {
            $('#' + item.name()).removeClass('skeleton-hover');

            if (container.selectedAreas.indexOf(item) > -1) {
                $('#' + item.name()).addClass('skeleton-selected');
            }
        };

        return {
            symptomaticRegions: new kb.CollectionObservable(symptomaticRegions),
            selectArea: selectArea,
            areaMouseOver: areaMouseOver,
            areaMouseOut: areaMouseOut,
            selectedAreas: container.selectedAreas
        };
    });