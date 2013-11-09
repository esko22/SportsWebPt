define('vm.examine.skeleton',
    ['jquery','config', 'model.symptomatic.region.collection', 'knockback', 'underscore', 'ko'],
    function ($, config, SymptomaticRegionCollection, kb, _, ko) {

        var symptomaticRegions = new SymptomaticRegionCollection(),
            selectedAreas = kb.collectionObservable(new SymptomaticRegionCollection()),
            isInitialized = ko.observable(false),
            isProcessing = ko.observable(true);

        function selectArea (item) {
            if (selectedAreas.indexOf(item) == -1) {
                if (selectedAreas().length < config.maxSelectableAreas()) {
                    $('#' + item.cssClassName()).addClass('skeleton-selected');
                    selectedAreas.push(item);
                } else {
                    config.notifier.clear();
                    config.notifier.error($.validator.format('Only {0} selectable areas allowed.', config.maxSelectableAreas()));
                }
            } else {
                $('#' + item.cssClassName()).removeClass('skeleton-selected');
                selectedAreas.remove(item);
            }
        }
        
        function areaMouseOver (item) {
            if (selectedAreas.indexOf(item) > -1) {
                $('#' + item.cssClassName()).removeClass('skeleton-selected');
            }
            
            $('#' + item.cssClassName()).addClass('skeleton-hover');
        }

        function areaMouseOut (item) {
            $('#' + item.cssClassName()).removeClass('skeleton-hover');

            if (selectedAreas.indexOf(item) > -1) {
                $('#' + item.cssClassName()).addClass('skeleton-selected');
            }
        }
        
        function formatBodyParts(symptomaticRegion) {

            var html = "";
            _.each(symptomaticRegion.bodyParts(), function (bodyPart) {
                if(!bodyPart.isSecondary())
                    html = $.validator.format('{0}<div>{1}</div>',html, bodyPart.commonName());
            });

            return html;
        }
        
        function init() {
            if (!isInitialized()) {
                isProcessing(true);
                symptomaticRegions.fetch({
                    success: function() {
                        isInitialized(true);
                        isProcessing(false);
                    }
                });
            }
        }

        return {
            symptomaticRegions: new kb.CollectionObservable(symptomaticRegions),
            selectArea: selectArea,
            areaMouseOver: areaMouseOver,
            areaMouseOut: areaMouseOut,
            selectedAreas: selectedAreas,
            formatBodyParts: formatBodyParts,
            isInitialized: isInitialized,
            isProcessing: isProcessing,
            maxSelectableAreas: config.maxSelectableAreas,
            init : init
        };
    });