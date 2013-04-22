define('vm.examine.skeleton',
    ['jquery', 'model.skeleton.area.collection', 'knockback', 'underscore', 'vm.examine.container'],
    function ($, AreaCollection, kb, _, container) {

        var areas = new AreaCollection();
        areas.reset(JSON.parse($('#skeleton-areas').val()));

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
            areas: new kb.CollectionObservable(areas),
            selectArea: selectArea,
            areaMouseOver: areaMouseOver,
            areaMouseOut: areaMouseOut,
            selectedAreas: container.selectedAreas
        };
    });