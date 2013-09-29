define('vm.examine.skeleton',
    ['jquery','config', 'model.symptomatic.region.collection', 'knockback', 'underscore', 'vm.examine.container'],
    function ($, config, SymptomaticRegionCollection, kb, _, container, toastr) {

        var symptomaticRegions = new SymptomaticRegionCollection();
        symptomaticRegions.reset(JSON.parse($('#skeleton-areas').val()));

        var selectArea = function (item) {
            if (container.selectedAreas.indexOf(item) == -1) {
                if (container.selectedAreas().length < config.maxSelectableAreas()) {
                    $('#' + item.cssClassName()).addClass('skeleton-selected');
                    container.selectedAreas.push(item);
                } else {
                    config.notifier.clear();
                    config.notifier.error($.validator.format('Only {0} selectable areas allowed.', config.maxSelectableAreas()));
                }
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
            formatBodyParts: formatBodyParts,
            maxSelectableAreas : config.maxSelectableAreas
        };
    });