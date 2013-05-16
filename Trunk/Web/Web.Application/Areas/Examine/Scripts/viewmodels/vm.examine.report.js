define('vm.examine.report',
    ['ko', 'underscore', 'knockback'],
    function (ko, _, kb) {

        var diagnosisReport = ko.observable();
        var injuries = ko.observableArray();

        var bindReport = function(report) {
            diagnosisReport(report);
            injuries.removeAll();
            _.each(report.get('potentialInjuries').models, function (injury) {
                injuries.push(kb.viewModel(injury));
            });
        };

        return {
            diagnosisReport: diagnosisReport,
            bindReport: bindReport,
            injuries : injuries
        };
    });