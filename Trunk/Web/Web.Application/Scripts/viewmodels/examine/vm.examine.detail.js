define('vm.examine.detail',
    ['underscore', 'ko', 'jquery', 'knockback', 'services', 'config'],
    function (_, ko, $, kb, services,config) {

        var areaTemplate = 'examine.detail.area';
        var componentTemplate = 'examine.detail.components';
        var selectedAreas = ko.observableArray();
        var detailReportId = ko.observable(0);
        var isProcessing = ko.observable(false);

        var bindSelectedAreas = function(selectedAreaViewModels) {
            selectedAreas.removeAll();
            isProcessing(true);
            _.each(selectedAreaViewModels, function (area) {
                _.each(area.bodyParts(), function (bodyPart) {
                    if (!bodyPart.symptomsFetched()) {
                        var potentialSymptoms = bodyPart.model().get('potentialSymptoms');
                        potentialSymptoms.url = $.validator.format("{0}/{1}", config.apiUris.potentialSymptoms, bodyPart.id());
                        potentialSymptoms.fetch({
                            success: function () {
                                bodyPart.symptomsFetched(true);
                                checkPendingSymptoms(area.bodyParts());
                            },
                            error: function () { isProcessing(false); }
                    });
                    }
                });
                selectedAreas.push(area);
            });
        };

        function checkPendingSymptoms(bodyParts) {
            var allSymptomsFetched = _.every(bodyParts, function(bodyPart) { return bodyPart.symptomsFetched(); });
            if (allSymptomsFetched) {
                isProcessing(false);
            };
        }

        var postTabRender = function(elements) {
            $('#examine-detail-tab-nav > :first-child').addClass('active');
            $('#examine-detail-container > :first-child').addClass('active');
        };

        var submitDiscomfortDetail = function () {

            $('#myModal').modal('show');

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

            var successSub = function (data) {
                detailReportId(data);
                window.location.hash = '/examine/report';
                //$('#myModal').modal('hide');
            };

            services.submitSymptomDetails(diffDiagSubmission,successSub);

        };
        
      
        return {
            isProcessing : isProcessing,
            selectedAreas: selectedAreas,
            areaTemplate: areaTemplate,
            componentTemplate: componentTemplate,
            bindSelectedAreas: bindSelectedAreas,
            postTabRender: postTabRender,
            submitDiscomfortDetail: submitDiscomfortDetail,
            detailReportId: detailReportId
        };
    });