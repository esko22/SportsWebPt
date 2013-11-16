define('vm.examine.detail',
    ['underscore', 'ko', 'jquery', 'knockback', 'services', 'config'],
    function (_, ko, $, kb, services,config) {

        var areaTemplate = 'examine.detail.area';
        var componentTemplate = 'examine.detail.components';
        var selectedAreas = ko.observableArray();
        var detailReportId = ko.observable(0);
        var isProcessing = ko.observable(false);
        var reportPending = ko.observable(false);
        
        var bindSelectedAreas = function(selectedAreaViewModels) {
            selectedAreas.removeAll();
            isProcessing(true);
            
            _.each(selectedAreaViewModels, function (area) {
                selectedAreas.push(area);
                var symptomsNeed = area.model().get('bodyParts').where({ symptomsFetched: false });

                if (symptomsNeed.length > 0) {
                    _.each(symptomsNeed, function(bodyPart) {
                        var potentialSymptoms = bodyPart.get('potentialSymptoms');
                        potentialSymptoms.url = $.validator.format("{0}/{1}", config.apiUris.potentialSymptoms, bodyPart.id);
                        potentialSymptoms.fetch({
                            success: function() {
                                bodyPart.set('symptomsFetched', true);
                                checkPendingSymptoms(area.bodyParts());
                            },
                            error: function() { isProcessing(false); }
                        });
                    });
                } else {
                    onSymptomsFetched();
                }

            });
        };

        function checkPendingSymptoms(bodyParts) {
            var allSymptomsFetched = _.every(bodyParts, function(bodyPart) { return bodyPart.symptomsFetched(); });
            if (allSymptomsFetched) {
                onSymptomsFetched();
            };
        }

        function onSymptomsFetched() {
            isProcessing(false);
            $('#examine-detail-tab-nav > :first-child').addClass('active');
            $('#examine-detail-container > :first-child').addClass('active');
        };

        function submitDiscomfortDetail() {

            $('#examine-report-modal').modal('show');
            reportPending(true);
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

            services.submitSymptomDetails(diffDiagSubmission, successSubmission);
            modalTimeout(4000);
        };
        
        function successSubmission(data) {
            detailReportId(data);
            reportPending(false);
        }
        
        function modalTimeout(timeout) {
                setTimeout(function () {
                    if (reportPending()) {
                        modalTimeout(1000);
                    } else {
                        transitionToReportDetail();
                    }
                        
                }, timeout);
        }
        
        function transitionToReportDetail() {
            window.location.hash = '/examine/report';
            $('#examine-report-modal').modal('hide');
        }
        
      
        return {
            isProcessing : isProcessing,
            selectedAreas: selectedAreas,
            areaTemplate: areaTemplate,
            componentTemplate: componentTemplate,
            bindSelectedAreas: bindSelectedAreas,
            submitDiscomfortDetail: submitDiscomfortDetail,
            detailReportId: detailReportId
        };
    });