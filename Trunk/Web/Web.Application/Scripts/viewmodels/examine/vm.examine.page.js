define('vm.examine.page',
['vm.examine.skeleton', 'vm.examine.detail', 'vm.examine.report', 'ko', 'model.diagnosis.report'],
    function (skeleton, detail, report, ko, DiagnosisReport) {

        var isVisible = ko.observable(false);
        
        function onVisible() {
            isVisible(true);
            skeleton.init();
        }
        
        function showDetail() {
            detail.bindSelectedAreas(skeleton.selectedAreas());
        }
        
        function submitDiscomfortDetail() {
            detail.submitDiscomfortDetail();
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
        }

        return {
            skeleton: skeleton,
            detail: detail,
            report: report,
            onVisible: onVisible,
            showDetail: showDetail,
            submitDiscomfortDetail: submitDiscomfortDetail,
            showReport: showReport,
            isVisible: isVisible
        };
    });