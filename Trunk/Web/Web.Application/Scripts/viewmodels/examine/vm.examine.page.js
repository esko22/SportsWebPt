define('vm.examine.page',
['vm.examine.skeleton', 'vm.examine.detail', 'vm.examine.report', 'ko', 'model.diagnosis.report'],
    function (skeleton, detail, report, ko, DiagnosisReport) {

        var isVisible = ko.observable(false);
        
        function onVisible() {
            isVisible(true);
            skeleton.init();
        }

        function showSkeleton() {
            $('#examine-nav a').removeClass('active');
            $('#skeleton-nav').addClass('active');
        }
        
        function showDetail() {
            detail.bindSelectedAreas(skeleton.selectedAreas());
            $('#examine-nav a').removeClass('active');
            $('#detail-nav').addClass('active');
        }
        
        function showReport() {
            //TODO: Diag Nav Temp
            //detail.detailReportId()
            var diagnosisReport = new DiagnosisReport({ diffDiagId: 45 });
            diagnosisReport.fetch({
                success: function () {
                    report.bindReport(diagnosisReport);
                }
            });
            $('#examine-nav a').removeClass('active');
            $('#report-nav').addClass('active');
        }

        function submitDiscomfortDetail() {
            detail.submitDiscomfortDetail();
        }
        
        function canShowDetail() {
            return skeleton.selectedAreas().length > 0;
        }
        
        function canShowReport() {
            //TODO: Diag Nav Temp
            return true;
            //return detail.detailReportId() > 0;
        }


        return {
            skeleton: skeleton,
            detail: detail,
            report: report,
            onVisible: onVisible,
            showDetail: showDetail,
            submitDiscomfortDetail: submitDiscomfortDetail,
            showReport: showReport,
            showSkeleton: showSkeleton,
            canShowDetail: canShowDetail,
            canShowReport: canShowReport,
            isVisible: isVisible
        };
    });