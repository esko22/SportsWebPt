define('vm.examine.detail',
    ['underscore', 'ko', 'jquery', 'knockback', 'services'],
    function (_, ko, $, kb, services) {

        var areaTemplate = 'examine.detail.area';
        var componentTemplate = 'examine.detail.components';
        var selectedAreas = ko.observableArray();

        var bindSelectedAreas = function(newSelectedAreas) {
            selectedAreas.removeAll();
            _.each(newSelectedAreas, function (area) {
                selectedAreas.push(area);
            });
        };

        var postTabRender = function(elements) {
            $('#examine-detail-tab-nav > :first-child').addClass('active');
            $('#examine-detail-container > :first-child').addClass('active');
        };

        var submitDiscomfortDetail = function () {
            var potentialSymptoms = new Array();
            
            _.each(selectedAreas(), function (area) {
                var selectedArea = kb.utils.wrappedModel(area);
                _.each(selectedArea.get('bodyParts').models, function(bodyPart) {
                    _.each(bodyPart.get('potentialSymptoms').models, function (symptom) {
                        potentialSymptoms.push(symptom);
                    });
                });
            });

            var diffDiagSubmission = {
                "submittedFor": 0,
                "symptomDetails": potentialSymptoms
            };

            var successSub = function(data) {
                window.location.hash = 'report/' + data;
            };

            services.submitSymptomDetails(diffDiagSubmission,successSub);

        };
        
      
        return {
            selectedAreas: selectedAreas,
            areaTemplate: areaTemplate,
            componentTemplate: componentTemplate,
            bindSelectedAreas: bindSelectedAreas,
            postTabRender: postTabRender,
            submitDiscomfortDetail: submitDiscomfortDetail
        };
    });