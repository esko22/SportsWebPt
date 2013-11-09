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
            var diagnosisReport = new DiagnosisReport({ diffDiagId: detail.detailReportId() });
            diagnosisReport.fetch({
                success: function () {
                    report.bindReport(diagnosisReport);
                    //setTimeout(function () {
                    //    sublime.load();
                    //}, 3000);
                    //setTimeout(function () {
                    //    sublime.load();
                    //}, 12000);
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
            return detail.detailReportId() > 0;
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