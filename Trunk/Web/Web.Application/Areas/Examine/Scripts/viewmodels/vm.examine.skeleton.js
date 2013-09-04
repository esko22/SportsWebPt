define('vm.examine.skeleton',
    ['jquery', 'model.symptomatic.region.collection', 'knockback', 'underscore', 'vm.examine.container'],
    function ($, SymptomaticRegionCollection, kb, _, container) {

        var symptomaticRegions = new SymptomaticRegionCollection();
        symptomaticRegions.reset(JSON.parse($('#skeleton-areas').val()));

        var selectArea = function (item) {
            if (container.selectedAreas.indexOf(item) == -1) {
                $('#' + item.cssClassName()).addClass('skeleton-selected');
                container.selectedAreas.push(item);
            } else {
                $('#' + item.cssClassName()).removeClass('skeleton-selected');
                container.selectedAreas.remove(item);
            }
        };
        
        var areaMouseOver = function (item) {
            if (container.selectedAreas.indexOf(item) > -1) {
                $('#' + item.cssClassName()).removeClass('skeleton-selected');
            }
            
            $('#' + item.cssClassName()).addClass('skeleton-hover');
        };

        var areaMouseOut = function (item) {
            $('#' + item.cssClassName()).removeClass('skeleton-hover');

            if (container.selectedAreas.indexOf(item) > -1) {
                $('#' + item.cssClassName()).addClass('skeleton-selected');
            }
        };
        
        var formatBodyParts = function (symptomaticRegion) {

            var html = "";
            _.each(symptomaticRegion.bodyParts(), function (bodyPart) {
                if(!bodyPart.isSecondary())
                    html = $.validator.format('{0}<div>{1}</div>',html, bodyPart.commonName());
            });

            return html;
        };

        return {
            symptomaticRegions: new kb.CollectionObservable(symptomaticRegions),
            selectArea: selectArea,
            areaMouseOver: areaMouseOver,
            areaMouseOut: areaMouseOut,
            selectedAreas: container.selectedAreas,
            formatBodyParts: formatBodyParts
        };
    });