define('vm.examine.report',
    ['ko', 'underscore', 'knockback'],
    function (ko, _, kb) {
        var injuries = kb.collectionObservable();
        
        var bindReport = function(report) {
            injuries.collection(report.get('potentialInjuries'));
        };

        return {
            bindReport: bindReport,
            injuries: injuries
        };
    });